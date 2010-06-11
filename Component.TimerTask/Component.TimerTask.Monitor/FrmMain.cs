using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Component.TimerTask.Monitor
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.notifyIcon1.Text = this.Text;
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
    }
}
