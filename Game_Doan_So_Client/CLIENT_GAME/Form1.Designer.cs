namespace CLIENT_GAME
{
    partial class Form_Exit_phong
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_Welcome = new System.Windows.Forms.Label();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.label_Port = new System.Windows.Forms.Label();
            this.label_IP = new System.Windows.Forms.Label();
            this.numericPort = new System.Windows.Forms.NumericUpDown();
            this.textBox_Port_IP = new System.Windows.Forms.TextBox();
            this.button_Connect = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.button_Nhap_so = new System.Windows.Forms.Button();
            this.listBox_Connect = new System.Windows.Forms.ListBox();
            this.button_Again = new System.Windows.Forms.Button();
            this.button_Disconnect = new System.Windows.Forms.Button();
            this.button_Tao_phong = new System.Windows.Forms.Button();
            this.label_Nhapmaphong = new System.Windows.Forms.Label();
            this.numericUpDown_Maphong = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button_vao_phong = new System.Windows.Forms.Button();
            this.button_Thoat_phong = new System.Windows.Forms.Button();
            this.button_San_Sang = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Maphong)).BeginInit();
            this.SuspendLayout();
            // 
            // label_Welcome
            // 
            this.label_Welcome.AutoSize = true;
            this.label_Welcome.Font = new System.Drawing.Font("Segoe UI Black", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Welcome.Location = new System.Drawing.Point(271, 32);
            this.label_Welcome.Name = "label_Welcome";
            this.label_Welcome.Size = new System.Drawing.Size(300, 47);
            this.label_Welcome.TabIndex = 6;
            this.label_Welcome.Text = "GAME ĐOÁN SỐ";
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(259, 133);
            this.maskedTextBox1.Mask = "999.999.999.999";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(115, 20);
            this.maskedTextBox1.TabIndex = 1;
            this.maskedTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.maskedTextBox1.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.maskedTextBox1_MaskInputRejected);
            // 
            // label_Port
            // 
            this.label_Port.AutoSize = true;
            this.label_Port.Font = new System.Drawing.Font("Microsoft New Tai Lue", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Port.ForeColor = System.Drawing.Color.DimGray;
            this.label_Port.Location = new System.Drawing.Point(15, 132);
            this.label_Port.Name = "label_Port";
            this.label_Port.Size = new System.Drawing.Size(51, 21);
            this.label_Port.TabIndex = 3;
            this.label_Port.Text = "PORT";
            // 
            // label_IP
            // 
            this.label_IP.AutoSize = true;
            this.label_IP.Font = new System.Drawing.Font("Microsoft New Tai Lue", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_IP.ForeColor = System.Drawing.Color.DimGray;
            this.label_IP.Location = new System.Drawing.Point(219, 133);
            this.label_IP.Name = "label_IP";
            this.label_IP.Size = new System.Drawing.Size(25, 21);
            this.label_IP.TabIndex = 4;
            this.label_IP.Text = "IP";
            // 
            // numericPort
            // 
            this.numericPort.Location = new System.Drawing.Point(83, 132);
            this.numericPort.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericPort.Name = "numericPort";
            this.numericPort.Size = new System.Drawing.Size(120, 20);
            this.numericPort.TabIndex = 0;
            this.numericPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericPort.ValueChanged += new System.EventHandler(this.numericPort_ValueChanged);
            // 
            // textBox_Port_IP
            // 
            this.textBox_Port_IP.BackColor = System.Drawing.Color.PowderBlue;
            this.textBox_Port_IP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_Port_IP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Port_IP.ForeColor = System.Drawing.Color.Red;
            this.textBox_Port_IP.Location = new System.Drawing.Point(48, 91);
            this.textBox_Port_IP.Multiline = true;
            this.textBox_Port_IP.Name = "textBox_Port_IP";
            this.textBox_Port_IP.ReadOnly = true;
            this.textBox_Port_IP.Size = new System.Drawing.Size(708, 36);
            this.textBox_Port_IP.TabIndex = 6;
            this.textBox_Port_IP.Text = "Yêu cầu: Khi bạn đã kết nối vào. Server sẽ random số từ 1-100 bạn hãy chọn đúng s" +
    "ố Server đã cho.";
            this.textBox_Port_IP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_Connect
            // 
            this.button_Connect.BackColor = System.Drawing.Color.Beige;
            this.button_Connect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Connect.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Connect.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button_Connect.Location = new System.Drawing.Point(392, 123);
            this.button_Connect.Name = "button_Connect";
            this.button_Connect.Size = new System.Drawing.Size(120, 38);
            this.button_Connect.TabIndex = 2;
            this.button_Connect.Text = "CONNECT";
            this.button_Connect.UseVisualStyleBackColor = false;
            this.button_Connect.Click += new System.EventHandler(this.button_Connect_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Beige;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(693, 123);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 38);
            this.button1.TabIndex = 5;
            this.button1.Text = "EXIT";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown1.Location = new System.Drawing.Point(359, 203);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(182, 24);
            this.numericUpDown1.TabIndex = 3;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // button_Nhap_so
            // 
            this.button_Nhap_so.BackColor = System.Drawing.Color.Beige;
            this.button_Nhap_so.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Nhap_so.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Nhap_so.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Nhap_so.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button_Nhap_so.Location = new System.Drawing.Point(568, 199);
            this.button_Nhap_so.Name = "button_Nhap_so";
            this.button_Nhap_so.Size = new System.Drawing.Size(88, 33);
            this.button_Nhap_so.TabIndex = 3;
            this.button_Nhap_so.Text = "Nhập số";
            this.button_Nhap_so.UseVisualStyleBackColor = false;
            this.button_Nhap_so.Click += new System.EventHandler(this.button_Nhap_so_Click);
            // 
            // listBox_Connect
            // 
            this.listBox_Connect.BackColor = System.Drawing.Color.PowderBlue;
            this.listBox_Connect.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox_Connect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox_Connect.ForeColor = System.Drawing.Color.Black;
            this.listBox_Connect.FormattingEnabled = true;
            this.listBox_Connect.ItemHeight = 20;
            this.listBox_Connect.Location = new System.Drawing.Point(121, 288);
            this.listBox_Connect.Name = "listBox_Connect";
            this.listBox_Connect.Size = new System.Drawing.Size(600, 240);
            this.listBox_Connect.TabIndex = 16;
            this.listBox_Connect.Tag = "     ";
            this.listBox_Connect.SelectedIndexChanged += new System.EventHandler(this.listBox_Connect_SelectedIndexChanged);
            // 
            // button_Again
            // 
            this.button_Again.BackColor = System.Drawing.Color.Beige;
            this.button_Again.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Again.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Again.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Again.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button_Again.Location = new System.Drawing.Point(693, 198);
            this.button_Again.Name = "button_Again";
            this.button_Again.Size = new System.Drawing.Size(88, 33);
            this.button_Again.TabIndex = 4;
            this.button_Again.Text = "Chơi lại";
            this.button_Again.UseVisualStyleBackColor = false;
            this.button_Again.Click += new System.EventHandler(this.button_Again_Click);
            // 
            // button_Disconnect
            // 
            this.button_Disconnect.BackColor = System.Drawing.Color.Beige;
            this.button_Disconnect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Disconnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Disconnect.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Disconnect.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button_Disconnect.Location = new System.Drawing.Point(535, 123);
            this.button_Disconnect.Name = "button_Disconnect";
            this.button_Disconnect.Size = new System.Drawing.Size(138, 38);
            this.button_Disconnect.TabIndex = 17;
            this.button_Disconnect.Text = "DISCONNECT";
            this.button_Disconnect.UseVisualStyleBackColor = false;
            this.button_Disconnect.Click += new System.EventHandler(this.button_Disconnect_Click);
            // 
            // button_Tao_phong
            // 
            this.button_Tao_phong.BackColor = System.Drawing.Color.Beige;
            this.button_Tao_phong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Tao_phong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Tao_phong.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Tao_phong.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button_Tao_phong.Location = new System.Drawing.Point(19, 198);
            this.button_Tao_phong.Name = "button_Tao_phong";
            this.button_Tao_phong.Size = new System.Drawing.Size(89, 33);
            this.button_Tao_phong.TabIndex = 18;
            this.button_Tao_phong.Text = "Tạo phòng";
            this.button_Tao_phong.UseVisualStyleBackColor = false;
            this.button_Tao_phong.Click += new System.EventHandler(this.button_Tao_phong_Click);
            // 
            // label_Nhapmaphong
            // 
            this.label_Nhapmaphong.AutoSize = true;
            this.label_Nhapmaphong.Font = new System.Drawing.Font("Microsoft New Tai Lue", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Nhapmaphong.ForeColor = System.Drawing.Color.DimGray;
            this.label_Nhapmaphong.Location = new System.Drawing.Point(15, 249);
            this.label_Nhapmaphong.Name = "label_Nhapmaphong";
            this.label_Nhapmaphong.Size = new System.Drawing.Size(135, 21);
            this.label_Nhapmaphong.TabIndex = 19;
            this.label_Nhapmaphong.Text = "Nhập mã phòng";
            // 
            // numericUpDown_Maphong
            // 
            this.numericUpDown_Maphong.Location = new System.Drawing.Point(156, 249);
            this.numericUpDown_Maphong.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numericUpDown_Maphong.Name = "numericUpDown_Maphong";
            this.numericUpDown_Maphong.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown_Maphong.TabIndex = 20;
            this.numericUpDown_Maphong.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown_Maphong.ValueChanged += new System.EventHandler(this.numericUpDown_Maphong_ValueChanged);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(156, 199);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(120, 28);
            this.textBox1.TabIndex = 21;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button_vao_phong
            // 
            this.button_vao_phong.BackColor = System.Drawing.Color.Beige;
            this.button_vao_phong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_vao_phong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_vao_phong.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_vao_phong.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button_vao_phong.Location = new System.Drawing.Point(310, 242);
            this.button_vao_phong.Name = "button_vao_phong";
            this.button_vao_phong.Size = new System.Drawing.Size(97, 33);
            this.button_vao_phong.TabIndex = 22;
            this.button_vao_phong.Text = "Vào phòng";
            this.button_vao_phong.UseVisualStyleBackColor = false;
            this.button_vao_phong.Click += new System.EventHandler(this.button_vao_phong_Click);
            // 
            // button_Thoat_phong
            // 
            this.button_Thoat_phong.BackColor = System.Drawing.Color.Beige;
            this.button_Thoat_phong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Thoat_phong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Thoat_phong.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Thoat_phong.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button_Thoat_phong.Location = new System.Drawing.Point(438, 242);
            this.button_Thoat_phong.Name = "button_Thoat_phong";
            this.button_Thoat_phong.Size = new System.Drawing.Size(103, 33);
            this.button_Thoat_phong.TabIndex = 23;
            this.button_Thoat_phong.Text = "Thoát phòng";
            this.button_Thoat_phong.UseVisualStyleBackColor = false;
            this.button_Thoat_phong.Click += new System.EventHandler(this.button_Thoat_phong_Click);
            // 
            // button_San_Sang
            // 
            this.button_San_Sang.BackColor = System.Drawing.Color.Beige;
            this.button_San_Sang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_San_Sang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_San_Sang.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_San_Sang.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button_San_Sang.Location = new System.Drawing.Point(568, 242);
            this.button_San_Sang.Name = "button_San_Sang";
            this.button_San_Sang.Size = new System.Drawing.Size(88, 33);
            this.button_San_Sang.TabIndex = 24;
            this.button_San_Sang.Text = "Sẵn sàng";
            this.button_San_Sang.UseVisualStyleBackColor = false;
            this.button_San_Sang.Click += new System.EventHandler(this.button_San_Sang_Click);
            // 
            // Form_Exit_phong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(800, 540);
            this.Controls.Add(this.button_San_Sang);
            this.Controls.Add(this.button_Thoat_phong);
            this.Controls.Add(this.button_vao_phong);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.numericUpDown_Maphong);
            this.Controls.Add(this.label_Nhapmaphong);
            this.Controls.Add(this.button_Tao_phong);
            this.Controls.Add(this.button_Disconnect);
            this.Controls.Add(this.button_Again);
            this.Controls.Add(this.listBox_Connect);
            this.Controls.Add(this.button_Nhap_so);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_Connect);
            this.Controls.Add(this.textBox_Port_IP);
            this.Controls.Add(this.numericPort);
            this.Controls.Add(this.label_IP);
            this.Controls.Add(this.label_Port);
            this.Controls.Add(this.maskedTextBox1);
            this.Controls.Add(this.label_Welcome);
            this.Name = "Form_Exit_phong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.numericPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Maphong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Welcome;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.Label label_Port;
        private System.Windows.Forms.Label label_IP;
        private System.Windows.Forms.NumericUpDown numericPort;
        private System.Windows.Forms.TextBox textBox_Port_IP;
        private System.Windows.Forms.Button button_Connect;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button button_Nhap_so;
        private System.Windows.Forms.ListBox listBox_Connect;
        private System.Windows.Forms.Button button_Again;
        private System.Windows.Forms.Button button_Disconnect;
        private System.Windows.Forms.Button button_Tao_phong;
        private System.Windows.Forms.Label label_Nhapmaphong;
        private System.Windows.Forms.NumericUpDown numericUpDown_Maphong;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_vao_phong;
        private System.Windows.Forms.Button button_Thoat_phong;
        private System.Windows.Forms.Button button_San_Sang;
    }
}

