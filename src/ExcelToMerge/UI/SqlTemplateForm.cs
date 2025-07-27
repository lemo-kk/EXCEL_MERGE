using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ExcelToMerge.Models;
using ExcelToMerge.Services;

namespace ExcelToMerge.UI
{
    /// <summary>
    /// SQL模板管理窗体
    /// </summary>
    public partial class SqlTemplateForm : Form
    {
        private readonly ConvertService _convertService;
        private List<SqlTemplate> _templates;
        private SqlTemplate _currentTemplate;
        private bool _isNewTemplate;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public SqlTemplateForm()
        {
            InitializeComponent();
            _convertService = new ConvertService();
        }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="convertService">转换服务</param>
        public SqlTemplateForm(ConvertService convertService)
        {
            InitializeComponent();
            _convertService = convertService ?? new ConvertService();
        }
        
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void SqlTemplateForm_Load(object sender, EventArgs e)
        {
            // 加载模板列表
            LoadTemplates();
            
            // 初始化分类下拉框
            InitializeCategories();
        }
        
        /// <summary>
        /// 加载模板列表
        /// </summary>
        private void LoadTemplates()
        {
            try
            {
                // 清空列表
                listViewTemplates.Items.Clear();
                
                // 获取所有模板
                _templates = _convertService.GetSqlTemplateList();
                
                // 添加到列表
                foreach (var template in _templates)
                {
                    ListViewItem item = new ListViewItem(template.Id.ToString());
                    item.SubItems.Add(template.Name);
                    item.SubItems.Add(template.Category);
                    item.SubItems.Add(template.Description);
                    item.SubItems.Add(template.IsSystem ? "是" : "否");
                    item.Tag = template;
                    
                    // 如果是系统模板，使用灰色字体
                    if (template.IsSystem)
                    {
                        item.ForeColor = Color.Gray;
                    }
                    
                    listViewTemplates.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载SQL模板失败: {ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 初始化分类下拉框
        /// </summary>
        private void InitializeCategories()
        {
            try
            {
                // 清空下拉框
                comboBoxCategory.Items.Clear();
                
                // 添加默认选项
                comboBoxCategory.Items.Add("-- 请选择分类 --");
                
                // 获取所有分类
                HashSet<string> categories = new HashSet<string>();
                
                foreach (var template in _templates)
                {
                    if (!string.IsNullOrEmpty(template.Category))
                    {
                        categories.Add(template.Category);
                    }
                }
                
                // 添加到下拉框
                foreach (var category in categories)
                {
                    comboBoxCategory.Items.Add(category);
                }
                
                // 选择默认选项
                comboBoxCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初始化分类下拉框失败: {ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 显示模板详情
        /// </summary>
        private void ShowTemplateDetails(SqlTemplate template)
        {
            if (template == null)
            {
                // 清空控件
                textBoxName.Text = string.Empty;
                textBoxDescription.Text = string.Empty;
                comboBoxCategory.SelectedIndex = 0;
                richTextBoxSql.Text = string.Empty;
                checkBoxSystem.Checked = false;
                
                // 禁用删除按钮
                btnDelete.Enabled = false;
                
                return;
            }
            
            // 显示模板信息
            textBoxName.Text = template.Name;
            textBoxDescription.Text = template.Description;
            richTextBoxSql.Text = template.SqlScript;
            checkBoxSystem.Checked = template.IsSystem;
            
            // 设置分类
            if (!string.IsNullOrEmpty(template.Category))
            {
                int index = comboBoxCategory.Items.IndexOf(template.Category);
                if (index >= 0)
                {
                    comboBoxCategory.SelectedIndex = index;
                }
                else
                {
                    comboBoxCategory.Items.Add(template.Category);
                    comboBoxCategory.SelectedIndex = comboBoxCategory.Items.Count - 1;
                }
            }
            else
            {
                comboBoxCategory.SelectedIndex = 0;
            }
            
            // 启用/禁用删除按钮
            btnDelete.Enabled = !template.IsSystem;
            
            // 启用/禁用系统模板复选框
            checkBoxSystem.Enabled = !template.IsSystem;
        }
        
        /// <summary>
        /// 新建按钮点击事件
        /// </summary>
        private void btnNew_Click(object sender, EventArgs e)
        {
            // 创建新模板
            _currentTemplate = new SqlTemplate();
            _isNewTemplate = true;
            
            // 显示模板详情
            ShowTemplateDetails(_currentTemplate);
            
            // 启用编辑控件
            EnableEditControls(true);
        }
        
        /// <summary>
        /// 编辑按钮点击事件
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (listViewTemplates.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择一个模板", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            // 获取选中的模板
            var item = listViewTemplates.SelectedItems[0];
            _currentTemplate = item.Tag as SqlTemplate;
            _isNewTemplate = false;
            
            // 显示模板详情
            ShowTemplateDetails(_currentTemplate);
            
            // 启用编辑控件
            EnableEditControls(true);
        }
        
        /// <summary>
        /// 删除按钮点击事件
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listViewTemplates.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择一个模板", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            // 获取选中的模板
            var item = listViewTemplates.SelectedItems[0];
            var template = item.Tag as SqlTemplate;
            
            if (template != null)
            {
                // 系统模板不能删除
                if (template.IsSystem)
                {
                    MessageBox.Show("系统模板不能删除", "提示", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                // 确认删除
                var result = MessageBox.Show($"确定要删除模板 \"{template.Name}\" 吗？", "确认删除", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // 删除模板
                        bool deleted = _convertService.DeleteSqlTemplate(template.Id);
                        
                        if (deleted)
                        {
                            // 重新加载模板列表
                            LoadTemplates();
                            
                            // 初始化分类下拉框
                            InitializeCategories();
                            
                            // 清空当前模板
                            _currentTemplate = null;
                            ShowTemplateDetails(null);
                            
                            MessageBox.Show("删除成功", "提示", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("删除失败", "错误", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"删除模板失败: {ex.Message}", "错误", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        
        /// <summary>
        /// 保存按钮点击事件
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("请输入模板名称", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            if (string.IsNullOrEmpty(richTextBoxSql.Text))
            {
                MessageBox.Show("请输入SQL脚本", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            try
            {
                // 更新模板信息
                _currentTemplate.Name = textBoxName.Text;
                _currentTemplate.Description = textBoxDescription.Text;
                _currentTemplate.SqlScript = richTextBoxSql.Text;
                _currentTemplate.IsSystem = checkBoxSystem.Checked;
                
                // 设置分类
                if (comboBoxCategory.SelectedIndex > 0)
                {
                    _currentTemplate.Category = comboBoxCategory.SelectedItem.ToString();
                }
                else if (comboBoxCategory.Text != "-- 请选择分类 --")
                {
                    _currentTemplate.Category = comboBoxCategory.Text;
                }
                else
                {
                    _currentTemplate.Category = null;
                }
                
                // 保存模板
                int templateId = _convertService.SaveSqlTemplate(_currentTemplate);
                
                if (templateId > 0)
                {
                    // 更新模板ID
                    _currentTemplate.Id = templateId;
                    
                    // 重新加载模板列表
                    LoadTemplates();
                    
                    // 初始化分类下拉框
                    InitializeCategories();
                    
                    // 禁用编辑控件
                    EnableEditControls(false);
                    
                    MessageBox.Show("保存成功", "提示", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("保存失败", "错误", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存模板失败: {ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 禁用编辑控件
            EnableEditControls(false);
            
            // 如果是新建模板，清空当前模板
            if (_isNewTemplate)
            {
                _currentTemplate = null;
                ShowTemplateDetails(null);
            }
            else
            {
                // 恢复模板信息
                ShowTemplateDetails(_currentTemplate);
            }
        }
        
        /// <summary>
        /// 应用按钮点击事件
        /// </summary>
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (_currentTemplate == null)
            {
                MessageBox.Show("请先选择一个模板", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            // 返回模板信息
            DialogResult = DialogResult.OK;
            Close();
        }
        
        /// <summary>
        /// 列表选择改变事件
        /// </summary>
        private void listViewTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewTemplates.SelectedItems.Count > 0)
            {
                // 获取选中的模板
                var item = listViewTemplates.SelectedItems[0];
                _currentTemplate = item.Tag as SqlTemplate;
                _isNewTemplate = false;
                
                // 显示模板详情
                ShowTemplateDetails(_currentTemplate);
                
                // 禁用编辑控件
                EnableEditControls(false);
            }
        }
        
        /// <summary>
        /// 启用/禁用编辑控件
        /// </summary>
        private void EnableEditControls(bool enable)
        {
            // 编辑控件
            textBoxName.Enabled = enable;
            textBoxDescription.Enabled = enable;
            comboBoxCategory.Enabled = enable;
            richTextBoxSql.Enabled = enable;
            checkBoxSystem.Enabled = enable && (_currentTemplate == null || !_currentTemplate.IsSystem);
            
            // 按钮
            btnSave.Enabled = enable;
            btnCancel.Enabled = enable;
            
            // 其他控件
            listViewTemplates.Enabled = !enable;
            btnNew.Enabled = !enable;
            btnEdit.Enabled = !enable;
            btnDelete.Enabled = !enable && (_currentTemplate != null && !_currentTemplate.IsSystem);
            btnApply.Enabled = !enable && _currentTemplate != null;
        }
        
        /// <summary>
        /// 获取选中的模板
        /// </summary>
        public SqlTemplate SelectedTemplate
        {
            get { return _currentTemplate; }
        }
    }
} 