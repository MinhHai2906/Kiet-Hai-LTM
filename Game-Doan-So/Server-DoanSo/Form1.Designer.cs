namespace Server_DoanSo
{
    partial class FormServer
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
            this.numericPORT = new System.Windows.Forms.NumericUpDown();
            this.label_Port = new System.Windows.Forms.Label();
            this.label_Wecome = new System.Windows.Forms.Label();
            this.listBox_Show = new System.Windows.Forms.ListBox();
            this.button_Start = new System.Windows.Forms.Button();
            this.buttonSTOP = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericPORT)).BeginInit();
            this.SuspendLayout();
            // 
            // numericPORT
            // 
            this.numericPORT.Location = new System.Drawing.Point(177, 143);
            this.numericPORT.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericPORT.Minimum = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            this.numericPORT.Name = "numericPORT";
            this.numericPORT.Size = new System.Drawing.Size(120, 20);
            this.numericPORT.TabIndex = 0;
            this.numericPORT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericPORT.Value = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            this.numericPORT.ValueChanged += new System.EventHandler(this.numericPORT_ValueChanged);
            // 
            // label_Port
            // 
            this.label_Port.AutoSize = true;
            this.label_Port.Font = new System.Drawing.Font("Microsoft New Tai Lue", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Port.ForeColor = System.Drawing.Color.Gainsboro;
            this.label_Port.Location = new System.Drawing.Point(110, 143);
            this.label_Port.Name = "label_Port";
            this.label_Port.Size = new System.Drawing.Size(51, 21);
            this.label_Port.TabIndex = 1;
            this.label_Port.Text = "PORT";
            // 
            // label_Wecome
            // 
            this.label_Wecome.AutoSize = true;
            this.label_Wecome.Font = new System.Drawing.Font("Yu Gothic", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Wecome.ForeColor = System.Drawing.Color.Silver;
            this.label_Wecome.Location = new System.Drawing.Point(135, 43);
            this.label_Wecome.Name = "label_Wecome";
            this.label_Wecome.Size = new System.Drawing.Size(542, 61);
            this.label_Wecome.TabIndex = 6;
            this.label_Wecome.Text = "WECOME TO SERVER";
            this.label_Wecome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBox_Show
            // 
            this.listBox_Show.BackColor = System.Drawing.Color.Silver;
            this.listBox_Show.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox_Show.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox_Show.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.listBox_Show.FormattingEnabled = true;
            this.listBox_Show.ItemHeight = 21;
            this.listBox_Show.Items.AddRange(new object[] {
            "                                                    HIỂN THỊ NGƯỜI DÙNG KẾT NỐI V" +
                "ÀO"});
            this.listBox_Show.Location = new System.Drawing.Point(46, 203);
            this.listBox_Show.Name = "listBox_Show";
            this.listBox_Show.Size = new System.Drawing.Size(706, 357);
            this.listBox_Show.TabIndex = 7;
            this.listBox_Show.SelectedIndexChanged += new System.EventHandler(this.listBox_Show_SelectedIndexChanged);
            // 
            // button_Start
            // 
            this.button_Start.BackColor = System.Drawing.Color.Silver;
            this.button_Start.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Start.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_Start.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Start.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button_Start.Location = new System.Drawing.Point(411, 134);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(111, 37);
            this.button_Start.TabIndex = 0;
            this.button_Start.Text = "START ";
            this.button_Start.UseVisualStyleBackColor = false;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // buttonSTOP
            // 
            this.buttonSTOP.BackColor = System.Drawing.Color.Silver;
            this.buttonSTOP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSTOP.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonSTOP.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSTOP.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonSTOP.Location = new System.Drawing.Point(550, 134);
            this.buttonSTOP.Name = "buttonSTOP";
            this.buttonSTOP.Size = new System.Drawing.Size(111, 37);
            this.buttonSTOP.TabIndex = 1;
            this.buttonSTOP.Text = "STOP";
            this.buttonSTOP.UseVisualStyleBackColor = false;
            this.buttonSTOP.Click += new System.EventHandler(this.buttonSTOP_Click);
            // 
            // FormServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(795, 587);
            this.Controls.Add(this.buttonSTOP);
            this.Controls.Add(this.button_Start);
            this.Controls.Add(this.listBox_Show);
            this.Controls.Add(this.label_Wecome);
            this.Controls.Add(this.label_Port);
            this.Controls.Add(this.numericPORT);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server";
            ((System.ComponentModel.ISupportInitialize)(this.numericPORT)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericPORT;
        private System.Windows.Forms.Label label_Port;
        private System.Windows.Forms.Label label_Wecome;
        private System.Windows.Forms.ListBox listBox_Show;
        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.Button buttonSTOP;
    }
}

