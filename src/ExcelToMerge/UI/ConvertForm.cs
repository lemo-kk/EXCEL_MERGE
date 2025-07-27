using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ExcelToMerge.Models;
using ExcelToMerge.Services;
using ExcelToMerge.Utils;

namespace ExcelToMerge.UI
{
    /// <summary>
    /// 转换窗体
    /// </summary>
    public partial class ConvertForm : Form
    {
        private readonly ConvertService _convertService;
        private readonly DatabaseService _databaseService;
        private ConvertTask _currentTask;
        private DataTable _currentResult;
        private bool _isHighlighting = false; // 防止递归高亮
        private List<ConvertTask> _batchTasks = new List<ConvertTask>(); // 批量执行任务列表
        
        // SQL关键字列表
        private static readonly string[] SqlKeywords = new string[]
        {
            "SELECT", "FROM", "WHERE", "GROUP", "BY", "HAVING", "ORDER",
            "INNER", "LEFT", "RIGHT", "OUTER", "JOIN", "ON", "AS",
            "INSERT", "INTO", "VALUES", "UPDATE", "SET", "DELETE",
            "CREATE", "ALTER", "DROP", "TABLE", "INDEX", "VIEW",
            "AND", "OR", "NOT", "IN", "LIKE", "BETWEEN", "IS", "NULL",
            "COUNT", "SUM", "AVG", "MIN", "MAX", "DISTINCT",
            "UNION", "ALL", "CASE", "WHEN", "THEN", "ELSE", "END"
        };
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public ConvertForm()
        {
            InitializeComponent();
            _convertService = new ConvertService();
            _databaseService = new DatabaseService();
            _currentTask = new ConvertTask();
        }
        
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void ConvertForm_Load(object sender, EventArgs e)
        {
            // 初始化控件状态
            progressBarExecution.Visible = false;
            
            // 设置输出格式选项
            radioButtonExcel.Checked = true;
            
            // 加载数据库表结构
            LoadDatabaseSchema();
            
            // 加载转换任务列表
            LoadConvertTasks();
            
            // 设置默认输出路径
            string defaultExportPath = System.Configuration.ConfigurationManager.AppSettings["DefaultExportPath"];
            if (!string.IsNullOrEmpty(defaultExportPath))
            {
                textBoxOutputPath.Text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, defaultExportPath);
            }
            
            // 设置树视图节点双击事件
            treeViewDatabase.NodeMouseDoubleClick += treeViewDatabase_NodeMouseDoubleClick;
            
            // 设置SQL文本框的TextChanged事件
            richTextBoxSql.TextChanged += richTextBoxSql_TextChanged;
            
            // 设置按钮点击事件
            btnExecute.Click += btnExecute_Click;
            btnSave.Click += btnSave_Click;
            btnExport.Click += btnExport_Click;
            btnBrowse.Click += btnBrowse_Click;
            btnNew.Click += btnNew_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnSqlTemplate.Click += btnSqlTemplate_Click;
            btnBatchExecute.Click += btnBatchExecute_Click;
        }
        
        /// <summary>
        /// SQL文本框内容改变事件
        /// </summary>
        private void richTextBoxSql_TextChanged(object sender, EventArgs e)
        {
            // 防止递归高亮
            if (_isHighlighting)
                return;
            
            _isHighlighting = true;
            
            try
            {
                // 保存当前选择位置
                int selectionStart = richTextBoxSql.SelectionStart;
                int selectionLength = richTextBoxSql.SelectionLength;
                
                // 高亮SQL关键字
                HighlightSqlKeywords();
                
                // 恢复选择位置
                richTextBoxSql.SelectionStart = selectionStart;
                richTextBoxSql.SelectionLength = selectionLength;
            }
            finally
            {
                _isHighlighting = false;
            }
        }
        
