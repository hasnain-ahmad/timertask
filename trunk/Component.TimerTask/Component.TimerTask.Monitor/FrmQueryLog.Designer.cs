namespace Component.TimerTask.Monitor
{
    partial class FrmQueryLog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtp_Start = new System.Windows.Forms.DateTimePicker();
            this.dtp_End = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxTasks = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 73);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.ShowCellErrors = false;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(574, 319);
            this.dataGridView1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.72152F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82.27848F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(580, 395);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtp_Start);
            this.groupBox1.Controls.Add(this.dtp_End);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbxTasks);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(574, 64);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询范围";
            // 
            // dtp_Start
            // 
            this.dtp_Start.Location = new System.Drawing.Point(229, 27);
            this.dtp_Start.Name = "dtp_Start";
            this.dtp_Start.Size = new System.Drawing.Size(127, 21);
            this.dtp_Start.TabIndex = 3;
            // 
            // dtp_End
            // 
            this.dtp_End.Location = new System.Drawing.Point(369, 27);
            this.dtp_End.Name = "dtp_End";
            this.dtp_End.Size = new System.Drawing.Size(121, 21);
            this.dtp_End.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(496, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(62, 25);
            this.button1.TabIndex = 6;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(357, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "－";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(174, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "时间范围:";
            // 
            // cbxTasks
            // 
            this.cbxTasks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTasks.FormattingEnabled = true;
            this.cbxTasks.Location = new System.Drawing.Point(79, 27);
            this.cbxTasks.Name = "cbxTasks";
            this.cbxTasks.Size = new System.Drawing.Size(88, 20);
            this.cbxTasks.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择计划:";
            // 
            // FrmQueryLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 395);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmQueryLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查看日志";
            this.Load += new System.EventHandler(this.FrmQueryLog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxTasks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtp_Start;
        private System.Windows.Forms.DateTimePicker dtp_End;
    }
}