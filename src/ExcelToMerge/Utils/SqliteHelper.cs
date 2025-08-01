using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ExcelToMerge.Models;

namespace ExcelToMerge.Utils
{
    /// <summary>
    /// SQLite辅助类
    /// </summary>
    public static class SqliteHelper
    {
        private static readonly string _connectionString;
        
        /// <summary>
        /// 静态构造函数，初始化连接字符串
        /// </summary>
        static SqliteHelper()
        {
            string dbPath = ConfigurationManager.AppSettings["DatabasePath"];
            if (string.IsNullOrEmpty(dbPath))
            {
                dbPath = "Data\\ExcelData.db";
            }
            
            // 确保路径是相对于应用程序目录的
            if (!Path.IsPathRooted(dbPath))
            {
                dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dbPath);
            }
            
            // 确保目录存在
            string directory = Path.GetDirectoryName(dbPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            
            _connectionString = $"Data Source={dbPath};Version=3;";
            
            // 初始化数据库
            InitializeDatabase();
        }
        
        /// <summary>
        /// 初始化数据库
        /// </summary>
        private static void InitializeDatabase()
        {
            // 检查数据库文件是否存在
            string dbPath = ConfigurationManager.AppSettings["DatabasePath"];
            if (string.IsNullOrEmpty(dbPath))
            {
                dbPath = "ExcelToMerge.db";
            }
            
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dbPath);
            bool isNewDb = !File.Exists(fullPath);
            