        /// <summary>
        /// 高亮SQL关键字
        /// </summary>
        private void HighlightSqlKeywords()
        {
            // 保存当前文本
            string text = richTextBoxSql.Text;
            
            // 重置文本格式
            richTextBoxSql.SelectAll();
            richTextBoxSql.SelectionColor = Color.Black;
            
            // 高亮关键字
            foreach (string keyword in SqlKeywords)
            {
                // 创建正则表达式，匹配整个单词
                string pattern = $@"\b{keyword}\b";
                
                // 查找所有匹配项
                foreach (Match match in Regex.Matches(text, pattern, RegexOptions.IgnoreCase))
                {
                    // 设置高亮
                    richTextBoxSql.Select(match.Index, match.Length);
                    richTextBoxSql.SelectionColor = Color.Blue;
                    richTextBoxSql.SelectionFont = new Font(richTextBoxSql.Font, FontStyle.Bold);
                }
            }
            
            // 高亮字符串
            foreach (Match match in Regex.Matches(text, @"'[^']*'"))
            {
                richTextBoxSql.Select(match.Index, match.Length);
                richTextBoxSql.SelectionColor = Color.Red;
            }
            
            // 高亮注释
            foreach (Match match in Regex.Matches(text, @"--.*$", RegexOptions.Multiline))
            {
                richTextBoxSql.Select(match.Index, match.Length);
                richTextBoxSql.SelectionColor = Color.Green;
            }
            
            // 取消选择
            richTextBoxSql.SelectionStart = 0;
            richTextBoxSql.SelectionLength = 0;
        }
        
