using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Configuration;
using ExcelToMerge.Models;
using ExcelToMerge.Utils;
using System.Linq;

namespace ExcelToMerge.Services
{
    /// <summary>
    /// 数据库服务
    /// </summary>
    public class DatabaseService
    {
        private readonly string _databasePath;
        private readonly string _backupPath;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public DatabaseService()
        {
            // 从配置文件中获取数据库路径
            _databasePath = ConfigurationManager.AppSettings["DatabasePath"];
            if (string.IsNullOrEmpty(_databasePath))
            {
                _databasePath = "Data\\ExcelData.db";
            }
            
            // 确保路径是相对于应用程序目录的
            if (!Path.IsPathRooted(_databasePath))
            {
                _databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _databasePath);
            }
            
            // 从配置文件中获取备份路径
            _backupPath = ConfigurationManager.AppSettings["BackupPath"];
            if (string.IsNullOrEmpty(_backupPath))
            {
                _backupPath = "Backup";
            }
            
            // 确保路径是相对于应用程序目录的
            if (!Path.IsPathRooted(_backupPath))
            {
                _backupPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _backupPath);
            }
            
            // 确保目录存在
            string directory = Path.GetDirectoryName(_databasePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            
            if (!Directory.Exists(_backupPath))
            {
                Directory.CreateDirectory(_backupPath);
            }
        }
        
        #region 导入历史
        
