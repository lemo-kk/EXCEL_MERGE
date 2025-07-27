using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using ExcelToMerge.Models;
using ExcelToMerge.Utils;

namespace ExcelToMerge.Services
{
    /// <summary>
    /// 调度服务
    /// </summary>
    public class ScheduleService
    {
        private readonly DatabaseService _databaseService;
        private readonly ConvertService _convertService;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public ScheduleService()
        {
            _databaseService = new DatabaseService();
            _convertService = new ConvertService();
        }
        
        /// <summary>
        /// 保存调度任务
        /// </summary>
        /// <param name="task">调度任务</param>
        /// <returns>任务ID</returns>
        public int SaveScheduleTask(ScheduleTask task)
        {
            // 验证参数
            if (task == null)
                throw new ArgumentNullException(nameof(task));
            
            if (string.IsNullOrEmpty(task.Name))
                throw new ArgumentException("任务名称不能为空", nameof(task.Name));
            
            if (string.IsNullOrEmpty(task.OutputPath))
                throw new ArgumentException("输出路径不能为空", nameof(task.OutputPath));
            
            // 保存调度任务
            int taskId = _databaseService.SaveScheduleTask(task);
            
            // 如果是新任务，设置ID
            if (task.Id == 0)
                task.Id = taskId;
            
            // 保存调度明细
            if (task.TaskItems != null && task.TaskItems.Count > 0)
            {
                // 先删除旧的明细
                _databaseService.DeleteScheduleTaskItems(task.Id);
                
                // 添加新的明细
                foreach (var item in task.TaskItems)
                {
                    item.ScheduleId = task.Id;
                    _databaseService.SaveScheduleTaskItem(item);
                }
            }
            
            return taskId;
        }
        
        /// <summary>
        /// 获取调度任务
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns>调度任务</returns>
        public ScheduleTask GetScheduleTask(int id)
        {
            // 获取任务基本信息
            var task = _databaseService.GetScheduleTask(id);
            
            if (task == null)
                return null;
            
            // 获取任务明细
            var items = _databaseService.GetScheduleTaskItems(id);
            task.TaskItems = items;
            
            // 获取关联的转换任务
            foreach (var item in task.TaskItems)
            {
                item.Task = _convertService.GetConvertTask(item.TaskId);
            }
            
            return task;
        }
        
        /// <summary>
        /// 获取调度任务列表
        /// </summary>
        /// <param name="activeOnly">是否只获取激活的任务</param>
        /// <returns>调度任务列表</returns>
        public List<ScheduleTask> GetScheduleTaskList(bool activeOnly = false)
        {
            return _databaseService.GetScheduleTaskList(activeOnly);
        }
        
        /// <summary>
        /// 删除调度任务
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns>是否成功</returns>
        public bool DeleteScheduleTask(int id)
        {
            // 先删除明细
            _databaseService.DeleteScheduleTaskItems(id);
            
            // 再删除主表
            return _databaseService.DeleteScheduleTask(id);
        }
        
