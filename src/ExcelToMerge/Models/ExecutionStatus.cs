namespace ExcelToMerge.Models
{
    /// <summary>
    /// 执行状态枚举
    /// </summary>
    public enum ExecutionStatus
    {
        /// <summary>
        /// 失败
        /// </summary>
        Failed = 0,
        
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,
        
        /// <summary>
        /// 运行中
        /// </summary>
        Running = 2
    }
} 