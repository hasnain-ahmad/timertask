using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Component.TimerTask.Model;
using Component.TimerTask.BLL;

namespace Component.TimerTask.Monitor
{
    public partial class FrmQueryLog : Form
    {
        private BLL.IBLLLogic _BLL;


        public FrmQueryLog(BLL.IBLLLogic paraBll)
        {
            InitializeComponent();
        }

        private void FrmQueryLog_Load(object sender, EventArgs e)
        {

        }
    }
}
