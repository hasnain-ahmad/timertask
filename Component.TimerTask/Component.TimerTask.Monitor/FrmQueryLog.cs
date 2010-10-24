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
        private DAL.TaskDataSet _DataSet;
        private long _SelectTaskID;

        public FrmQueryLog(BLL.IBLLLogic paraBll)
        {
            InitializeComponent();

            this.Icon = Component.TimerTask.Monitor.Properties.Resources.kworldclock;

            this.dtp_End.Value = this.dtp_Start.Value.AddDays(1);
            _BLL = paraBll;
            _DataSet = new Component.TimerTask.DAL.TaskDataSet();

            InitControls();
        }

        public FrmQueryLog(BLL.IBLLLogic paraBll, long paraTaskId):
            this(paraBll)
        {
            _SelectTaskID = paraTaskId;
            _DataSet.PL_TimerTask_Log.Merge(_BLL.GetTaskLogByTask(_SelectTaskID));
        }

        private void FrmQueryLog_Load(object sender, EventArgs e)
        {
            //this.InitControls();
            this.BindGrid();
        }

        private void InitControls()
        {
            this.cbxTasks.DataSource = _BLL.GetTaskEntityList();
            this.cbxTasks.DisplayMember = "Name";
            this.cbxTasks.ValueMember = "ID";

            this.cbxTasks.SelectedValue = _SelectTaskID;
        }

        private void BindGrid()
        {
            DataView dv = new DataView(_DataSet.PL_TimerTask_Log);
            dv.Sort = "ID Desc";
            this.dataGridView1.DataSource = dv;
            this.dataGridView1.AutoResizeColumns();

            this.dataGridView1.Columns["ID"].HeaderText = "序号";
            this.dataGridView1.Columns["LogDate"].HeaderText = "记录时间";
            this.dataGridView1.Columns["TaskID"].HeaderText = "计划编号";
            this.dataGridView1.Columns["TaskName"].HeaderText = "计划名称";
            this.dataGridView1.Columns["LogType"].HeaderText = "日志类型";
            this.dataGridView1.Columns["LogContent"].HeaderText = "日志内容";
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (this.cbxTasks.SelectedValue != null)
            {
                _SelectTaskID = (long)this.cbxTasks.SelectedValue;
                DataTable dt = _BLL.GetTaskLogByTask(_SelectTaskID);
                dt.DefaultView.RowFilter = "LogDate>='" + this.dtp_Start.Value.Date.ToString() + "' And LogDate<='" + this.dtp_End.Value.Date.ToString() + "' And TaskID=" + this.cbxTasks.SelectedValue.ToString();
                _DataSet.PL_TimerTask_Log.Clear();
                _DataSet.PL_TimerTask_Log.Merge(dt.DefaultView.ToTable());
                this.BindGrid();
            }
        }
    }
}
