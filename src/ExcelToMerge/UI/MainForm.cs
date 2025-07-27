using System;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework.Controls;
using MetroFramework;

namespace ExcelToMerge.UI
{
    /// <summary>
    /// 主窗体
    /// </summary>
    public partial class MainForm : MetroForm
    {
        private ImportForm _importForm;
        private ConvertForm _convertForm;
        private ScheduleForm _scheduleForm;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // 设置窗体样式
            this.StyleManager = metroStyleManager;
            metroStyleManager.Theme = MetroThemeStyle.Dark;
            metroStyleManager.Style = MetroColorStyle.Blue;
            
            // 创建子窗体
            _importForm = new ImportForm();
            _convertForm = new ConvertForm();
            _scheduleForm = new ScheduleForm();
            
            // 设置子窗体属性，使用Panel作为容器
            _importForm.TopLevel = false;
            _importForm.FormBorderStyle = FormBorderStyle.None;
            _importForm.Dock = DockStyle.Fill;
            
            _convertForm.TopLevel = false;
            _convertForm.FormBorderStyle = FormBorderStyle.None;
            _convertForm.Dock = DockStyle.Fill;
            
            _scheduleForm.TopLevel = false;
            _scheduleForm.FormBorderStyle = FormBorderStyle.None;
            _scheduleForm.Dock = DockStyle.Fill;
            
            // 添加子窗体到面板
            panelContent.Controls.Add(_importForm);
            panelContent.Controls.Add(_convertForm);
            panelContent.Controls.Add(_scheduleForm);
            
            // 显示导入窗体
            ShowForm(_importForm);
            
            // 设置选项卡按钮状态
            metroTabImport.Tag = true;
            UpdateTabAppearance();
        }
        
        /// <summary>
        /// 显示指定窗体
        /// </summary>
        /// <param name="form">要显示的窗体</param>
        private void ShowForm(Form form)
        {
            // 隐藏所有窗体
            _importForm?.Hide();
            _convertForm?.Hide();
            _scheduleForm?.Hide();
            
            // 显示指定窗体
            form?.Show();
        }

        /// <summary>
        /// 导入选项卡点击事件
        /// </summary>
        private void metroTabImport_Click(object sender, EventArgs e)
        {
            ShowForm(_importForm);
            
            // 更新选项卡按钮状态
            metroTabImport.Tag = true;
            metroTabConvert.Tag = false;
            metroTabSchedule.Tag = false;
            UpdateTabAppearance();
        }
        
        /// <summary>
        /// 转换选项卡点击事件
        /// </summary>
        private void metroTabConvert_Click(object sender, EventArgs e)
        {
            ShowForm(_convertForm);
            
            // 更新选项卡按钮状态
            metroTabImport.Tag = false;
            metroTabConvert.Tag = true;
            metroTabSchedule.Tag = false;
            UpdateTabAppearance();
        }
        
        /// <summary>
        /// 调度选项卡点击事件
        /// </summary>
        private void metroTabSchedule_Click(object sender, EventArgs e)
        {
            ShowForm(_scheduleForm);
            
            // 更新选项卡按钮状态
            metroTabImport.Tag = false;
            metroTabConvert.Tag = false;
            metroTabSchedule.Tag = true;
            UpdateTabAppearance();
        }
        
        /// <summary>
        /// 更新选项卡外观
        /// </summary>
        private void UpdateTabAppearance()
        {
            // 使用不同的颜色或样式来表示选中状态
            // 检查Tag属性是否为null，如果为null则默认为false
            bool importActive = metroTabImport.Tag != null && (bool)metroTabImport.Tag;
            bool convertActive = metroTabConvert.Tag != null && (bool)metroTabConvert.Tag;
            bool scheduleActive = metroTabSchedule.Tag != null && (bool)metroTabSchedule.Tag;
            
            metroTabImport.Style = importActive ? MetroColorStyle.Blue : MetroColorStyle.Silver;
            metroTabConvert.Style = convertActive ? MetroColorStyle.Blue : MetroColorStyle.Silver;
            metroTabSchedule.Style = scheduleActive ? MetroColorStyle.Blue : MetroColorStyle.Silver;
        }
    }
} 