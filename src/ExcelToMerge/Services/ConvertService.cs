using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using ExcelToMerge.Models;
using ExcelToMerge.Utils;

namespace ExcelToMerge.Services
{
    /// <summary>
    /// 数据转换服务类
    /// </summary>
    public class ConvertService
    {
        private readonly DatabaseService _databaseService;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public ConvertService()
        {
            _databaseService = new DatabaseService();
            
            // 确保SQL模板表存在
            EnsureSqlTemplateTableExists();
            
            // 初始化系统SQL模板
            InitializeSystemSqlTemplates();
        }
        
        /// <summary>
        /// 确保SQL模板表存在
        /// </summary>
        public void EnsureSqlTemplateTableExists()
        {
            try
            {
                if (!SqliteHelper.TableExists("SqlTemplates"))
                {
                    // 创建SQL模板表
                    string sql = @"
                    CREATE TABLE SqlTemplates (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Description TEXT,
                        SqlScript TEXT NOT NULL,
                        CreatedTime TEXT NOT NULL,
                        LastModifiedTime TEXT NOT NULL,
                        IsSystem INTEGER NOT NULL DEFAULT 0,
                        Category TEXT
                    );";
                    
                    SqliteHelper.ExecuteNonQuery(sql);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"创建SQL模板表失败: {ex.Message}", ex);
            }
        }
        
