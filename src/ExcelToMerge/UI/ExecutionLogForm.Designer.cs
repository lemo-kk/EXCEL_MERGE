namespace ExcelToMerge.UI
{
    partial class ExecutionLogForm
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
            this.listViewLogs = new System.Windows.Forms.ListView();
            this.columnHeaderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderStartTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderEndTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDuration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnClose = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listViewLogs
            // 
            this.listViewLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewLogs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderId,
            this.columnHeaderStartTime,
            this.columnHeaderEndTime,
            this.columnHeaderDuration,
            this.columnHeaderStatus});
            this.listViewLogs.FullRowSelect = true;
            this.listViewLogs.GridLines = true;
            this.listViewLogs.HideSelection = false;
            this.listViewLogs.Location = new System.Drawing.Point(12, 41);
            this.listViewLogs.Name = "listViewLogs";
            this.listViewLogs.Size = new System.Drawing.Size(776, 358);
            this.listViewLogs.TabIndex = 0;
            this.listViewLogs.UseCompatibleStateImageBehavior = false;
            this.listViewLogs.View = System.Windows.Forms.View.Details;
            this.listViewLogs.DoubleClick += new System.EventHandler(this.listViewLogs_DoubleClick);
            // 
            // columnHeaderId
            // 
            this.columnHeaderId.Text = "ID";
            this.columnHeaderId.Width = 50;
            // 
            // columnHeaderStartTime
            // 
            this.columnHeaderStartTime.Text = "开始时间";
            this.columnHeaderStartTime.Width = 150;
            // 
            // columnHeaderEndTime
            // 
            this.columnHeaderEndTime.Text = "结束时间";
            this.columnHeaderEndTime.Width = 150;
            // 
            // columnHeaderDuration
            // 
            this.columnHeaderDuration.Text = "执行时长";
            this.columnHeaderDuration.Width = 100;
            // 
            // columnHeaderStatus
            // 
            this.columnHeaderStatus.Text = "状态";
            this.columnHeaderStatus.Width = 100;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(713, 415);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTitle.Location = new System.Drawing.Point(12, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(74, 22);
            this.labelTitle.TabIndex = 2;
            this.labelTitle.Text = "执行日志";
            // 
            // ExecutionLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.listViewLogs);
            this.Name = "ExecutionLogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "执行日志";
            this.Load += new System.EventHandler(this.ExecutionLogForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewLogs;
        private System.Windows.Forms.ColumnHeader columnHeaderId;
        private System.Windows.Forms.ColumnHeader columnHeaderStartTime;
        private System.Windows.Forms.ColumnHeader columnHeaderEndTime;
        private System.Windows.Forms.ColumnHeader columnHeaderDuration;
        private System.Windows.Forms.ColumnHeader columnHeaderStatus;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label labelTitle;
    }
} 