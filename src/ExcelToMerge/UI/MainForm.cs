using System;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework.Controls;
using MetroFramework;
using ExcelToMerge.Utils;

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
        
        // 添加导航菜单项
        private NavMenuItem _navImport;
        private NavMenuItem _navConvert;
        private NavMenuItem _navSchedule;
        private FlowLayoutPanel _navPanel;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitializeNavMenu();
        }
        
        /// <summary>
        /// 初始化导航菜单
        /// </summary>
        private void InitializeNavMenu()
        {
            // 创建导航面板
            _navPanel = new FlowLayoutPanel
            {
                Location = new Point(23, 63),
                Size = new Size(312, 40),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.FromArgb(45, 45, 48)
            };
            
            // 创建导入菜单项
            _navImport = new NavMenuItem
            {
                Size = new Size(100, 40),
                MenuText = "导入",
                IsSelected = true,
                NormalColor = Color.FromArgb(45, 45, 48),
                SelectedColor = Color.FromArgb(0, 122, 204),
                HoverColor = Color.FromArgb(62, 62, 64)
            };
            _navImport.Click += metroTabImport_Click;
            
            // 创建转换菜单项
            _navConvert = new NavMenuItem
            {
                Size = new Size(100, 40),
                MenuText = "转换",
                IsSelected = false,
                NormalColor = Color.FromArgb(45, 45, 48),
                SelectedColor = Color.FromArgb(0, 122, 204),
                HoverColor = Color.FromArgb(62, 62, 64)
            };
            _navConvert.Click += metroTabConvert_Click;
            
            // 创建调度菜单项
            _navSchedule = new NavMenuItem
            {
                Size = new Size(100, 40),
                MenuText = "调度",
                IsSelected = false,
                NormalColor = Color.FromArgb(45, 45, 48),
                SelectedColor = Color.FromArgb(0, 122, 204),
                HoverColor = Color.FromArgb(62, 62, 64)
            };
            _navSchedule.Click += metroTabSchedule_Click;
            
            // 添加菜单项到导航面板
            _navPanel.Controls.Add(_navImport);
            _navPanel.Controls.Add(_navConvert);
            _navPanel.Controls.Add(_navSchedule);
            
            // 添加导航面板到窗体
            this.Controls.Add(_navPanel);
            _navPanel.BringToFront();
            
            // 隐藏原来的按钮
            metroTabImport.Visible = false;
            metroTabConvert.Visible = false;
            metroTabSchedule.Visible = false;
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
            
            // 设置选项卡按钮状态 - 使用新的导航菜单项
            _navImport.IsSelected = true;
            _navConvert.IsSelected = false;
            _navSchedule.IsSelected = false;
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
            
            // 更新选项卡按钮状态 - 使用新的导航菜单项
            _navImport.IsSelected = true;
            _navConvert.IsSelected = false;
            _navSchedule.IsSelected = false;
        }
        
        /// <summary>
        /// 转换选项卡点击事件
        /// </summary>
        private void metroTabConvert_Click(object sender, EventArgs e)
        {
            ShowForm(_convertForm);
            
            // 更新选项卡按钮状态 - 使用新的导航菜单项
            _navImport.IsSelected = false;
            _navConvert.IsSelected = true;
            _navSchedule.IsSelected = false;
        }
        
        /// <summary>
        /// 调度选项卡点击事件
        /// </summary>
        private void metroTabSchedule_Click(object sender, EventArgs e)
        {
            ShowForm(_scheduleForm);
            
            // 更新选项卡按钮状态 - 使用新的导航菜单项
            _navImport.IsSelected = false;
            _navConvert.IsSelected = false;
            _navSchedule.IsSelected = true;
        }
        
        /// <summary>
        /// 更新选项卡外观 - 此方法保留但不再使用
        /// </summary>
        private void UpdateTabAppearance()
        {
            // 使用不同的颜色或样式来表示选中状态
            // 检查Tag属性是否为null，如果为null则默认为false
            bool importActive = metroTabImport.Tag != null && (bool)metroTabImport.Tag;
            bool convertActive = metroTabConvert.Tag != null && (bool)metroTabConvert.Tag;
            bool scheduleActive = metroTabSchedule.Tag != null && (bool)metroTabSchedule.Tag;
            
            // 设置选中为蓝色，非选中为灰色
            metroTabImport.Style = importActive ? MetroColorStyle.Blue : MetroColorStyle.Silver;
            metroTabConvert.Style = convertActive ? MetroColorStyle.Blue : MetroColorStyle.Silver;
            metroTabSchedule.Style = scheduleActive ? MetroColorStyle.Blue : MetroColorStyle.Silver;
            
            // 增强视觉对比度
            metroTabImport.BackColor = importActive ? Color.FromArgb(0, 122, 204) : Color.FromArgb(224, 224, 224);
            metroTabConvert.BackColor = convertActive ? Color.FromArgb(0, 122, 204) : Color.FromArgb(224, 224, 224);
            metroTabSchedule.BackColor = scheduleActive ? Color.FromArgb(0, 122, 204) : Color.FromArgb(224, 224, 224);
            
            metroTabImport.ForeColor = importActive ? Color.White : Color.DimGray;
            metroTabConvert.ForeColor = convertActive ? Color.White : Color.DimGray;
            metroTabSchedule.ForeColor = scheduleActive ? Color.White : Color.DimGray;
            
            // 更新选中按钮的突出显示效果
            metroTabImport.UseCustomBackColor = true;
            metroTabConvert.UseCustomBackColor = true;
            metroTabSchedule.UseCustomBackColor = true;
            
            metroTabImport.UseCustomForeColor = true;
            metroTabConvert.UseCustomForeColor = true;
            metroTabSchedule.UseCustomForeColor = true;
        }
    }
} 