using System;
using System.Collections.Generic;

namespace ExcelToMerge.Models
{
    /// <summary>
    /// 调度任务
    /// </summary>
    public class ScheduleTask
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ScheduleTask()
        {
            TaskItems = new List<ScheduleTaskItem>();
            IsActive = true;
            CreatedTime = DateTime.Now;
        }
        
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// 输出路径
        /// </summary>
        public string OutputPath { get; set; }
        
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// 创建时间（与数据库字段名对应）
        /// </summary>
        public DateTime CreatedTime { get; set; }
        
        /// <summary>
        /// Cron表达式（定时执行规则）
        /// </summary>
        public string CronExpression { get; set; }
        
        /// <summary>
        /// 业务开始日期
        /// </summary>
        public DateTime? BusinessStartDate { get; set; }
        
        /// <summary>
        /// 业务结束日期
        /// </summary>
        public DateTime? BusinessEndDate { get; set; }
        
        /// <summary>
        /// 上次执行的业务日期
        /// </summary>
        public DateTime? LastExecutionDate { get; set; }
        
        /// <summary>
        /// 任务项数量（用于列表显示）
        /// </summary>
        public int ItemCount { get; set; }
        
        /// <summary>
        /// 任务项列表
        /// </summary>
        public List<ScheduleTaskItem> TaskItems { get; set; }
    }
    
    /// <summary>
    /// 调度任务项
    /// </summary>
    public class ScheduleTaskItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 调度任务ID
        /// </summary>
        public int ScheduleId { get; set; }
        
        /// <summary>
        /// 转换任务ID
        /// </summary>
        public int TaskId { get; set; }
        
        /// <summary>
        /// 序号
        /// </summary>
        public int Sequence { get; set; }
        
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// 转换任务
        /// </summary>
        public ConvertTask Task { get; set; }
    }
} 