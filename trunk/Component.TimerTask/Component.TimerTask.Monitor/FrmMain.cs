using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Component.TimerTask.BLL;

namespace Component.TimerTask.Monitor
{
    public partial class FrmMain : Form
    {
        private const string TIMERMANAGER_PROCESSNAME = "Component.TimerTask.TaskManager";
        //private Process _MonitorProcess;
        IBLLService _Bll = BLlFactory.GetBLL();

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.notifyIcon1.Text = this.Text;
            this.timer1.Start();
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.notifyIcon1.Visible = false;
            this.notifyIcon1.Dispose();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.ShowInTaskbar = false;
            e.Cancel = true;
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Visible = true;
        }

        private void tsmi_Show_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        /// <summary>
        /// 鼠标按下时，判断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.Visible)
            {
                this.tsmi_Show.Visible = false;
                this.tsmi_Hide.Visible = true;
            }
            else
            {
                this.tsmi_Show.Visible = true;
                this.tsmi_Hide.Visible = false;
            }
        }

        private void tsmi_Hide_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (false == _Bll.IsTaskManagerAlive())
            {//没有启动
                this.lbl_State.Text = ProcessState.没有启动.ToString();
                bool b = _Bll.StartTaskManager();
                if (false == b)
                {
                    this.tssl_Info.Text = "当前目录下不存在任务管理器进程，请检查程序。";
                }
                else
                {
                    this.tssl_Info.Text = "成功启动任务管理";
                }
            }
            else
            {//已经启动
                this.lbl_State.Text = ProcessState.已经启动.ToString();
            }
        }
    }
}
