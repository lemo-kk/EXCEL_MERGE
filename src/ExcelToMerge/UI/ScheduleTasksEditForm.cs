using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ExcelToMerge.Models;
using ExcelToMerge.Services;

namespace ExcelToMerge.UI
{
    /// <summary>
    /// 调度任务编辑窗体
    /// </summary>
    public partial class ScheduleTasksEditForm : Form
    {
        private readonly ScheduleService _scheduleService;
        private readonly ConvertService _convertService;
        private ScheduleTask _task;
        private List<ConvertTask> _availableTasks;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="task">调度任务</param>
        public ScheduleTasksEditForm(ScheduleTask task)
        {
            InitializeComponent();
            
            _scheduleService = new ScheduleService();
            _convertService = new ConvertService();
            _task = task ?? new ScheduleTask();
        }
        
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void ScheduleTasksEditForm_Load(object sender, EventArgs e)
        {
            // 设置窗体标题
            this.Text = $"编辑任务列表 - {_task.Name}";
            
            // 加载可用的转换任务
            LoadAvailableTasks();
            
            // 加载当前任务项
            LoadTaskItems();
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
        /// 加载当前任务项
        /// </summary>
        private void LoadTaskItems()
        {
            try
            {
                // 清空列表
                listViewTasks.Items.Clear();
                
                // 如果需要，加载完整的任务信息
                if (_task.TaskItems == null || _task.TaskItems.Count == 0)
                {
                    var fullTask = _scheduleService.GetScheduleTask(_task.Id);
                    if (fullTask != null)
                    {
                        _task = fullTask;
                    }
                }
                
                // 添加任务项
                if (_task.TaskItems != null)
                {
                    foreach (var item in _task.TaskItems)
                    {
                        if (item.Task != null)
                        {
                            var listItem = new ListViewItem(item.Sequence.ToString());
                            listItem.SubItems.Add(item.Task.Id.ToString());
                            listItem.SubItems.Add(item.Task.Name);
                            listItem.SubItems.Add(item.Task.Description ?? string.Empty);
                            listItem.SubItems.Add(item.Task.OutputFormat.ToString());
                            listItem.Checked = item.IsActive;
                            listItem.Tag = item;
                            
                            listViewTasks.Items.Add(listItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载任务项失败: {ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 添加任务按钮点击事件
        /// </summary>
        private void btnAddTask_Click(object sender, EventArgs e)
        {
            if (_availableTasks == null || _availableTasks.Count == 0)
            {
                MessageBox.Show("没有可用的转换任务", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            // 显示转换任务选择对话框
            using (var form = new TaskSelectionForm(_availableTasks))
            {
                if (form.ShowDialog() == DialogResult.OK && form.SelectedTasks.Count > 0)
                {
                    // 初始化任务项列表
                    if (_task.TaskItems == null)
                    {
                        _task.TaskItems = new List<ScheduleTaskItem>();
                    }
                    
                    // 添加选中的任务
                    foreach (var selectedTask in form.SelectedTasks)
                    {
                        // 检查是否已存在
                        bool exists = false;
                        foreach (var item in _task.TaskItems)
                        {
                            if (item.TaskId == selectedTask.Id)
                            {
                                exists = true;
                                break;
                            }
                        }
                        
                        if (!exists)
                        {
                            // 添加到当前任务
                            var taskItem = new ScheduleTaskItem
                            {
                                TaskId = selectedTask.Id,
                                Task = selectedTask,
                                Sequence = _task.TaskItems.Count + 1,
                                IsActive = true
                            };
                            
                            _task.TaskItems.Add(taskItem);
                            
                            // 添加到列表
                            var listItem = new ListViewItem(taskItem.Sequence.ToString());
                            listItem.SubItems.Add(taskItem.Task.Id.ToString());
                            listItem.SubItems.Add(taskItem.Task.Name);
                            listItem.SubItems.Add(taskItem.Task.Description ?? string.Empty);
                            listItem.SubItems.Add(taskItem.Task.OutputFormat.ToString());
                            listItem.Checked = taskItem.IsActive;
                            listItem.Tag = taskItem;
                            
                            listViewTasks.Items.Add(listItem);
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// 移除任务按钮点击事件
        /// </summary>
        private void btnRemoveTask_Click(object sender, EventArgs e)
        {
            if (listViewTasks.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择一个转换任务", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            // 获取选中的任务
            var item = listViewTasks.SelectedItems[0];
            var taskItem = item.Tag as ScheduleTaskItem;
            
            if (taskItem != null)
            {
                // 确认删除
                var result = MessageBox.Show($"确定要移除任务 \"{taskItem.Task.Name}\" 吗？", "确认移除", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    // 从列表中移除
                    _task.TaskItems.Remove(taskItem);
                    listViewTasks.Items.Remove(item);
                    
                    // 重新排序
                    ResequenceTasks();
                }
            }
        }
        
        /// <summary>
        /// 上移按钮点击事件
        /// </summary>
        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (listViewTasks.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择一个转换任务", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            // 获取选中的项
            int index = listViewTasks.SelectedIndices[0];
            if (index <= 0)
                return;
            
            // 交换位置
            SwapItems(index, index - 1);
            
            // 重新排序
            ResequenceTasks();
        }
        
        /// <summary>
        /// 下移按钮点击事件
        /// </summary>
        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (listViewTasks.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择一个转换任务", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            // 获取选中的项
            int index = listViewTasks.SelectedIndices[0];
            if (index >= listViewTasks.Items.Count - 1)
                return;
            
            // 交换位置
            SwapItems(index, index + 1);
            
            // 重新排序
            ResequenceTasks();
        }
        
        /// <summary>
        /// 交换列表项位置
        /// </summary>
        /// <param name="index1">第一项索引</param>
        /// <param name="index2">第二项索引</param>
        private void SwapItems(int index1, int index2)
        {
            // 获取列表项
            ListViewItem item1 = listViewTasks.Items[index1];
            ListViewItem item2 = listViewTasks.Items[index2];
            
            // 获取任务项
            var taskItem1 = item1.Tag as ScheduleTaskItem;
            var taskItem2 = item2.Tag as ScheduleTaskItem;
            
            if (taskItem1 != null && taskItem2 != null)
            {
                // 交换序号
                int temp = taskItem1.Sequence;
                taskItem1.Sequence = taskItem2.Sequence;
                taskItem2.Sequence = temp;
                
                // 更新列表项文本
                item1.SubItems[0].Text = taskItem1.Sequence.ToString();
                item2.SubItems[0].Text = taskItem2.Sequence.ToString();
                
                // 交换列表项
                listViewTasks.BeginUpdate();
                listViewTasks.Items.RemoveAt(index1);
                listViewTasks.Items.Insert(index2, item1);
                listViewTasks.EndUpdate();
                
                // 选中移动后的项
                listViewTasks.Items[index2].Selected = true;
                listViewTasks.Focus();
            }
        }
        
        /// <summary>
        /// 重新排序任务
        /// </summary>
        private void ResequenceTasks()
        {
            // 更新列表项序号
            for (int i = 0; i < listViewTasks.Items.Count; i++)
            {
                var item = listViewTasks.Items[i];
                var taskItem = item.Tag as ScheduleTaskItem;
                
                if (taskItem != null)
                {
                    taskItem.Sequence = i + 1;
                    item.SubItems[0].Text = taskItem.Sequence.ToString();
                }
            }
        }
        
        /// <summary>
        /// 列表项选中状态变化事件
        /// </summary>
        private void listViewTasks_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var taskItem = e.Item.Tag as ScheduleTaskItem;
            
            if (taskItem != null)
            {
                taskItem.IsActive = e.Item.Checked;
            }
        }
        
        /// <summary>
        /// 保存按钮点击事件
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
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
    }
} 