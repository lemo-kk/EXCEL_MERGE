using System;

namespace ExcelToMerge.Models
{
    /// <summary>
    /// 执行日志
    /// </summary>
    public class ExecutionLog
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExecutionLog()
        {
            StartTime = DateTime.Now;
            Status = "Pending";
        }
        
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 调度任务ID
        /// </summary>
        public int ScheduleId { get; set; }
        
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
} 