        /// <summary>
        /// 保存导入历史
        /// </summary>
        /// <param name="task">导入任务</param>
        /// <returns>记录ID</returns>
        public int SaveImportHistory(ImportTask task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));
            
            string sql = @"
                INSERT INTO ImportHistory 
                (FileName, FilePath, FileType, SheetCount, ImportedSheets, TotalRows, Status, StartTime, EndTime, ErrorMessage)
                VALUES 
                (@FileName, @FilePath, @FileType, @SheetCount, @ImportedSheets, @TotalRows, @Status, @StartTime, @EndTime, @ErrorMessage);
                SELECT last_insert_rowid();";
            
                            var parameters = new
                {
                    task.FileName,
                    task.FilePath,
                    task.FileType,
                    task.SheetCount,
                    task.ImportedSheets,
                    task.TotalRows,
                    Status = task.Status.ToString(),
                    StartTime = task.StartTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    EndTime = task.EndTime.HasValue ? task.EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : (object)DBNull.Value,
                    task.ErrorMessage
                };
            
            return SqliteHelper.ExecuteScalar<int>(sql, parameters);
        }
        
        /// <summary>
        /// 获取导入历史列表
        /// </summary>
        /// <returns>导入历史列表</returns>
        public List<ImportTask> GetImportHistoryList()
        {
            string sql = "SELECT * FROM ImportHistory ORDER BY StartTime DESC;";
            
            var dataTable = SqliteHelper.ExecuteQuery(sql);
            var result = new List<ImportTask>();
            
            foreach (DataRow row in dataTable.Rows)
            {
                var task = new ImportTask
                {
                    Id = Convert.ToInt32(row["Id"]),
                    FileName = row["FileName"].ToString(),
                    FilePath = row["FilePath"].ToString(),
                    FileType = row["FileType"].ToString(),
                    SheetCount = Convert.ToInt32(row["SheetCount"]),
                    ImportedSheets = Convert.ToInt32(row["ImportedSheets"]),
                    TotalRows = Convert.ToInt32(row["TotalRows"]),
                    Status = (ImportStatus)Enum.Parse(typeof(ImportStatus), row["Status"].ToString()),
                    StartTime = Convert.ToDateTime(row["StartTime"]),
                    EndTime = row["EndTime"] != DBNull.Value ? Convert.ToDateTime(row["EndTime"]) : (DateTime?)null,
                    ErrorMessage = row["ErrorMessage"] != DBNull.Value ? row["ErrorMessage"].ToString() : null
                };
                
                result.Add(task);
            }
            
            return result;
        }
        
        #endregion
        
        #region 转换任务
        
        /// <summary>
        /// 保存转换任务
        /// </summary>
        /// <param name="task">转换任务</param>
        /// <returns>任务ID</returns>
        public int SaveConvertTask(ConvertTask task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));
            
            if (string.IsNullOrEmpty(task.Name))
                throw new ArgumentException("任务名称不能为空", nameof(task.Name));
            
            if (string.IsNullOrEmpty(task.SqlScript))
                throw new ArgumentException("SQL脚本不能为空", nameof(task.SqlScript));
            
            if (task.Id == 0)
            {
                // 新增
                string sql = @"
                    INSERT INTO ConvertTask 
                    (Name, Description, SqlScript, OutputFormat, IsActive, CreatedTime)
                    VALUES 
                    (@Name, @Description, @SqlScript, @OutputFormat, @IsActive, @CreatedTime);
                    SELECT last_insert_rowid();";
                
                var parameters = new
                {
                    task.Name,
                    task.Description,
                    task.SqlScript,
                    OutputFormat = task.OutputFormat.ToString(),
                    IsActive = task.IsActive ? 1 : 0,
                    CreatedTime = task.CreatedTime
                };
                
                return SqliteHelper.ExecuteScalar<int>(sql, parameters);
            }
            else
            {
                // 修改
                string sql = @"
                    UPDATE ConvertTask 
                    SET Name = @Name, 
                        Description = @Description, 
                        SqlScript = @SqlScript, 
                        OutputFormat = @OutputFormat, 
                        IsActive = @IsActive
                    WHERE Id = @Id;";
                
                var parameters = new
                {
                    task.Id,
                    task.Name,
                    task.Description,
                    task.SqlScript,
                    OutputFormat = task.OutputFormat.ToString(),
                    IsActive = task.IsActive ? 1 : 0
                };
                
                SqliteHelper.ExecuteNonQuery(sql, parameters);
                return task.Id;
            }
        }
        
        /// <summary>
        /// 获取转换任务
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns>转换任务</returns>
        public ConvertTask GetConvertTask(int id)
        {
            string sql = "SELECT * FROM ConvertTask WHERE Id = @Id;";
            
            var dataTable = SqliteHelper.ExecuteQuery(sql, new { Id = id });
            
            if (dataTable.Rows.Count == 0)
                return null;
            
            var row = dataTable.Rows[0];
            return new ConvertTask
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = row["Name"].ToString(),
                Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,
                SqlScript = row["SqlScript"].ToString(),
                OutputFormat = (OutputFormat)Enum.Parse(typeof(OutputFormat), row["OutputFormat"].ToString()),
                IsActive = Convert.ToBoolean(row["IsActive"]),
                CreatedTime = Convert.ToDateTime(row["CreatedTime"])
            };
        }
        
        /// <summary>
        /// 获取转换任务列表
        /// </summary>
        /// <param name="activeOnly">是否只获取激活的任务</param>
        /// <returns>转换任务列表</returns>
        public List<ConvertTask> GetConvertTaskList(bool activeOnly = false)
        {
            string sql = "SELECT * FROM ConvertTask";
            
            if (activeOnly)
            {
                sql += " WHERE IsActive = 1";
            }
            
            sql += " ORDER BY CreatedTime DESC;";
            
            var dataTable = SqliteHelper.ExecuteQuery(sql);
            var result = new List<ConvertTask>();
            
            foreach (DataRow row in dataTable.Rows)
            {
                var task = new ConvertTask
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString(),
                    Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,
                    SqlScript = row["SqlScript"].ToString(),
                    OutputFormat = (OutputFormat)Enum.Parse(typeof(OutputFormat), row["OutputFormat"].ToString()),
                    IsActive = Convert.ToBoolean(row["IsActive"]),
                    CreatedTime = Convert.ToDateTime(row["CreatedTime"])
                };
                
                result.Add(task);
            }
            
            return result;
        }
        
        /// <summary>
        /// 删除转换任务
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns>是否成功</returns>
        public bool DeleteConvertTask(int id)
        {
            string sql = "DELETE FROM ConvertTask WHERE Id = @Id;";
            
            int rowsAffected = SqliteHelper.ExecuteNonQuery(sql, new { Id = id });
            return rowsAffected > 0;
        }
        
        #endregion
        
        #region 调度任务
        
        /// <summary>
        /// 保存调度任务
        /// </summary>
        /// <param name="task">调度任务</param>
        /// <returns>任务ID</returns>
        public int SaveScheduleTask(ScheduleTask task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));
            
            if (task.Id == 0)
            {
                // 新增
                string sql = @"
                    INSERT INTO ScheduleConfig 
                    (Name, Description, OutputPath, IsActive, CreatedTime, CronExpression, BusinessStartDate, BusinessEndDate, LastExecutionDate)
                    VALUES 
                    (@Name, @Description, @OutputPath, @IsActive, @CreatedTime, @CronExpression, @BusinessStartDate, @BusinessEndDate, @LastExecutionDate);
                    SELECT last_insert_rowid();";
                
                var parameters = new
                {
                    task.Name,
                    task.Description,
                    task.OutputPath,
                    IsActive = task.IsActive ? 1 : 0,
                    CreatedTime = task.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    task.CronExpression,
                    BusinessStartDate = task.BusinessStartDate.HasValue ? task.BusinessStartDate.Value.ToString("yyyy-MM-dd") : (object)DBNull.Value,
                    BusinessEndDate = task.BusinessEndDate.HasValue ? task.BusinessEndDate.Value.ToString("yyyy-MM-dd") : (object)DBNull.Value,
                    LastExecutionDate = task.LastExecutionDate.HasValue ? task.LastExecutionDate.Value.ToString("yyyy-MM-dd") : (object)DBNull.Value
                };
                
                return SqliteHelper.ExecuteScalar<int>(sql, parameters);
            }
            else
            {
                // 修改
                string sql = @"
                    UPDATE ScheduleConfig 
                    SET Name = @Name, 
                        Description = @Description, 
                        OutputPath = @OutputPath, 
                        IsActive = @IsActive,
                        CronExpression = @CronExpression,
                        BusinessStartDate = @BusinessStartDate,
                        BusinessEndDate = @BusinessEndDate,
                        LastExecutionDate = @LastExecutionDate
                    WHERE Id = @Id;";
                
                var parameters = new
                {
                    task.Id,
                    task.Name,
                    task.Description,
                    task.OutputPath,
                    IsActive = task.IsActive ? 1 : 0,
                    task.CronExpression,
                    BusinessStartDate = task.BusinessStartDate.HasValue ? task.BusinessStartDate.Value.ToString("yyyy-MM-dd") : (object)DBNull.Value,
                    BusinessEndDate = task.BusinessEndDate.HasValue ? task.BusinessEndDate.Value.ToString("yyyy-MM-dd") : (object)DBNull.Value,
                    LastExecutionDate = task.LastExecutionDate.HasValue ? task.LastExecutionDate.Value.ToString("yyyy-MM-dd") : (object)DBNull.Value
                };
                
                SqliteHelper.ExecuteNonQuery(sql, parameters);
                return task.Id;
            }
        }
        
        /// <summary>
        /// 获取调度任务
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns>调度任务</returns>
        public ScheduleTask GetScheduleTask(int id)
        {
            string sql = "SELECT * FROM ScheduleConfig WHERE Id = @Id;";
            
            var dataTable = SqliteHelper.ExecuteQuery(sql, new { Id = id });
            
            if (dataTable.Rows.Count == 0)
                return null;
            
            var row = dataTable.Rows[0];
            var task = new ScheduleTask
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = row["Name"].ToString(),
                Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,
                OutputPath = row["OutputPath"].ToString(),
                IsActive = Convert.ToInt32(row["IsActive"]) != 0,
                CreatedTime = Convert.ToDateTime(row["CreatedTime"]),
                CronExpression = row["CronExpression"] != DBNull.Value ? row["CronExpression"].ToString() : null,
                BusinessStartDate = row["BusinessStartDate"] != DBNull.Value ? Convert.ToDateTime(row["BusinessStartDate"]) : (DateTime?)null,
                BusinessEndDate = row["BusinessEndDate"] != DBNull.Value ? Convert.ToDateTime(row["BusinessEndDate"]) : (DateTime?)null,
                LastExecutionDate = row["LastExecutionDate"] != DBNull.Value ? Convert.ToDateTime(row["LastExecutionDate"]) : (DateTime?)null
            };
            
            // 获取任务项数量
            string countSql = "SELECT COUNT(*) FROM ScheduleDetail WHERE ScheduleId = @ScheduleId;";
            task.ItemCount = SqliteHelper.ExecuteScalar<int>(countSql, new { ScheduleId = id });
            
            return task;
        }
        
        /// <summary>
        /// 获取调度任务列表
        /// </summary>
        /// <param name="activeOnly">是否只获取激活的任务</param>
        /// <returns>调度任务列表</returns>
        public List<ScheduleTask> GetScheduleTaskList(bool activeOnly = false)
        {
            string sql = activeOnly 
                ? "SELECT * FROM ScheduleConfig WHERE IsActive = 1 ORDER BY Id DESC;" 
                : "SELECT * FROM ScheduleConfig ORDER BY Id DESC;";
            
            var dataTable = SqliteHelper.ExecuteQuery(sql);
            var result = new List<ScheduleTask>();
            
            foreach (DataRow row in dataTable.Rows)
            {
                var task = new ScheduleTask
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString(),
                    Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,
                    OutputPath = row["OutputPath"].ToString(),
                    IsActive = Convert.ToInt32(row["IsActive"]) != 0,
                    CreatedTime = Convert.ToDateTime(row["CreatedTime"]),
                    CronExpression = row["CronExpression"] != DBNull.Value ? row["CronExpression"].ToString() : null,
                    BusinessStartDate = row["BusinessStartDate"] != DBNull.Value ? Convert.ToDateTime(row["BusinessStartDate"]) : (DateTime?)null,
                    BusinessEndDate = row["BusinessEndDate"] != DBNull.Value ? Convert.ToDateTime(row["BusinessEndDate"]) : (DateTime?)null,
                    LastExecutionDate = row["LastExecutionDate"] != DBNull.Value ? Convert.ToDateTime(row["LastExecutionDate"]) : (DateTime?)null
                };
                
                // 获取任务项数量
                string countSql = "SELECT COUNT(*) FROM ScheduleDetail WHERE ScheduleId = @ScheduleId;";
                task.ItemCount = SqliteHelper.ExecuteScalar<int>(countSql, new { ScheduleId = task.Id });
                
                result.Add(task);
            }
            
            return result;
        }
        
        /// <summary>
        /// 删除调度任务
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns>是否成功</returns>
        public bool DeleteScheduleTask(int id)
        {
            // 先删除明细
            DeleteScheduleTaskItems(id);
            
            // 再删除主表
            string sql = "DELETE FROM ScheduleConfig WHERE Id = @Id;";
            
            int rowsAffected = SqliteHelper.ExecuteNonQuery(sql, new { Id = id });
            return rowsAffected > 0;
        }
        
        /// <summary>
        /// 保存调度任务项
        /// </summary>
        /// <param name="item">调度任务项</param>
        /// <returns>项ID</returns>
        public int SaveScheduleTaskItem(ScheduleTaskItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            
            if (item.ScheduleId <= 0)
                throw new ArgumentException("调度任务ID无效", nameof(item.ScheduleId));
            
            if (item.TaskId <= 0)
                throw new ArgumentException("转换任务ID无效", nameof(item.TaskId));
            
            if (item.Id == 0)
            {
                // 新增
                string sql = @"
                    INSERT INTO ScheduleDetail 
                    (ScheduleId, TaskId, Sequence, IsActive)
                    VALUES 
                    (@ScheduleId, @TaskId, @Sequence, @IsActive);
                    SELECT last_insert_rowid();";
                
                var parameters = new
                {
                    item.ScheduleId,
                    item.TaskId,
                    item.Sequence,
                    IsActive = item.IsActive ? 1 : 0
                };
                
                return SqliteHelper.ExecuteScalar<int>(sql, parameters);
            }
            else
            {
                // 修改
                string sql = @"
                    UPDATE ScheduleDetail 
                    SET ScheduleId = @ScheduleId, 
                        TaskId = @TaskId, 
                        Sequence = @Sequence, 
                        IsActive = @IsActive
                    WHERE Id = @Id;";
                
                var parameters = new
                {
                    item.Id,
                    item.ScheduleId,
                    item.TaskId,
                    item.Sequence,
                    IsActive = item.IsActive ? 1 : 0
                };
                
                SqliteHelper.ExecuteNonQuery(sql, parameters);
                return item.Id;
            }
        }
        
        /// <summary>
        /// 获取调度任务项列表
        /// </summary>
        /// <param name="scheduleId">调度任务ID</param>
        /// <returns>调度任务项列表</returns>
        public List<ScheduleTaskItem> GetScheduleTaskItems(int scheduleId)
        {
            string sql = @"
                SELECT d.*, c.Name, c.Description, c.SqlScript, c.OutputFormat, c.IsActive AS TaskIsActive, c.CreatedTime
                FROM ScheduleDetail d
                INNER JOIN ConvertTask c ON d.TaskId = c.Id
                WHERE d.ScheduleId = @ScheduleId
                ORDER BY d.Sequence;";
            
            var dataTable = SqliteHelper.ExecuteQuery(sql, new { ScheduleId = scheduleId });
            var result = new List<ScheduleTaskItem>();
            
            foreach (DataRow row in dataTable.Rows)
            {
                var task = new ConvertTask
                {
                    Id = Convert.ToInt32(row["TaskId"]),
                    Name = row["Name"].ToString(),
                    Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,
                    SqlScript = row["SqlScript"].ToString(),
                    OutputFormat = (OutputFormat)Enum.Parse(typeof(OutputFormat), row["OutputFormat"].ToString()),
                    IsActive = Convert.ToBoolean(row["TaskIsActive"]),
                    CreatedTime = Convert.ToDateTime(row["CreatedTime"])
                };
                
                var item = new ScheduleTaskItem
                {
                    Id = Convert.ToInt32(row["Id"]),
                    ScheduleId = Convert.ToInt32(row["ScheduleId"]),
                    TaskId = Convert.ToInt32(row["TaskId"]),
                    Sequence = Convert.ToInt32(row["Sequence"]),
                    IsActive = Convert.ToBoolean(row["IsActive"]),
                    Task = task
                };
                
                result.Add(item);
            }
            
            return result;
        }
        
        /// <summary>
        /// 删除调度任务项
        /// </summary>
        /// <param name="scheduleId">调度任务ID</param>
        /// <returns>是否成功</returns>
        public bool DeleteScheduleTaskItems(int scheduleId)
        {
            string sql = "DELETE FROM ScheduleDetail WHERE ScheduleId = @ScheduleId;";
            
            int rowsAffected = SqliteHelper.ExecuteNonQuery(sql, new { ScheduleId = scheduleId });
            return rowsAffected >= 0;
        }
        
        #endregion
        
        #region 执行日志
        
        /// <summary>
        /// 保存执行日志
        /// </summary>
        /// <param name="log">执行日志</param>
        /// <returns>日志ID</returns>
        public int SaveExecutionLog(ExecutionLog log)
        {
            if (log == null)
                throw new ArgumentNullException(nameof(log));
            
            if (log.Id == 0)
            {
                // 新增
                string sql = @"
                    INSERT INTO ExecutionLog 
                    (ScheduleId, StartTime, EndTime, Status, ErrorMessage)
                    VALUES 
                    (@ScheduleId, @StartTime, @EndTime, @Status, @ErrorMessage);
                    SELECT last_insert_rowid();";
                
                var parameters = new
                {
                    log.ScheduleId,
                    StartTime = log.StartTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    EndTime = log.EndTime != default ? log.EndTime.ToString("yyyy-MM-dd HH:mm:ss") : (object)DBNull.Value,
                    log.Status,
                    log.ErrorMessage
                };
                
                return SqliteHelper.ExecuteScalar<int>(sql, parameters);
            }
            else
            {
                // 修改
                string sql = @"
                    UPDATE ExecutionLog 
                    SET EndTime = @EndTime, 
                        Status = @Status, 
                        ErrorMessage = @ErrorMessage
                    WHERE Id = @Id;";
                
                var parameters = new
                {
                    log.Id,
                    EndTime = log.EndTime != default ? log.EndTime.ToString("yyyy-MM-dd HH:mm:ss") : (object)DBNull.Value,
                    log.Status,
                    log.ErrorMessage
                };
                
                SqliteHelper.ExecuteNonQuery(sql, parameters);
                return log.Id;
            }
        }

        /// <summary>
        /// 获取执行日志列表
        /// </summary>
        /// <param name="scheduleId">调度任务ID</param>
        /// <returns>执行日志列表</returns>
        public List<ExecutionLog> GetExecutionLogs(int scheduleId)
        {
            string sql = "SELECT * FROM ExecutionLog WHERE ScheduleId = @ScheduleId ORDER BY StartTime DESC;";
            
            var dataTable = SqliteHelper.ExecuteQuery(sql, new { ScheduleId = scheduleId });
            var result = new List<ExecutionLog>();
            
            foreach (DataRow row in dataTable.Rows)
            {
                var log = new ExecutionLog
                {
                    Id = Convert.ToInt32(row["Id"]),
                    ScheduleId = Convert.ToInt32(row["ScheduleId"]),
                    StartTime = Convert.ToDateTime(row["StartTime"]),
                    Status = row["Status"].ToString(),
                    ErrorMessage = row["ErrorMessage"] != DBNull.Value ? row["ErrorMessage"].ToString() : null
                };
                
                if (row["EndTime"] != DBNull.Value)
                {
                    log.EndTime = Convert.ToDateTime(row["EndTime"]);
                }
                
                result.Add(log);
            }
            
            return result;
        }
        
        #endregion
        
        #region 数据库操作
        
        /// <summary>
        /// 获取所有表
        /// </summary>
        /// <returns>表名列表</returns>
        public List<string> GetAllTables()
        {
            string sql = "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%';";
            
            var dataTable = SqliteHelper.ExecuteQuery(sql);
            var result = new List<string>();
            
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(row["name"].ToString());
            }
            
            return result;
        }
        
        /// <summary>
        /// 获取表的列信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>列信息列表</returns>
        public List<ColumnInfo> GetTableColumns(string tableName)
        {
            string sql = $"PRAGMA table_info({tableName});";
            
            var dataTable = SqliteHelper.ExecuteQuery(sql);
            var result = new List<ColumnInfo>();
            
            foreach (DataRow row in dataTable.Rows)
            {
                var column = new ColumnInfo
                {
                    Name = row["name"].ToString(),
                    Type = row["type"].ToString(),
                    IsNullable = Convert.ToInt32(row["notnull"]) == 0,
                    IsPrimaryKey = Convert.ToInt32(row["pk"]) > 0,
                    DefaultValue = row["dflt_value"] != DBNull.Value ? row["dflt_value"].ToString() : null
                };
                
                result.Add(column);
            }
            
            return result;
        }
        
        /// <summary>
        /// 执行SQL查询
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询结果</returns>
        public DataTable ExecuteQuery(string sql, object parameters = null)
        {
            return SqliteHelper.ExecuteQuery(sql, parameters);
        }
        
        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <returns>备份文件路径</returns>
        public string BackupDatabase()
        {
            string backupFileName = $"ExcelData_{DateTime.Now:yyyyMMdd_HHmmss}.db";
            string backupFilePath = Path.Combine(_backupPath, backupFileName);
            
            // 确保目录存在
            if (!Directory.Exists(_backupPath))
            {
                Directory.CreateDirectory(_backupPath);
            }
            
            // 复制数据库文件
            File.Copy(_databasePath, backupFilePath);
            
            return backupFilePath;
        }
        
        /// <summary>
        /// 恢复数据库
        /// </summary>
        /// <param name="backupFilePath">备份文件路径</param>
        /// <returns>是否成功</returns>
        public bool RestoreDatabase(string backupFilePath)
        {
            if (!File.Exists(backupFilePath))
                throw new FileNotFoundException("备份文件不存在", backupFilePath);
            
            // 先备份当前数据库
            string currentBackup = BackupDatabase();
            
            try
            {
                // 关闭所有连接
                GC.Collect();
                GC.WaitForPendingFinalizers();
                
                // 复制备份文件
                File.Copy(backupFilePath, _databasePath, true);
                
                return true;
            }
            catch (Exception)
            {
                // 恢复失败，还原原来的数据库
                File.Copy(currentBackup, _databasePath, true);
                throw;
            }
        }
        
        /// <summary>
        /// 压缩数据库
        /// </summary>
        /// <returns>是否成功</returns>
        public bool VacuumDatabase()
        {
            return SqliteHelper.VacuumDatabase();
        }
        
        #endregion
    }
} 