using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ExcelToMerge.Models;

namespace ExcelToMerge.UI
{
    /// <summary>
    /// 批量执行任务选择窗体
    /// </summary>
    public partial class BatchExecutionForm : Form
    {
        /// <summary>
        /// 获取用户选择的任务列表
        /// </summary>
        public List<ConvertTask> SelectedTasks { get; private set; }
        
        /// <summary>
        /// 所有可用任务列表
        /// </summary>
        private List<ConvertTask> _allTasks;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tasks">可用任务列表</param>
        public BatchExecutionForm(List<ConvertTask> tasks)
        {
            InitializeComponent();
            
            _allTasks = tasks;
            SelectedTasks = new List<ConvertTask>();
        }
        
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void BatchExecutionForm_Load(object sender, EventArgs e)
        {
            // 加载任务列表
            LoadTaskList();
        }
        
        /// <summary>
        /// 加载任务列表
        /// </summary>
        private void LoadTaskList()
        {
            // 清空列表
            checkedListBoxTasks.Items.Clear();
            
            // 添加任务项
            foreach (var task in _allTasks)
            {
                string itemText = $"{task.Id} - {task.Name}";
                if (!string.IsNullOrEmpty(task.Description))
                {
                    itemText += $" ({task.Description})";
                }
                
                checkedListBoxTasks.Items.Add(itemText, false);
            }
        }
        
        /// <summary>
        /// 全选按钮点击事件
        /// </summary>
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxTasks.Items.Count; i++)
            {
                checkedListBoxTasks.SetItemChecked(i, true);
            }
        }
        
        /// <summary>
        /// 取消全选按钮点击事件
        /// </summary>
        private void btnUnselectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxTasks.Items.Count; i++)
            {
                checkedListBoxTasks.SetItemChecked(i, false);
            }
        }
        
        /// <summary>
        /// 确定按钮点击事件
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            // 获取选中的任务
            SelectedTasks.Clear();
            
            for (int i = 0; i < checkedListBoxTasks.Items.Count; i++)
            {
                if (checkedListBoxTasks.GetItemChecked(i))
                {
                    SelectedTasks.Add(_allTasks[i]);
                }
            }
            
            // 设置对话框结果
            DialogResult = DialogResult.OK;
            Close();
        }
        
        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
} 