using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ExcelToMerge.Models;
using ExcelToMerge.Services;
using ExcelToMerge.Utils;
using System.Configuration;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExcelToMerge.UI
{
    /// <summary>
    /// 调度窗体
    /// </summary>
    public partial class ScheduleForm : Form
    {
        private readonly ScheduleService _scheduleService;
        private readonly ConvertService _convertService;
        private ScheduleTask _currentTask;
        private List<ConvertTask> _availableTasks;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public ScheduleForm()
        {
            InitializeComponent();
            _scheduleService = new ScheduleService();
            _convertService = new ConvertService();
            _currentTask = new ScheduleTask();
        }
        
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void ScheduleForm_Load(object sender, EventArgs e)
        {
            // 初始化控件状态
            progressBarExecution.Visible = false;
            
            // 加载调度任务列表
            LoadScheduleTasks();
            
            // 加载可用的转换任务
            LoadAvailableTasks();
            
            // 禁用右侧按钮，需要选择调度任务后才能使用
            EnableActionButtons(false);
        }
        
        /// <summary>
        /// 加载调度任务列表
        /// </summary>
        private void LoadScheduleTasks()
        {
            try
            {
                // 清空列表
                listViewSchedules.Items.Clear();
                
                // 获取所有调度任务
                var tasks = _scheduleService.GetScheduleTaskList();
                
                // 添加到列表
                foreach (var task in tasks)
                {
                    ListViewItem item = new ListViewItem(task.Id.ToString());
                    item.SubItems.Add(task.Name);
                    item.SubItems.Add(task.Description ?? string.Empty);
                    item.SubItems.Add(task.CronExpression ?? string.Empty);
                    item.SubItems.Add(task.LastExecutionDate.HasValue ? task.LastExecutionDate.Value.ToString("yyyy-MM-dd") : string.Empty);
                    item.SubItems.Add(task.ItemCount.ToString());
                    item.Tag = task;
                    
                    listViewSchedules.Items.Add(item);
                }
                
                // 如果有数据，选中第一项
                if (listViewSchedules.Items.Count > 0)
                {
                    listViewSchedules.Items[0].Selected = true;
                    _currentTask = (ScheduleTask)listViewSchedules.Items[0].Tag;
                    EnableActionButtons(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载调度任务失败: {ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 加载可用的转换任务
        /// </summary>
        private void LoadAvailableTasks()
        {
            try
            {
                // 获取所有激活的转换任务
                _availableTasks = _convertService.GetConvertTaskList(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载转换任务失败: {ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 启用或禁用操作按钮
        /// </summary>
        /// <param name="enable">是否启用</param>
        private void EnableActionButtons(bool enable)
        {
            btnEditTasks.Enabled = enable;
            btnExecute.Enabled = enable;
            btnViewLogs.Enabled = enable;
        }
        
        /// <summary>
        /// 新增调度按钮点击事件
        /// </summary>
        private void btnNewSchedule_Click(object sender, EventArgs e)
        {
            // 打开调度编辑窗口
            using (var form = new ScheduleEditForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 重新加载调度任务列表
                    LoadScheduleTasks();
                }
            }
        }
        
        /// <summary>
        /// 编辑调度按钮点击事件
        /// </summary>
        private void btnEditSchedule_Click(object sender, EventArgs e)
        {
            if (listViewSchedules.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择一个调度任务", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            // 获取选中的调度任务
            var item = listViewSchedules.SelectedItems[0];
            var task = (ScheduleTask)item.Tag;
            
            // 打开调度编辑窗口
            using (var form = new ScheduleEditForm(task))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 重新加载调度任务列表
                    LoadScheduleTasks();
                }
            }
        }
        
        /// <summary>
        /// 删除调度按钮点击事件
        /// </summary>
        private void btnDeleteSchedule_Click(object sender, EventArgs e)
        {
            if (listViewSchedules.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择一个调度任务", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            try
            {
                // 获取选中的任务
                var item = listViewSchedules.SelectedItems[0];
                int taskId = Convert.ToInt32(item.SubItems[0].Text);
                string taskName = item.SubItems[1].Text;
                
                // 确认删除
                var result = MessageBox.Show($"确定要删除调度任务 \"{taskName}\" 吗？", "确认删除", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    // 删除任务
                    bool deleted = _scheduleService.DeleteScheduleTask(taskId);
                    
                    if (deleted)
                    {
                        // 重新加载任务列表
                        LoadScheduleTasks();
                        
                        MessageBox.Show("删除成功", "提示", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("删除失败", "错误", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"删除任务失败: {ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 编辑任务按钮点击事件
        /// </summary>
        private void btnEditTasks_Click(object sender, EventArgs e)
        {
            if (listViewSchedules.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择一个调度任务", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            // 获取选中的调度任务
            var item = listViewSchedules.SelectedItems[0];
            var task = (ScheduleTask)item.Tag;
            
            // 打开任务编辑窗口
            using (var form = new ScheduleTasksEditForm(task))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 重新加载调度任务列表
                    LoadScheduleTasks();
                }
            }
        }
        
        /// <summary>
        /// 手动执行按钮点击事件
        /// </summary>
        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (listViewSchedules.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择一个调度任务", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            // 获取选中的调度任务
            var item = listViewSchedules.SelectedItems[0];
            var task = (ScheduleTask)item.Tag;
            
            // 确认执行
            var result = MessageBox.Show($"确定要执行调度任务 \"{task.Name}\" 吗？", "确认执行", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                // 执行调度任务
                ExecuteScheduleTaskAsync(task);
            }
        }
        
        /// <summary>
        /// 执行历史按钮点击事件
        /// </summary>
        private void btnViewLogs_Click(object sender, EventArgs e)
        {
            if (listViewSchedules.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择一个调度任务", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            // 获取选中的调度任务
            var item = listViewSchedules.SelectedItems[0];
            var task = (ScheduleTask)item.Tag;
            
            try
            {
                // 获取执行日志
                var logs = _scheduleService.GetExecutionLogs(task.Id);
                
                if (logs == null || logs.Count == 0)
                {
                    MessageBox.Show("没有找到执行日志", "提示", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                // 显示日志窗体
                using (var logForm = new ExecutionLogForm(logs))
                {
                    logForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取执行日志失败: {ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 异步执行调度任务
        /// </summary>
        /// <param name="task">调度任务</param>
        private async void ExecuteScheduleTaskAsync(ScheduleTask task)
        {
            if (task == null || task.Id == 0)
            {
                MessageBox.Show("无效的调度任务", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            try
            {
                // 加载完整的任务信息
                task = _scheduleService.GetScheduleTask(task.Id);
                
                if (task.TaskItems == null || task.TaskItems.Count == 0)
                {
                    MessageBox.Show("调度任务不包含任何转换作业", "提示", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                // 显示进度条
                progressBarExecution.Visible = true;
                progressBarExecution.Value = 0;
                labelStatus.Text = "正在准备执行...";
                
                // 禁用控件
                DisableControls();
                
                // 创建进度报告器
                var progress = new Progress<ProgressInfo>(OnProgressChanged);
                
                // 异步执行调度任务
                var result = await Task.Run(() => _scheduleService.ExecuteScheduleTask(task, progress));
                
                // 启用控件
                EnableControls();
                
                // 隐藏进度条
                progressBarExecution.Visible = false;
                
                if (result.Success)
                {
                    int successCount = 0;
                    int failedCount = 0;
                    int skippedCount = 0;
                    
                    foreach (var taskResult in result.TaskResults)
                    {
                        if (taskResult.Skipped)
                            skippedCount++;
                        else if (taskResult.Success)
                            successCount++;
                        else
                            failedCount++;
                    }
                    
                    labelStatus.Text = $"执行完成，成功: {successCount}，失败: {failedCount}，跳过: {skippedCount}";
                    
                    MessageBox.Show($"调度任务执行完成\n成功: {successCount}\n失败: {failedCount}\n跳过: {skippedCount}", 
                        "执行结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // 如果有输出文件，询问是否打开输出目录
                    if (successCount > 0)
                    {
                        var openResult = MessageBox.Show("是否打开输出目录？", "提示", 
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        
                        if (openResult == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start("explorer.exe", task.OutputPath);
                        }
                    }
                    
                    // 重新加载调度任务列表，更新上次执行日期
                    LoadScheduleTasks();
                }
                else
                {
                    labelStatus.Text = "执行失败";
                    
                    MessageBox.Show($"调度任务执行失败: {result.ErrorMessage}", 
                        "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // 启用控件
                EnableControls();
                
                // 隐藏进度条
                progressBarExecution.Visible = false;
                labelStatus.Text = "执行失败";
                
                MessageBox.Show($"执行调度任务失败: {ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 进度变化事件处理
        /// </summary>
        /// <param name="info">进度信息</param>
        private void OnProgressChanged(ProgressInfo info)
        {
            // 更新进度条
            progressBarExecution.Value = (int)info.Percentage;
            
            // 更新进度标签
            labelStatus.Text = $"{info.Status} ({info.ProcessedItems}/{info.TotalItems})";
            
            // 如果出现错误，显示错误信息
            if (info.Error != null)
            {
                MessageBox.Show($"执行过程中出错: {info.Error.Message}", 
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 禁用控件
        /// </summary>
        private void DisableControls()
        {
            btnNewSchedule.Enabled = false;
            btnEditSchedule.Enabled = false;
            btnDeleteSchedule.Enabled = false;
            btnEditTasks.Enabled = false;
            btnExecute.Enabled = false;
            btnViewLogs.Enabled = false;
            listViewSchedules.Enabled = false;
        }
        
        /// <summary>
        /// 启用控件
        /// </summary>
        private void EnableControls()
        {
            btnNewSchedule.Enabled = true;
            btnEditSchedule.Enabled = true;
            btnDeleteSchedule.Enabled = true;
            btnEditTasks.Enabled = true;
            btnExecute.Enabled = true;
            btnViewLogs.Enabled = true;
            listViewSchedules.Enabled = true;
        }
    }
}
