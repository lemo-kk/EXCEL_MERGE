using System;
using System.Collections.Generic;

namespace ExcelToMerge.Models
{
    /// <summary>
    /// 导入任务
    /// </summary>
    public class ImportTask
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ImportTask()
        {
            StartTime = DateTime.Now;
            Status = ImportStatus.Pending;
        }
        
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
        
        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType { get; set; }
        
        /// <summary>
        /// 工作表数量
        /// </summary>
        public int SheetCount { get; set; }
        
        /// <summary>
        /// 已导入的工作表数量
        /// </summary>
        public int ImportedSheets { get; set; }
        
        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalRows { get; set; }
        
        /// <summary>
        /// 状态
        /// </summary>
        public ImportStatus Status { get; set; }
        
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// 额外数据
        /// </summary>
        public object Tag { get; set; }
    }
    
    /// <summary>
    /// 导入状态
    /// </summary>
    public enum ImportStatus
    {
        /// <summary>
        /// 等待中
        /// </summary>
        Pending,
        
        /// <summary>
        /// 正在导入
        /// </summary>
        Importing,
        
        /// <summary>
        /// 成功
        /// </summary>
        Success,
        
        /// <summary>
        /// 失败
        /// </summary>
        Failed
    }
} 