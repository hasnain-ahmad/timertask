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
using Component.TimerTask.Model;

namespace Component.TimerTask.Monitor
{
    public partial class FrmMain : Form
    {
        private const string TIMERMANAGER_PROCESSNAME = "Component.TimerTask.TaskManager";
        IBLLLogic _Bll = BLlFactory.GetBllLogic();

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
            InitTaskList();
        }

        private void InitTaskList()
        {
            int selected = this.listView1.SelectedIndices.Count > 0 ? this.listView1.SelectedIndices[0] : 0;
            this.listView1.BeginUpdate();
            this.listView1.Items.Clear();
            List<TaskEntity> list = _Bll.GetTaskEntityList();
            foreach (TaskEntity entity in list)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = entity.Name;
                lvi.Tag = entity;
                lvi.SubItems.Add(new ListViewItem.ListViewSubItem());
                lvi.SubItems[1].Text = GetTaskState(entity).ToString();
                this.listView1.Items.Add(lvi);
            }
            if (this.listView1.Items.Count > 0)

                this.listView1.Items[selected].Selected = true;
            this.listView1.EndUpdate();

        }

        private TaskState GetTaskState(TaskEntity paraTask)
        {
            DateTime dtNow = DateTime.Now;
            if (paraTask.DateEnd < dtNow) return TaskState.超时;
            if (paraTask.DateStart > dtNow) return TaskState.等待执行;
            if (paraTask.Enable == false) return TaskState.已删除;
            return TaskState.正在执行;
        }


        #region 计划维护菜单
        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            ListViewItem item = this.listView1.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                item.Selected = true;


                if (e.Button == MouseButtons.Right)
                {
                    tsmi_Run.Enabled = true;
                    tsmi_Del.Enabled = true;
                    tsmi_Add.Enabled = true;
                    tsmi_Update.Enabled = true;
                    TaskEntity entity = (TaskEntity)item.Tag;
                    TaskState s = (TaskState)Enum.Parse(typeof(TaskState), item.SubItems[1].Text);
                    switch (s)
                    {
                        case TaskState.超时:
                            break;
                        case TaskState.等待执行:
                            break;
                        case TaskState.正在执行:

                            break;
                        case TaskState.已删除:
                            tsmi_Del.Enabled = false;
                            tsmi_Update.Enabled = false;
                            tsmi_Run.Enabled = false;
                            break;
                    }
                }
            }
            else
            {
                if (e.Button == MouseButtons.Right)
                {
                    tsmi_Del.Enabled = false;
                    tsmi_Run.Enabled = false;
                    tsmi_Update.Enabled = false;

                }
            }
        }

        private void tsmi_Add_Click(object sender, EventArgs e)
        {
            FrmTaskEdit frm = new FrmTaskEdit(_Bll);
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                if (frm.Task != null)
                {
                    _Bll.AddTask(frm.Task);
                    this.InitTaskList();
                }
            }
        }

        private void tsmi_Del_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                TaskEntity entity = (TaskEntity)this.listView1.SelectedItems[0].Tag;
                if (entity != null)
                {
                    DialogResult dr = MessageBox.Show("确定要删除该任务吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        bool b = _Bll.DelTask(entity.ID);
                        if (b == true)
                        {
                            MessageBox.Show("删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.InitTaskList();
                        }
                    }
                }
            }
        }

        private void tsmi_Update_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                TaskEntity entity = (TaskEntity)this.listView1.SelectedItems[0].Tag;
                if (entity != null)
                {
                    FrmTaskEdit frm = new FrmTaskEdit(entity, _Bll);
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        _Bll.UpdateTask(entity);
                        this.InitTaskList();
                    }
                }
            }
        }

        private void tsmi_Log_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                TaskEntity entity = (TaskEntity)this.listView1.SelectedItems[0].Tag;
                if (entity != null)
                {
                    FrmQueryLog frm = new FrmQueryLog(_Bll, entity.ID);
                    frm.ShowDialog();
                }
            }
            else
            {
                FrmQueryLog frm = new FrmQueryLog(_Bll);
                frm.ShowDialog();
            }
        }

        #endregion

        private void tsmi_Run_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                TaskEntity entity = (TaskEntity)this.listView1.SelectedItems[0].Tag;
                if (entity != null)
                {
                    DialogResult dr = MessageBox.Show("确定要立即执行该任务？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        _Bll.RunTaskImmediate(entity.ID);
                        MessageBox.Show("执行成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.InitTaskList();
                    }
                }
            }
        }

        private void tsmi_Stop_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                TaskEntity entity = (TaskEntity)this.listView1.SelectedItems[0].Tag;
                if (entity != null)
                {
                    DialogResult dr = MessageBox.Show("确定要立即停止该任务？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        _Bll.StopRuningTask(entity.ID);
                        MessageBox.Show("停止成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.InitTaskList();
                    }
                }
            }
        }
    }
}
