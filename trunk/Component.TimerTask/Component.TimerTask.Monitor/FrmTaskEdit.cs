using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Component.TimerTask.BLL;
using Component.TimerTask.Model;

namespace Component.TimerTask.Monitor
{
    public partial class FrmTaskEdit : Form
    {
        private TaskEntity _Task;

        public TaskEntity Task
        {
            get { return _Task; }
        }

        private IBLLLogic _BLL;


        /// <summary>
        /// 新增方式
        /// </summary>
        public FrmTaskEdit(IBLLLogic paraLogic)
        {
            InitializeComponent();
            _BLL = paraLogic;
        }

        /// <summary>
        /// 修改方式
        /// </summary>
        /// <param name="paraTask"></param>
        public FrmTaskEdit(TaskEntity paraTask, IBLLLogic paraLogic)
            : this(paraLogic)
        {
            _Task = paraTask;
        }

        private void FrmTaskEdit_Load(object sender, EventArgs e)
        {
            this.cbx_Apps.DataSource =  _BLL.GetRegestedApp();
            this.cbx_Frequnce.DataSource=  Enum.GetNames(typeof(Model.Enums.TaskFrequence));
        }
    }
}
