using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace Server_DoanSo
{
    public partial class FormServer : Form
    {
        Socket sckServer;
        Socket sckClient;
        // Socket client; 
        bool isServer = false;
        int secretNumber = 0;
        bool isRunning = false;
        Random rnd = new Random();

        int clientCount = 0; // Biến đếm client
        // Lưu trữ thông tin client: client socket → số bí mật (Cho chế độ chơi đơn)
        Dictionary<Socket, int> clientSecrets = new Dictionary<Socket, int>();

        // Khai báo các Dictionary quản lý phòng chơi (Chế độ Multiplayer)
        Dictionary<string, List<Socket>> roomsPlayers = new Dictionary<string, List<Socket>>();
        Dictionary<string, int> roomsSecret = new Dictionary<string, int>();
        Dictionary<Socket, string> clientRoom = new Dictionary<Socket, string>();
        Dictionary<string, Dictionary<Socket, bool>> roomReady = new Dictionary<string, Dictionary<Socket, bool>>();
        Dictionary<string, int> roomTurnIndex = new Dictionary<string, int>(); // lượt ai đoán

        public FormServer()
        {
            InitializeComponent();
        }

        private void numericPORT_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                listBox_Show.Items.Add("Server đã chạy!");
                return;
            }

            int portServer = 8000;
            string hostName = Dns.GetHostName();
            string ipAddress = Dns.GetHostEntry(hostName)
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?
                .ToString();

            if (ipAddress == null)
            {
                listBox_Show.Items.Add("Không tìm thấy IP IPv4 hợp lệ!");
                return;
            }

            isRunning = true;
            listBox_Show.Items.Add($"Server đã khởi động trên IP: {ipAddress}, PORT: {portServer}");
            listBox_Show.Items.Add("Đang chờ client kết nối...");

            // Luồng chính để lắng nghe kết nối
            Task.Run(() =>
            {
                try
                {
                    sckServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    sckServer.Bind(new IPEndPoint(IPAddress.Parse(ipAddress), portServer));
                    sckServer.Listen(5);

                    while (true)
                    {
                        // Chấp nhận kết nối mới
                        Socket newClient = sckServer.Accept();
                        clientCount++;
                        int clientID = clientCount;

                        this.Invoke((Action)(() =>
                        {
                            listBox_Show.Items.Add("");
                            listBox_Show.Items.Add($"Client {clientID} đã kết nối từ: {((IPEndPoint)newClient.RemoteEndPoint).Address}");
                            listBox_Show.Items.Add($"Bắt đầu trò chơi đoán số cho Client {clientID}!");
                        }));

                        // Tạo số bí mật cho client (Chế độ đơn)
                        Random rd = new Random();
                        int secretNumber = rd.Next(1, 101);
                        clientSecrets[newClient] = secretNumber;

                        this.Invoke((Action)(() =>
                        {
                            listBox_Show.Items.Add($"(Số bí mật Client {clientID}: {secretNumber})");
                        }));

                        // --- XỬ LÝ RIÊNG CHO TỪNG CLIENT ---
                        Task.Run(() =>
                        {
                            try
                            {
                                byte[] buffer = new byte[1024];
                                while (true)
                                {
                                    if (!newClient.Connected) break;

                                    int received = newClient.Receive(buffer);
                                    if (received == 0) break;

                                    string msg = Encoding.UTF8.GetString(buffer, 0, received).Trim();

                                    // 1. Xử lý ngắt kết nối
                                    if (msg == "DISCONNECT")
                                    {
                                        this.Invoke((Action)(() =>
                                        {
                                            listBox_Show.Items.Add($"Client {clientID} gửi yêu cầu ngắt kết nối.");
                                            listBox_Show.Items.Add(" ");
                                        }));
                                        newClient.Shutdown(SocketShutdown.Both);
                                        newClient.Close();
                                        clientSecrets.Remove(newClient);
                                        break;
                                    }

                                    // 2. Xử lý chơi lại (Chế độ đơn)
                                    if (msg == "PLAY_AGAIN")
                                    {
                                        secretNumber = new Random().Next(1, 101);
                                        clientSecrets[newClient] = secretNumber;
                                        newClient.Send(Encoding.UTF8.GetBytes("Trò chơi đã được khởi động lại! Hãy đoán số mới!\n"));
                                        this.Invoke((Action)(() =>
                                        {
                                            listBox_Show.Items.Add($"Client {clientID} yêu cầu chơi lại → Đã tạo số mới!");
                                            listBox_Show.Items.Add($"Số bí mật mới Client {clientID}: {secretNumber}");
                                        }));
                                        continue;
                                    }

                                    // 3. Tạo phòng (Multiplayer)
                                    if (msg == "CREATE_ROOM")
                                    {
                                        string roomId = CreateOrJoinRoom(newClient);
                                        int playerIndex = roomsPlayers[roomId].Count;
                                        string roomMsg = $"ROOM:{roomId};PLAYER:{playerIndex}/2";
                                        newClient.Send(Encoding.UTF8.GetBytes(roomMsg + "\n"));
                                        continue;
                                    }

                                    // 4. Tham gia phòng cụ thể
                                    if (msg.StartsWith("JOIN_ROOM:"))
                                    {
                                        string roomId = msg.Split(':')[1];

                                        lock (roomsPlayers)
                                        {
                                            if (roomsPlayers.ContainsKey(roomId))
                                            {
                                                if (roomsPlayers[roomId].Count < 2)
                                                {
                                                    roomsPlayers[roomId].Add(newClient);
                                                    clientRoom[newClient] = roomId;
                                                    int playerIndex = roomsPlayers[roomId].Count;
                                                    string roomMsg = $"ROOM:{roomId}; PLAYER:{playerIndex}/2\n";
                                                    newClient.Send(Encoding.UTF8.GetBytes(roomMsg + "\n"));

                                                    this.Invoke((Action)(() =>
                                                    {
                                                        listBox_Show.Items.Add($"Client tham gia phòng {roomId}");
                                                    }));
                                                }
                                                else
                                                {
                                                    newClient.Send(Encoding.UTF8.GetBytes($"Server: Phòng {roomId} đã đầy\n"));
                                                }
                                            }
                                            else
                                            {
                                                newClient.Send(Encoding.UTF8.GetBytes($"Server: Phòng {roomId} không tồn tại\n"));
                                            }
                                        }
                                        continue;
                                    }

                                    // 5. Sẵn sàng (Ready)
                                    if (msg == "READY")
                                    {
                                        if (clientRoom.TryGetValue(newClient, out string roomId))
                                        {
                                            if (!roomReady.ContainsKey(roomId))
                                                roomReady[roomId] = new Dictionary<Socket, bool>();

                                            roomReady[roomId][newClient] = true;

                                            this.Invoke((Action)(() =>
                                            {
                                                listBox_Show.Items.Add($"Client {clientID} đã sẵn sàng trong phòng {roomId}");
                                            }));

                                            // Kiểm tra cả phòng (2 người) đã sẵn sàng chưa
                                            if (roomsPlayers[roomId].Count == 2 && roomsPlayers[roomId].All(c => roomReady[roomId].ContainsKey(c) && roomReady[roomId][c]))
                                            {
                                                foreach (var c in roomsPlayers[roomId])
                                                {
                                                    c.Send(Encoding.UTF8.GetBytes("ALL_READY\n"));
                                                }
                                            }
                                        }
                                        continue;
                                    }

                                    // 6. XỬ LÝ ĐOÁN SỐ TRONG PHÒNG (LOGIC MỚI ĐƯỢC CẬP NHẬT)
                                    if (msg.StartsWith("GUESS:"))
                                    {
                                        int guessed = int.Parse(msg.Split(':')[1]);

                                        if (clientRoom.TryGetValue(newClient, out string roomId))
                                        {
                                            if (!roomTurnIndex.ContainsKey(roomId))
                                                roomTurnIndex[roomId] = 0;

                                            int turn = roomTurnIndex[roomId];
                                            Socket currentPlayer = roomsPlayers[roomId][turn];

                                            if (currentPlayer != newClient)
                                            {
                                                newClient.Send(Encoding.UTF8.GetBytes("Chưa tới lượt bạn!\n"));
                                                continue;
                                            }

                                            int secret = roomsSecret[roomId];
                                            string response;
                                            bool isWin = false; // Cờ kiểm tra chiến thắng

                                            if (guessed < secret)
                                                response = "Số bạn đoán nhỏ hơn số bí mật phòng!";
                                            else if (guessed > secret)
                                                response = "Số bạn đoán lớn hơn số bí mật phòng!";
                                            else
                                            {
                                                response = "Chính xác!";
                                                isWin = true; // Đã có người thắng
                                            }

                                            // --- Gửi phản hồi cho cả 2 client ---
                                            foreach (var c in roomsPlayers[roomId])
                                            {
                                                string msgToClient;

                                                if (isWin)
                                                {
                                                    // Logic thắng/thua: Gửi thông điệp GAMEOVER
                                                    if (c == newClient)
                                                        msgToClient = $"GAMEOVER:Chúc mừng! Bạn đoán đúng số {secret}. BẠN ĐÃ THẮNG!";
                                                    else
                                                        msgToClient = $"GAMEOVER:Đối thủ đã đoán đúng số {secret}. BẠN ĐÃ THUA!";
                                                }
                                                else
                                                {
                                                    // Logic bình thường
                                                    if (c == newClient)
                                                        msgToClient = $"Bạn đoán: {guessed} → {response}";
                                                    else
                                                        msgToClient = $"Người chơi bên kia đoán: {guessed}";
                                                }

                                                c.Send(Encoding.UTF8.GetBytes(msgToClient + "\n"));
                                            }

                                            this.Invoke((Action)(() =>
                                            {
                                                string status = isWin ? " (WIN)" : "";
                                                listBox_Show.Items.Add($"Client {newClient.RemoteEndPoint} đoán: {guessed} → {response}{status}");
                                            }));

                                            // Chỉ chuyển lượt nếu CHƯA ai thắng
                                            if (!isWin)
                                            {
                                                roomTurnIndex[roomId] = (turn + 1) % roomsPlayers[roomId].Count;
                                                Socket nextPlayer = roomsPlayers[roomId][roomTurnIndex[roomId]];
                                                nextPlayer.Send(Encoding.UTF8.GetBytes("Đến lượt bạn đoán!\n"));
                                            }
                                        }
                                        continue;
                                    }

                                    // 7. Rời phòng
                                    if (msg == "DISCONNECT_ROOM")
                                    {
                                        if (clientRoom.TryGetValue(newClient, out string roomId))
                                        {
                                            RemoveClientFromRoom(newClient);

                                            if (roomsPlayers.ContainsKey(roomId) && roomsPlayers[roomId].Count > 0)
                                            {
                                                foreach (var c in roomsPlayers[roomId])
                                                {
                                                    try { c.Send(Encoding.UTF8.GetBytes("Đối thủ đã rời phòng, bạn sẽ chơi 1 mình.\n")); } catch { }
                                                }
                                            }

                                            this.Invoke((Action)(() =>
                                            {
                                                listBox_Show.Items.Add($"Client {newClient.RemoteEndPoint} đã rời phòng {roomId}");
                                            }));
                                        }
                                        continue;
                                    }

                                    // 8. Đoán số (Chế độ đơn)
                                    if (int.TryParse(msg, out int guessedNumber))
                                    {
                                        // Lưu ý: Đoạn này dùng clientSecrets của chế độ đơn
                                        // Nếu muốn chơi đơn ổn định, cần check xem client có trong clientSecrets không
                                        if (clientSecrets.ContainsKey(newClient))
                                        {
                                            int secret = clientSecrets[newClient];
                                            string response;

                                            if (guessedNumber < secret) response = "Số bạn đoán nhỏ hơn số bí mật!";
                                            else if (guessedNumber > secret) response = "Số bạn đoán lớn hơn số bí mật!";
                                            else response = "Chính xác! Bạn đã đoán đúng!";

                                            newClient.Send(Encoding.UTF8.GetBytes(response + "\n"));

                                            this.Invoke((Action)(() =>
                                            {
                                                listBox_Show.Items.Add($"Client {clientID} đoán: {guessedNumber} → {response}");
                                            }));
                                        }
                                    }
                                    else
                                    {
                                        // [FIXED] Sửa lỗi NullReferenceException ở đây
                                        // Dùng newClient thay vì biến client toàn cục
                                        newClient.Send(Encoding.UTF8.GetBytes("Vui lòng nhập số hợp lệ!\n"));
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // Xử lý khi Client ngắt kết nối đột ngột
                                this.Invoke((Action)(() =>
                                {
                                    // listBox_Show.Items.Add($"Client ngắt kết nối: {ex.Message}");
                                }));
                            }
                        });
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke((Action)(() =>
                    {
                        listBox_Show.Items.Add($"Lỗi server chính: {ex.Message}");
                    }));
                }
            });
        }

        private void buttonSTOP_Click(object sender, EventArgs e)
        {
            try
            {
                isRunning = false;
                sckClient?.Close();
                sckServer?.Close();
                listBox_Show.Items.Add(" ");
                listBox_Show.Items.Add(" Server đã dừng. Nhấn Start để chạy SERVER!");
                listBox_Show.Items.Add(" ");
            }
            catch (Exception ex)
            {
                listBox_Show.Items.Add(" Lỗi khi dừng server: " + ex.Message);
            }
        }

        private void listBox_Show_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private string CreateOrJoinRoom(Socket client)
        {
            lock (roomsPlayers)
            {
                // Tìm phòng còn trống (<2 người)
                string roomId = roomsPlayers.FirstOrDefault(r => r.Value.Count < 2).Key;

                if (roomId == null)
                {
                    // Tạo phòng mới
                    roomId = rnd.Next(100000, 1000000).ToString();
                    roomsPlayers[roomId] = new List<Socket>();
                    roomsSecret[roomId] = rnd.Next(1, 101); // số bí mật chung phòng
                }

                // Thêm client vào phòng
                roomsPlayers[roomId].Add(client);
                clientRoom[client] = roomId;

                this.Invoke((Action)(() =>
                {
                    listBox_Show.Items.Add($"Client vào phòng {roomId} (số bí mật: {roomsSecret[roomId]})");
                }));

                return roomId;
            }
        }

        private int GetRoomSecret(Socket client)
        {
            if (clientRoom.TryGetValue(client, out string roomId))
                return roomsSecret[roomId];
            return -1; // lỗi
        }

        private void RemoveClientFromRoom(Socket client)
        {
            lock (roomsPlayers)
            {
                if (clientRoom.TryGetValue(client, out string roomId))
                {
                    roomsPlayers[roomId].Remove(client);
                    clientRoom.Remove(client);

                    // Nếu phòng trống thì xóa luôn
                    if (roomsPlayers[roomId].Count == 0)
                    {
                        roomsPlayers.Remove(roomId);
                        roomsSecret.Remove(roomId);
                        roomReady.Remove(roomId); // Xóa luôn trạng thái ready
                        roomTurnIndex.Remove(roomId); // Xóa lượt chơi
                    }
                }
            }
        }
    }
}