        /// <summary>
        /// 初始化系统SQL模板
        /// </summary>
        public void InitializeSystemSqlTemplates()
        {
            try
            {
                // 检查是否已经存在系统模板
                string checkSql = "SELECT COUNT(*) FROM SqlTemplates WHERE IsSystem = 1";
                int count = SqliteHelper.ExecuteScalar<int>(checkSql);
                
                if (count == 0)
                {
                    // 添加系统模板
                    List<SqlTemplate> systemTemplates = new List<SqlTemplate>
                    {
                        new SqlTemplate
                        {
                            Name = "表数据查询",
                            Description = "查询表的所有数据",
                            SqlScript = "SELECT * FROM {表名} LIMIT 100;",
                            CreatedTime = DateTime.Now,
                            LastModifiedTime = DateTime.Now,
                            IsSystem = true,
                            Category = "基础查询"
                        },
                        new SqlTemplate
                        {
                            Name = "表记录统计",
                            Description = "统计表的记录数",
                            SqlScript = "SELECT COUNT(*) AS 记录数 FROM {表名};",
                            CreatedTime = DateTime.Now,
                            LastModifiedTime = DateTime.Now,
                            IsSystem = true,
                            Category = "统计查询"
                        },
                        new SqlTemplate
                        {
                            Name = "表结构查询",
                            Description = "查询表的结构信息",
                            SqlScript = "PRAGMA table_info({表名});",
                            CreatedTime = DateTime.Now,
                            LastModifiedTime = DateTime.Now,
                            IsSystem = true,
                            Category = "元数据查询"
                        },
                        new SqlTemplate
                        {
                            Name = "数据去重查询",
                            Description = "查询表中的去重数据",
                            SqlScript = "SELECT DISTINCT {字段列表} FROM {表名} LIMIT 100;",
                            CreatedTime = DateTime.Now,
                            LastModifiedTime = DateTime.Now,
                            IsSystem = true,
                            Category = "数据处理"
                        },
                        new SqlTemplate
                        {
                            Name = "数据分组统计",
                            Description = "按字段分组统计数据",
                            SqlScript = "SELECT {分组字段}, COUNT(*) AS 数量 FROM {表名} GROUP BY {分组字段} ORDER BY 数量 DESC;",
                            CreatedTime = DateTime.Now,
                            LastModifiedTime = DateTime.Now,
                            IsSystem = true,
                            Category = "统计查询"
                        }
                    };
                    
                    // 保存系统模板
                    foreach (var template in systemTemplates)
                    {
                        SaveSqlTemplate(template);
                    }
                }
            }
            catch (Exception ex)
            {
                // 初始化系统模板失败，记录错误但不抛出异常
                System.Diagnostics.Debug.WriteLine($"初始化系统SQL模板失败: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 执行SQL查询
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <returns>查询结果</returns>
        public DataTable ExecuteQuery(string sql)
        {
            if (string.IsNullOrEmpty(sql))
                throw new ArgumentNullException(nameof(sql));
            
            return _databaseService.ExecuteQuery(sql);
        }
        
        /// <summary>
        /// 批量执行SQL查询
        /// </summary>
        /// <param name="sqlStatements">SQL查询语句集合</param>
        /// <returns>执行结果</returns>
        public List<BatchQueryResult> ExecuteBatchQuery(List<string> sqlStatements)
        {
            if (sqlStatements == null || sqlStatements.Count == 0)
                throw new ArgumentNullException(nameof(sqlStatements));
            
            List<BatchQueryResult> results = new List<BatchQueryResult>();
            
            foreach (string sql in sqlStatements)
            {
                BatchQueryResult result = new BatchQueryResult
                {
                    Sql = sql
                };
                
                try
                {
                    // 执行查询
                    result.Result = _databaseService.ExecuteQuery(sql);
                    result.Success = true;
                    result.RowCount = result.Result.Rows.Count;
                }
                catch (Exception ex)
                {
                    // 查询失败
                    result.Success = false;
                    result.ErrorMessage = ex.Message;
                }
                
                results.Add(result);
            }
            
            return results;
        }
        
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
            
            // 调用同步方法
            return _databaseService.SaveConvertTask(task);
        }
        
        /// <summary>
        /// 获取转换任务
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns>转换任务</returns>
        public ConvertTask GetConvertTask(int id)
        {
            // 调用同步方法
            return _databaseService.GetConvertTask(id);
        }
        
        /// <summary>
        /// 获取转换任务列表
        /// </summary>
        /// <param name="activeOnly">是否只获取激活的任务</param>
        /// <returns>转换任务列表</returns>
        public System.Collections.Generic.List<ConvertTask> GetConvertTaskList(bool activeOnly = false)
        {
            // 调用同步方法
            return _databaseService.GetConvertTaskList(activeOnly);
        }
        
        /// <summary>
        /// 删除转换任务
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns>是否删除成功</returns>
        public bool DeleteConvertTask(int id)
        {
            // 调用同步方法
            return _databaseService.DeleteConvertTask(id);
        }
        
        /// <summary>
        /// 保存SQL模板
        /// </summary>
        /// <param name="template">SQL模板</param>
        /// <returns>模板ID</returns>
        public int SaveSqlTemplate(SqlTemplate template)
        {
            if (template == null)
                throw new ArgumentNullException(nameof(template));
            
            if (string.IsNullOrEmpty(template.Name))
                throw new ArgumentException("模板名称不能为空", nameof(template.Name));
            
            if (string.IsNullOrEmpty(template.SqlScript))
                throw new ArgumentException("SQL脚本不能为空", nameof(template.SqlScript));
            
            try
            {
                // 设置时间
                if (template.Id == 0)
                {
                    template.CreatedTime = DateTime.Now;
                }
                template.LastModifiedTime = DateTime.Now;
                
                // 构建SQL语句
                string sql;
                object parameters;
                
                if (template.Id == 0)
                {
                    // 插入新记录
                    sql = @"
                    INSERT INTO SqlTemplates (Name, Description, SqlScript, CreatedTime, LastModifiedTime, IsSystem, Category)
                    VALUES (@Name, @Description, @SqlScript, @CreatedTime, @LastModifiedTime, @IsSystem, @Category);
                    SELECT last_insert_rowid();";
                    
                    parameters = new
                    {
                        Name = template.Name,
                        Description = template.Description,
                        SqlScript = template.SqlScript,
                        CreatedTime = template.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        LastModifiedTime = template.LastModifiedTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        IsSystem = template.IsSystem ? 1 : 0,
                        Category = template.Category
                    };
                    
                    // 执行插入并获取ID
                    int id = SqliteHelper.ExecuteScalar<int>(sql, parameters);
                    template.Id = id;
                    return id;
                }
                else
                {
                    // 更新记录
                    sql = @"
                    UPDATE SqlTemplates
                    SET Name = @Name,
                        Description = @Description,
                        SqlScript = @SqlScript,
                        LastModifiedTime = @LastModifiedTime,
                        IsSystem = @IsSystem,
                        Category = @Category
                    WHERE Id = @Id;";
                    
                    parameters = new
                    {
                        Id = template.Id,
                        Name = template.Name,
                        Description = template.Description,
                        SqlScript = template.SqlScript,
                        LastModifiedTime = template.LastModifiedTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        IsSystem = template.IsSystem ? 1 : 0,
                        Category = template.Category
                    };
                    
                    // 执行更新
                    SqliteHelper.ExecuteNonQuery(sql, parameters);
                    return template.Id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"保存SQL模板失败: {ex.Message}", ex);
            }
        }
        
        /// <summary>
        /// 获取SQL模板
        /// </summary>
        /// <param name="id">模板ID</param>
        /// <returns>SQL模板</returns>
        public SqlTemplate GetSqlTemplate(int id)
        {
            try
            {
                string sql = "SELECT * FROM SqlTemplates WHERE Id = @Id";
                var parameters = new { Id = id };
                
                var result = SqliteHelper.ExecuteQuery(sql, parameters);
                
                if (result.Rows.Count == 0)
                {
                    return null;
                }
                
                DataRow row = result.Rows[0];
                
                return new SqlTemplate
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString(),
                    Description = row["Description"].ToString(),
                    SqlScript = row["SqlScript"].ToString(),
                    CreatedTime = Convert.ToDateTime(row["CreatedTime"]),
                    LastModifiedTime = Convert.ToDateTime(row["LastModifiedTime"]),
                    IsSystem = Convert.ToInt32(row["IsSystem"]) == 1,
                    Category = row["Category"].ToString()
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"获取SQL模板失败: {ex.Message}", ex);
            }
        }
        
        /// <summary>
        /// 获取SQL模板列表
        /// </summary>
        /// <param name="includeSystem">是否包含系统模板</param>
        /// <param name="category">分类</param>
        /// <returns>SQL模板列表</returns>
        public List<SqlTemplate> GetSqlTemplateList(bool includeSystem = true, string category = null)
        {
            try
            {
                string sql = "SELECT * FROM SqlTemplates WHERE 1=1";
                
                if (!includeSystem)
                {
                    sql += " AND IsSystem = 0";
                }
                
                if (!string.IsNullOrEmpty(category))
                {
                    sql += " AND Category = @Category";
                }
                
                sql += " ORDER BY Category, Name";
                
                object parameters = null;
                if (!string.IsNullOrEmpty(category))
                {
                    parameters = new { Category = category };
                }
                
                var result = SqliteHelper.ExecuteQuery(sql, parameters);
                
                List<SqlTemplate> templates = new List<SqlTemplate>();
                
                foreach (DataRow row in result.Rows)
                {
                    templates.Add(new SqlTemplate
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Name = row["Name"].ToString(),
                        Description = row["Description"].ToString(),
                        SqlScript = row["SqlScript"].ToString(),
                        CreatedTime = Convert.ToDateTime(row["CreatedTime"]),
                        LastModifiedTime = Convert.ToDateTime(row["LastModifiedTime"]),
                        IsSystem = Convert.ToInt32(row["IsSystem"]) == 1,
                        Category = row["Category"].ToString()
                    });
                }
                
                return templates;
            }
            catch (Exception ex)
            {
                throw new Exception($"获取SQL模板列表失败: {ex.Message}", ex);
            }
        }
        
