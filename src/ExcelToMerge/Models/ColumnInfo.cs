using System;

namespace ExcelToMerge.Models
{
    /// <summary>
    /// 数据库列信息
    /// </summary>
    public class ColumnInfo
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 列类型
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// 是否可为空
        /// </summary>
        public bool IsNullable { get; set; } = true;
        
        /// <summary>
        /// 是否为主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }
        
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }
    }
}