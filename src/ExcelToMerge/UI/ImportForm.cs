using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelToMerge.Models;
using ExcelToMerge.Services;
using ExcelToMerge.Utils;

namespace ExcelToMerge.UI
{
    /// <summary>
    /// 导入窗体
    /// </summary>
    public partial class ImportForm : Form
    {
        private readonly ImportService _importService;
        private ImportTask _currentTask;
        private List<string> _sheetNames;
        private List<string> _selectedFiles;
        private IProgress<ProgressInfo> _progress;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public ImportForm()
        {
            InitializeComponent();
            _importService = new ImportService();
            _sheetNames = new List<string>();
            _selectedFiles = new List<string>();
        }
        
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void ImportForm_Load(object sender, EventArgs e)
        {
            // 初始化控件状态
            metroRadioExcel.Checked = true;
            metroCheckHeader.Checked = true;
            metroCheckAllSheets.Checked = false;
            metroListSheets.Enabled = false;
            metroBtnImport.Enabled = false;
            
            // 隐藏进度条
            metroProgressBar.Visible = false;
            lblProgress.Visible = false;
        }
        
        /// <summary>
        /// 浏览按钮点击事件
        /// </summary>
        private void metroBtnBrowse_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                // 设置文件对话框属性
                if (metroRadioExcel.Checked)
                {
                    openFileDialog.Filter = "Excel文件|*.xlsx;*.xls|所有文件|*.*";
                }
                else
                {
                    openFileDialog.Filter = "CSV文件|*.csv|所有文件|*.*";
                }
                
                openFileDialog.Multiselect = true;
                openFileDialog.Title = "选择要导入的文件";
                
                // 显示文件对话框
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _selectedFiles = openFileDialog.FileNames.ToList();
                    
                    // 显示选中的文件数量
                    if (_selectedFiles.Count == 1)
                    {
                        metroTextFilePath.Text = _selectedFiles[0];
                    }
                    else
                    {
                        metroTextFilePath.Text = $"已选择 {_selectedFiles.Count} 个文件";
                    }
                    
