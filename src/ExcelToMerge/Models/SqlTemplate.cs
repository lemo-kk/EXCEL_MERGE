using System;

namespace ExcelToMerge.Models
{
    /// <summary>
    /// SQL模板类
    /// </summary>
    public class SqlTemplate
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 模板描述
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// SQL脚本
        /// </summary>
        public string SqlScript { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifiedTime { get; set; }
        
        /// <summary>
        /// 是否系统模板
        /// </summary>
        public bool IsSystem { get; set; }
        
        /// <summary>
        /// 分类
        /// </summary>
        public string Category { get; set; }
    }
} 