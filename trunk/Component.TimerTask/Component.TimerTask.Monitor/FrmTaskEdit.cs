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
using Component.TimerTask.Model.Enums;

namespace Component.TimerTask.Monitor
{
    /// <summary>
    /// 计划维护界面
    /// </summary>
    public partial class FrmTaskEdit : Form
    {
        private TaskEntity _Task;

        public TaskEntity Task
        {
            get { return _Task; }
        }

        private bool _IsReadOnly;
        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsReadOnly
        {
            get { return _IsReadOnly; }
            set { _IsReadOnly = value; }
        }

        private IBLLLogic _BLL;


        /// <summary>
        /// 新增方式
        /// </summary>
        public FrmTaskEdit(IBLLLogic paraLogic)
        {
            InitializeComponent();
            //this.Icon = Component.TimerTask.Monitor.Properties.Resources.kworldclock;
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
            this.cbx_Apps.DataSource = _BLL.GetRegestedApp();
            this.cbx_Frequnce.DataSource = Enum.GetNames(typeof(Model.Enums.TaskFrequence));

            if (_Task == null)
            {
                _Task = new TaskEntity();
            }
            this.InitControls();
        }

        private void InitControls()
        {
            this.txt_Name.Text = _Task.Name;
            this.txtParams.Name = _Task.ExtraParaStr;
            this.dtpStart.Value = _Task.DateStart < this.dtpStart.MinDate ? DateTime.Now:_Task.DateStart;
            this.dtpEnd.Value = _Task.DateEnd < this.dtpEnd.MinDate ? DateTime.Now : _Task.DateEnd;
            this.cbx_Apps.Text = _Task.RegestesAppName;
            this.cbx_Frequnce.Text = _Task.RunSpaceType.ToString();
            this.nud_OutTime.Value = _Task.RunTimeOutSecs;
            this.nud_SpaceTime.Value = _Task.RunSpaceTime;

            if (this._IsReadOnly)
            {
                this.txt_Name.ReadOnly = this.txtParams.ReadOnly = this.nud_OutTime.ReadOnly = this.nud_SpaceTime.ReadOnly = true;
                this.dtpStart.Enabled = this.dtpEnd.Enabled = this.cbx_Apps.Enabled = this.cbx_Frequnce.Enabled = false;
                
            }
        }

        private void cbx_Frequnce_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbx_Frequnce.Text == Model.Enums.TaskFrequence.CustomSecs.ToString())
            {
                this.nud_SpaceTime.Enabled = true;
            }
            else
            {
                this.nud_SpaceTime.Enabled = false;
            }
            if (this.cbx_Frequnce.Text == Model.Enums.TaskFrequence.Once.ToString())
            {
                this.nud_SpaceTime.Value = 0;
            }
            if (this.cbx_Frequnce.Text == Model.Enums.TaskFrequence.Day.ToString())
            {
                this.nud_SpaceTime.Value = 24 * 60 * 60;
            }

            if (this.cbx_Frequnce.Text == Model.Enums.TaskFrequence.Hour.ToString())
            {
                this.nud_SpaceTime.Value = 60 * 60;
            }
            if (this.cbx_Frequnce.Text == Model.Enums.TaskFrequence.Minute.ToString())
            {
                this.nud_SpaceTime.Value = 60;
            }
            if (this.cbx_Frequnce.Text == Model.Enums.TaskFrequence.Month.ToString())
            {
                this.nud_SpaceTime.Value = 30 * 24 * 60 * 60;
            }
            if (this.cbx_Frequnce.Text == Model.Enums.TaskFrequence.Week.ToString())
            {
                this.nud_SpaceTime.Value = 7 * 24 * 60 * 60;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            if (this._IsReadOnly) return;

            if (string.IsNullOrEmpty(this.txt_Name.Text.Trim()))
            {
                MessageBox.Show("名称不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txt_Name.Focus();
                return;
            }
            if (string.IsNullOrEmpty(this.cbx_Apps.Text))
            {
                MessageBox.Show("计划程序不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.cbx_Apps.Focus();
                return;
            }
            //注意：之前允许不设置超时小于0为不限制执行时间，后来发现运行时间长了后程序会慢慢阻死，所以加上此限制
            if (((long)this.nud_OutTime.Value) <= 0)
            {
                MessageBox.Show("设置超时时间必须大于0秒", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.nud_OutTime.Focus();
                return;
            }

            _Task.Name = this.txt_Name.Text.Trim();
            _Task.RegestesAppName = this.cbx_Apps.Text;
            _Task.RunSpaceTime = (long)this.nud_SpaceTime.Value;
            _Task.RunSpaceType = (TaskFrequence)Enum.Parse(typeof(TaskFrequence), this.cbx_Frequnce.Text);
            _Task.RunTimeOutSecs = (long)this.nud_OutTime.Value;
            _Task.ExtraParaStr = this.txtParams.Text;
            _Task.Enable = true;
            _Task.DateStart = this.dtpStart.Value;
            _Task.DateEnd = this.dtpEnd.Value;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
