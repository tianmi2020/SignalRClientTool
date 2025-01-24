namespace SignalRClientTool
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnConnect = new Button();
            label1 = new Label();
            txtLog = new TextBox();
            label2 = new Label();
            cmbMethodName = new ComboBox();
            btnBindListener = new Button();
            btnRemoveListener = new Button();
            label3 = new Label();
            cmbHubUrl = new ComboBox();
            cmbServerMethodName = new ComboBox();
            label4 = new Label();
            btnSend = new Button();
            txtMessage = new TextBox();
            label5 = new Label();
            SuspendLayout();
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(587, 6);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(75, 23);
            btnConnect.TabIndex = 1;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(70, 15);
            label1.TabIndex = 2;
            label1.Text = "WS address:";
            // 
            // txtLog
            // 
            txtLog.Location = new Point(672, 42);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(538, 593);
            txtLog.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(672, 9);
            label2.Name = "label2";
            label2.Size = new Size(30, 15);
            label2.TabIndex = 4;
            label2.Text = "Log:";
            // 
            // cmbMethodName
            // 
            cmbMethodName.FormattingEnabled = true;
            cmbMethodName.Location = new Point(106, 42);
            cmbMethodName.Name = "cmbMethodName";
            cmbMethodName.Size = new Size(273, 23);
            cmbMethodName.TabIndex = 7;
            // 
            // btnBindListener
            // 
            btnBindListener.Location = new Point(385, 44);
            btnBindListener.Name = "btnBindListener";
            btnBindListener.Size = new Size(75, 23);
            btnBindListener.TabIndex = 8;
            btnBindListener.Text = "Listen";
            btnBindListener.UseVisualStyleBackColor = true;
            btnBindListener.Click += btnBindListener_Click;
            // 
            // btnRemoveListener
            // 
            btnRemoveListener.Location = new Point(466, 43);
            btnRemoveListener.Name = "btnRemoveListener";
            btnRemoveListener.Size = new Size(75, 23);
            btnRemoveListener.TabIndex = 9;
            btnRemoveListener.Text = "Remove";
            btnRemoveListener.UseVisualStyleBackColor = true;
            btnRemoveListener.Click += btnRemoveListener_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 42);
            label3.Name = "label3";
            label3.Size = new Size(52, 15);
            label3.TabIndex = 10;
            label3.Text = "Method:";
            // 
            // cmbHubUrl
            // 
            cmbHubUrl.FormattingEnabled = true;
            cmbHubUrl.Location = new Point(106, 5);
            cmbHubUrl.Name = "cmbHubUrl";
            cmbHubUrl.Size = new Size(466, 23);
            cmbHubUrl.TabIndex = 11;
            // 
            // cmbServerMethodName
            // 
            cmbServerMethodName.FormattingEnabled = true;
            cmbServerMethodName.Location = new Point(106, 74);
            cmbServerMethodName.Name = "cmbServerMethodName";
            cmbServerMethodName.Size = new Size(273, 23);
            cmbServerMethodName.TabIndex = 17;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 73);
            label4.Name = "label4";
            label4.Size = new Size(87, 15);
            label4.TabIndex = 16;
            label4.Text = "Server Method:";
            // 
            // btnSend
            // 
            btnSend.Location = new Point(385, 73);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(75, 23);
            btnSend.TabIndex = 15;
            btnSend.Text = "Invoke";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(12, 122);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(627, 503);
            txtMessage.TabIndex = 14;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 104);
            label5.Name = "label5";
            label5.Size = new Size(97, 15);
            label5.TabIndex = 18;
            label5.Text = "Invoke with Json:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1212, 637);
            Controls.Add(label5);
            Controls.Add(cmbServerMethodName);
            Controls.Add(label4);
            Controls.Add(btnSend);
            Controls.Add(txtMessage);
            Controls.Add(cmbHubUrl);
            Controls.Add(label3);
            Controls.Add(btnRemoveListener);
            Controls.Add(btnBindListener);
            Controls.Add(cmbMethodName);
            Controls.Add(label2);
            Controls.Add(txtLog);
            Controls.Add(label1);
            Controls.Add(btnConnect);
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Asp.net SignalR Test ";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnConnect;
        private Label label1;
        private TextBox txtLog;
        private Label label2;
        private ComboBox cmbMethodName;
        private Button btnBindListener;
        private Button btnRemoveListener;
        private Label label3;
        private ComboBox cmbHubUrl;
        private ComboBox cmbServerMethodName;
        private Label label4;
        private Button btnSend;
        private TextBox txtMessage;
        private Label label5;
    }
}