            // 创建数据库目录
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            
            // 如果是新数据库，创建表结构
            if (isNewDb)
            {
                using (var connection = GetConnection())
                {
                    // 导入历史表
                    string importHistorySql = @"
                        CREATE TABLE IF NOT EXISTS ImportHistory (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            FileName TEXT NOT NULL,
                            FilePath TEXT NOT NULL,
                            FileType TEXT NOT NULL,
                            SheetCount INTEGER NOT NULL,
                            ImportedSheets INTEGER NOT NULL,
                            TotalRows INTEGER NOT NULL,
                            Status TEXT NOT NULL,
                            StartTime TEXT NOT NULL,
                            EndTime TEXT,
                            ErrorMessage TEXT
                        );";
                    
                    // 转换任务表
                    string convertTaskSql = @"
                        CREATE TABLE IF NOT EXISTS ConvertTask (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            Description TEXT,
                            SqlScript TEXT NOT NULL,
                            OutputFormat TEXT NOT NULL,
                            IsActive INTEGER NOT NULL,
                            CreatedTime TEXT NOT NULL
                        );";
                    
                    // 调度任务表
                    string scheduleConfigSql = @"
                        CREATE TABLE IF NOT EXISTS ScheduleConfig (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            Description TEXT,
                            OutputPath TEXT NOT NULL,
                            IsActive INTEGER NOT NULL,
                            CreatedTime TEXT NOT NULL,
                            CronExpression TEXT,
                            BusinessStartDate TEXT,
                            BusinessEndDate TEXT,
                            LastExecutionDate TEXT
                        );";
                    
                    // 调度明细表
                    string scheduleDetailSql = @"
                        CREATE TABLE IF NOT EXISTS ScheduleDetail (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            ScheduleId INTEGER NOT NULL,
                            TaskId INTEGER NOT NULL,
                            Sequence INTEGER NOT NULL,
                            IsActive INTEGER NOT NULL,
                            FOREIGN KEY (ScheduleId) REFERENCES ScheduleConfig (Id),
                            FOREIGN KEY (TaskId) REFERENCES ConvertTask (Id)
                        );";
                    
                    // 执行日志表
                    string executionLogSql = @"
                        CREATE TABLE IF NOT EXISTS ExecutionLog (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            ScheduleId INTEGER NOT NULL,
                            StartTime TEXT NOT NULL,
                            EndTime TEXT,
                            Status TEXT NOT NULL,
                            ErrorMessage TEXT,
                            FOREIGN KEY (ScheduleId) REFERENCES ScheduleConfig (Id)
                        );";
                    
                    // SQL模板表
                    string sqlTemplateSql = @"
                        CREATE TABLE IF NOT EXISTS SqlTemplate (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            Category TEXT,
                            Content TEXT NOT NULL,
                            IsSystem INTEGER NOT NULL,
                            CreatedTime TEXT NOT NULL
                        );";
                    
                    // 执行建表语句
                    using (var command = GetConnection().CreateCommand())
                    {
                        command.CommandText = importHistorySql;
                        command.ExecuteNonQuery();
                        
                        command.CommandText = convertTaskSql;
                        command.ExecuteNonQuery();
                        
                        command.CommandText = scheduleConfigSql;
                        command.ExecuteNonQuery();
                        
                        command.CommandText = scheduleDetailSql;
                        command.ExecuteNonQuery();
                        
                        command.CommandText = executionLogSql;
                        command.ExecuteNonQuery();
                        
                        command.CommandText = sqlTemplateSql;
                        command.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                // 检查是否需要升级表结构
                using (var connection = GetConnection())
                {
                    // 检查ScheduleConfig表是否有新增字段
                    bool hasNewFields = false;
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "PRAGMA table_info(ScheduleConfig);";
                        using (var reader = command.ExecuteReader())
                        {
                            var columnNames = new List<string>();
                            while (reader.Read())
                            {
                                columnNames.Add(reader["name"].ToString());
                            }
                            
                            hasNewFields = columnNames.Contains("CronExpression") && 
                                          columnNames.Contains("BusinessStartDate") && 
                                          columnNames.Contains("BusinessEndDate") && 
                                          columnNames.Contains("LastExecutionDate");
                        }
                    }
                    
                    // 如果没有新增字段，添加它们
                    if (!hasNewFields)
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = @"
                                ALTER TABLE ScheduleConfig ADD COLUMN CronExpression TEXT;
                                ALTER TABLE ScheduleConfig ADD COLUMN BusinessStartDate TEXT;
                                ALTER TABLE ScheduleConfig ADD COLUMN BusinessEndDate TEXT;
                                ALTER TABLE ScheduleConfig ADD COLUMN LastExecutionDate TEXT;";
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns>数据库连接</returns>
        public static SQLiteConnection GetConnection()
        {
            var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            return connection;
        }
        
        /// <summary>
        /// 执行非查询SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string sql, object parameters = null)
        {
            using (var connection = GetConnection())
            {
                return connection.Execute(sql, parameters);
            }
        }
        
        /// <summary>
        /// 执行查询SQL语句，返回单个值
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询结果</returns>
        public static T ExecuteScalar<T>(string sql, object parameters = null)
        {
            using (var connection = GetConnection())
            {
                return connection.ExecuteScalar<T>(sql, parameters);
            }
        }
        
        /// <summary>
        /// 执行查询SQL语句，返回DataTable
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询结果</returns>
        public static DataTable ExecuteQuery(string sql, object parameters = null)
        {
            using (var connection = GetConnection())
            {
                using (var command = new SQLiteCommand(sql, connection))
                {
                    // 添加参数
                    if (parameters != null)
                    {
                        var properties = parameters.GetType().GetProperties();
                        foreach (var prop in properties)
                        {
                            var value = prop.GetValue(parameters, null);
                            command.Parameters.AddWithValue($"@{prop.Name}", value ?? DBNull.Value);
                        }
                    }
                    
                    // 执行查询
                    using (var adapter = new SQLiteDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }
        
        /// <summary>
        /// 检查表是否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>表是否存在</returns>
        public static bool TableExists(string tableName)
        {
            string sql = "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name=@TableName;";
            int count = ExecuteScalar<int>(sql, new { TableName = tableName });
            return count > 0;
        }
        
        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>是否成功</returns>
        public static bool DropTable(string tableName)
        {
            if (!TableExists(tableName))
                return true;
            
            string sql = $"DROP TABLE IF EXISTS {tableName};";
            int result = ExecuteNonQuery(sql);
            return result >= 0;
        }
        
        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columns">列定义</param>
        /// <returns>是否成功</returns>
        public static bool CreateTable(string tableName, List<ColumnInfo> columns)
        {
            if (columns == null || columns.Count == 0)
                throw new ArgumentException("列定义不能为空", nameof(columns));
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"CREATE TABLE IF NOT EXISTS {tableName} (");
            
            for (int i = 0; i < columns.Count; i++)
            {
                var column = columns[i];
                sb.Append($"    {column.Name} {column.Type}");
                
                if (column.IsPrimaryKey)
                    sb.Append(" PRIMARY KEY");
                
                if (!column.IsNullable)
                    sb.Append(" NOT NULL");
                
                if (column.DefaultValue != null)
                    sb.Append($" DEFAULT {column.DefaultValue}");
                
                if (i < columns.Count - 1)
                    sb.AppendLine(",");
                else
                    sb.AppendLine();
            }
            
            sb.AppendLine(");");
            
            int result = ExecuteNonQuery(sb.ToString());
            return result >= 0;
        }
        
        /// <summary>
        /// 获取表的列信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>列信息列表</returns>
        public static List<ColumnInfo> GetTableColumns(string tableName)
        {
            var result = new List<ColumnInfo>();
            
            using (var connection = GetConnection())
            {
                // 获取表结构信息
                using (var command = new SQLiteCommand($"PRAGMA table_info({tableName});", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var columnInfo = new ColumnInfo
                            {
                                Name = reader["name"].ToString(),
                                Type = reader["type"].ToString(),
                                IsNullable = Convert.ToInt32(reader["notnull"]) == 0,
                                IsPrimaryKey = Convert.ToInt32(reader["pk"]) > 0,
                                DefaultValue = reader["dflt_value"]?.ToString()
                            };
                            result.Add(columnInfo);
                        }
                    }
                }
            }
            
            return result;
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="data">数据表</param>
        /// <param name="useReplace">是否使用REPLACE语法（用于合并数据）</param>
        /// <param name="batchSize">批处理大小</param>
        /// <returns>插入的行数</returns>
        public static int BulkInsert(string tableName, DataTable data, bool useReplace = false, int batchSize = 1000)
        {
            if (data == null || data.Rows.Count == 0)
                return 0;
            
            int totalRows = 0;
            
            using (var connection = GetConnection())
            {
                // 构建INSERT语句
                StringBuilder sb = new StringBuilder();
                
                // 根据useReplace参数决定使用INSERT还是INSERT OR REPLACE
                if (useReplace)
                {
                    sb.Append($"INSERT OR REPLACE INTO {tableName} (");
                }
                else
                {
                    sb.Append($"INSERT INTO {tableName} (");
                }
                
                // 添加列名
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    sb.Append(data.Columns[i].ColumnName);
                    if (i < data.Columns.Count - 1)
                        sb.Append(", ");
                }
                
                sb.Append(") VALUES (");
                
                // 添加参数占位符
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    sb.Append($"@p{i}");
                    if (i < data.Columns.Count - 1)
                        sb.Append(", ");
                }
                
                sb.Append(");");
                
                string sql = sb.ToString();
                
                // 分批处理数据
                for (int batchStart = 0; batchStart < data.Rows.Count; batchStart += batchSize)
                {
                    // 计算当前批次的大小
                    int currentBatchSize = Math.Min(batchSize, data.Rows.Count - batchStart);
                    
                    // 开始事务
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            using (var command = new SQLiteCommand(sql, connection, transaction))
                            {
                                // 创建参数
                                for (int i = 0; i < data.Columns.Count; i++)
                                {
                                    command.Parameters.Add(new SQLiteParameter($"@p{i}"));
                                }
                                
                                // 处理当前批次
                                for (int i = 0; i < currentBatchSize; i++)
                                {
                                    int rowIndex = batchStart + i;
                                    
                                    // 设置参数值
                                    for (int j = 0; j < data.Columns.Count; j++)
                                    {
                                        // 使用ExcelHelper.ConvertValueForSqlite处理值，特别是日期和时间
                                        object value = ExcelHelper.ConvertValueForSqlite(data.Rows[rowIndex][j]);
                                        command.Parameters[$"@p{j}"].Value = value ?? DBNull.Value;
                                    }
                                    
                                    try
                                    {
                                        // 执行命令
                                        command.ExecuteNonQuery();
                                        totalRows++;
                                    }
                                    catch (SQLiteException ex)
                                    {
                                        // 如果是主键冲突错误，并且不使用REPLACE语法，则忽略该行
                                        if (!useReplace && (ex.Message.Contains("UNIQUE constraint failed") || 
                                                           ex.Message.Contains("unique constraint") ||
                                                           ex.Message.Contains("constraint failed: UNIQUE")))
                                        {
                                            // 忽略该行，继续处理下一行
                                            continue;
                                        }
                                        else
                                        {
                                            // 其他错误，继续抛出
                                            throw;
                                        }
                                    }
                                }
                            }
                            
                            // 提交事务
                            transaction.Commit();
                        }
                        catch
                        {
                            // 回滚事务
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            
            return totalRows;
        }
        
        /// <summary>
        /// 批量插入数据，使用UPSERT语法处理主键冲突
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="data">数据表</param>
        /// <param name="primaryKeyColumn">主键列名</param>
        /// <param name="batchSize">批处理大小</param>
        /// <returns>插入或更新的行数</returns>
        public static int BulkInsertWithUpsert(string tableName, DataTable data, string primaryKeyColumn, int batchSize = 1000)
        {
            if (data == null || data.Rows.Count == 0)
                return 0;
            
            // 确保数据中包含主键列
            if (!data.Columns.Contains(primaryKeyColumn))
                throw new ArgumentException($"数据中不包含主键列 {primaryKeyColumn}");
            
            try
            {
                // 直接使用INSERT OR REPLACE语法，这是SQLite支持的标准语法
                return BulkInsert(tableName, data, true, batchSize);
            }
            catch (Exception ex)
            {
                // 如果INSERT OR REPLACE失败，尝试为每行分配新的主键值
                if (ex.Message.Contains("UNIQUE constraint failed") || 
                    ex.Message.Contains("unique constraint") ||
                    ex.Message.Contains("ON CONFLICT clause"))
                {
                    // 获取表中的主键最大值
                    long maxId = 0;
                    string sql = $"SELECT MAX({primaryKeyColumn}) FROM {tableName}";
                    var result = ExecuteScalar<object>(sql);
                    if (result != null && result != DBNull.Value)
                    {
                        maxId = Convert.ToInt64(result);
                    }
                    
                    // 为每行分配递增的主键值
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        data.Rows[i][primaryKeyColumn] = ++maxId;
                    }
                    
                    // 使用普通INSERT语法，因为我们已经确保主键不会冲突
                    return BulkInsert(tableName, data, false, batchSize);
                }
                else
                {
                    // 其他错误，继续抛出
                    throw;
                }
            }
        }
        
        /// <summary>
        /// 压缩数据库
        /// </summary>
        /// <returns>是否压缩成功</returns>
        public static bool VacuumDatabase()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    ExecuteNonQuery("VACUUM;");
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
} 