        /// <summary>
        /// 删除SQL模板
        /// </summary>
        /// <param name="id">模板ID</param>
        /// <returns>是否删除成功</returns>
        public bool DeleteSqlTemplate(int id)
        {
            try
            {
                // 检查是否为系统模板
                string checkSql = "SELECT IsSystem FROM SqlTemplates WHERE Id = @Id";
                var checkResult = SqliteHelper.ExecuteQuery(checkSql, new { Id = id });
                
                if (checkResult.Rows.Count > 0 && Convert.ToInt32(checkResult.Rows[0]["IsSystem"]) == 1)
                {
                    throw new Exception("系统模板不能删除");
                }
                
                string sql = "DELETE FROM SqlTemplates WHERE Id = @Id";
                int rowsAffected = SqliteHelper.ExecuteNonQuery(sql, new { Id = id });
                
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"删除SQL模板失败: {ex.Message}", ex);
            }
        }
        
        /// <summary>
        /// 执行转换任务并导出数据
        /// </summary>
        /// <param name="task">转换任务</param>
        /// <param name="outputPath">输出路径</param>
        /// <param name="progress">进度回调</param>
        /// <returns>转换结果</returns>
        public ConvertResult ExecuteConvertTask(ConvertTask task, string outputPath, IProgress<ProgressInfo> progress = null)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));
            
            if (string.IsNullOrEmpty(task.SqlScript))
                throw new ArgumentException("SQL脚本不能为空", nameof(task.SqlScript));
            
            if (string.IsNullOrEmpty(outputPath))
                throw new ArgumentException("输出路径不能为空", nameof(outputPath));
            
            // 创建进度报告器
            using (var progressReporter = progress != null ? new ProgressReporter(progress, 3) : null)
            {
                try
                {
                    // 更新进度：执行SQL查询
                    progressReporter?.SetStatus("正在执行SQL查询...");
                    
                    // 执行SQL查询
                    DataTable result = _databaseService.ExecuteQuery(task.SqlScript);
                    
                    // 更新进度
                    progressReporter?.Increment();
                    progressReporter?.SetStatus("查询完成，正在准备导出数据...");
                    
                    // 确保输出目录存在
                    string directory = Path.GetDirectoryName(outputPath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    
                    // 根据输出格式导出数据
                    bool exportSuccess;
                    if (task.OutputFormat == OutputFormat.Excel)
                    {
                        // 确保文件扩展名正确
                        if (!outputPath.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                        {
                            outputPath += ".xlsx";
                        }
                        
                        progressReporter?.SetStatus("正在导出为Excel文件...");
                        exportSuccess = ExcelHelper.ExportToExcel(result, outputPath, task.Name);
                    }
                    else // CSV
                    {
                        // 确保文件扩展名正确
                        if (!outputPath.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                        {
                            outputPath += ".csv";
                        }
                        
                        progressReporter?.SetStatus("正在导出为CSV文件...");
                        exportSuccess = ExcelHelper.ExportToCsv(result, outputPath);
                    }
                    
                    // 更新进度
                    progressReporter?.Increment();
                    
                    if (!exportSuccess)
                    {
                        throw new Exception("导出数据失败");
                    }
                    
                    // 完成进度报告
                    progressReporter?.Complete();
                    
                    return new ConvertResult
                    {
                        Success = true,
                        RowCount = result.Rows.Count,
                        OutputPath = outputPath
                    };
                }
                catch (Exception ex)
                {
                    progressReporter?.SetError(ex);
                    
                    return new ConvertResult
                    {
                        Success = false,
                        ErrorMessage = ex.Message
                    };
                }
            }
        }
    }
    
    /// <summary>
    /// 转换结果类
    /// </summary>
    public class ConvertResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// 导出的行数
        /// </summary>
        public int RowCount { get; set; }
        
        /// <summary>
        /// 输出文件路径
        /// </summary>
        public string OutputPath { get; set; }
    }
    
    /// <summary>
    /// 批量查询结果
    /// </summary>
    public class BatchQueryResult
    {
        /// <summary>
        /// SQL语句
        /// </summary>
        public string Sql { get; set; }
        
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// 查询结果
        /// </summary>
        public DataTable Result { get; set; }
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// 行数
        /// </summary>
        public int RowCount { get; set; }
    }
} 