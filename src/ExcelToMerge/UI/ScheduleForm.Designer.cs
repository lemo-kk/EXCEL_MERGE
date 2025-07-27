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
            this.columnHeaderId = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderDescription = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderTaskCount = new System.Windows.Forms.ColumnHeader();
            this.listViewTasks = new System.Windows.Forms.ListView();
            this.columnHeaderTaskId = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderTaskName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderTaskDescription = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderTaskFormat = new System.Windows.Forms.ColumnHeader();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.textBoxOutputPath = new System.Windows.Forms.TextBox();
            this.checkBoxActive = new System.Windows.Forms.CheckBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAddTask = new System.Windows.Forms.Button();
            this.btnRemoveTask = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.progressBarExecution = new System.Windows.Forms.ProgressBar();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelOutputPath = new System.Windows.Forms.Label();
            this.groupBoxSchedules = new System.Windows.Forms.GroupBox();
            this.groupBoxDetails = new System.Windows.Forms.GroupBox();
            this.groupBoxTasks = new System.Windows.Forms.GroupBox();
            this.groupBoxTimer = new System.Windows.Forms.GroupBox();
            this.dateTimePickerSchedule = new System.Windows.Forms.DateTimePicker();
            this.btnScheduleTimer = new System.Windows.Forms.Button();
            this.labelTimerStatus = new System.Windows.Forms.Label();
            this.btnViewLogs = new System.Windows.Forms.Button();
            this.groupBoxSchedules.SuspendLayout();
            this.groupBoxDetails.SuspendLayout();
            this.groupBoxTasks.SuspendLayout();
            this.groupBoxTimer.SuspendLayout();
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
            this.columnHeaderTaskCount});
            this.listViewSchedules.FullRowSelect = true;
            this.listViewSchedules.HideSelection = false;
            this.listViewSchedules.Location = new System.Drawing.Point(6, 20);
            this.listViewSchedules.MultiSelect = false;
            this.listViewSchedules.Name = "listViewSchedules";
            this.listViewSchedules.Size = new System.Drawing.Size(903, 150);
            this.listViewSchedules.TabIndex = 0;
            this.listViewSchedules.UseCompatibleStateImageBehavior = false;
            this.listViewSchedules.View = System.Windows.Forms.View.Details;
            this.listViewSchedules.SelectedIndexChanged += new System.EventHandler(this.listViewSchedules_SelectedIndexChanged);
            // 
            // columnHeaderId
            // 
            this.columnHeaderId.Text = "ID";
            this.columnHeaderId.Width = 50;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "名称";
            this.columnHeaderName.Width = 200;
            // 
            // columnHeaderDescription
            // 
            this.columnHeaderDescription.Text = "描述";
            this.columnHeaderDescription.Width = 300;
            // 
            // columnHeaderTaskCount
            // 
            this.columnHeaderTaskCount.Text = "任务数";
            this.columnHeaderTaskCount.Width = 70;
            // 
            // listViewTasks
            // 
            this.listViewTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTaskId,
            this.columnHeaderTaskName,
            this.columnHeaderTaskDescription,
            this.columnHeaderTaskFormat});
            this.listViewTasks.FullRowSelect = true;
            this.listViewTasks.HideSelection = false;
            this.listViewTasks.Location = new System.Drawing.Point(6, 20);
            this.listViewTasks.MultiSelect = false;
            this.listViewTasks.Name = "listViewTasks";
            this.listViewTasks.Size = new System.Drawing.Size(812, 150);
            this.listViewTasks.TabIndex = 0;
            this.listViewTasks.UseCompatibleStateImageBehavior = false;
            this.listViewTasks.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderTaskId
            // 
            this.columnHeaderTaskId.Text = "ID";
            this.columnHeaderTaskId.Width = 50;
            // 
            // columnHeaderTaskName
            // 
            this.columnHeaderTaskName.Text = "名称";
            this.columnHeaderTaskName.Width = 200;
            // 
            // columnHeaderTaskDescription
            // 
            this.columnHeaderTaskDescription.Text = "描述";
            this.columnHeaderTaskDescription.Width = 300;
            // 
            // columnHeaderTaskFormat
            // 
            this.columnHeaderTaskFormat.Text = "格式";
            this.columnHeaderTaskFormat.Width = 70;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(80, 20);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(200, 21);
            this.textBoxName.TabIndex = 0;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(80, 47);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(300, 50);
            this.textBoxDescription.TabIndex = 1;
            // 
            // textBoxOutputPath
            // 
            this.textBoxOutputPath.Location = new System.Drawing.Point(80, 103);
            this.textBoxOutputPath.Name = "textBoxOutputPath";
            this.textBoxOutputPath.Size = new System.Drawing.Size(300, 21);
            this.textBoxOutputPath.TabIndex = 2;
            // 
            // checkBoxActive
            // 
            this.checkBoxActive.AutoSize = true;
            this.checkBoxActive.Checked = true;
            this.checkBoxActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxActive.Location = new System.Drawing.Point(80, 130);
            this.checkBoxActive.Name = "checkBoxActive";
            this.checkBoxActive.Size = new System.Drawing.Size(48, 16);
            this.checkBoxActive.TabIndex = 4;
            this.checkBoxActive.Text = "激活";
            this.checkBoxActive.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Location = new System.Drawing.Point(915, 20);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 1;
            this.btnNew.Text = "新建";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(915, 49);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "编辑";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(915, 78);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAddTask
            // 
            this.btnAddTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddTask.Location = new System.Drawing.Point(824, 20);
            this.btnAddTask.Name = "btnAddTask";
            this.btnAddTask.Size = new System.Drawing.Size(75, 23);
            this.btnAddTask.TabIndex = 1;
            this.btnAddTask.Text = "添加";
            this.btnAddTask.UseVisualStyleBackColor = true;
            this.btnAddTask.Click += new System.EventHandler(this.btnAddTask_Click);
            // 
            // btnRemoveTask
            // 
            this.btnRemoveTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveTask.Location = new System.Drawing.Point(824, 49);
            this.btnRemoveTask.Name = "btnRemoveTask";
            this.btnRemoveTask.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveTask.TabIndex = 2;
            this.btnRemoveTask.Text = "移除";
            this.btnRemoveTask.UseVisualStyleBackColor = true;
            this.btnRemoveTask.Click += new System.EventHandler(this.btnRemoveTask_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveUp.Location = new System.Drawing.Point(824, 78);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(75, 23);
            this.btnMoveUp.TabIndex = 3;
            this.btnMoveUp.Text = "上移";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveDown.Location = new System.Drawing.Point(824, 107);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(75, 23);
            this.btnMoveDown.TabIndex = 4;
            this.btnMoveDown.Text = "下移";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(386, 103);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(386, 103);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "浏览...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExecute.Location = new System.Drawing.Point(824, 147);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 23);
            this.btnExecute.TabIndex = 5;
            this.btnExecute.Text = "执行";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // progressBarExecution
            // 
            this.progressBarExecution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarExecution.Location = new System.Drawing.Point(12, 485);
            this.progressBarExecution.Name = "progressBarExecution";
            this.progressBarExecution.Size = new System.Drawing.Size(911, 23);
            this.progressBarExecution.TabIndex = 5;
            this.progressBarExecution.Visible = false;
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(12, 470);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(41, 12);
            this.labelStatus.TabIndex = 6;
            this.labelStatus.Text = "就绪...";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(6, 23);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(41, 12);
            this.labelName.TabIndex = 6;
            this.labelName.Text = "名称：";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(6, 50);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(41, 12);
            this.labelDescription.TabIndex = 7;
            this.labelDescription.Text = "描述：";
            // 
            // labelOutputPath
            // 
            this.labelOutputPath.AutoSize = true;
            this.labelOutputPath.Location = new System.Drawing.Point(6, 106);
            this.labelOutputPath.Name = "labelOutputPath";
            this.labelOutputPath.Size = new System.Drawing.Size(65, 12);
            this.labelOutputPath.TabIndex = 8;
            this.labelOutputPath.Text = "输出路径：";
            // 
            // groupBoxSchedules
            // 
            this.groupBoxSchedules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSchedules.Controls.Add(this.listViewSchedules);
            this.groupBoxSchedules.Controls.Add(this.btnNew);
            this.groupBoxSchedules.Controls.Add(this.btnEdit);
            this.groupBoxSchedules.Controls.Add(this.btnDelete);
            this.groupBoxSchedules.Location = new System.Drawing.Point(12, 12);
            this.groupBoxSchedules.Name = "groupBoxSchedules";
            this.groupBoxSchedules.Size = new System.Drawing.Size(996, 176);
            this.groupBoxSchedules.TabIndex = 0;
            this.groupBoxSchedules.TabStop = false;
            this.groupBoxSchedules.Text = "调度任务列表";
            // 
            // groupBoxDetails
            // 
            this.groupBoxDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDetails.Controls.Add(this.labelName);
            this.groupBoxDetails.Controls.Add(this.labelDescription);
            this.groupBoxDetails.Controls.Add(this.labelOutputPath);
            this.groupBoxDetails.Controls.Add(this.textBoxName);
            this.groupBoxDetails.Controls.Add(this.textBoxDescription);
            this.groupBoxDetails.Controls.Add(this.textBoxOutputPath);
            this.groupBoxDetails.Controls.Add(this.checkBoxActive);
            this.groupBoxDetails.Controls.Add(this.btnBrowse);
            this.groupBoxDetails.Controls.Add(this.btnSave);
            this.groupBoxDetails.Location = new System.Drawing.Point(12, 194);
            this.groupBoxDetails.Name = "groupBoxDetails";
            this.groupBoxDetails.Size = new System.Drawing.Size(996, 160);
            this.groupBoxDetails.TabIndex = 1;
            this.groupBoxDetails.TabStop = false;
            this.groupBoxDetails.Text = "任务详情";
            // 
            // groupBoxTasks
            // 
            this.groupBoxTasks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTasks.Controls.Add(this.listViewTasks);
            this.groupBoxTasks.Controls.Add(this.btnAddTask);
            this.groupBoxTasks.Controls.Add(this.btnRemoveTask);
            this.groupBoxTasks.Controls.Add(this.btnMoveUp);
            this.groupBoxTasks.Controls.Add(this.btnMoveDown);
            this.groupBoxTasks.Location = new System.Drawing.Point(12, 312);
            this.groupBoxTasks.Name = "groupBoxTasks";
            this.groupBoxTasks.Size = new System.Drawing.Size(915, 200);
            this.groupBoxTasks.TabIndex = 11;
            this.groupBoxTasks.TabStop = false;
            this.groupBoxTasks.Text = "转换任务";
            // 
            // groupBoxTimer
            // 
            this.groupBoxTimer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTimer.Controls.Add(this.dateTimePickerSchedule);
            this.groupBoxTimer.Controls.Add(this.btnScheduleTimer);
            this.groupBoxTimer.Controls.Add(this.labelTimerStatus);
            this.groupBoxTimer.Controls.Add(this.btnViewLogs);
            this.groupBoxTimer.Location = new System.Drawing.Point(12, 518);
            this.groupBoxTimer.Name = "groupBoxTimer";
            this.groupBoxTimer.Size = new System.Drawing.Size(915, 60);
            this.groupBoxTimer.TabIndex = 12;
            this.groupBoxTimer.TabStop = false;
            this.groupBoxTimer.Text = "定时执行";
            // 
            // dateTimePickerSchedule
            // 
            this.dateTimePickerSchedule.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePickerSchedule.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerSchedule.Location = new System.Drawing.Point(6, 24);
            this.dateTimePickerSchedule.Name = "dateTimePickerSchedule";
            this.dateTimePickerSchedule.Size = new System.Drawing.Size(200, 21);
            this.dateTimePickerSchedule.TabIndex = 0;
            // 
            // btnScheduleTimer
            // 
            this.btnScheduleTimer.Location = new System.Drawing.Point(212, 22);
            this.btnScheduleTimer.Name = "btnScheduleTimer";
            this.btnScheduleTimer.Size = new System.Drawing.Size(100, 23);
            this.btnScheduleTimer.TabIndex = 1;
            this.btnScheduleTimer.Text = "定时执行";
            this.btnScheduleTimer.UseVisualStyleBackColor = true;
            // 
            // labelTimerStatus
            // 
            this.labelTimerStatus.AutoSize = true;
            this.labelTimerStatus.Location = new System.Drawing.Point(318, 27);
            this.labelTimerStatus.Name = "labelTimerStatus";
            this.labelTimerStatus.Size = new System.Drawing.Size(89, 12);
            this.labelTimerStatus.TabIndex = 2;
            this.labelTimerStatus.Text = "未设置定时执行";
            // 
            // btnViewLogs
            // 
            this.btnViewLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewLogs.Location = new System.Drawing.Point(809, 22);
            this.btnViewLogs.Name = "btnViewLogs";
            this.btnViewLogs.Size = new System.Drawing.Size(100, 23);
            this.btnViewLogs.TabIndex = 3;
            this.btnViewLogs.Text = "查看执行日志";
            this.btnViewLogs.UseVisualStyleBackColor = true;
            // 
            // ScheduleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 618);
            this.Controls.Add(this.groupBoxTimer);
            this.Controls.Add(this.groupBoxTasks);
            this.Controls.Add(this.groupBoxDetails);
            this.Controls.Add(this.groupBoxSchedules);
            this.Controls.Add(this.progressBarExecution);
            this.Controls.Add(this.labelStatus);
            this.Name = "ScheduleForm";
            this.Text = "调度";
            this.Load += new System.EventHandler(this.ScheduleForm_Load);
            this.groupBoxSchedules.ResumeLayout(false);
            this.groupBoxDetails.ResumeLayout(false);
            this.groupBoxDetails.PerformLayout();
            this.groupBoxTasks.ResumeLayout(false);
            this.groupBoxTimer.ResumeLayout(false);
            this.groupBoxTimer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewSchedules;
        private System.Windows.Forms.ListView listViewTasks;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.TextBox textBoxOutputPath;
        private System.Windows.Forms.CheckBox checkBoxActive;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAddTask;
        private System.Windows.Forms.Button btnRemoveTask;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.ProgressBar progressBarExecution;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ColumnHeader columnHeaderId;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderDescription;
        private System.Windows.Forms.ColumnHeader columnHeaderTaskCount;
        private System.Windows.Forms.ColumnHeader columnHeaderTaskId;
        private System.Windows.Forms.ColumnHeader columnHeaderTaskName;
        private System.Windows.Forms.ColumnHeader columnHeaderTaskDescription;
        private System.Windows.Forms.ColumnHeader columnHeaderTaskFormat;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelOutputPath;
        private System.Windows.Forms.GroupBox groupBoxSchedules;
        private System.Windows.Forms.GroupBox groupBoxDetails;
        private System.Windows.Forms.GroupBox groupBoxTasks;
        private System.Windows.Forms.GroupBox groupBoxTimer;
        private System.Windows.Forms.DateTimePicker dateTimePickerSchedule;
        private System.Windows.Forms.Button btnScheduleTimer;
        private System.Windows.Forms.Label labelTimerStatus;
        private System.Windows.Forms.Button btnViewLogs;
    }
} 