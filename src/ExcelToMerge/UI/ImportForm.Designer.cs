namespace ExcelToMerge.UI
{
    partial class ImportForm
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
            this.metroLabelFile = new MetroFramework.Controls.MetroLabel();
            this.metroTextFilePath = new MetroFramework.Controls.MetroTextBox();
            this.metroBtnBrowse = new MetroFramework.Controls.MetroButton();
            this.metroGroupFileType = new MetroFramework.Controls.MetroPanel();
            this.metroLabelFileType = new MetroFramework.Controls.MetroLabel();
            this.metroRadioExcel = new MetroFramework.Controls.MetroRadioButton();
            this.metroRadioCsv = new MetroFramework.Controls.MetroRadioButton();
            this.metroGroupOptions = new MetroFramework.Controls.MetroPanel();
            this.metroLabelOptions = new MetroFramework.Controls.MetroLabel();
            this.metroCheckHeader = new MetroFramework.Controls.MetroCheckBox();
            this.metroCheckAllSheets = new MetroFramework.Controls.MetroCheckBox();
            this.metroLabelSheets = new MetroFramework.Controls.MetroLabel();
            this.metroListSheets = new System.Windows.Forms.CheckedListBox();
            this.metroBtnImport = new MetroFramework.Controls.MetroButton();
            this.metroProgressBar = new MetroFramework.Controls.MetroProgressBar();
            this.lblProgress = new MetroFramework.Controls.MetroLabel();
            this.metroGroupFileType.SuspendLayout();
            this.metroGroupOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroLabelFile
            // 
            this.metroLabelFile.AutoSize = true;
            this.metroLabelFile.Location = new System.Drawing.Point(23, 20);
            this.metroLabelFile.Name = "metroLabelFile";
            this.metroLabelFile.Size = new System.Drawing.Size(65, 19);
            this.metroLabelFile.TabIndex = 0;
            this.metroLabelFile.Text = "选择文件:";
            // 
            // metroTextFilePath
            // 
            this.metroTextFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroTextFilePath.Location = new System.Drawing.Point(94, 20);
            this.metroTextFilePath.Name = "metroTextFilePath";
            this.metroTextFilePath.ReadOnly = true;
            this.metroTextFilePath.Size = new System.Drawing.Size(737, 23);
            this.metroTextFilePath.TabIndex = 1;
            // 
            // metroBtnBrowse
            // 
            this.metroBtnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroBtnBrowse.Location = new System.Drawing.Point(837, 20);
            this.metroBtnBrowse.Name = "metroBtnBrowse";
            this.metroBtnBrowse.Size = new System.Drawing.Size(75, 23);
            this.metroBtnBrowse.TabIndex = 2;
            this.metroBtnBrowse.Text = "浏览...";
            this.metroBtnBrowse.Click += new System.EventHandler(this.metroBtnBrowse_Click);
            // 
            // metroGroupFileType
            // 
            this.metroGroupFileType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroGroupFileType.Controls.Add(this.metroRadioCsv);
            this.metroGroupFileType.Controls.Add(this.metroRadioExcel);
            this.metroGroupFileType.Controls.Add(this.metroLabelFileType);
            this.metroGroupFileType.HorizontalScrollbarBarColor = true;
            this.metroGroupFileType.HorizontalScrollbarHighlightOnWheel = false;
            this.metroGroupFileType.HorizontalScrollbarSize = 10;
            this.metroGroupFileType.Location = new System.Drawing.Point(23, 60);
            this.metroGroupFileType.Name = "metroGroupFileType";
            this.metroGroupFileType.Size = new System.Drawing.Size(250, 80);
            this.metroGroupFileType.TabIndex = 3;
            this.metroGroupFileType.VerticalScrollbarBarColor = true;
            this.metroGroupFileType.VerticalScrollbarHighlightOnWheel = false;
            this.metroGroupFileType.VerticalScrollbarSize = 10;
            // 
            // metroLabelFileType
            // 
            this.metroLabelFileType.AutoSize = true;
            this.metroLabelFileType.Location = new System.Drawing.Point(3, 10);
            this.metroLabelFileType.Name = "metroLabelFileType";
            this.metroLabelFileType.Size = new System.Drawing.Size(65, 19);
            this.metroLabelFileType.TabIndex = 2;
            this.metroLabelFileType.Text = "文件类型:";
            // 
            // metroRadioExcel
            // 
            this.metroRadioExcel.AutoSize = true;
            this.metroRadioExcel.Location = new System.Drawing.Point(20, 40);
            this.metroRadioExcel.Name = "metroRadioExcel";
            this.metroRadioExcel.Size = new System.Drawing.Size(75, 15);
            this.metroRadioExcel.TabIndex = 3;
            this.metroRadioExcel.Text = "Excel文件";
            // this.metroRadioExcel.UseVisualStyleBackColor = true;
            this.metroRadioExcel.CheckedChanged += new System.EventHandler(this.metroRadioFileType_CheckedChanged);
            // 
            // metroRadioCsv
            // 
            this.metroRadioCsv.AutoSize = true;
            this.metroRadioCsv.Location = new System.Drawing.Point(120, 40);
            this.metroRadioCsv.Name = "metroRadioCsv";
            this.metroRadioCsv.Size = new System.Drawing.Size(75, 15);
            this.metroRadioCsv.TabIndex = 4;
            this.metroRadioCsv.Text = "CSV文件";
            //this.metroRadioCsv.UseVisualStyleBackColor = true;
            this.metroRadioCsv.CheckedChanged += new System.EventHandler(this.metroRadioFileType_CheckedChanged);
            // 
            // metroGroupOptions
            // 
            this.metroGroupOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroGroupOptions.Controls.Add(this.metroCheckAllSheets);
            this.metroGroupOptions.Controls.Add(this.metroCheckHeader);
            this.metroGroupOptions.Controls.Add(this.metroLabelOptions);
            this.metroGroupOptions.HorizontalScrollbarBarColor = true;
            this.metroGroupOptions.HorizontalScrollbarHighlightOnWheel = false;
            this.metroGroupOptions.HorizontalScrollbarSize = 10;
            this.metroGroupOptions.Location = new System.Drawing.Point(279, 60);
            this.metroGroupOptions.Name = "metroGroupOptions";
            this.metroGroupOptions.Size = new System.Drawing.Size(250, 80);
            this.metroGroupOptions.TabIndex = 4;
            this.metroGroupOptions.VerticalScrollbarBarColor = true;
            this.metroGroupOptions.VerticalScrollbarHighlightOnWheel = false;
            this.metroGroupOptions.VerticalScrollbarSize = 10;
            // 
            // metroLabelOptions
            // 
            this.metroLabelOptions.AutoSize = true;
            this.metroLabelOptions.Location = new System.Drawing.Point(3, 10);
            this.metroLabelOptions.Name = "metroLabelOptions";
            this.metroLabelOptions.Size = new System.Drawing.Size(65, 19);
            this.metroLabelOptions.TabIndex = 2;
            this.metroLabelOptions.Text = "导入选项:";
            // 
            // metroCheckHeader
            // 
            this.metroCheckHeader.AutoSize = true;
            this.metroCheckHeader.Location = new System.Drawing.Point(20, 40);
            this.metroCheckHeader.Name = "metroCheckHeader";
            this.metroCheckHeader.Size = new System.Drawing.Size(84, 15);
            this.metroCheckHeader.TabIndex = 3;
            this.metroCheckHeader.Text = "第一行为标题";
            // this.metroCheckHeader.UseVisualStyleBackColor = true;
            // 
            // metroCheckAllSheets
            // 
            this.metroCheckAllSheets.AutoSize = true;
            this.metroCheckAllSheets.Location = new System.Drawing.Point(120, 40);
            this.metroCheckAllSheets.Name = "metroCheckAllSheets";
            this.metroCheckAllSheets.Size = new System.Drawing.Size(98, 15);
            this.metroCheckAllSheets.TabIndex = 4;
            this.metroCheckAllSheets.Text = "导入所有工作表";
            // this.metroCheckAllSheets.UseVisualStyleBackColor = true;
            this.metroCheckAllSheets.CheckedChanged += new System.EventHandler(this.metroCheckAllSheets_CheckedChanged);
            // 
            // metroLabelSheets
            // 
            this.metroLabelSheets.AutoSize = true;
            this.metroLabelSheets.Location = new System.Drawing.Point(23, 160);
            this.metroLabelSheets.Name = "metroLabelSheets";
            this.metroLabelSheets.Size = new System.Drawing.Size(65, 19);
            this.metroLabelSheets.TabIndex = 5;
            this.metroLabelSheets.Text = "工作表:";
            // 
            // metroListSheets
            // 
            this.metroListSheets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroListSheets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.metroListSheets.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroListSheets.CheckOnClick = true;
            this.metroListSheets.ForeColor = System.Drawing.Color.White;
            this.metroListSheets.FormattingEnabled = true;
            this.metroListSheets.Location = new System.Drawing.Point(23, 190);
            this.metroListSheets.Name = "metroListSheets";
            this.metroListSheets.Size = new System.Drawing.Size(889, 242);
            this.metroListSheets.TabIndex = 6;
            // 
            // metroBtnImport
            // 
            this.metroBtnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.metroBtnImport.Highlight = true;
            this.metroBtnImport.Location = new System.Drawing.Point(807, 450);
            this.metroBtnImport.Name = "metroBtnImport";
            this.metroBtnImport.Size = new System.Drawing.Size(105, 40);
            this.metroBtnImport.TabIndex = 7;
            this.metroBtnImport.Text = "开始导入";
            this.metroBtnImport.Click += new System.EventHandler(this.metroBtnImport_Click);
            // 
            // metroProgressBar
            // 
            this.metroProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroProgressBar.Location = new System.Drawing.Point(23, 450);
            this.metroProgressBar.Name = "metroProgressBar";
            this.metroProgressBar.Size = new System.Drawing.Size(778, 20);
            this.metroProgressBar.TabIndex = 8;
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgress.Location = new System.Drawing.Point(23, 473);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(778, 19);
            this.lblProgress.TabIndex = 9;
            this.lblProgress.Text = "准备中...";
            // 
            // ImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 510);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.metroProgressBar);
            this.Controls.Add(this.metroBtnImport);
            this.Controls.Add(this.metroListSheets);
            this.Controls.Add(this.metroLabelSheets);
            this.Controls.Add(this.metroGroupOptions);
            this.Controls.Add(this.metroGroupFileType);
            this.Controls.Add(this.metroBtnBrowse);
            this.Controls.Add(this.metroTextFilePath);
            this.Controls.Add(this.metroLabelFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ImportForm";
            this.Text = "导入";
            this.Load += new System.EventHandler(this.ImportForm_Load);
            this.metroGroupFileType.ResumeLayout(false);
            this.metroGroupFileType.PerformLayout();
            this.metroGroupOptions.ResumeLayout(false);
            this.metroGroupOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabelFile;
        private MetroFramework.Controls.MetroTextBox metroTextFilePath;
        private MetroFramework.Controls.MetroButton metroBtnBrowse;
        private MetroFramework.Controls.MetroPanel metroGroupFileType;
        private MetroFramework.Controls.MetroRadioButton metroRadioCsv;
        private MetroFramework.Controls.MetroRadioButton metroRadioExcel;
        private MetroFramework.Controls.MetroLabel metroLabelFileType;
        private MetroFramework.Controls.MetroPanel metroGroupOptions;
        private MetroFramework.Controls.MetroCheckBox metroCheckAllSheets;
        private MetroFramework.Controls.MetroCheckBox metroCheckHeader;
        private MetroFramework.Controls.MetroLabel metroLabelOptions;
        private MetroFramework.Controls.MetroLabel metroLabelSheets;
        private System.Windows.Forms.CheckedListBox metroListSheets;
        private MetroFramework.Controls.MetroButton metroBtnImport;
        private MetroFramework.Controls.MetroProgressBar metroProgressBar;
        private MetroFramework.Controls.MetroLabel lblProgress;
    }
} 