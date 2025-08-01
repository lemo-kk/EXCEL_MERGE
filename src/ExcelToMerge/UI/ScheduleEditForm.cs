using System;
using System.IO;
using System.Windows.Forms;
using ExcelToMerge.Models;
using ExcelToMerge.Services;
using System.Configuration;

namespace ExcelToMerge.UI
{
    /// <summary>
    /// 调度编辑窗体
    /// </summary>
    public partial class ScheduleEditForm : Form
    {
        private readonly ScheduleService _scheduleService;
        private ScheduleTask _task;
        
        /// <summary>
        /// 构造函数 - 新建调度
        /// </summary>
        public ScheduleEditForm()
        {
            InitializeComponent();
            
            _scheduleService = new ScheduleService();
            _task = new ScheduleTask();
            
            // 设置默认输出路径
            string defaultExportPath = ConfigurationManager.AppSettings["DefaultExportPath"];
            if (!string.IsNullOrEmpty(defaultExportPath))
            {
                textBoxOutputPath.Text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, defaultExportPath);
            }
        }
        
        /// <summary>
        /// 构造函数 - 编辑调度
        /// </summary>
        /// <param name="task">调度任务</param>
        public ScheduleEditForm(ScheduleTask task)
        {
            InitializeComponent();
            
            _scheduleService = new ScheduleService();
            _task = task ?? new ScheduleTask();
            
            // 加载任务数据
            LoadTaskData();
        }
        
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void ScheduleEditForm_Load(object sender, EventArgs e)
        {
            // 设置窗体标题
            this.Text = _task.Id > 0 ? "编辑调度" : "新建调度";
        }
        
        /// <summary>
        /// 加载任务数据
        /// </summary>
        private void LoadTaskData()
        {
            if (_task == null)
                return;
            
            textBoxName.Text = _task.Name;
            textBoxDescription.Text = _task.Description;
            textBoxOutputPath.Text = _task.OutputPath;
            textBoxCronExpression.Text = _task.CronExpression;
            checkBoxActive.Checked = _task.IsActive;
            
            if (_task.BusinessStartDate.HasValue)
            {
                dateTimePickerStartDate.Value = _task.BusinessStartDate.Value;
                checkBoxUseStartDate.Checked = true;
            }
            else
            {
                dateTimePickerStartDate.Value = DateTime.Today;
                checkBoxUseStartDate.Checked = false;
            }
            
            if (_task.BusinessEndDate.HasValue)
            {
                dateTimePickerEndDate.Value = _task.BusinessEndDate.Value;
                checkBoxUseEndDate.Checked = true;
            }
            else
            {
                dateTimePickerEndDate.Value = DateTime.Today;
                checkBoxUseEndDate.Checked = false;
            }
            
            // 更新日期控件状态
            UpdateDateControlsState();
        }
        
        /// <summary>
        /// 更新日期控件状态
        /// </summary>
        private void UpdateDateControlsState()
        {
            dateTimePickerStartDate.Enabled = checkBoxUseStartDate.Checked;
            dateTimePickerEndDate.Enabled = checkBoxUseEndDate.Checked;
        }
        
        /// <summary>
        /// 浏览按钮点击事件
        /// </summary>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "选择输出目录";
                
                if (!string.IsNullOrEmpty(textBoxOutputPath.Text) && Directory.Exists(textBoxOutputPath.Text))
                {
                    folderDialog.SelectedPath = textBoxOutputPath.Text;
                }
                
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxOutputPath.Text = folderDialog.SelectedPath;
                }
            }
        }
        
        /// <summary>
        /// 保存按钮点击事件
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("请输入调度名称", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxName.Focus();
                return;
            }
            
            if (string.IsNullOrEmpty(textBoxOutputPath.Text))
            {
                MessageBox.Show("请输入输出路径", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxOutputPath.Focus();
                return;
            }
            
            try
            {
                // 更新任务信息
                _task.Name = textBoxName.Text;
                _task.Description = textBoxDescription.Text;
                _task.OutputPath = textBoxOutputPath.Text;
                _task.IsActive = checkBoxActive.Checked;
                _task.CronExpression = textBoxCronExpression.Text;
                
                // 设置业务日期
                _task.BusinessStartDate = checkBoxUseStartDate.Checked ? dateTimePickerStartDate.Value : (DateTime?)null;
                _task.BusinessEndDate = checkBoxUseEndDate.Checked ? dateTimePickerEndDate.Value : (DateTime?)null;
                
                // 保存任务
                int taskId = _scheduleService.SaveScheduleTask(_task);
                
                if (taskId > 0)
                {
                    // 更新任务ID
                    _task.Id = taskId;
                    
                    // 设置对话框结果
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("保存失败", "错误", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存任务失败: {ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        
        /// <summary>
        /// 使用开始日期复选框状态变化事件
        /// </summary>
        private void checkBoxUseStartDate_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerStartDate.Enabled = checkBoxUseStartDate.Checked;
        }
        
        /// <summary>
        /// 使用结束日期复选框状态变化事件
        /// </summary>
        private void checkBoxUseEndDate_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerEndDate.Enabled = checkBoxUseEndDate.Checked;
        }
    }
} 