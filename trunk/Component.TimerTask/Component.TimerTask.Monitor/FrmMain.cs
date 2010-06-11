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
            this.Visible = false;
            e.Cancel = true;
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Visible = true;
        }
    }
}