        /// <summary>
        /// 加载数据库表结构
        /// </summary>
        private void LoadDatabaseSchema()
        {
            try
            {
                // 清空树视图
                treeViewDatabase.Nodes.Clear();
                
                // 添加数据库根节点
                TreeNode rootNode = new TreeNode("SQLite数据库");
                // 移除ImageIndex设置
                // rootNode.ImageIndex = 0; // 数据库图标
                // rootNode.SelectedImageIndex = 0;
                treeViewDatabase.Nodes.Add(rootNode);
                
                // 获取所有表
                var tables = _databaseService.GetAllTables();
                
                // 添加表节点
                foreach (string tableName in tables)
                {
                    TreeNode tableNode = new TreeNode(tableName);
                    tableNode.Tag = tableName;
                    // 移除ImageIndex设置
                    // tableNode.ImageIndex = 1; // 表图标
                    // tableNode.SelectedImageIndex = 1;
                    
                    // 获取表的列信息
                    var columns = _databaseService.GetTableColumns(tableName);
                    
                    // 添加列节点
                    foreach (var column in columns)
                    {
                        string columnText = $"{column.Name} ({column.Type})";
                        if (column.IsPrimaryKey)
                            columnText = $"{columnText} [PK]";
                        
                        TreeNode columnNode = new TreeNode(columnText);
                        columnNode.Tag = column;
                        // 移除ImageIndex设置
                        // columnNode.ImageIndex = column.IsPrimaryKey ? 3 : 2; // 主键或普通列图标
                        // columnNode.SelectedImageIndex = columnNode.ImageIndex;
                        tableNode.Nodes.Add(columnNode);
                    }
                    
                    rootNode.Nodes.Add(tableNode);
                }
                
                // 展开根节点
                rootNode.Expand();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载数据库结构失败: {ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 加载转换任务列表
        /// </summary>
        private void LoadConvertTasks()
        {
            try
            {
                // 清空列表视图
                listViewTasks.Items.Clear();
                
                // 获取所有转换任务
                var tasks = _convertService.GetConvertTaskList();
                
                // 添加任务项
                foreach (var task in tasks)
                {
                    ListViewItem item = new ListViewItem(task.Id.ToString());
                    item.SubItems.Add(task.Name);
                    item.SubItems.Add(task.Description);
                    item.SubItems.Add(task.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    item.Tag = task;
                    
                    listViewTasks.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载转换任务失败: {ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 树视图节点双击事件
        /// </summary>
        private void treeViewDatabase_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // 如果双击的是表节点，则生成SELECT语句
            if (e.Node.Tag is string tableName)
            {
                string sql = $"SELECT * FROM {tableName} LIMIT 100;";
                richTextBoxSql.Text = sql;
            }
            // 如果双击的是列节点，则将列名插入到SQL编辑框的当前位置
            else if (e.Node.Tag != null && e.Node.Parent != null && e.Node.Parent.Tag is string)
            {
                string columnName = e.Node.Text.Split(' ')[0]; // 获取列名（去掉类型信息）
                richTextBoxSql.SelectedText = columnName;
            }
        }
        
        /// <summary>
        /// 表节点右键点击事件
        /// </summary>
        private void treeViewDatabase_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // 如果是右键点击表节点，显示上下文菜单
            if (e.Button == MouseButtons.Right && e.Node.Tag is string tableName)
            {
                treeViewDatabase.SelectedNode = e.Node;
                
                // 创建上下文菜单
                ContextMenuStrip contextMenu = new ContextMenuStrip();
                
                // 添加"预览数据"菜单项
                ToolStripMenuItem previewItem = new ToolStripMenuItem("预览数据");
                previewItem.Click += (s, args) => PreviewTableData(tableName);
                contextMenu.Items.Add(previewItem);
                
                // 添加"生成SELECT语句"菜单项
                ToolStripMenuItem selectItem = new ToolStripMenuItem("生成SELECT语句");
                selectItem.Click += (s, args) => GenerateSelectStatement(tableName);
                contextMenu.Items.Add(selectItem);
                
                // 添加"生成COUNT语句"菜单项
                ToolStripMenuItem countItem = new ToolStripMenuItem("生成COUNT语句");
                countItem.Click += (s, args) => GenerateCountStatement(tableName);
                contextMenu.Items.Add(countItem);
                
                // 显示上下文菜单
                contextMenu.Show(treeViewDatabase, e.Location);
            }
        }
        
        /// <summary>
        /// 预览表数据
        /// </summary>
        private void PreviewTableData(string tableName)
        {
            try
            {
                // 执行查询
                string sql = $"SELECT * FROM {tableName} LIMIT 100;";
                DataTable result = _convertService.ExecuteQuery(sql);
                
                // 显示结果
                dataGridViewResult.DataSource = result;
                
                // 更新状态
                labelStatus.Text = $"预览表 {tableName}，显示前100行数据";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"预览表数据失败: {ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 生成SELECT语句
        /// </summary>
        private void GenerateSelectStatement(string tableName)
        {
            string sql = $"SELECT * FROM {tableName} LIMIT 100;";
            richTextBoxSql.Text = sql;
        }
        
        /// <summary>
        /// 生成COUNT语句
        /// </summary>
        private void GenerateCountStatement(string tableName)
        {
            string sql = $"SELECT COUNT(*) FROM {tableName};";
            richTextBoxSql.Text = sql;
        }
        
        /// <summary>
        /// 新建按钮点击事件
        /// </summary>
        private void btnNew_Click(object sender, EventArgs e)
        {
            // 清空当前任务
            _currentTask = new ConvertTask();
            textBoxTaskName.Text = string.Empty;
            textBoxTaskDescription.Text = string.Empty;
            richTextBoxSql.Text = string.Empty;
            
            // 清空结果
            dataGridViewResult.DataSource = null;
            _currentResult = null;
        }
        
        /// <summary>
        /// 编辑按钮点击事件
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (listViewTasks.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择一个转换任务", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            try
            {
                // 获取选中的任务
                var item = listViewTasks.SelectedItems[0];
                var task = item.Tag as ConvertTask;
                
                if (task != null)
                {
                    // 加载任务信息
                    _currentTask = task;
                    textBoxTaskName.Text = task.Name;
                    textBoxTaskDescription.Text = task.Description;
                    richTextBoxSql.Text = task.SqlScript;
                    
                    // 设置输出格式
                    if (task.OutputFormat == OutputFormat.Excel)
                    {
                        radioButtonExcel.Checked = true;
                    }
                    else
                    {
                        radioButtonCsv.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载任务失败: {ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 删除按钮点击事件
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listViewTasks.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择一个转换任务", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            try
            {
                // 获取选中的任务
                var item = listViewTasks.SelectedItems[0];
                var task = item.Tag as ConvertTask;
                
                if (task != null)
                {
                    // 确认删除
                    var result = MessageBox.Show($"确定要删除任务 \"{task.Name}\" 吗？", "确认删除", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        // 删除任务
                        bool deleted = _convertService.DeleteConvertTask(task.Id);
                        
                        if (deleted)
                        {
                            // 重新加载任务列表
                            LoadConvertTasks();
                            
                            // 清空当前任务
                            if (_currentTask.Id == task.Id)
                            {
                                btnNew_Click(sender, e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("删除任务失败", "错误", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"删除任务失败: {ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 列表选择改变事件
        /// </summary>
        private void listViewTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewTasks.SelectedItems.Count > 0)
            {
                btnEdit_Click(sender, e);
            }
        }
        
        /// <summary>
        /// 执行查询按钮点击事件
        /// </summary>
        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(richTextBoxSql.Text))
            {
                MessageBox.Show("请输入SQL查询语句", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            try
            {
                // 显示进度条
                progressBarExecution.Visible = true;
                progressBarExecution.Value = 0;
                labelStatus.Text = "正在执行查询...";
                
                // 禁用控件
                DisableControls();
                
                // 执行查询
                _currentResult = _convertService.ExecuteQuery(richTextBoxSql.Text);
                
                // 显示结果
                dataGridViewResult.DataSource = _currentResult;
                
                // 更新进度
                progressBarExecution.Value = 100;
                labelStatus.Text = $"查询完成，返回 {_currentResult.Rows.Count} 行数据";
                
                // 启用控件
                EnableControls();
                
                // 隐藏进度条
                progressBarExecution.Visible = false;
            }
            catch (Exception ex)
            {
                // 启用控件
                EnableControls();
                
                // 隐藏进度条
                progressBarExecution.Visible = false;
                labelStatus.Text = "查询失败";
                
                MessageBox.Show($"执行查询失败: {ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 保存任务按钮点击事件
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxTaskName.Text))
            {
                MessageBox.Show("请输入任务名称", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            if (string.IsNullOrEmpty(richTextBoxSql.Text))
            {
                MessageBox.Show("请输入SQL查询语句", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            try
            {
                // 更新任务信息
                _currentTask.Name = textBoxTaskName.Text;
                _currentTask.Description = textBoxTaskDescription.Text;
                _currentTask.SqlScript = richTextBoxSql.Text;
                _currentTask.OutputFormat = radioButtonExcel.Checked ? OutputFormat.Excel : OutputFormat.CSV;
                
                if (_currentTask.Id == 0)
                {
                    // 新任务，设置创建时间
                    _currentTask.CreatedTime = DateTime.Now;
                }
                
                // 保存任务
                int taskId = _convertService.SaveConvertTask(_currentTask);
                
                if (taskId > 0)
                {
                    // 更新任务ID
                    _currentTask.Id = taskId;
                    
                    // 重新加载任务列表
                    LoadConvertTasks();
                    
                    MessageBox.Show("保存任务成功", "成功", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("保存任务失败", "错误", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存任务失败: {ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 浏览按钮点击事件
        /// </summary>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            // 创建文件夹浏览对话框
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "选择输出目录";
                
                if (!string.IsNullOrEmpty(textBoxOutputPath.Text) && Directory.Exists(textBoxOutputPath.Text))
                {
                    folderDialog.SelectedPath = textBoxOutputPath.Text;
                }
                
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxOutputPath.Text = folderDialog.SelectedPath;
                }
            }
        }
        
        /// <summary>
        /// 导出数据按钮点击事件
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (_currentResult == null || _currentResult.Rows.Count == 0)
            {
                MessageBox.Show("没有可导出的数据，请先执行查询", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            if (string.IsNullOrEmpty(textBoxOutputPath.Text))
            {
                MessageBox.Show("请选择输出路径", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            try
            {
                // 显示进度条
                progressBarExecution.Visible = true;
                progressBarExecution.Value = 0;
                labelStatus.Text = "正在导出数据...";
                
                // 禁用控件
                DisableControls();
                
                // 确保输出目录存在
                string outputDir = textBoxOutputPath.Text;
                if (!Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }
                
                // 构建输出文件名
                string fileName = string.IsNullOrEmpty(textBoxTaskName.Text) ? 
                    $"导出数据_{DateTime.Now:yyyyMMdd_HHmmss}" : textBoxTaskName.Text;
                
                string outputPath;
                bool exportSuccess;
                
                // 根据选择的格式导出数据
                if (radioButtonExcel.Checked)
                {
                    outputPath = Path.Combine(outputDir, $"{fileName}.xlsx");
                    exportSuccess = ExcelHelper.ExportToExcel(_currentResult, outputPath, fileName);
                }
                else
                {
                    outputPath = Path.Combine(outputDir, $"{fileName}.csv");
                    exportSuccess = ExcelHelper.ExportToCsv(_currentResult, outputPath);
                }
                
                // 更新进度
                progressBarExecution.Value = 100;
                
                // 启用控件
                EnableControls();
                
                // 隐藏进度条
                progressBarExecution.Visible = false;
                
                if (exportSuccess)
                {
                    labelStatus.Text = "导出完成";
                    
                    var result = MessageBox.Show($"数据已成功导出到：\n{outputPath}\n\n是否打开输出目录？", "导出成功", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    
                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{outputPath}\"");
                    }
                }
                else
                {
                    labelStatus.Text = "导出失败";
                    MessageBox.Show("导出数据失败", "错误", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // 启用控件
                EnableControls();
                
                // 隐藏进度条
                progressBarExecution.Visible = false;
                labelStatus.Text = "导出失败";
                
                MessageBox.Show($"导出数据失败: {ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// SQL模板按钮点击事件
        /// </summary>
        private void btnSqlTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                // 创建SQL模板窗体
                SqlTemplateForm templateForm = new SqlTemplateForm(_convertService);
                
                // 显示为对话框
                if (templateForm.ShowDialog() == DialogResult.OK)
                {
                    // 如果用户选择了一个模板，将其加载到SQL编辑框
                    if (templateForm.SelectedTemplate != null)
                    {
                        richTextBoxSql.Text = templateForm.SelectedTemplate.SqlScript;
                        
                        // 如果模板名称不为空，则更新任务名称和描述
                        if (!string.IsNullOrEmpty(templateForm.SelectedTemplate.Name))
                        {
                            textBoxTaskName.Text = templateForm.SelectedTemplate.Name;
                            textBoxTaskDescription.Text = templateForm.SelectedTemplate.Description;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打开SQL模板管理器失败: {ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 批量执行按钮点击事件
        /// </summary>
        private void btnBatchExecute_Click(object sender, EventArgs e)
        {
            try
            {
                // 获取所有转换任务
                var allTasks = _convertService.GetConvertTaskList();
                
                if (allTasks.Count == 0)
                {
                    MessageBox.Show("没有可用的转换任务，请先创建任务", "提示", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                // 创建批量执行任务选择窗体
                using (BatchExecutionForm batchForm = new BatchExecutionForm(allTasks))
                {
                    if (batchForm.ShowDialog() == DialogResult.OK)
                    {
                        // 获取用户选择的任务
                        _batchTasks = batchForm.SelectedTasks;
                        
                        if (_batchTasks.Count == 0)
                        {
                            MessageBox.Show("未选择任何任务", "提示", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        
                        // 确认批量执行
                        var result = MessageBox.Show($"确定要执行选定的 {_batchTasks.Count} 个任务吗？", "确认执行", 
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        
                        if (result == DialogResult.Yes)
                        {
                            // 执行批量任务
                            ExecuteBatchTasks();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"批量执行失败: {ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 执行批量任务
        /// </summary>
        private void ExecuteBatchTasks()
        {
            try
            {
                // 显示进度条
                progressBarExecution.Visible = true;
                progressBarExecution.Value = 0;
                labelStatus.Text = $"正在执行批量任务 (0/{_batchTasks.Count})...";
                
                // 禁用控件
                DisableControls();
                
                // 确保输出目录存在
                string outputDir = textBoxOutputPath.Text;
                if (string.IsNullOrEmpty(outputDir))
                {
                    outputDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Export");
                }
                
                if (!Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }
                
                // 创建批量执行结果目录
                string batchDir = Path.Combine(outputDir, $"批量执行_{DateTime.Now:yyyyMMdd_HHmmss}");
                Directory.CreateDirectory(batchDir);
                
                // 执行每个任务
                int successCount = 0;
                int failCount = 0;
                List<string> errorMessages = new List<string>();
                
                for (int i = 0; i < _batchTasks.Count; i++)
                {
                    var task = _batchTasks[i];
                    
                    try
                    {
                        // 更新进度
                        int percentage = (i + 1) * 100 / _batchTasks.Count;
                        progressBarExecution.Value = percentage;
                        labelStatus.Text = $"正在执行任务 {i + 1}/{_batchTasks.Count}: {task.Name}";
                        Application.DoEvents();
                        
                        // 执行SQL查询
                        DataTable result = _convertService.ExecuteQuery(task.SqlScript);
                        
                        // 导出结果
                        string fileName = string.IsNullOrEmpty(task.Name) ? 
                            $"Task_{task.Id}" : task.Name;
                        
                        string outputPath;
                        bool exportSuccess;
                        
                        // 根据任务设置的格式导出数据
                        if (task.OutputFormat == OutputFormat.Excel)
                        {
                            outputPath = Path.Combine(batchDir, $"{fileName}.xlsx");
                            exportSuccess = ExcelHelper.ExportToExcel(result, outputPath, fileName);
                        }
                        else
                        {
                            outputPath = Path.Combine(batchDir, $"{fileName}.csv");
                            exportSuccess = ExcelHelper.ExportToCsv(result, outputPath);
                        }
                        
                        if (exportSuccess)
                        {
                            successCount++;
                        }
                        else
                        {
                            failCount++;
                            errorMessages.Add($"任务 {task.Name} 导出失败");
                        }
                    }
                    catch (Exception ex)
                    {
                        failCount++;
                        errorMessages.Add($"任务 {task.Name} 执行失败: {ex.Message}");
                    }
                }
                
                // 更新进度
                progressBarExecution.Value = 100;
                labelStatus.Text = $"批量执行完成，成功: {successCount}，失败: {failCount}";
                
                // 启用控件
                EnableControls();
                
                // 显示执行结果
                string resultMessage = $"批量执行完成\n\n" +
                    $"总任务数: {_batchTasks.Count}\n" +
                    $"成功: {successCount}\n" +
                    $"失败: {failCount}\n\n";
                
                if (failCount > 0)
                {
                    resultMessage += "失败任务:\n" + string.Join("\n", errorMessages);
                }
                
                resultMessage += $"\n\n数据已导出到目录:\n{batchDir}\n\n是否打开输出目录？";
                
                var dialogResult = MessageBox.Show(resultMessage, "批量执行结果", 
                    MessageBoxButtons.YesNo, failCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
                
                if (dialogResult == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("explorer.exe", batchDir);
                }
            }
            catch (Exception ex)
            {
                // 启用控件
                EnableControls();
                
                // 隐藏进度条
                progressBarExecution.Visible = false;
                labelStatus.Text = "批量执行失败";
                
                MessageBox.Show($"批量执行失败: {ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 禁用控件
        /// </summary>
        private void DisableControls()
        {
            textBoxTaskName.Enabled = false;
            textBoxTaskDescription.Enabled = false;
            richTextBoxSql.Enabled = false;
            textBoxOutputPath.Enabled = false;
            radioButtonExcel.Enabled = false;
            radioButtonCsv.Enabled = false;
            btnBrowse.Enabled = false;
            btnExecute.Enabled = false;
            btnSave.Enabled = false;
            btnExport.Enabled = false;
            btnNew.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnSqlTemplate.Enabled = false;
            btnBatchExecute.Enabled = false;
            listViewTasks.Enabled = false;
            treeViewDatabase.Enabled = false;
        }
        
        /// <summary>
        /// 启用控件
        /// </summary>
        private void EnableControls()
        {
            textBoxTaskName.Enabled = true;
            textBoxTaskDescription.Enabled = true;
            richTextBoxSql.Enabled = true;
            textBoxOutputPath.Enabled = true;
            radioButtonExcel.Enabled = true;
            radioButtonCsv.Enabled = true;
            btnBrowse.Enabled = true;
            btnExecute.Enabled = true;
            btnSave.Enabled = true;
            btnExport.Enabled = true;
            btnNew.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            btnSqlTemplate.Enabled = true;
            btnBatchExecute.Enabled = true;
            listViewTasks.Enabled = true;
            treeViewDatabase.Enabled = true;
        }
    }
} 