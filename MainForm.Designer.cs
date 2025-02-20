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
            btnClear = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel7 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            panel4 = new Panel();
            tableLayoutPanel6 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            tableLayoutPanel5 = new TableLayoutPanel();
            ckJsonParameter = new CheckBox();
            tableLayoutPanel8 = new TableLayoutPanel();
            txtParameter = new TextBox();
            lblParamExample = new TextBox();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            panel4.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel8.SuspendLayout();
            SuspendLayout();
            // 
            // btnConnect
            // 
            btnConnect.Anchor = AnchorStyles.Left;
            btnConnect.Location = new Point(618, 5);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(75, 23);
            btnConnect.TabIndex = 1;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(3, 9);
            label1.Name = "label1";
            label1.Size = new Size(94, 15);
            label1.TabIndex = 2;
            label1.Text = "WS address:";
            // 
            // txtLog
            // 
            txtLog.BorderStyle = BorderStyle.FixedSingle;
            txtLog.Dock = DockStyle.Fill;
            txtLog.Location = new Point(3, 38);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(473, 650);
            txtLog.TabIndex = 3;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(3, 7);
            label2.Name = "label2";
            label2.Size = new Size(30, 15);
            label2.TabIndex = 4;
            label2.Text = "Log:";
            // 
            // cmbMethodName
            // 
            cmbMethodName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbMethodName.FormattingEnabled = true;
            cmbMethodName.Location = new Point(103, 5);
            cmbMethodName.Name = "cmbMethodName";
            cmbMethodName.Size = new Size(429, 23);
            cmbMethodName.TabIndex = 7;
            // 
            // btnBindListener
            // 
            btnBindListener.Anchor = AnchorStyles.Left;
            btnBindListener.Location = new Point(538, 5);
            btnBindListener.Name = "btnBindListener";
            btnBindListener.Size = new Size(74, 23);
            btnBindListener.TabIndex = 8;
            btnBindListener.Text = "Listen";
            btnBindListener.UseVisualStyleBackColor = true;
            btnBindListener.Click += btnBindListener_Click;
            // 
            // btnRemoveListener
            // 
            btnRemoveListener.Anchor = AnchorStyles.Left;
            btnRemoveListener.Location = new Point(618, 5);
            btnRemoveListener.Name = "btnRemoveListener";
            btnRemoveListener.Size = new Size(75, 23);
            btnRemoveListener.TabIndex = 9;
            btnRemoveListener.Text = "Remove";
            btnRemoveListener.UseVisualStyleBackColor = true;
            btnRemoveListener.Click += btnRemoveListener_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(3, 9);
            label3.Name = "label3";
            label3.Size = new Size(94, 15);
            label3.TabIndex = 10;
            label3.Text = "Method:";
            // 
            // cmbHubUrl
            // 
            cmbHubUrl.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbHubUrl.FormattingEnabled = true;
            cmbHubUrl.Location = new Point(103, 5);
            cmbHubUrl.Name = "cmbHubUrl";
            cmbHubUrl.Size = new Size(509, 23);
            cmbHubUrl.TabIndex = 11;
            // 
            // cmbServerMethodName
            // 
            cmbServerMethodName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbServerMethodName.FormattingEnabled = true;
            cmbServerMethodName.Location = new Point(103, 5);
            cmbServerMethodName.Name = "cmbServerMethodName";
            cmbServerMethodName.Size = new Size(509, 23);
            cmbServerMethodName.TabIndex = 17;
            cmbServerMethodName.SelectedIndexChanged += cmbServerMethodName_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(3, 9);
            label4.Name = "label4";
            label4.Size = new Size(94, 15);
            label4.TabIndex = 16;
            label4.Text = "Server Method:";
            // 
            // btnSend
            // 
            btnSend.Anchor = AnchorStyles.Left;
            btnSend.Location = new Point(618, 5);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(75, 23);
            btnSend.TabIndex = 15;
            btnSend.Text = "Invoke";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Left;
            btnClear.Location = new Point(376, 3);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(75, 23);
            btnClear.TabIndex = 19;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1212, 697);
            tableLayoutPanel1.TabIndex = 20;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(txtLog, 0, 1);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel7, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(730, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(479, 691);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.ColumnCount = 3;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel7.Controls.Add(btnClear, 2, 0);
            tableLayoutPanel7.Controls.Add(label2, 0, 0);
            tableLayoutPanel7.Dock = DockStyle.Fill;
            tableLayoutPanel7.Location = new Point(3, 3);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 1;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.Size = new Size(473, 29);
            tableLayoutPanel7.TabIndex = 20;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(panel4, 0, 2);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 0, 0);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel5, 0, 1);
            tableLayoutPanel3.Controls.Add(ckJsonParameter, 0, 3);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel8, 0, 4);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 5;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.Size = new Size(721, 691);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // panel4
            // 
            panel4.Controls.Add(tableLayoutPanel6);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(3, 83);
            panel4.Name = "panel4";
            panel4.Size = new Size(715, 34);
            panel4.TabIndex = 2;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 3;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel6.Controls.Add(btnSend, 2, 0);
            tableLayoutPanel6.Controls.Add(cmbServerMethodName, 1, 0);
            tableLayoutPanel6.Controls.Add(label4, 0, 0);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(0, 0);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Size = new Size(715, 34);
            tableLayoutPanel6.TabIndex = 18;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 3;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel4.Controls.Add(cmbHubUrl, 1, 0);
            tableLayoutPanel4.Controls.Add(btnConnect, 2, 0);
            tableLayoutPanel4.Controls.Add(label1, 0, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(715, 34);
            tableLayoutPanel4.TabIndex = 19;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 4;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel5.Controls.Add(label3, 0, 0);
            tableLayoutPanel5.Controls.Add(cmbMethodName, 1, 0);
            tableLayoutPanel5.Controls.Add(btnRemoveListener, 3, 0);
            tableLayoutPanel5.Controls.Add(btnBindListener, 2, 0);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(3, 43);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Size = new Size(715, 34);
            tableLayoutPanel5.TabIndex = 20;
            // 
            // ckJsonParameter
            // 
            ckJsonParameter.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ckJsonParameter.AutoSize = true;
            ckJsonParameter.Location = new Point(10, 128);
            ckJsonParameter.Margin = new Padding(10, 3, 3, 3);
            ckJsonParameter.Name = "ckJsonParameter";
            ckJsonParameter.Size = new Size(172, 19);
            ckJsonParameter.TabIndex = 21;
            ckJsonParameter.Text = "Invoke with json parameter:";
            ckJsonParameter.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel8
            // 
            tableLayoutPanel8.ColumnCount = 1;
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel8.Controls.Add(txtParameter, 0, 0);
            tableLayoutPanel8.Controls.Add(lblParamExample, 0, 1);
            tableLayoutPanel8.Dock = DockStyle.Fill;
            tableLayoutPanel8.Location = new Point(3, 153);
            tableLayoutPanel8.Name = "tableLayoutPanel8";
            tableLayoutPanel8.RowCount = 2;
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 300F));
            tableLayoutPanel8.Size = new Size(715, 535);
            tableLayoutPanel8.TabIndex = 22;
            // 
            // txtParameter
            // 
            txtParameter.BorderStyle = BorderStyle.FixedSingle;
            txtParameter.Dock = DockStyle.Fill;
            txtParameter.Location = new Point(3, 3);
            txtParameter.Multiline = true;
            txtParameter.Name = "txtParameter";
            txtParameter.ScrollBars = ScrollBars.Vertical;
            txtParameter.Size = new Size(709, 229);
            txtParameter.TabIndex = 15;
            // 
            // lblParamExample
            // 
            lblParamExample.Dock = DockStyle.Fill;
            lblParamExample.Location = new Point(3, 238);
            lblParamExample.Multiline = true;
            lblParamExample.Name = "lblParamExample";
            lblParamExample.ReadOnly = true;
            lblParamExample.ScrollBars = ScrollBars.Vertical;
            lblParamExample.Size = new Size(709, 294);
            lblParamExample.TabIndex = 16;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1212, 697);
            Controls.Add(tableLayoutPanel1);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Asp.net SignalR Test ";
            Load += MainForm_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel7.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            panel4.ResumeLayout(false);
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tableLayoutPanel8.ResumeLayout(false);
            tableLayoutPanel8.PerformLayout();
            ResumeLayout(false);
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
        private Button btnClear;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private Panel panel4;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel5;
        private TableLayoutPanel tableLayoutPanel6;
        private CheckBox ckJsonParameter;
        private TableLayoutPanel tableLayoutPanel7;
        private TableLayoutPanel tableLayoutPanel8;
        private TextBox txtParameter;
        private TextBox lblParamExample;
    }
}
