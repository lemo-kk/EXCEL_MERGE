namespace ExcelToMerge.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.metroStyleManager = new MetroFramework.Components.MetroStyleManager(this.components);
            this.metroTabImport = new MetroFramework.Controls.MetroTile();
            this.metroTabConvert = new MetroFramework.Controls.MetroTile();
            this.metroTabSchedule = new MetroFramework.Controls.MetroTile();
            this.panelContent = new System.Windows.Forms.Panel();
            this.metroStatusStrip = new MetroFramework.Controls.MetroPanel();
            this.lblStatus = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).BeginInit();
            this.metroStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroStyleManager
            // 
            this.metroStyleManager.Owner = this;
            this.metroStyleManager.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroTabImport
            // 
            this.metroTabImport.ActiveControl = null;
            this.metroTabImport.Location = new System.Drawing.Point(23, 63);
            this.metroTabImport.Name = "metroTabImport";
            this.metroTabImport.Size = new System.Drawing.Size(100, 40);
            this.metroTabImport.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTabImport.TabIndex = 0;
            this.metroTabImport.Text = "导入";
            this.metroTabImport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroTabImport.UseSelectable = true;
            this.metroTabImport.Click += new System.EventHandler(this.metroTabImport_Click);
            // 
            // metroTabConvert
            // 
            this.metroTabConvert.ActiveControl = null;
            this.metroTabConvert.Location = new System.Drawing.Point(129, 63);
            this.metroTabConvert.Name = "metroTabConvert";
            this.metroTabConvert.Size = new System.Drawing.Size(100, 40);
            this.metroTabConvert.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTabConvert.TabIndex = 1;
            this.metroTabConvert.Text = "转换";
            this.metroTabConvert.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroTabConvert.UseSelectable = true;
            this.metroTabConvert.Click += new System.EventHandler(this.metroTabConvert_Click);
            // 
            // metroTabSchedule
            // 
            this.metroTabSchedule.ActiveControl = null;
            this.metroTabSchedule.Location = new System.Drawing.Point(235, 63);
            this.metroTabSchedule.Name = "metroTabSchedule";
            this.metroTabSchedule.Size = new System.Drawing.Size(100, 40);
            this.metroTabSchedule.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTabSchedule.TabIndex = 2;
            this.metroTabSchedule.Text = "调度";
            this.metroTabSchedule.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroTabSchedule.UseSelectable = true;
            this.metroTabSchedule.Click += new System.EventHandler(this.metroTabSchedule_Click);
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.panelContent.Location = new System.Drawing.Point(23, 109);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(954, 518);
            this.panelContent.TabIndex = 3;
            // 
            // metroStatusStrip
            // 
            this.metroStatusStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroStatusStrip.Controls.Add(this.lblStatus);
            this.metroStatusStrip.HorizontalScrollbarBarColor = true;
            this.metroStatusStrip.HorizontalScrollbarHighlightOnWheel = false;
            this.metroStatusStrip.HorizontalScrollbarSize = 10;
            this.metroStatusStrip.Location = new System.Drawing.Point(23, 633);
            this.metroStatusStrip.Name = "metroStatusStrip";
            this.metroStatusStrip.Size = new System.Drawing.Size(954, 24);
            this.metroStatusStrip.TabIndex = 4;
            this.metroStatusStrip.VerticalScrollbarBarColor = true;
            this.metroStatusStrip.VerticalScrollbarHighlightOnWheel = false;
            this.metroStatusStrip.VerticalScrollbarSize = 10;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(3, 2);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 19);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "就绪";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 680);
            this.Controls.Add(this.metroStatusStrip);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.metroTabSchedule);
            this.Controls.Add(this.metroTabConvert);
            this.Controls.Add(this.metroTabImport);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "Excel数据处理转换工具";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).EndInit();
            this.metroStatusStrip.ResumeLayout(false);
            this.metroStatusStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Components.MetroStyleManager metroStyleManager;
        private MetroFramework.Controls.MetroTile metroTabImport;
        private MetroFramework.Controls.MetroTile metroTabConvert;
        private MetroFramework.Controls.MetroTile metroTabSchedule;
        private System.Windows.Forms.Panel panelContent;
        private MetroFramework.Controls.MetroPanel metroStatusStrip;
        private MetroFramework.Controls.MetroLabel lblStatus;
    }
} 