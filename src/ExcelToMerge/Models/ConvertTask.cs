using System;

namespace ExcelToMerge.Models
{
    /// <summary>
    /// 转换任务
    /// </summary>
    public class ConvertTask
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ConvertTask()
        {
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
        /// SQL脚本
        /// </summary>
        public string SqlScript { get; set; }
        
        /// <summary>
        /// 输出格式
        /// </summary>
        public OutputFormat OutputFormat { get; set; }
        
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// 创建时间（与数据库字段名对应）
        /// </summary>
        public DateTime CreatedTime { get; set; }
    }
    
    /// <summary>
    /// 输出格式
    /// </summary>
    public enum OutputFormat
    {
        /// <summary>
        /// Excel
        /// </summary>
        Excel,
        
        /// <summary>
        /// CSV
        /// </summary>
        CSV
    }
} 