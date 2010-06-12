namespace Component.TimerTask.Monitor
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.cms_NotifyIco = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_Show = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Hide = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_State = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssl_Info = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colState = new System.Windows.Forms.ColumnHeader();
            this.cms_TaskList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Del = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Update = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_Log = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cms_NotifyIco.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.cms_TaskList.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.cms_NotifyIco;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            this.notifyIcon1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDown);
            // 
            // cms_NotifyIco
            // 
            this.cms_NotifyIco.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Show,
            this.tsmi_Hide});
            this.cms_NotifyIco.Name = "contextMenuStrip1";
            this.cms_NotifyIco.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cms_NotifyIco.ShowImageMargin = false;
            this.cms_NotifyIco.Size = new System.Drawing.Size(76, 48);
            // 
            // tsmi_Show
            // 
            this.tsmi_Show.Name = "tsmi_Show";
            this.tsmi_Show.Size = new System.Drawing.Size(75, 22);
            this.tsmi_Show.Text = "显示";
            this.tsmi_Show.Click += new System.EventHandler(this.tsmi_Show_Click);
            // 
            // tsmi_Hide
            // 
            this.tsmi_Hide.Name = "tsmi_Hide";
            this.tsmi_Hide.Size = new System.Drawing.Size(75, 22);
            this.tsmi_Hide.Text = "隐藏";
            this.tsmi_Hide.Click += new System.EventHandler(this.tsmi_Hide_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "任务管理器进程状态：";
            // 
            // lbl_State
            // 
            this.lbl_State.AutoSize = true;
            this.lbl_State.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_State.Location = new System.Drawing.Point(134, 0);
            this.lbl_State.Name = "lbl_State";
            this.lbl_State.Size = new System.Drawing.Size(89, 22);
            this.lbl_State.TabIndex = 2;
            this.lbl_State.Text = "正在检测...";
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_Info});
            this.statusStrip1.Location = new System.Drawing.Point(0, 271);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(454, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssl_Info
            // 
            this.tssl_Info.Name = "tssl_Info";
            this.tssl_Info.Size = new System.Drawing.Size(76, 17);
            this.tssl_Info.Text = "                 ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listView1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(23, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(408, 221);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "任务列表";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colState});
            this.listView1.ContextMenuStrip = this.cms_TaskList;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(3, 17);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.ShowGroups = false;
            this.listView1.Size = new System.Drawing.Size(402, 201);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDown);
            // 
            // colName
            // 
            this.colName.Text = "任务";
            this.colName.Width = 296;
            // 
            // colState
            // 
            this.colState.Text = "状态";
            this.colState.Width = 96;
            // 
            // cms_TaskList
            // 
            this.cms_TaskList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Add,
            this.tsmi_Del,
            this.tsmi_Update,
            this.toolStripMenuItem1,
            this.tsmi_Log});
            this.cms_TaskList.Name = "cms_TaskList";
            this.cms_TaskList.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cms_TaskList.ShowImageMargin = false;
            this.cms_TaskList.Size = new System.Drawing.Size(100, 98);
            // 
            // tsmi_Add
            // 
            this.tsmi_Add.Name = "tsmi_Add";
            this.tsmi_Add.Size = new System.Drawing.Size(127, 22);
            this.tsmi_Add.Text = "新增任务";
            this.tsmi_Add.Click += new System.EventHandler(this.tsmi_Add_Click);
            // 
            // tsmi_Del
            // 
            this.tsmi_Del.Name = "tsmi_Del";
            this.tsmi_Del.Size = new System.Drawing.Size(127, 22);
            this.tsmi_Del.Text = "删除任务";
            this.tsmi_Del.Click += new System.EventHandler(this.tsmi_Del_Click);
            // 
            // tsmi_Update
            // 
            this.tsmi_Update.Name = "tsmi_Update";
            this.tsmi_Update.Size = new System.Drawing.Size(127, 22);
            this.tsmi_Update.Text = "修改任务";
            this.tsmi_Update.Click += new System.EventHandler(this.tsmi_Update_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(124, 6);
            // 
            // tsmi_Log
            // 
            this.tsmi_Log.Name = "tsmi_Log";
            this.tsmi_Log.Size = new System.Drawing.Size(127, 22);
            this.tsmi_Log.Text = "查看日志";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.91304F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86.08696F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(454, 271);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.lbl_State);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(23, 11);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(408, 30);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 293);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "定时任务管理-监控器";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.cms_NotifyIco.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.cms_TaskList.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip cms_NotifyIco;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Show;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Hide;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_State;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssl_Info;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colState;
        private System.Windows.Forms.ContextMenuStrip cms_TaskList;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Add;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Del;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Update;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Log;
    }
}

