using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ExcelToMerge.Utils
{
    /// <summary>
    /// 圆角按钮控件
    /// </summary>
    public class RoundButton : Button
    {
        private int _cornerRadius = 10;
        private Color _borderColor = Color.Transparent;
        private int _borderSize = 0;
        private bool _isHovering = false;
        private bool _isPressed = false;
        
        /// <summary>
        /// 圆角半径
        /// </summary>
        public int CornerRadius
        {
            get { return _cornerRadius; }
            set 
            { 
                _cornerRadius = value;
                Invalidate();
            }
        }
        
        /// <summary>
        /// 边框颜色
        /// </summary>
        public Color BorderColor
        {
            get { return _borderColor; }
            set 
            { 
                _borderColor = value;
                Invalidate();
            }
        }
        
        /// <summary>
        /// 边框大小
        /// </summary>
        public int BorderSize
        {
            get { return _borderSize; }
            set 
            { 
                _borderSize = value;
                Invalidate();
            }
        }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public RoundButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.BackColor = Color.FromArgb(0, 122, 204);
            this.ForeColor = Color.White;
            this.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular);
            this.Cursor = Cursors.Hand;
            this.Size = new Size(120, 40);
            
            // 启用双缓冲减少闪烁
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.UserPaint,
                true);
        }
        
        /// <summary>
        /// 重写OnPaint方法绘制圆角按钮
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            Rectangle rectSurface = this.ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -_borderSize, -_borderSize);
            int smoothSize = _borderSize > 0 ? _borderSize : 1;
            
            using (GraphicsPath pathSurface = GetFigurePath(rectSurface, _cornerRadius))
            using (GraphicsPath pathBorder = GetFigurePath(rectBorder, _cornerRadius - _borderSize))
            using (Pen penSurface = new Pen(this.Parent.BackColor, smoothSize))
            using (Pen penBorder = new Pen(_borderColor, _borderSize))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                
                // 按钮表面
                this.Region = new Region(pathSurface);
                
                // 绘制表面
                Color buttonColor = this.BackColor;
                
                // 根据按钮状态调整颜色
                if (_isPressed)
                {
                    buttonColor = ControlPaint.Dark(this.BackColor, 0.2f);
                }
                else if (_isHovering)
                {
                    buttonColor = ControlPaint.Light(this.BackColor, 0.2f);
                }
                
                using (SolidBrush brushSurface = new SolidBrush(buttonColor))
                {
                    e.Graphics.FillPath(brushSurface, pathSurface);
                }
                
                // 绘制边框
                if (_borderSize > 0)
                {
                    e.Graphics.DrawPath(penBorder, pathBorder);
                }
                
                // 绘制文本
                TextRenderer.DrawText(e.Graphics, this.Text, this.Font, 
                    rectSurface, this.ForeColor, 
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }
        
        /// <summary>
        /// 获取图形路径
        /// </summary>
        private GraphicsPath GetFigurePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;
            
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            
            return path;
        }
        
        /// <summary>
        /// 鼠标进入事件
        /// </summary>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovering = true;
            Invalidate();
        }
        
        /// <summary>
        /// 鼠标离开事件
        /// </summary>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovering = false;
            Invalidate();
        }
        
        /// <summary>
        /// 鼠标按下事件
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _isPressed = true;
            Invalidate();
        }
        
        /// <summary>
        /// 鼠标抬起事件
        /// </summary>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _isPressed = false;
            Invalidate();
        }
    }

    /// <summary>
    /// 自定义进度条控件
    /// </summary>
    public class CustomProgressBar : Control
    {
        private int _value = 0;
        private int _maximum = 100;
        private Color _channelColor = Color.LightGray;
        private Color _progressColor = Color.FromArgb(0, 122, 204);
        private Color _textColor = Color.Black;
        private int _channelHeight = 6;
        private int _cornerRadius = 3;
        private bool _showPercentage = true;
        private string _customText = "";
        private Timer _animationTimer = new Timer();
        private int _animationValue = 0;

        /// <summary>
        /// 当前值
        /// </summary>
        public int Value
        {
            get { return _value; }
            set
            {
                if (value > _maximum)
                    _value = _maximum;
                else if (value < 0)
                    _value = 0;
                else
                    _value = value;

                // 启动动画
                _animationTimer.Start();
                Invalidate();
            }
        }

        /// <summary>
        /// 最大值
        /// </summary>
        public int Maximum
        {
            get { return _maximum; }
            set
            {
                if (value > 0)
                {
                    _maximum = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 轨道颜色
        /// </summary>
        public Color ChannelColor
        {
            get { return _channelColor; }
            set
            {
                _channelColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 进度颜色
        /// </summary>
        public Color ProgressColor
        {
            get { return _progressColor; }
            set
            {
                _progressColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 文本颜色
        /// </summary>
        public Color TextColor
        {
            get { return _textColor; }
            set
            {
                _textColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 轨道高度
        /// </summary>
        public int ChannelHeight
        {
            get { return _channelHeight; }
            set
            {
                _channelHeight = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 圆角半径
        /// </summary>
        public int CornerRadius
        {
            get { return _cornerRadius; }
            set
            {
                _cornerRadius = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 是否显示百分比
        /// </summary>
        public bool ShowPercentage
        {
            get { return _showPercentage; }
            set
            {
                _showPercentage = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 自定义文本
        /// </summary>
        public string CustomText
        {
            get { return _customText; }
            set
            {
                _customText = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomProgressBar()
        {
            this.Size = new Size(300, 30);
            this.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular);
            this.ForeColor = Color.White;
            this.BackColor = Color.White;
            
            // 启用双缓冲减少闪烁
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                true);

            // 配置动画计时器
            _animationTimer.Interval = 10;
            _animationTimer.Tick += AnimationTimer_Tick;
        }

        /// <summary>
        /// 动画计时器事件
        /// </summary>
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (_animationValue < _value)
            {
                _animationValue += 2;
                if (_animationValue > _value)
                    _animationValue = _value;
                Invalidate();
            }
            else if (_animationValue > _value)
            {
                _animationValue -= 2;
                if (_animationValue < _value)
                    _animationValue = _value;
                Invalidate();
            }
            else
            {
                _animationTimer.Stop();
            }
        }

        /// <summary>
        /// 重写OnPaint方法绘制进度条
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // 计算进度条位置
            Rectangle rect = this.ClientRectangle;
            Rectangle channelRect = new Rectangle(
                rect.X,
                rect.Y + (rect.Height - _channelHeight) / 2,
                rect.Width,
                _channelHeight);

            // 绘制轨道
            using (GraphicsPath channelPath = GetRoundedRectPath(channelRect, _cornerRadius))
            using (SolidBrush channelBrush = new SolidBrush(_channelColor))
            {
                e.Graphics.FillPath(channelBrush, channelPath);
            }

            // 计算进度宽度
            int progressWidth = (int)((float)_animationValue / _maximum * rect.Width);
            if (progressWidth > 0)
            {
                // 绘制进度
                Rectangle progressRect = new Rectangle(
                    rect.X,
                    rect.Y + (rect.Height - _channelHeight) / 2,
                    progressWidth,
                    _channelHeight);

                using (GraphicsPath progressPath = GetRoundedRectPath(progressRect, _cornerRadius))
                using (SolidBrush progressBrush = new SolidBrush(_progressColor))
                {
                    e.Graphics.FillPath(progressBrush, progressPath);
                }
            }

            // 绘制文本
            string text = _customText;
            if (_showPercentage)
            {
                int percentage = (int)((float)_value / _maximum * 100);
                text = string.IsNullOrEmpty(_customText) ? percentage.ToString() + "%" : _customText + " - " + percentage.ToString() + "%";
            }

            if (!string.IsNullOrEmpty(text))
            {
                using (SolidBrush textBrush = new SolidBrush(_textColor))
                {
                    SizeF textSize = e.Graphics.MeasureString(text, this.Font);
                    PointF textLocation = new PointF(
                        (rect.Width - textSize.Width) / 2,
                        (rect.Height - textSize.Height) / 2);
                    e.Graphics.DrawString(text, this.Font, textBrush, textLocation);
                }
            }
        }

        /// <summary>
        /// 获取圆角矩形路径
        /// </summary>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
    
    /// <summary>
    /// 导航菜单项控件
    /// </summary>
    public class NavMenuItem : Panel
    {
        private bool _isSelected = false;
        private bool _isHovering = false;
        private Color _normalColor = Color.FromArgb(45, 45, 48);
        private Color _selectedColor = Color.FromArgb(0, 122, 204);
        private Color _hoverColor = Color.FromArgb(62, 62, 64);
        private Color _iconColor = Color.White;
        private Color _textColor = Color.White;
        private string _text = "菜单项";
        private Image _icon = null;
        private int _iconSize = 24;
        private int _leftBarWidth = 5;
        
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                Invalidate();
            }
        }
        
        /// <summary>
        /// 正常状态颜色
        /// </summary>
        public Color NormalColor
        {
            get { return _normalColor; }
            set
            {
                _normalColor = value;
                Invalidate();
            }
        }
        
        /// <summary>
        /// 选中状态颜色
        /// </summary>
        public Color SelectedColor
        {
            get { return _selectedColor; }
            set
            {
                _selectedColor = value;
                Invalidate();
            }
        }
        
        /// <summary>
        /// 悬停状态颜色
        /// </summary>
        public Color HoverColor
        {
            get { return _hoverColor; }
            set
            {
                _hoverColor = value;
                Invalidate();
            }
        }
        
        /// <summary>
        /// 图标颜色
        /// </summary>
        public Color IconColor
        {
            get { return _iconColor; }
            set
            {
                _iconColor = value;
                Invalidate();
            }
        }
        
        /// <summary>
        /// 文本颜色
        /// </summary>
        public Color TextColor
        {
            get { return _textColor; }
            set
            {
                _textColor = value;
                Invalidate();
            }
        }
        
        /// <summary>
        /// 菜单文本
        /// </summary>
        public string MenuText
        {
            get { return _text; }
            set
            {
                _text = value;
                Invalidate();
            }
        }
        
        /// <summary>
        /// 菜单图标
        /// </summary>
        public Image Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                Invalidate();
            }
        }
        
        /// <summary>
        /// 图标大小
        /// </summary>
        public int IconSize
        {
            get { return _iconSize; }
            set
            {
                _iconSize = value;
                Invalidate();
            }
        }
        
        /// <summary>
        /// 左侧指示条宽度
        /// </summary>
        public int LeftBarWidth
        {
            get { return _leftBarWidth; }
            set
            {
                _leftBarWidth = value;
                Invalidate();
            }
        }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public NavMenuItem()
        {
            this.Size = new Size(200, 50);
            this.BackColor = _normalColor;
            this.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Regular);
            this.Cursor = Cursors.Hand;
            
            // 启用双缓冲减少闪烁
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                true);
        }
        
        /// <summary>
        /// 重写OnPaint方法绘制菜单项
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            
            // 设置背景色
            Color backColor;
            if (_isSelected)
                backColor = _selectedColor;
            else if (_isHovering)
                backColor = _hoverColor;
            else
                backColor = _normalColor;
                
            using (SolidBrush brush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
            
            // 如果选中，绘制左侧指示条
            if (_isSelected)
            {
                using (SolidBrush brush = new SolidBrush(_selectedColor))
                {
                    e.Graphics.FillRectangle(brush, 0, 0, _leftBarWidth, this.Height);
                }
            }
            
            // 绘制图标
            if (_icon != null)
            {
                Rectangle iconRect = new Rectangle(
                    _leftBarWidth + 10,
                    (this.Height - _iconSize) / 2,
                    _iconSize,
                    _iconSize);
                
                e.Graphics.DrawImage(_icon, iconRect);
            }
            
            // 绘制文本
            using (SolidBrush brush = new SolidBrush(_textColor))
            {
                int textX = _leftBarWidth + 10;
                if (_icon != null)
                    textX += _iconSize + 10;
                
                Rectangle textRect = new Rectangle(
                    textX,
                    0,
                    this.Width - textX,
                    this.Height);
                
                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Near;
                
                e.Graphics.DrawString(_text, this.Font, brush, textRect, sf);
            }
        }
        
        /// <summary>
        /// 鼠标进入事件
        /// </summary>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovering = true;
            Invalidate();
        }
        
        /// <summary>
        /// 鼠标离开事件
        /// </summary>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovering = false;
            Invalidate();
        }
        
        /// <summary>
        /// 鼠标点击事件
        /// </summary>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            _isSelected = true;
            Invalidate();
        }
    }
} 