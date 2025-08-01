namespace ExcelToMerge.UI
{
    partial class ScheduleForm
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
            this.listViewSchedules = new System.Windows.Forms.ListView();
            this.columnHeaderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCronExpression = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLastExecutionDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTaskCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnNewSchedule = new System.Windows.Forms.Button();
            this.btnEditSchedule = new System.Windows.Forms.Button();
            this.btnDeleteSchedule = new System.Windows.Forms.Button();
            this.btnEditTasks = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnViewLogs = new System.Windows.Forms.Button();
            this.progressBarExecution = new System.Windows.Forms.ProgressBar();
            this.labelStatus = new System.Windows.Forms.Label();
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.panelToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewSchedules
            // 
            this.listViewSchedules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewSchedules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderId,
            this.columnHeaderName,
            this.columnHeaderDescription,
            this.columnHeaderCronExpression,
            this.columnHeaderLastExecutionDate,
            this.columnHeaderTaskCount});
            this.listViewSchedules.FullRowSelect = true;
            this.listViewSchedules.HideSelection = false;
            this.listViewSchedules.Location = new System.Drawing.Point(12, 62);
            this.listViewSchedules.MultiSelect = false;
            this.listViewSchedules.Name = "listViewSchedules";
            this.listViewSchedules.Size = new System.Drawing.Size(1026, 486);
            this.listViewSchedules.TabIndex = 0;
            this.listViewSchedules.UseCompatibleStateImageBehavior = false;
            this.listViewSchedules.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderId
            // 
            this.columnHeaderId.Text = "ID";
            this.columnHeaderId.Width = 50;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "名称";
            this.columnHeaderName.Width = 180;
            // 
            // columnHeaderDescription
            // 
            this.columnHeaderDescription.Text = "描述";
            this.columnHeaderDescription.Width = 250;
            // 
            // columnHeaderCronExpression
            // 
            this.columnHeaderCronExpression.Text = "Cron表达式";
            this.columnHeaderCronExpression.Width = 150;
            // 
            // columnHeaderLastExecutionDate
            // 
            this.columnHeaderLastExecutionDate.Text = "上次执行日期";
            this.columnHeaderLastExecutionDate.Width = 120;
            // 
            // columnHeaderTaskCount
            // 
            this.columnHeaderTaskCount.Text = "任务数";
            this.columnHeaderTaskCount.Width = 70;
            // 
            // btnNewSchedule
            // 
            this.btnNewSchedule.Location = new System.Drawing.Point(12, 12);
            this.btnNewSchedule.Name = "btnNewSchedule";
            this.btnNewSchedule.Size = new System.Drawing.Size(100, 35);
            this.btnNewSchedule.TabIndex = 1;
            this.btnNewSchedule.Text = "新增调度";
            this.btnNewSchedule.UseVisualStyleBackColor = true;
            this.btnNewSchedule.Click += new System.EventHandler(this.btnNewSchedule_Click);
            // 
            // btnEditSchedule
            // 
            this.btnEditSchedule.Location = new System.Drawing.Point(118, 12);
            this.btnEditSchedule.Name = "btnEditSchedule";
            this.btnEditSchedule.Size = new System.Drawing.Size(100, 35);
            this.btnEditSchedule.TabIndex = 2;
            this.btnEditSchedule.Text = "编辑调度";
            this.btnEditSchedule.UseVisualStyleBackColor = true;
            this.btnEditSchedule.Click += new System.EventHandler(this.btnEditSchedule_Click);
            // 
            // btnDeleteSchedule
            // 
            this.btnDeleteSchedule.Location = new System.Drawing.Point(224, 12);
            this.btnDeleteSchedule.Name = "btnDeleteSchedule";
            this.btnDeleteSchedule.Size = new System.Drawing.Size(100, 35);
            this.btnDeleteSchedule.TabIndex = 3;
            this.btnDeleteSchedule.Text = "删除调度";
            this.btnDeleteSchedule.UseVisualStyleBackColor = true;
            this.btnDeleteSchedule.Click += new System.EventHandler(this.btnDeleteSchedule_Click);
            // 
            // btnEditTasks
            // 
            this.btnEditTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditTasks.Location = new System.Drawing.Point(3, 3);
            this.btnEditTasks.Name = "btnEditTasks";
            this.btnEditTasks.Size = new System.Drawing.Size(100, 35);
            this.btnEditTasks.TabIndex = 4;
            this.btnEditTasks.Text = "编辑任务";
            this.btnEditTasks.UseVisualStyleBackColor = true;
            this.btnEditTasks.Click += new System.EventHandler(this.btnEditTasks_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExecute.Location = new System.Drawing.Point(109, 3);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(100, 35);
            this.btnExecute.TabIndex = 5;
            this.btnExecute.Text = "手动执行";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnViewLogs
            // 
            this.btnViewLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewLogs.Location = new System.Drawing.Point(215, 3);
            this.btnViewLogs.Name = "btnViewLogs";
            this.btnViewLogs.Size = new System.Drawing.Size(100, 35);
            this.btnViewLogs.TabIndex = 6;
            this.btnViewLogs.Text = "执行历史";
            this.btnViewLogs.UseVisualStyleBackColor = true;
            this.btnViewLogs.Click += new System.EventHandler(this.btnViewLogs_Click);
            // 
            // progressBarExecution
            // 
            this.progressBarExecution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarExecution.Location = new System.Drawing.Point(12, 575);
            this.progressBarExecution.Name = "progressBarExecution";
            this.progressBarExecution.Size = new System.Drawing.Size(1026, 23);
            this.progressBarExecution.TabIndex = 7;
            this.progressBarExecution.Visible = false;
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(12, 557);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(55, 15);
            this.labelStatus.TabIndex = 8;
            this.labelStatus.Text = "就绪";
            // 
            // panelToolbar
            // 
            this.panelToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolbar.Controls.Add(this.btnEditTasks);
            this.panelToolbar.Controls.Add(this.btnExecute);
            this.panelToolbar.Controls.Add(this.btnViewLogs);
            this.panelToolbar.Location = new System.Drawing.Point(720, 12);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(318, 41);
            this.panelToolbar.TabIndex = 9;
            // 
            // ScheduleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 610);
            this.Controls.Add(this.panelToolbar);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.progressBarExecution);
            this.Controls.Add(this.btnDeleteSchedule);
            this.Controls.Add(this.btnEditSchedule);
            this.Controls.Add(this.btnNewSchedule);
            this.Controls.Add(this.listViewSchedules);
            this.Name = "ScheduleForm";
            this.Text = "调度管理";
            this.Load += new System.EventHandler(this.ScheduleForm_Load);
            this.panelToolbar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewSchedules;
        private System.Windows.Forms.ColumnHeader columnHeaderId;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderDescription;
        private System.Windows.Forms.ColumnHeader columnHeaderTaskCount;
        private System.Windows.Forms.Button btnNewSchedule;
        private System.Windows.Forms.Button btnEditSchedule;
        private System.Windows.Forms.Button btnDeleteSchedule;
        private System.Windows.Forms.Button btnEditTasks;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnViewLogs;
        private System.Windows.Forms.ProgressBar progressBarExecution;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ColumnHeader columnHeaderCronExpression;
        private System.Windows.Forms.ColumnHeader columnHeaderLastExecutionDate;
        private System.Windows.Forms.Panel panelToolbar;
    }
} 