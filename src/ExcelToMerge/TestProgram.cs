using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ExcelToMerge.Models;
using ExcelToMerge.Services;
using ExcelToMerge.UI;

namespace ExcelToMerge
{
    /// <summary>
    /// 测试程序，用于验证所有修复是否有效
    /// </summary>
    public class TestProgram
    {
        /// <summary>
        /// 测试方法
        /// </summary>
        public static void Test()
        {
            try
            {
                // 测试ConvertService
                ConvertService convertService = new ConvertService();
                convertService.EnsureSqlTemplateTableExists();
                convertService.InitializeSystemSqlTemplates();
                
                // 测试SqlTemplateForm
                using (SqlTemplateForm templateForm = new SqlTemplateForm(convertService))
                {
                    // 只创建实例，不显示窗体
                }
                
                // 测试TaskSelectionForm
                List<ConvertTask> tasks = new List<ConvertTask>
                {
                    new ConvertTask
                    {
                        Id = 1,
                        Name = "测试任务",
                        Description = "测试任务描述",
                        SqlScript = "SELECT 1",
                        OutputFormat = OutputFormat.Excel,
                        CreatedTime = DateTime.Now
                    }
                };
                
                using (TaskSelectionForm taskForm = new TaskSelectionForm(tasks))
                {
                    // 只创建实例，不显示窗体
                    var selectedTasks = taskForm.SelectedTasks;
                }
                
                // 测试BatchExecutionForm
                using (BatchExecutionForm batchForm = new BatchExecutionForm(tasks))
                {
                    // 只创建实例，不显示窗体
                    var selectedTasks = batchForm.SelectedTasks;
                }
                
                MessageBox.Show("测试成功！所有类都可以正确实例化。", "测试结果", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"测试失败: {ex.Message}\n\n{ex.StackTrace}", "测试结果", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
} 