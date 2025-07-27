using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using ExcelToMerge.Models;
using ExcelToMerge.Utils;

namespace ExcelToMerge.Services
{
    /// <summary>
    /// 导入服务 
    /// </summary>
    public class ImportService
    {
        private readonly DatabaseService _databaseService;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public ImportService()
        {
            _databaseService = new DatabaseService();
        }
        
        /// <summary>
        /// 获取Excel文件中的所有工作表名称
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <returns>工作表名称列表</returns>
        public List<string> GetExcelSheets(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));
            
            if (!File.Exists(filePath))
                throw new FileNotFoundException("文件不存在", filePath);
            
            return ExcelHelper.GetSheetNames(filePath);
        }
        
        /// <summary>
        /// 导入Excel文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="hasHeader">是否有表头</param>
        /// <param name="tableExistsAction">表存在时的处理方式</param>
        /// <param name="progress">进度报告</param>
        /// <param name="tableActions">表名和对应的处理方式映射</param>
        /// <returns>导入任务</returns>
        public ImportTask ImportExcel(string filePath, bool hasHeader = true, TableExistsAction tableExistsAction = TableExistsAction.Ask, 
            IProgress<ProgressInfo> progress = null, Dictionary<string, TableExistsAction> tableActions = null)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));
            
            if (!File.Exists(filePath))
                throw new FileNotFoundException("文件不存在", filePath);
            
            // 创建导入任务
            var task = new ImportTask
            {
                FilePath = filePath,
                FileName = Path.GetFileNameWithoutExtension(filePath),
                FileType = Path.GetExtension(filePath).ToLower(),
                Status = ImportStatus.Importing,
                StartTime = DateTime.Now
            };
            
            try
            {
                // 根据文件类型选择导入方法
                DataSet dataSet;
                if (task.FileType == ".csv")
                {
                    // 导入CSV
                    var dt = ExcelHelper.ImportCsv(filePath, hasHeader);
                    dataSet = new DataSet();
                    dataSet.Tables.Add(dt);
                    task.SheetCount = 1;
                }
                else
                {
                    // 导入Excel
                    dataSet = ExcelHelper.ImportExcel(filePath, hasHeader);
                    task.SheetCount = dataSet.Tables.Count;
                }
                
                // 计算总行数
                int totalRows = 0;
                foreach (DataTable table in dataSet.Tables)
                {
                    totalRows += table.Rows.Count;
                }
                task.TotalRows = totalRows;
                
                // 报告初始进度
                progress?.Report(new ProgressInfo
                {
                    Status = "开始导入数据",
                    Percentage = 0,
                    ProcessedItems = 0,
                    TotalItems = totalRows
                });
                
                // 使用文件名作为表名的基础
                string baseTableName;
                if (task.FileType.ToLower() == ".csv")
                {
                    baseTableName = $"CSV_{task.FileName}";
                }
                else
                {
                    baseTableName = $"EXCEL_{task.FileName}";
                }
                
                // 如果只有一个工作表或者是CSV文件，直接使用文件名作为表名
                if (dataSet.Tables.Count == 1)
                {
                    string tableName = baseTableName;
                    var table = dataSet.Tables[0];
                    
                    // 检查表是否存在
                    bool tableExists = SqliteHelper.TableExists(tableName);
                    
                    // 如果表存在，根据用户选择的操作处理
                    if (tableExists)
                    {
                        // 如果有表名和操作的映射，使用映射中的操作
                        if (tableActions != null && tableActions.ContainsKey(tableName))
                        {
                            tableExistsAction = tableActions[tableName];
                        }
                        
                        switch (tableExistsAction)
                        {
                            case TableExistsAction.Append:
                                // 追加数据
                                // 首先获取表结构，确定主键（如果有）
                                var tableColumns = SqliteHelper.GetTableColumns(tableName);
                                bool hasPrimaryKey = tableColumns.Any(c => c.IsPrimaryKey);
                                
                                // 如果有主键，使用INSERT OR REPLACE语法进行数据合并
                                // 如果没有主键，则直接追加数据
                                SqliteHelper.BulkInsert(tableName, table, hasPrimaryKey);
                                break;
                            case TableExistsAction.Recreate:
                                // 删除并重建表
                                SqliteHelper.DropTable(tableName);
                                tableExists = false;
                                break;
                            case TableExistsAction.Cancel:
                                // 取消导入
                                throw new Exception($"表 {tableName} 已存在，导入已取消");
                            case TableExistsAction.Ask:
                                // 询问用户，但在这里我们无法询问，所以抛出异常
                                throw new Exception($"表 {tableName} 已存在，请指定处理方式");
                        }
                    }
                    
                    // 如果表不存在，创建表
                    if (!tableExists)
                    {
                        // 从DataTable创建列定义
                        var columns = new List<ColumnInfo>();
                        
                        // 添加数据列，不设置主键
                        foreach (DataColumn column in table.Columns)
                        {
                            var columnInfo = new ColumnInfo
                            {
                                Name = column.ColumnName,
                                Type = GetSqliteType(column.DataType),
                                IsNullable = column.AllowDBNull,
                                IsPrimaryKey = false // 不设置主键
                            };
                            columns.Add(columnInfo);
                        }
                        
                        // 创建表
                        SqliteHelper.CreateTable(tableName, columns);
                    }
                    
                    // 导入数据
                    SqliteHelper.BulkInsert(tableName, table, false);
                    
                    // 更新进度
                    task.ImportedSheets++;
                    
                    // 报告进度
                    progress?.Report(new ProgressInfo
                    {
                        Status = "导入完成",
                        Percentage = 100,
                        ProcessedItems = totalRows,
                        TotalItems = totalRows
                    });
                }
                else
                {
                    // 如果有多个工作表，为每个工作表创建一个表，表名为"EXCEL_文件名_工作表名"
                    int processedRows = 0;
                    for (int i = 0; i < dataSet.Tables.Count; i++)
                    {
                        var table = dataSet.Tables[i];
                        string sheetName = string.IsNullOrEmpty(table.TableName) ? $"Sheet{i + 1}" : table.TableName;
                        string tableName = $"{baseTableName}_{sheetName}";
                        
                        // 检查表是否存在
                        bool tableExists = SqliteHelper.TableExists(tableName);
                        
                        // 如果表存在，根据用户选择的操作处理
                        if (tableExists)
                        {
                            // 如果有表名和操作的映射，使用映射中的操作
                            TableExistsAction action = tableExistsAction;
                            if (tableActions != null && tableActions.ContainsKey(tableName))
                            {
                                action = tableActions[tableName];
                            }
                            
                            switch (action)
                            {
                                case TableExistsAction.Append:
                                    // 追加数据
                                    // 首先获取表结构，确定主键（如果有）
                                    var tableColumns = SqliteHelper.GetTableColumns(tableName);
                                    bool hasPrimaryKey = tableColumns.Any(c => c.IsPrimaryKey);
                                    
                                    // 如果有主键，使用INSERT OR REPLACE语法进行数据合并
                                    // 如果没有主键，则直接追加数据
                                    SqliteHelper.BulkInsert(tableName, table, hasPrimaryKey);
                                    break;
                                case TableExistsAction.Recreate:
                                    // 删除并重建表
                                    SqliteHelper.DropTable(tableName);
                                    tableExists = false;
                                    break;
                                case TableExistsAction.Cancel:
                                    // 取消导入当前工作表，继续处理下一个
                                    continue;
                                case TableExistsAction.Ask:
                                    // 询问用户，但在这里我们无法询问，所以抛出异常
                                    throw new Exception($"表 {tableName} 已存在，请指定处理方式");
                            }
                        }
                        
                        // 如果表不存在，创建表
                        if (!tableExists)
                        {
                            // 从DataTable创建列定义
                            var columns = new List<ColumnInfo>();
                            
                            // 添加数据列，不设置主键
                            foreach (DataColumn column in table.Columns)
                            {
                                var columnInfo = new ColumnInfo
                                {
                                    Name = column.ColumnName,
                                    Type = GetSqliteType(column.DataType),
                                    IsNullable = column.AllowDBNull,
                                    IsPrimaryKey = false // 不设置主键
                                };
                                columns.Add(columnInfo);
                            }
                            
                            // 创建表
                            SqliteHelper.CreateTable(tableName, columns);
                        }
                        
                        // 导入数据
                        SqliteHelper.BulkInsert(tableName, table, false);
                        
                        // 更新进度
                        processedRows += table.Rows.Count;
                        task.ImportedSheets++;
                        
                        // 报告进度
                        progress?.Report(new ProgressInfo
                        {
                            Status = $"已导入 {task.ImportedSheets}/{task.SheetCount} 个工作表",
                            Percentage = (processedRows * 100) / totalRows,
                            ProcessedItems = processedRows,
                            TotalItems = totalRows
                        });
                    }
                }
                
                // 更新任务状态
                task.Status = ImportStatus.Success;
                task.EndTime = DateTime.Now;
                
                // 保存导入历史
                _databaseService.SaveImportHistory(task);
                
                // 报告完成
                progress?.Report(new ProgressInfo
                {
                    Status = "导入完成",
                    Percentage = 100,
                    ProcessedItems = totalRows,
                    TotalItems = totalRows
                });
                
                return task;
            }
            catch (Exception ex)
            {
                // 更新任务状态
                task.Status = ImportStatus.Failed;
                task.EndTime = DateTime.Now;
                task.ErrorMessage = ex.Message;
                
                // 保存导入历史
                _databaseService.SaveImportHistory(task);
                
                // 报告错误
                progress?.Report(new ProgressInfo
                {
                    Status = "导入失败",
                    Percentage = 0,
                    ProcessedItems = 0,
                    TotalItems = task.TotalRows,
                    Error = ex
                });
                
                // 如果是表已存在的异常，继续抛出以便UI层处理
                if (ex.Message.Contains("表") && ex.Message.Contains("已存在"))
                {
                    throw;
                }
                
                // 其他异常，返回失败的任务
                return task;
            }
        }
        
        /// <summary>
        /// 获取SQLite数据类型
        /// </summary>
        /// <param name="type">CLR类型</param>
        /// <returns>SQLite类型字符串</returns>
        private string GetSqliteType(Type type)
        {
            if (type == typeof(int) || type == typeof(long) || type == typeof(short) || 
                type == typeof(uint) || type == typeof(ulong) || type == typeof(ushort) || 
                type == typeof(byte) || type == typeof(sbyte))
                return "INTEGER";
            
            if (type == typeof(float) || type == typeof(double) || type == typeof(decimal))
                return "REAL";
            
            if (type == typeof(bool))
                return "INTEGER";
            
            if (type == typeof(DateTime) || type == typeof(DateTimeOffset))
                return "TEXT";
            
            if (type == typeof(Guid))
                return "TEXT";
            
            if (type == typeof(byte[]))
                return "BLOB";
            
            return "TEXT";
        }
    }
    
    /// <summary>
    /// 表存在时的处理方式
    /// </summary>
    public enum TableExistsAction
    {
        /// <summary>
        /// 询问用户
        /// </summary>
        Ask,
        
        /// <summary>
        /// 追加数据
        /// </summary>
        Append,
        
        /// <summary>
        /// 删除并重建
        /// </summary>
        Recreate,
        
        /// <summary>
        /// 取消导入
        /// </summary>
        Cancel,
        
        /// <summary>
        /// 取消所有后续文件导入
        /// </summary>
        CancelAll
    }
} 