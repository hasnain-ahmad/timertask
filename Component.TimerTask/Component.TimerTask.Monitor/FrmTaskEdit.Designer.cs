namespace Component.TimerTask.Monitor
{
    partial class FrmTaskEdit
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbx_Apps = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbx_Frequnce = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nud_SpaceTime = new System.Windows.Forms.NumericUpDown();
            this.nud_OutTime = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.txtParams = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_SpaceTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_OutTime)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(348, 358);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(441, 358);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 1;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtParams);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.nud_OutTime);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.nud_SpaceTime);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbx_Frequnce);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbx_Apps);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpEnd);
            this.groupBox1.Controls.Add(this.dtpStart);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_Name);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(46, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(466, 291);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "计划信息";
            // 
            // cbx_Apps
            // 
            this.cbx_Apps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_Apps.FormattingEnabled = true;
            this.cbx_Apps.Location = new System.Drawing.Point(95, 125);
            this.cbx_Apps.Name = "cbx_Apps";
            this.cbx_Apps.Size = new System.Drawing.Size(121, 20);
            this.cbx_Apps.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "计划程序:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "结束日期:";
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy-MM-dd hh:mm:ss";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(89, 89);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(172, 21);
            this.dtpEnd.TabIndex = 4;
            // 
            // dtpStart
            // 
            this.dtpStart.CustomFormat = "yyyy-MM-dd hh:mm:ss";
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(89, 62);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(172, 21);
            this.dtpStart.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "开始日期:";
            // 
            // txt_Name
            // 
            this.txt_Name.Location = new System.Drawing.Point(67, 26);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(124, 21);
            this.txt_Name.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称:";
            // 
            // cbx_Frequnce
            // 
            this.cbx_Frequnce.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_Frequnce.FormattingEnabled = true;
            this.cbx_Frequnce.Location = new System.Drawing.Point(95, 166);
            this.cbx_Frequnce.Name = "cbx_Frequnce";
            this.cbx_Frequnce.Size = new System.Drawing.Size(121, 20);
            this.cbx_Frequnce.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "执行频率:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 201);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "频率间隔（秒）:";
            // 
            // nud_SpaceTime
            // 
            this.nud_SpaceTime.Location = new System.Drawing.Point(116, 199);
            this.nud_SpaceTime.Name = "nud_SpaceTime";
            this.nud_SpaceTime.Size = new System.Drawing.Size(120, 21);
            this.nud_SpaceTime.TabIndex = 11;
            // 
            // nud_OutTime
            // 
            this.nud_OutTime.Location = new System.Drawing.Point(116, 226);
            this.nud_OutTime.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nud_OutTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nud_OutTime.Name = "nud_OutTime";
            this.nud_OutTime.Size = new System.Drawing.Size(120, 21);
            this.nud_OutTime.TabIndex = 13;
            this.nud_OutTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 228);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "超时时间:";
            // 
            // txtParams
            // 
            this.txtParams.Location = new System.Drawing.Point(116, 253);
            this.txtParams.Name = "txtParams";
            this.txtParams.Size = new System.Drawing.Size(124, 21);
            this.txtParams.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(39, 256);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "运行参数:";
            // 
            // FrmTaskEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 390);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTaskEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "编辑计划";
            this.Load += new System.EventHandler(this.FrmTaskEdit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_SpaceTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_OutTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_Name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbx_Apps;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nud_SpaceTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbx_Frequnce;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nud_OutTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtParams;
        private System.Windows.Forms.Label label8;
    }
}