        /// <summary>
        /// 执行调度任务
        /// </summary>
        /// <param name="task">调度任务</param>
        /// <param name="progress">进度报告</param>
        /// <returns>执行结果</returns>
        public ScheduleExecutionResult ExecuteScheduleTask(ScheduleTask task, IProgress<ProgressInfo> progress = null)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));
            
            if (task.TaskItems == null || task.TaskItems.Count == 0)
                throw new ArgumentException("调度任务不包含任何转换作业", nameof(task));
            
            // 创建结果对象
            var result = new ScheduleExecutionResult
            {
                ScheduleId = task.Id,
                ScheduleName = task.Name,
                StartTime = DateTime.Now,
                Success = true,
                TaskResults = new List<TaskExecutionResult>()
            };
            
            try
            {
                // 创建执行日志
                var log = new ExecutionLog
                {
                    ScheduleId = task.Id,
                    StartTime = DateTime.Now,
                    Status = "Running"
                };
                
                int logId = _databaseService.SaveExecutionLog(log);
                
                // 确保输出目录存在
                if (!Directory.Exists(task.OutputPath))
                {
                    Directory.CreateDirectory(task.OutputPath);
                }
                
                // 执行每个转换任务
                int totalTasks = task.TaskItems.Count;
                int completedTasks = 0;
                
                foreach (var item in task.TaskItems.OrderBy(i => i.Sequence))
                {
                    if (!item.IsActive || item.Task == null)
                    {
                        // 跳过未激活的任务
                        result.TaskResults.Add(new TaskExecutionResult
                        {
                            TaskId = item.TaskId,
                            TaskName = item.Task?.Name ?? "未知任务",
                            Skipped = true
                        });
                        
                        completedTasks++;
                        continue;
                    }
                    
                    try
                    {
                        // 报告进度
                        progress?.Report(new ProgressInfo
                        {
                            Status = $"正在执行任务: {item.Task.Name}",
                            Percentage = (completedTasks * 100) / totalTasks,
                            ProcessedItems = completedTasks,
                            TotalItems = totalTasks
                        });
                        
                        // 执行SQL查询
                        var dataTable = _databaseService.ExecuteQuery(item.Task.SqlScript);
                        
                        // 生成输出文件名
                        string fileName = $"{item.Task.Name}_{DateTime.Now:yyyyMMdd_HHmmss}";
                        string filePath = Path.Combine(task.OutputPath, fileName);
                        
                        // 导出数据
                        if (item.Task.OutputFormat == OutputFormat.Excel)
                        {
                            filePath += ".xlsx";
                            ExcelHelper.ExportToExcel(dataTable, filePath);
                        }
                        else
                        {
                            filePath += ".csv";
                            ExcelHelper.ExportToCsv(dataTable, filePath);
                        }
                        
                        // 添加执行结果
                        result.TaskResults.Add(new TaskExecutionResult
                        {
                            TaskId = item.TaskId,
                            TaskName = item.Task.Name,
                            Success = true,
                            OutputFile = filePath,
                            RecordCount = dataTable.Rows.Count
                        });
                    }
                    catch (Exception ex)
                    {
                        // 记录任务执行错误
                        result.TaskResults.Add(new TaskExecutionResult
                        {
                            TaskId = item.TaskId,
                            TaskName = item.Task.Name,
                            Success = false,
                            ErrorMessage = ex.Message
                        });
                        
                        // 继续执行其他任务
                    }
                    
                    completedTasks++;
                    
                    // 报告进度
                    progress?.Report(new ProgressInfo
                    {
                        Status = $"已完成: {completedTasks}/{totalTasks}",
                        Percentage = (completedTasks * 100) / totalTasks,
                        ProcessedItems = completedTasks,
                        TotalItems = totalTasks
                    });
                }
                
                // 更新执行日志
                log.EndTime = DateTime.Now;
                log.Status = "Completed";
                _databaseService.SaveExecutionLog(log);
                
                // 设置结果
                result.EndTime = DateTime.Now;
                result.Success = result.TaskResults.All(r => r.Success || r.Skipped);
                
                if (!result.Success)
                {
                    var failedTasks = result.TaskResults.Where(r => !r.Success && !r.Skipped).ToList();
                    result.ErrorMessage = $"有 {failedTasks.Count} 个任务执行失败";
                }
                
                return result;
            }
            catch (Exception ex)
            {
                // 更新执行日志
                var log = new ExecutionLog
                {
                    ScheduleId = task.Id,
                    StartTime = result.StartTime,
                    EndTime = DateTime.Now,
                    Status = "Failed",
                    ErrorMessage = ex.Message
                };
                
                _databaseService.SaveExecutionLog(log);
                
                // 设置结果
                result.EndTime = DateTime.Now;
                result.Success = false;
                result.ErrorMessage = ex.Message;
                
                return result;
            }
        }
    }
    
    /// <summary>
    /// 调度执行结果
    /// </summary>
    public class ScheduleExecutionResult
    {
        /// <summary>
        /// 调度任务ID
        /// </summary>
        public int ScheduleId { get; set; }
        
        /// <summary>
        /// 调度任务名称
        /// </summary>
        public string ScheduleName { get; set; }
        
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// 任务执行结果列表
        /// </summary>
        public List<TaskExecutionResult> TaskResults { get; set; }
    }
    
    /// <summary>
    /// 任务执行结果
    /// </summary>
    public class TaskExecutionResult
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        public int TaskId { get; set; }
        
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }
        
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// 是否跳过
        /// </summary>
        public bool Skipped { get; set; }
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// 输出文件
        /// </summary>
        public string OutputFile { get; set; }
        
        /// <summary>
        /// 处理的记录数
        /// </summary>
        public int RecordCount { get; set; }
    }
}