                    // 如果是Excel文件，加载工作表列表
                    if (metroRadioExcel.Checked && _selectedFiles.Count == 1)
                    {
                    LoadSheets();
                    }
                    else
                    {
                        // 清空工作表列表
                        metroListSheets.Items.Clear();
                        metroListSheets.Enabled = false;
                        
                        // 启用导入按钮
                        metroBtnImport.Enabled = true;
                    }
                }
            }
        }
        
        /// <summary>
        /// 文件类型单选按钮改变事件
        /// </summary>
        private void metroRadioFileType_CheckedChanged(object sender, EventArgs e)
        {
            // 清空文件路径和工作表列表
            metroTextFilePath.Text = string.Empty;
            metroListSheets.Items.Clear();
            _selectedFiles.Clear();
            
            // 禁用导入按钮
            metroBtnImport.Enabled = false;
            
            // 如果选择了CSV，禁用工作表相关控件
            if (metroRadioCsv.Checked)
            {
                metroCheckAllSheets.Enabled = false;
                metroListSheets.Enabled = false;
            }
            else
            {
                metroCheckAllSheets.Enabled = true;
                metroListSheets.Enabled = !metroCheckAllSheets.Checked;
            }
        }
        
        /// <summary>
        /// 全部工作表复选框改变事件
        /// </summary>
        private void metroCheckAllSheets_CheckedChanged(object sender, EventArgs e)
        {
            // 启用或禁用工作表列表
            metroListSheets.Enabled = !metroCheckAllSheets.Checked;
            
            // 如果选择了全部工作表，全选列表中的项
            if (metroCheckAllSheets.Checked)
            {
                for (int i = 0; i < metroListSheets.Items.Count; i++)
                {
                    metroListSheets.SetItemChecked(i, true);
                }
            }
        }
        
        /// <summary>
        /// 加载工作表列表
        /// </summary>
        private void LoadSheets()
        {
            try
            {
                // 清空列表
                metroListSheets.Items.Clear();
                _sheetNames = new List<string>();
                
                // 检查文件是否存在
                if (_selectedFiles.Count == 0 || !File.Exists(_selectedFiles[0]))
                {
                    MessageBox.Show("文件不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // 根据文件类型加载工作表列表
                if (metroRadioExcel.Checked)
                {
                    // 获取Excel工作表列表
                    _sheetNames = _importService.GetExcelSheets(_selectedFiles[0]);
                    
                    // 添加到列表控件
                    foreach (var sheet in _sheetNames)
                    {
                        metroListSheets.Items.Add(sheet, true);
                    }
                    
                    // 启用工作表列表和导入按钮
                    metroListSheets.Enabled = !metroCheckAllSheets.Checked;
                    metroBtnImport.Enabled = true;
                }
                else // CSV
                {
                    // CSV文件只有一个工作表，使用文件名作为工作表名
                    string sheetName = Path.GetFileNameWithoutExtension(_selectedFiles[0]);
                    _sheetNames.Add(sheetName);
                    
                    // 添加到列表控件
                    metroListSheets.Items.Add(sheetName, true);
                    
                    // 禁用工作表列表，启用导入按钮
                    metroListSheets.Enabled = false;
                    metroBtnImport.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载工作表列表失败: {ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 导入按钮点击事件
        /// </summary>
        private async void metroBtnImport_Click(object sender, EventArgs e)
        {
            // 检查是否选择了文件
            if (_selectedFiles.Count == 0)
            {
                MessageBox.Show("请选择要导入的文件", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            // 如果是Excel文件，检查是否选择了工作表
            if (metroRadioExcel.Checked && _selectedFiles.Count == 1 && !metroCheckAllSheets.Checked && metroListSheets.CheckedItems.Count == 0)
            {
                MessageBox.Show("请至少选择一个工作表", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            try
            {
                // 禁用控件
                DisableControls();
                
                // 显示进度条
                metroProgressBar.Visible = true;
                lblProgress.Visible = true;
                metroProgressBar.Value = 0;
                
                // 创建进度报告器
                _progress = new Progress<ProgressInfo>(OnProgressChanged);
                
                // 导入所有选中的文件
                for (int i = 0; i < _selectedFiles.Count; i++)
                {
                    string filePath = _selectedFiles[i];
                    
                    // 创建导入任务
                    _currentTask = new ImportTask
                    {
                        FilePath = filePath,
                        FileName = Path.GetFileNameWithoutExtension(filePath),
                        FileType = metroRadioExcel.Checked ? ".xlsx" : ".csv"
                    };
                    
                    // 更新进度状态
                    if (_selectedFiles.Count > 1)
                    {
                        // 使用OnProgressChanged方法直接更新UI，而不是调用Progress.Report
                        OnProgressChanged(new ProgressInfo
                        {
                            Status = $"正在处理文件 {i + 1}/{_selectedFiles.Count}: {Path.GetFileName(filePath)}",
                            Percentage = (i * 100) / _selectedFiles.Count,
                            ProcessedItems = i,
                            TotalItems = _selectedFiles.Count
                        });
                    }
                    
                    try
                    {
                        // 预先检查表是否存在
                        bool tableExists = false;
                        TableExistsAction tableAction = TableExistsAction.Ask;
                        
                        // 获取可能的表名
                        string baseTableName;
                        if (_currentTask.FileType.ToLower() == ".csv")
                        {
                            baseTableName = $"CSV_{_currentTask.FileName}";
                            
                            // 检查表是否存在
                            tableExists = SqliteHelper.TableExists(baseTableName);
                            if (tableExists)
                            {
                                // 询问用户如何处理
                                tableAction = HandleTableExists(baseTableName, _selectedFiles.Count > 1);
                                
                                // 如果用户选择取消所有，退出整个导入循环
                                if (tableAction == TableExistsAction.CancelAll)
                                {
                                    EnableControls();
                                    metroProgressBar.Visible = false;
                                    lblProgress.Visible = false;
                                    MessageBox.Show("已取消所有文件的导入", "已取消", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    break;
                                }
                                
                                // 如果用户选择取消，跳过当前文件
                                if (tableAction == TableExistsAction.Cancel)
                                {
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            baseTableName = $"EXCEL_{_currentTask.FileName}";
                            
                            // 如果是Excel文件，检查所有可能的表名
                            var sheets = _importService.GetExcelSheets(filePath);
                            if (sheets.Count == 1)
                            {
                                // 如果只有一个工作表，表名为EXCEL_文件名
                                tableExists = SqliteHelper.TableExists(baseTableName);
                                if (tableExists)
                                {
                                    // 询问用户如何处理
                                    tableAction = HandleTableExists(baseTableName, _selectedFiles.Count > 1);
                                    
                                    // 如果用户选择取消所有，退出整个导入循环
                                    if (tableAction == TableExistsAction.CancelAll)
                                    {
                                        EnableControls();
                                        metroProgressBar.Visible = false;
                                        lblProgress.Visible = false;
                                        MessageBox.Show("已取消所有文件的导入", "已取消", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;
                                    }
                                    
                                    // 如果用户选择取消，跳过当前文件
                                    if (tableAction == TableExistsAction.Cancel)
                                    {
                                        continue;
                                    }
                                }
                            }
                            else
                            {
                                // 如果有多个工作表，检查每个可能的表名
                                bool anyTableExists = false;
                                Dictionary<string, TableExistsAction> tableActions = new Dictionary<string, TableExistsAction>();
                                
                                foreach (var sheet in sheets)
                                {
                                    string tableName = $"{baseTableName}_{sheet}";
                                    if (SqliteHelper.TableExists(tableName))
                                    {
                                        anyTableExists = true;
                                        
                                        // 询问用户如何处理
                                        TableExistsAction action = HandleTableExists(tableName, _selectedFiles.Count > 1);
                                        tableActions[tableName] = action;
                                        
                                        // 如果用户选择取消所有，退出整个导入循环
                                        if (action == TableExistsAction.CancelAll)
                                        {
                                            EnableControls();
                                            metroProgressBar.Visible = false;
                                            lblProgress.Visible = false;
                                            MessageBox.Show("已取消所有文件的导入", "已取消", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            break;
                                        }
                                    }
                                }
                                
                                // 如果用户选择了取消所有，退出整个导入循环
                                if (tableActions.Values.Contains(TableExistsAction.CancelAll))
                                {
                                    break;
                                }
                                
                                // 如果用户对所有表都选择了取消，跳过当前文件
                                if (anyTableExists && tableActions.Values.All(a => a == TableExistsAction.Cancel))
                                {
                                    continue;
                                }
                                
                                // 存储表名和对应的操作，以便在导入时使用
                                _currentTask.Tag = tableActions;
                            }
                        }
                
                // 执行导入任务
                        var result = await Task.Run(() => _importService.ImportExcel(
                            filePath, 
                            metroCheckHeader.Checked, 
                            tableAction, 
                            _progress,
                            _currentTask.Tag as Dictionary<string, TableExistsAction>));
                        
                        // 如果是最后一个文件，显示结果
                        if (i == _selectedFiles.Count - 1)
                        {
                // 启用控件
                EnableControls();
                
                // 隐藏进度条
                metroProgressBar.Visible = false;
                lblProgress.Visible = false;
                
                            if (result.Status == ImportStatus.Success)
                {
                                MessageBox.Show($"导入成功\n文件数: {_selectedFiles.Count}\n总行数: {result.TotalRows}", 
                        "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"导入失败: {result.ErrorMessage}", 
                        "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // 其他异常，显示错误信息
                        EnableControls();
                        metroProgressBar.Visible = false;
                        lblProgress.Visible = false;
                        MessageBox.Show($"导入失败: {ex.Message}", 
                            "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                // 启用控件
                EnableControls();
                
                // 隐藏进度条
                metroProgressBar.Visible = false;
                lblProgress.Visible = false;
                
                MessageBox.Show($"导入失败: {ex.Message}", 
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 进度变化事件处理
        /// </summary>
        /// <param name="info">进度信息</param>
        private void OnProgressChanged(ProgressInfo info)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<ProgressInfo>(OnProgressChanged), info);
                return;
            }

            // 更新进度条
            metroProgressBar.Value = (int)info.Percentage;
            
            // 更新进度标签
            lblProgress.Text = $"{info.Status} ({info.ProcessedItems}/{info.TotalItems})";
            
            // 如果出现错误，显示错误信息
            if (info.Error != null)
            {
                MessageBox.Show($"导入过程中出错: {info.Error.Message}", 
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            // 确保进度条和标签可见
            if (!metroProgressBar.Visible)
            {
                metroProgressBar.Visible = true;
                lblProgress.Visible = true;
            }
            
            // 更新UI
            Application.DoEvents();
        }
        
        /// <summary>
        /// 禁用控件
        /// </summary>
        private void DisableControls()
        {
            metroTextFilePath.Enabled = false;
            metroBtnBrowse.Enabled = false;
            metroRadioExcel.Enabled = false;
            metroRadioCsv.Enabled = false;
            metroCheckHeader.Enabled = false;
            metroCheckAllSheets.Enabled = false;
            metroListSheets.Enabled = false;
            metroBtnImport.Enabled = false;
        }
        
        /// <summary>
        /// 启用控件
        /// </summary>
        private void EnableControls()
        {
            metroTextFilePath.Enabled = true;
            metroBtnBrowse.Enabled = true;
            metroRadioExcel.Enabled = true;
            metroRadioCsv.Enabled = true;
            metroCheckHeader.Enabled = true;
            metroCheckAllSheets.Enabled = true;
            metroListSheets.Enabled = !metroCheckAllSheets.Checked;
            metroBtnImport.Enabled = true;
        }
        
        /// <summary>
        /// 处理表已存在的情况
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="isMultiFile">是否为多文件导入</param>
        /// <returns>处理方式</returns>
        private TableExistsAction HandleTableExists(string tableName, bool isMultiFile = false)
        {
            // 显示询问对话框
            var message = $"数据库中已存在表 \"{tableName}\"\n\n请选择操作:";
            
            if (isMultiFile)
            {
                message += "\n是 - 追加数据到现有表\n" +
                           "否 - 删除现有表并创建新表\n" +
                           "取消 - 跳过此文件\n" +
                           "全部取消 - 取消所有后续文件导入";
                
            var result = MessageBox.Show(
                    message,
                    "表已存在",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);
                
                switch (result)
                {
                    case DialogResult.Yes:
                        return TableExistsAction.Append;
                    case DialogResult.No:
                        return TableExistsAction.Recreate;
                    case DialogResult.Cancel:
                        // 显示另一个对话框询问是跳过还是取消所有
                        var cancelResult = MessageBox.Show(
                            "是否取消所有后续文件的导入？\n\n是 - 取消所有后续文件导入\n否 - 仅跳过当前文件",
                            "确认取消",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
                        
                        return cancelResult == DialogResult.Yes ? 
                            TableExistsAction.CancelAll : TableExistsAction.Cancel;
                    default:
                        return TableExistsAction.Cancel;
                }
            }
            else
            {
                message += "\n是 - 追加数据到现有表\n" +
                "否 - 删除现有表并创建新表\n" +
                           "取消 - 取消导入";
                
                var result = MessageBox.Show(
                    message,
                "表已存在",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);
            
            // 根据用户选择返回处理方式
            switch (result)
            {
                case DialogResult.Yes:
                    return TableExistsAction.Append;
                case DialogResult.No:
                    return TableExistsAction.Recreate;
                case DialogResult.Cancel:
                default:
                    return TableExistsAction.Cancel;
                }
            }
        }
    }
} 