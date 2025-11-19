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
        Socket client;
        bool isServer = false;
        int secretNumber = 0;
        bool isRunning = false;
        Random rnd = new Random();

        int clientCount = 0; // Biến đếm client
                             // Lưu trữ thông tin client: client socket → số bí mật
        Dictionary<Socket, int> clientSecrets = new Dictionary<Socket, int>();

        // Ở FormServer.cs, khai báo các Dictionary toàn cục
        Dictionary<string, List<Socket>> roomsPlayers = new Dictionary<string, List<Socket>>();
        Dictionary<string, int> roomsSecret = new Dictionary<string, int>();
        Dictionary<Socket, string> clientRoom = new Dictionary<Socket, string>();
        Dictionary<string, Dictionary<Socket, bool>> roomReady = new Dictionary<string, Dictionary<Socket, bool>>();
        Dictionary<string, int> roomTurnIndex = new Dictionary<string, int>(); // lượt ai đoán

        private class Room
        {
            public string RoomId { get; set; }
            public List<Socket> Players { get; } = new List<Socket>();
            public Dictionary<Socket, bool> Ready { get; } = new Dictionary<Socket, bool>();
            public int SecretNumber { get; set; }
        }

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

            Task.Run(() =>
            {
                try
                {
                    sckServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    sckServer.Bind(new IPEndPoint(IPAddress.Parse(ipAddress), portServer));
                    sckServer.Listen(5);

                    while (true)
                    {
                        Socket newClient = sckServer.Accept();
                        clientCount++; // Tăng số client
                        int clientID = clientCount;

                        this.Invoke((Action)(() =>
                        {
                            listBox_Show.Items.Add("");
                            listBox_Show.Items.Add($"Client {clientID} đã kết nối từ: {((IPEndPoint)newClient.RemoteEndPoint).Address}");
                            listBox_Show.Items.Add($"Bắt đầu trò chơi đoán số cho Client {clientID}!");
                        }));

                        // Tạo số bí mật cho client
                        Random rd = new Random();
                        int secretNumber = rd.Next(1, 101);
                        clientSecrets[newClient] = secretNumber;

                        this.Invoke((Action)(() =>
                        {
                            listBox_Show.Items.Add($"(Số bí mật Client {clientID}: {secretNumber})");
                        }));

                        // Xử lý client riêng
                        Task.Run(() =>
                        {
                            try
                            {
                                byte[] buffer = new byte[1024];
                                while (true)
                                {
                                    int received = newClient.Receive(buffer);
                                    if (received == 0) break;

                                    string msg = Encoding.UTF8.GetString(buffer, 0, received).Trim();

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

                                    // Tạo hoặc tham gia phòng
                                    if (msg == "CREATE_ROOM")
                                    {
                                        string roomId = CreateOrJoinRoom(newClient); // tạo/tham gia phòng
                                        int playerIndex = roomsPlayers[roomId].Count; // vị trí client trong phòng
                                        string roomMsg = $"ROOM:{roomId}; PLAYER:{playerIndex}/2";
                                        newClient.Send(Encoding.UTF8.GetBytes(roomMsg + "\n"));
                                        continue;
                                    }
                                    // Tham gia phòng
                                    if (msg.StartsWith("JOIN_ROOM:"))
                                    {
                                        string roomId = msg.Split(':')[1];

                                        lock (roomsPlayers) // bảo vệ truy cập Dictionary
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

                                    if (msg == "READY")
                                    {
                                        if (clientRoom.TryGetValue(newClient, out string roomId))
                                        {
                                            // Khởi tạo dictionary sẵn sàng cho phòng nếu chưa có
                                            if (!roomReady.ContainsKey(roomId))
                                                roomReady[roomId] = new Dictionary<Socket, bool>();

                                            // Đánh dấu client này đã sẵn sàng
                                            roomReady[roomId][newClient] = true;

                                            this.Invoke((Action)(() =>
                                            {
                                                listBox_Show.Items.Add($"Client {clientID} đã sẵn sàng trong phòng {roomId}");
                                            }));

                                            // Kiểm tra cả phòng (2 người) đã sẵn sàng chưa
                                            if (roomsPlayers[roomId].Count == 2 && roomsPlayers[roomId].All(c => roomReady[roomId].ContainsKey(c) && roomReady[roomId][c]))
                                            {
                                                // Gửi thông báo bắt đầu trò chơi tới cả 2 client
                                                foreach (var c in roomsPlayers[roomId])
                                                {
                                                    c.Send(Encoding.UTF8.GetBytes("ALL_READY\n"));
                                                }
                                            }
                                        }
                                        continue;
                                    }
                                   if (msg.StartsWith("GUESS:")){
                                        int guessed = int.Parse(msg.Split(':')[1]); // số đoán của client

                                        if (clientRoom.TryGetValue(newClient, out string roomId))
                                        {
                                            // Xác định lượt hiện tại
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

                                            if (guessed < secret) response = "Số bạn đoán nhỏ hơn số bí mật phòng!";
                                            else if (guessed > secret) response = "Số bạn đoán lớn hơn số bí mật phòng!";
                                            else response = "Chính xác! Bạn đã đoán đúng số bí mật phòng!";

                                            // --- Gửi phản hồi cho cả 2 client ---
                                            foreach (var c in roomsPlayers[roomId])
                                            {
                                                string msgToClient;
                                                if (c == newClient)
                                                    msgToClient = $"Bạn đoán: {guessed} → {response}";
                                                else
                                                    msgToClient = $"Người chơi bên kia đoán: {guessed}";

                                                c.Send(Encoding.UTF8.GetBytes(msgToClient + "\n"));
                                            }

                                            // Chuyển lượt
                                            roomTurnIndex[roomId] = (turn + 1) % roomsPlayers[roomId].Count;

                                            // Thông báo tới lượt người tiếp theo
                                            Socket nextPlayer = roomsPlayers[roomId][roomTurnIndex[roomId]];
                                            nextPlayer.Send(Encoding.UTF8.GetBytes("Đến lượt bạn đoán!\n"));

                                            this.Invoke((Action)(() =>
                                            {
                                                listBox_Show.Items.Add($"Client {newClient.RemoteEndPoint} đoán: {guessed} → {response}");
                                            }));
                                        }
                                    }

                                    if (msg == "DISCONNECT_ROOM")
                                    {
                                        if (clientRoom.TryGetValue(newClient, out string roomId))
                                        {
                                            // Xóa client khỏi phòng nhưng KHÔNG đóng socket
                                            RemoveClientFromRoom(newClient);

                                            // Thông báo cho người còn lại (nếu có) rằng họ đang chơi 1 mình
                                            if (roomsPlayers.ContainsKey(roomId) && roomsPlayers[roomId].Count > 0)
                                            {
                                                foreach (var c in roomsPlayers[roomId])
                                                {
                                                    try
                                                    {
                                                        c.Send(Encoding.UTF8.GetBytes("Đối thủ đã rời phòng, bạn sẽ chơi 1 mình.\n"));
                                                    }
                                                    catch { }
                                                }
                                            }

                                            // Hiển thị trên server
                                            this.Invoke((Action)(() =>
                                            {
                                                listBox_Show.Items.Add($"Client {newClient.RemoteEndPoint} đã rời phòng {roomId}");
                                            }));

                                            // KHÔNG đóng socket
                                        }
                                        continue;
                                    }

                                    // Đoán số
                                    if (int.TryParse(msg, out int guessedNumber))
                                    {
                                        int secret = GetRoomSecret(newClient); // dùng newClient
                                        string response;

                                        if (secret == -1)
                                            response = "Lỗi: không tìm thấy phòng!";
                                        else if (guessedNumber < secret)
                                            response = "Số bạn đoán nhỏ hơn số bí mật phòng!";
                                        else if (guessedNumber > secret)
                                            response = "Số bạn đoán lớn hơn số bí mật phòng!";
                                        else
                                            response = "Chính xác! Bạn đã đoán đúng số bí mật phòng!";

                                        newClient.Send(Encoding.UTF8.GetBytes(response + "\n"));

                                        this.Invoke((Action)(() =>
                                        {
                                            listBox_Show.Items.Add($"Client {clientID} đoán: {guessedNumber} → {response}");
                                        }));
                                    }

                                    else
                                    {
                                        client.Send(Encoding.UTF8.GetBytes("Vui lòng nhập số hợp lệ!\n"));
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                this.Invoke((Action)(() =>
                                {
                                    listBox_Show.Items.Add($"");
                                }));
                            }
                        }); // closes per-client Task.Run
                    } // closes while(true) for Accept()
                }
                catch (Exception ex)
                {
                    this.Invoke((Action)(() =>
                    {
                        listBox_Show.Items.Add($"Lỗi server333: {ex.Message}");
                    }));
                }
            }); // closes main server Task.Run
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
                    }
                }
            }
        }



    }

}



