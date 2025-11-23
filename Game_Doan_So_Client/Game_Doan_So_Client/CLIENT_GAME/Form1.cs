using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CLIENT_GAME
{
    public partial class Form_Exit_phong : Form
    {
        Socket clientSocket;
        public Form_Exit_phong()
        {
            InitializeComponent();
        }

        private void numericPort_ValueChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                // --- Lấy IP và PORT từ giao diện ---
                string ipInput = maskedTextBox1.Text.Replace(" ", "").Replace("_", "").Trim();
                int port = (int)numericPort.Value;

                // --- Kiểm tra IP hợp lệ ---
                if (!IPAddress.TryParse(ipInput, out IPAddress ipAddress))
                {
                    MessageBox.Show("Địa chỉ IP không hợp lệ!");
                    return;
                }

                if (port <= 0 || port > 65535)
                {
                    MessageBox.Show("Port không hợp lệ!");
                    return;
                }

                // --- Tạo socket ---
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint serverEP = new IPEndPoint(ipAddress, port);

                // --- Kết nối tới server ---
                clientSocket.Connect(serverEP);
                listBox_Connect.Items.Add($"Đã kết nối tới server với IP: {ipAddress} và Port: {port}" + Environment.NewLine);

                // --- Lắng nghe phản hồi từ server ---
                Task.Run(() => ReceiveFromServer());
            }
            catch (Exception ex)
            {
                listBox_Connect.Items.Add($"Không thể kết nối tới server: {ex.Message}" + Environment.NewLine);
            }
        }
        private void ReceiveFromServer()
        {
            Task.Run(() =>
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    while (true)
                    {
                        if (clientSocket == null || !clientSocket.Connected) break;

                        int received = clientSocket.Receive(buffer);
                        if (received == 0) break;

                        string msg = Encoding.UTF8.GetString(buffer, 0, received).Trim();

                        this.Invoke((Action)(() =>
                        {
                            // Hiển thị tất cả tin nhắn server
                            listBox_Connect.Items.Add(msg);

                            // Nếu nhận được thông báo đối thủ thoát phòng
                            if (msg.Contains("Người chơi bên kia đã thoát"))
                            {
                                MessageBox.Show("Người chơi bên kia đã thoát phòng!",
                                                "Thông báo",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Information);
                                this.Close(); // đóng form phòng
                            }

                            // Nếu nhận ROOM info
                            else if (msg.StartsWith("ROOM:"))
                            {
                                string[] parts = msg.Split(';');
                                string roomId = parts[0].Split(':')[1];
                                string playerInfo = parts[1].Split(':')[1];

                                textBox1.Text = roomId;
                                listBox_Connect.Items.Add($"Bạn tham gia phòng {roomId}");
                                listBox_Connect.Items.Add($"Bạn là người chơi {playerInfo}");
                            }

                            // Nếu nhận thông báo sẵn sàng bắt đầu
                            else if (msg == "ALL_READY")
                            {
                                listBox_Connect.Items.Add("Cả 2 người chơi đã sẵn sàng! Bắt đầu đoán số ngay.");
                            }

                            // Có thể thêm các thông báo khác từ server


                        }));
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke((Action)(() =>
                    {
                        listBox_Connect.Items.Add("Mất kết nối với server: " + ex.Message);
                    }));
                }
            });
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
       "Bạn có muốn thoát khỏi game không?",
       "Xác nhận thoát",
       MessageBoxButtons.YesNo,
       MessageBoxIcon.Question
   );

            if (result == DialogResult.Yes)
            {
                try
                {
                    clientSocket?.Close();   // đóng kết nối nếu có
                    Application.Exit();       // thoát chương trình
                }
                catch
                {
                    Application.Exit();
                }
            }
            else
            {
                // Người dùng chọn "No" → không thoát
                listBox_Connect.Items.Add("Bạn vẫn đang trong game.");
                listBox_Connect.Items.Add(" ");
            }
        }

        private void button_Nhap_so_Click(object sender, EventArgs e)
        {
            try
            {
                if (clientSocket == null || !clientSocket.Connected)
                {
                    MessageBox.Show("Bạn chưa kết nối đến server!");
                    return;
                }

                int soDoan = (int)numericUpDown1.Value;

                // Thêm tiền tố GUESS để server phân biệt đoán số
                string message = $"GUESS:{soDoan}\n";
                clientSocket.Send(Encoding.UTF8.GetBytes(message));

                listBox_Connect.Items.Add($"Bạn đoán: {soDoan}");
            }
            catch (Exception ex)
            {
                listBox_Connect.Items.Add($"Lỗi khi gửi số: {ex.Message}");
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button_Again_Click(object sender, EventArgs e)
        {
            try
            {
                if (clientSocket != null && clientSocket.Connected)
                {
                    listBox_Connect.Items.Clear();

                    string message = "PLAY_AGAIN";
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    clientSocket.Send(data);
                    listBox_Connect.Items.Add(" ");
                    listBox_Connect.Items.Add("Đã gửi yêu cầu chơi lại tới server...");
                }
                else
                {
                    MessageBox.Show("Bạn chưa kết nối đến server!");
                }
            }
            catch (Exception ex)
            {
                listBox_Connect.Items.Add($"Lỗi khi gửi yêu cầu chơi lại: {ex.Message}");
            }
        }

        private void button_Disconnect_Click(object sender, EventArgs e)
        {
            if (clientSocket != null && clientSocket.Connected)
            {
                // Gửi thông báo ngắt kết nối (không phải EXIT)
                clientSocket.Send(Encoding.UTF8.GetBytes("DISCONNECT"));

                // Đóng kết nối socket an toàn
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                clientSocket = null;  

                listBox_Connect.Items.Add(" ");
                listBox_Connect.Items.Add("Đã ngắt kết nối với server.");
            }
            else
            {
                listBox_Connect.Items.Add("Chưa có kết nối nào để ngắt.");
            }
        }

        private void button_Tao_phong_Click(object sender, EventArgs e)
        {
            if (clientSocket == null || !clientSocket.Connected)
            {
                MessageBox.Show("Chưa kết nối server!");
                return;
            }

            // Gửi yêu cầu tạo phòng
            byte[] request = Encoding.UTF8.GetBytes("CREATE_ROOM\n");
            clientSocket.Send(request);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void numericUpDown_Maphong_ValueChanged(object sender, EventArgs e)
        {
        }
        private void button_vao_phong_Click(object sender, EventArgs e)
        {
            if (clientSocket != null && clientSocket.Connected)
            {
                string roomId = numericUpDown_Maphong.Value.ToString();
                string joinMsg = $"JOIN_ROOM:{roomId}\n";
                clientSocket.Send(Encoding.UTF8.GetBytes(joinMsg));
                listBox_Connect.Items.Add($"Đã gửi yêu cầu vào phòng {roomId}");
            }
        }
        private void button_Thoat_phong_Click(object sender, EventArgs e)
        {
            try
            {
                if (clientSocket != null && clientSocket.Connected)
                {
                    clientSocket.Send(Encoding.UTF8.GetBytes("DISCONNECT_ROOM\n"));
                }

                // Thay vì dùng panel, chỉ hiển thị thông báo
                MessageBox.Show("Bạn đã rời phòng, vẫn có thể chơi tiếp hoặc vào phòng khác.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi rời phòng: " + ex.Message);
            }
        }


        private void button_San_Sang_Click(object sender, EventArgs e)
        {
            try
            {
                if (clientSocket != null && clientSocket.Connected)
                {
                    clientSocket.Send(Encoding.UTF8.GetBytes("READY\n"));
                    listBox_Connect.Items.Add("Bạn đã nhấn Sẵn sàng, đang chờ đối phương...");
                }
                else
                {
                    MessageBox.Show("Bạn chưa kết nối đến server!");
                }
            }
            catch (Exception ex)
            {
                listBox_Connect.Items.Add($"Lỗi khi gửi trạng thái sẵn sàng: {ex.Message}");
            }
        }

        private void listBox_Connect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

