using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using ExcelToMerge.Models;

namespace ExcelToMerge.Utils
{
    /// <summary>
    /// Excel文件操作辅助类
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static ExcelHelper()
        {
            // EPPlus 4.5.3.3版本不需要设置LicenseContext
            // 在此版本中没有LicenseContext属性
        }
        
        /// <summary>
        /// 获取Excel文件中的所有工作表名称
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <returns>工作表名称列表</returns>
        public static List<string> GetSheetNames(string filePath)
        {
            var sheetNames = new List<string>();
            
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                foreach (var worksheet in package.Workbook.Worksheets)
                {
                    sheetNames.Add(worksheet.Name);
                }
            }
            
            return sheetNames;
        }
        
        /// <summary>
        /// 将Excel工作表转换为DataTable
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="hasHeader">是否有标题行</param>
        /// <returns>DataTable对象</returns>
        public static DataTable ConvertSheetToDataTable(string filePath, string sheetName, bool hasHeader)
        {
            var dataTable = new DataTable();
            
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[sheetName];
                if (worksheet == null)
                    throw new ArgumentException($"工作表 '{sheetName}' 不存在");
                
                // 获取数据范围
                int startRow = hasHeader ? 2 : 1;
                int startCol = 1;
                int endRow = worksheet.Dimension.End.Row;
                int endCol = worksheet.Dimension.End.Column;
                
                // 添加列
                for (int col = startCol; col <= endCol; col++)
                {
                    string columnName = hasHeader ? 
                        (worksheet.Cells[1, col].Text ?? $"Column{col}") : 
                        $"Column{col}";
                    
                    // 确保列名不为空且唯一
                    if (string.IsNullOrWhiteSpace(columnName))
                        columnName = $"Column{col}";
                    
                    // 如果列名已存在，添加后缀
                    if (dataTable.Columns.Contains(columnName))
                    {
                        int suffix = 1;
                        while (dataTable.Columns.Contains($"{columnName}_{suffix}"))
                        {
                            suffix++;
                        }
                        columnName = $"{columnName}_{suffix}";
                    }
                    
                    dataTable.Columns.Add(columnName);
                }
                
                // 添加行
                for (int row = startRow; row <= endRow; row++)
                {
                    var dataRow = dataTable.NewRow();
                    bool hasData = false;
                    
                    for (int col = startCol; col <= endCol; col++)
                    {
                        var cell = worksheet.Cells[row, col];
                        var value = cell.Value;
                        
                        if (value != null)
                        {
                            dataRow[col - startCol] = value;
                            hasData = true;
                        }
                    }
                    
                    // 只添加非空行
                    if (hasData)
                    {
                        dataTable.Rows.Add(dataRow);
                    }
                }
            }
            
            return dataTable;
        }
        
        /// <summary>
        /// 将CSV文件转换为DataTable
        /// </summary>
        /// <param name="filePath">CSV文件路径</param>
        /// <param name="hasHeader">是否有标题行</param>
        /// <param name="delimiter">分隔符</param>
        /// <returns>DataTable对象</returns>
        public static DataTable ConvertCsvToDataTable(string filePath, bool hasHeader, char delimiter = ',')
        {
            var dataTable = new DataTable();
            
            using (var reader = new StreamReader(filePath, Encoding.Default))
            {
                // 读取第一行，可能是标题行
                string firstLine = reader.ReadLine();
                if (firstLine == null)
                    return dataTable;
                
                string[] headers = firstLine.Split(delimiter);
                
                // 添加列
                if (hasHeader)
                {
                    for (int i = 0; i < headers.Length; i++)
                    {
                        string columnName = headers[i].Trim();
                        
                        // 确保列名不为空且唯一
                        if (string.IsNullOrWhiteSpace(columnName))
                            columnName = $"Column{i + 1}";
                        
                        // 如果列名已存在，添加后缀
                        if (dataTable.Columns.Contains(columnName))
                        {
                            int suffix = 1;
                            while (dataTable.Columns.Contains($"{columnName}_{suffix}"))
                            {
                                suffix++;
                            }
                            columnName = $"{columnName}_{suffix}";
                        }
                        
                        dataTable.Columns.Add(columnName);
                    }
                }
                else
                {
                    // 如果没有标题行，使用默认列名
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dataTable.Columns.Add($"Column{i + 1}");
                    }
                    
                    // 添加第一行数据
                    dataTable.Rows.Add(headers);
                }
                
                // 读取剩余行
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(delimiter);
                    
                    // 如果字段数量与列数不匹配，调整字段数组
                    if (fields.Length != dataTable.Columns.Count)
                    {
                        Array.Resize(ref fields, dataTable.Columns.Count);
                    }
                    
                    dataTable.Rows.Add(fields);
                }
            }
            
            return dataTable;
        }
        
        /// <summary>
        /// 将DataTable导出为Excel文件
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="filePath">导出文件路径</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>是否导出成功</returns>
        public static bool ExportToExcel(DataTable dataTable, string filePath, string sheetName = "Sheet1")
        {
            try
            {
                // 确保目录存在
                string directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add(sheetName);
                    
                    // 写入标题行
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = dataTable.Columns[i].ColumnName;
                        worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                    }
                    
                    // 写入数据行
                    for (int row = 0; row < dataTable.Rows.Count; row++)
                    {
                        for (int col = 0; col < dataTable.Columns.Count; col++)
                        {
                            worksheet.Cells[row + 2, col + 1].Value = dataTable.Rows[row][col];
                        }
                    }
                    
                    // 自动调整列宽
                    worksheet.Cells.AutoFitColumns();
                    
                    // 保存文件
                    package.SaveAs(new FileInfo(filePath));
                }
                
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// 将DataTable导出为CSV文件
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="filePath">导出文件路径</param>
        /// <param name="delimiter">分隔符</param>
        /// <returns>是否导出成功</returns>
        public static bool ExportToCsv(DataTable dataTable, string filePath, char delimiter = ',')
        {
            try
            {
                // 确保目录存在
                string directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
                using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    // 写入标题行
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        writer.Write(EscapeCsvField(dataTable.Columns[i].ColumnName));
                        
                        if (i < dataTable.Columns.Count - 1)
                            writer.Write(delimiter);
                    }
                    writer.WriteLine();
                    
                    // 写入数据行
                    foreach (DataRow row in dataTable.Rows)
                    {
                        for (int i = 0; i < dataTable.Columns.Count; i++)
                        {
                            writer.Write(EscapeCsvField(row[i].ToString()));
                            
                            if (i < dataTable.Columns.Count - 1)
                                writer.Write(delimiter);
                        }
                        writer.WriteLine();
                    }
                }
                
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// 转义CSV字段
        /// </summary>
        /// <param name="field">字段值</param>
        /// <returns>转义后的字段值</returns>
        private static string EscapeCsvField(string field)
        {
            if (string.IsNullOrEmpty(field))
                return "";
            
            if (field.Contains("\"") || field.Contains(",") || field.Contains("\n"))
            {
                // 将字段中的双引号替换为两个双引号
                field = field.Replace("\"", "\"\"");
                // 用双引号包围字段
                field = $"\"{field}\"";
            }
            
            return field;
        }
        
        /// <summary>
        /// 从对象值推断SQLite数据类型
        /// </summary>
        /// <param name="value">对象值</param>
        /// <returns>SQLite数据类型</returns>
        public static string InferSqliteType(object value)
        {
            if (value == null || value == DBNull.Value)
                return "TEXT";
            
            Type type = value.GetType();
            
            if (type == typeof(int) || type == typeof(long) || type == typeof(short) || type == typeof(byte))
                return "INTEGER";
            
            if (type == typeof(float) || type == typeof(double) || type == typeof(decimal))
                return "REAL";
            
            if (type == typeof(bool))
                return "INTEGER"; // SQLite没有布尔类型，使用INTEGER
            
            if (type == typeof(DateTime) || type == typeof(DateTimeOffset))
                return "TEXT"; // 存储为ISO8601字符串
            
            // 默认使用TEXT类型
            return "TEXT";
        }
        
        /// <summary>
        /// 将值转换为SQLite兼容的格式
        /// </summary>
        /// <param name="value">原始值</param>
        /// <returns>转换后的值</returns>
        public static object ConvertValueForSqlite(object value)
        {
            if (value == null || value == DBNull.Value)
                return DBNull.Value;
            
            if (value is DateTime dateTime)
            {
                // 将DateTime转换为ISO8601格式字符串
                return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            
            if (value is DateTimeOffset dateTimeOffset)
            {
                // 将DateTimeOffset转换为ISO8601格式字符串
                return dateTimeOffset.ToString("yyyy-MM-dd HH:mm:ss");
            }
            
            // 检查是否为Excel日期时间值（数字表示的日期）
            if (value is double numericValue)
            {
                try
                {
                    // 尝试将数字转换为DateTime
                    DateTime excelDateTime = DateTime.FromOADate(numericValue);
                    // 如果转换成功且年份在合理范围内（避免错误识别）
                    if (excelDateTime.Year >= 1900 && excelDateTime.Year <= 2100)
                    {
                        return excelDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
                catch
                {
                    // 转换失败，不是有效的日期时间值
                }
            }
            
            return value;
        }
        
        /// <summary>
        /// 从DataTable推断SQLite表结构
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <returns>列定义列表</returns>
        public static List<ColumnInfo> InferTableStructure(DataTable dataTable)
        {
            var columns = new List<ColumnInfo>();
            
            foreach (DataColumn column in dataTable.Columns)
            {
                // 初始化为TEXT类型
                string sqliteType = "TEXT";
                
                // 尝试从数据中推断类型
                if (dataTable.Rows.Count > 0)
                {
                    // 检查前100行（或所有行，如果少于100行）
                    int rowsToCheck = Math.Min(100, dataTable.Rows.Count);
                    var types = new List<string>();
                    
                    for (int i = 0; i < rowsToCheck; i++)
                    {
                        var value = dataTable.Rows[i][column];
                        if (value != null && value != DBNull.Value)
                        {
                            types.Add(InferSqliteType(value));
                        }
                    }
                    
                    // 如果所有非空值都是同一类型，使用该类型
                    if (types.Count > 0 && types.All(t => t == types[0]))
                    {
                        sqliteType = types[0];
                    }
                }
                
                columns.Add(new ColumnInfo
                {
                    Name = column.ColumnName,
                    Type = sqliteType,
                    IsPrimaryKey = false,
                    IsNullable = true,
                    DefaultValue = null
                });
            }
            
            return columns;
        }
        
        /// <summary>
        /// 导入Excel文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="hasHeader">是否包含表头</param>
        /// <returns>DataSet</returns>
        public static DataSet ImportExcel(string filePath, bool hasHeader = true)
        {
            var dataSet = new DataSet();
            
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                foreach (var worksheet in package.Workbook.Worksheets)
                {
                    if (worksheet.Dimension == null)
                        continue; // 跳过空工作表
                    
                    var dataTable = new DataTable(worksheet.Name);
                    
                    // 获取数据范围
                    int startRow = hasHeader ? 2 : 1;
                    int startCol = 1;
                    int endRow = worksheet.Dimension.End.Row;
                    int endCol = worksheet.Dimension.End.Column;
                    
                    // 添加列
                    for (int col = startCol; col <= endCol; col++)
                    {
                        string columnName = hasHeader ? 
                            (worksheet.Cells[1, col].Text ?? $"Column{col}") : 
                            $"Column{col}";
                        
                        // 确保列名不为空且唯一
                        if (string.IsNullOrWhiteSpace(columnName))
                            columnName = $"Column{col}";
                        
                        // 如果列名已存在，添加后缀
                        if (dataTable.Columns.Contains(columnName))
                        {
                            int suffix = 1;
                            while (dataTable.Columns.Contains($"{columnName}_{suffix}"))
                            {
                                suffix++;
                            }
                            columnName = $"{columnName}_{suffix}";
                        }
                        
                        dataTable.Columns.Add(columnName);
                    }
                    
                    // 添加行
                    for (int row = startRow; row <= endRow; row++)
                    {
                        var dataRow = dataTable.NewRow();
                        bool hasData = false;
                        
                        for (int col = startCol; col <= endCol; col++)
                        {
                            var cell = worksheet.Cells[row, col];
                            var value = cell.Value;
                            
                            if (value != null)
                            {
                                // 处理日期时间值
                                if (cell.Style.Numberformat.NumFmtID >= 14 && cell.Style.Numberformat.NumFmtID <= 22 ||
                                    cell.Style.Numberformat.Format.Contains("yyyy") ||
                                    cell.Style.Numberformat.Format.Contains("mm") ||
                                    cell.Style.Numberformat.Format.Contains("dd") ||
                                    cell.Style.Numberformat.Format.Contains("hh") ||
                                    cell.Style.Numberformat.Format.Contains("ss"))
                                {
                                    // 是日期时间格式
                                    if (value is double numericDateValue)
                                    {
                                        try
                                        {
                                            // 尝试将数字转换为DateTime
                                            DateTime cellDateTime = DateTime.FromOADate(numericDateValue);
                                            dataRow[col - startCol] = cellDateTime;
                                        }
                                        catch
                                        {
                                            // 转换失败，使用原始值
                                            dataRow[col - startCol] = value;
                                        }
                                    }
                                    else
                                    {
                                        dataRow[col - startCol] = value;
                                    }
                                }
                                else
                                {
                                    dataRow[col - startCol] = value;
                                }
                                
                                hasData = true;
                            }
                        }
                        
                        // 只添加非空行
                        if (hasData)
                        {
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                    
                    dataSet.Tables.Add(dataTable);
                }
            }
            
            return dataSet;
        }
        
        /// <summary>
        /// 导入CSV文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="hasHeader">是否包含表头</param>
        /// <param name="delimiter">分隔符</param>
        /// <returns>DataTable</returns>
        public static DataTable ImportCsv(string filePath, bool hasHeader = true, char delimiter = ',')
        {
            return ConvertCsvToDataTable(filePath, hasHeader, delimiter);
        }
    }
} 