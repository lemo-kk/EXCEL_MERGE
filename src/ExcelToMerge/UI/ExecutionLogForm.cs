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
    /// 执行日志窗体
    /// </summary>
    public partial class ExecutionLogForm : Form
    {
        private List<ExecutionLog> _logs;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logs">执行日志列表</param>
        public ExecutionLogForm(List<ExecutionLog> logs)
        {
            InitializeComponent();
            
            _logs = logs ?? new List<ExecutionLog>();
        }
        
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void ExecutionLogForm_Load(object sender, EventArgs e)
        {
            // 加载日志数据
            LoadLogData();
        }
        
        /// <summary>
        /// 加载日志数据
        /// </summary>
        private void LoadLogData()
        {
            // 清空列表
            listViewLogs.Items.Clear();
            
            // 添加日志项
            foreach (var log in _logs)
            {
                // 创建列表项
                var item = new ListViewItem(log.Id.ToString());
                
                // 添加子项
                item.SubItems.Add(log.StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
                
                if (log.EndTime != default)
                {
                    item.SubItems.Add(log.EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    
                    // 计算执行时间
                    TimeSpan duration = log.EndTime - log.StartTime;
                    item.SubItems.Add(string.Format("{0:D2}:{1:D2}:{2:D2}", 
                        duration.Hours, duration.Minutes, duration.Seconds));
                }
                else
                {
                    item.SubItems.Add("-");
                    item.SubItems.Add("-");
                }
                
                item.SubItems.Add(log.Status);
                
                // 设置状态颜色
                switch (log.Status.ToLower())
                {
                    case "completed":
                        item.ForeColor = Color.Green;
                        break;
                    case "failed":
                        item.ForeColor = Color.Red;
                        break;
                    case "running":
                        item.ForeColor = Color.Blue;
                        break;
                }
                
                // 设置Tag属性，用于双击查看详情
                item.Tag = log;
                
                // 添加到列表
                listViewLogs.Items.Add(item);
            }
        }
        
        /// <summary>
        /// 列表项双击事件
        /// </summary>
        private void listViewLogs_DoubleClick(object sender, EventArgs e)
        {
            // 获取选中项
            if (listViewLogs.SelectedItems.Count == 0)
                return;
            
            // 获取日志对象
            var log = listViewLogs.SelectedItems[0].Tag as ExecutionLog;
            if (log == null)
                return;
            
            // 显示详情
            string message = $"执行ID: {log.Id}\n" +
                            $"调度任务ID: {log.ScheduleId}\n" +
                            $"开始时间: {log.StartTime:yyyy-MM-dd HH:mm:ss}\n" +
                            $"结束时间: {(log.EndTime != default ? log.EndTime.ToString("yyyy-MM-dd HH:mm:ss") : "-")}\n" +
                            $"状态: {log.Status}\n" +
                            $"错误信息: {(log.ErrorMessage ?? "-")}";
            
            MessageBox.Show(message, "执行日志详情", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        /// <summary>
        /// 关闭按钮点击事件
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
} 