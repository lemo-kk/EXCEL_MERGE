namespace ExcelToMerge.UI
{
    partial class ScheduleEditForm
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
            this.labelName = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelOutputPath = new System.Windows.Forms.Label();
            this.labelCronExpression = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.textBoxOutputPath = new System.Windows.Forms.TextBox();
            this.textBoxCronExpression = new System.Windows.Forms.TextBox();
            this.checkBoxActive = new System.Windows.Forms.CheckBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBoxBusinessDate = new System.Windows.Forms.GroupBox();
            this.dateTimePickerEndDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStartDate = new System.Windows.Forms.DateTimePicker();
            this.labelEndDate = new System.Windows.Forms.Label();
            this.labelStartDate = new System.Windows.Forms.Label();
            this.checkBoxUseEndDate = new System.Windows.Forms.CheckBox();
            this.checkBoxUseStartDate = new System.Windows.Forms.CheckBox();
            this.groupBoxBusinessDate.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 15);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(67, 15);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "调度名称";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(12, 46);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(67, 15);
            this.labelDescription.TabIndex = 1;
            this.labelDescription.Text = "调度描述";
            // 
            // labelOutputPath
            // 
            this.labelOutputPath.AutoSize = true;
            this.labelOutputPath.Location = new System.Drawing.Point(12, 117);
            this.labelOutputPath.Name = "labelOutputPath";
            this.labelOutputPath.Size = new System.Drawing.Size(67, 15);
            this.labelOutputPath.TabIndex = 2;
            this.labelOutputPath.Text = "输出路径";
            // 
            // labelCronExpression
            // 
            this.labelCronExpression.AutoSize = true;
            this.labelCronExpression.Location = new System.Drawing.Point(12, 148);
            this.labelCronExpression.Name = "labelCronExpression";
            this.labelCronExpression.Size = new System.Drawing.Size(79, 15);
            this.labelCronExpression.TabIndex = 3;
            this.labelCronExpression.Text = "Cron表达式";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(97, 12);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(351, 25);
            this.textBoxName.TabIndex = 4;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(97, 43);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(351, 65);
            this.textBoxDescription.TabIndex = 5;
            // 
            // textBoxOutputPath
            // 
            this.textBoxOutputPath.Location = new System.Drawing.Point(97, 114);
            this.textBoxOutputPath.Name = "textBoxOutputPath";
            this.textBoxOutputPath.Size = new System.Drawing.Size(270, 25);
            this.textBoxOutputPath.TabIndex = 6;
            // 
            // textBoxCronExpression
            // 
            this.textBoxCronExpression.Location = new System.Drawing.Point(97, 145);
            this.textBoxCronExpression.Name = "textBoxCronExpression";
            this.textBoxCronExpression.Size = new System.Drawing.Size(351, 25);
            this.textBoxCronExpression.TabIndex = 7;
            // 
            // checkBoxActive
            // 
            this.checkBoxActive.AutoSize = true;
            this.checkBoxActive.Checked = true;
            this.checkBoxActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxActive.Location = new System.Drawing.Point(97, 176);
            this.checkBoxActive.Name = "checkBoxActive";
            this.checkBoxActive.Size = new System.Drawing.Size(59, 19);
            this.checkBoxActive.TabIndex = 8;
            this.checkBoxActive.Text = "激活";
            this.checkBoxActive.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(373, 114);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 25);
            this.btnBrowse.TabIndex = 9;
            this.btnBrowse.Text = "浏览...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(292, 334);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(373, 334);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBoxBusinessDate
            // 
            this.groupBoxBusinessDate.Controls.Add(this.dateTimePickerEndDate);
            this.groupBoxBusinessDate.Controls.Add(this.dateTimePickerStartDate);
            this.groupBoxBusinessDate.Controls.Add(this.labelEndDate);
            this.groupBoxBusinessDate.Controls.Add(this.labelStartDate);
            this.groupBoxBusinessDate.Controls.Add(this.checkBoxUseEndDate);
            this.groupBoxBusinessDate.Controls.Add(this.checkBoxUseStartDate);
            this.groupBoxBusinessDate.Location = new System.Drawing.Point(15, 201);
            this.groupBoxBusinessDate.Name = "groupBoxBusinessDate";
            this.groupBoxBusinessDate.Size = new System.Drawing.Size(433, 117);
            this.groupBoxBusinessDate.TabIndex = 12;
            this.groupBoxBusinessDate.TabStop = false;
            this.groupBoxBusinessDate.Text = "业务日期";
            // 
            // dateTimePickerEndDate
            // 
            this.dateTimePickerEndDate.Enabled = false;
            this.dateTimePickerEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerEndDate.Location = new System.Drawing.Point(82, 81);
            this.dateTimePickerEndDate.Name = "dateTimePickerEndDate";
            this.dateTimePickerEndDate.Size = new System.Drawing.Size(200, 25);
            this.dateTimePickerEndDate.TabIndex = 5;
            // 
            // dateTimePickerStartDate
            // 
            this.dateTimePickerStartDate.Enabled = false;
            this.dateTimePickerStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerStartDate.Location = new System.Drawing.Point(82, 40);
            this.dateTimePickerStartDate.Name = "dateTimePickerStartDate";
            this.dateTimePickerStartDate.Size = new System.Drawing.Size(200, 25);
            this.dateTimePickerStartDate.TabIndex = 4;
            // 
            // labelEndDate
            // 
            this.labelEndDate.AutoSize = true;
            this.labelEndDate.Location = new System.Drawing.Point(7, 86);
            this.labelEndDate.Name = "labelEndDate";
            this.labelEndDate.Size = new System.Drawing.Size(67, 15);
            this.labelEndDate.TabIndex = 3;
            this.labelEndDate.Text = "结束日期";
            // 
            // labelStartDate
            // 
            this.labelStartDate.AutoSize = true;
            this.labelStartDate.Location = new System.Drawing.Point(7, 45);
            this.labelStartDate.Name = "labelStartDate";
            this.labelStartDate.Size = new System.Drawing.Size(67, 15);
            this.labelStartDate.TabIndex = 2;
            this.labelStartDate.Text = "开始日期";
            // 
            // checkBoxUseEndDate
            // 
            this.checkBoxUseEndDate.AutoSize = true;
            this.checkBoxUseEndDate.Location = new System.Drawing.Point(288, 84);
            this.checkBoxUseEndDate.Name = "checkBoxUseEndDate";
            this.checkBoxUseEndDate.Size = new System.Drawing.Size(89, 19);
            this.checkBoxUseEndDate.TabIndex = 1;
            this.checkBoxUseEndDate.Text = "使用日期";
            this.checkBoxUseEndDate.UseVisualStyleBackColor = true;
            this.checkBoxUseEndDate.CheckedChanged += new System.EventHandler(this.checkBoxUseEndDate_CheckedChanged);
            // 
            // checkBoxUseStartDate
            // 
            this.checkBoxUseStartDate.AutoSize = true;
            this.checkBoxUseStartDate.Location = new System.Drawing.Point(288, 43);
            this.checkBoxUseStartDate.Name = "checkBoxUseStartDate";
            this.checkBoxUseStartDate.Size = new System.Drawing.Size(89, 19);
            this.checkBoxUseStartDate.TabIndex = 0;
            this.checkBoxUseStartDate.Text = "使用日期";
            this.checkBoxUseStartDate.UseVisualStyleBackColor = true;
            this.checkBoxUseStartDate.CheckedChanged += new System.EventHandler(this.checkBoxUseStartDate_CheckedChanged);
            // 
            // ScheduleEditForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(462, 376);
            this.Controls.Add(this.groupBoxBusinessDate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.checkBoxActive);
            this.Controls.Add(this.textBoxCronExpression);
            this.Controls.Add(this.textBoxOutputPath);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelCronExpression);
            this.Controls.Add(this.labelOutputPath);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScheduleEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "调度编辑";
            this.Load += new System.EventHandler(this.ScheduleEditForm_Load);
            this.groupBoxBusinessDate.ResumeLayout(false);
            this.groupBoxBusinessDate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelOutputPath;
        private System.Windows.Forms.Label labelCronExpression;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.TextBox textBoxOutputPath;
        private System.Windows.Forms.TextBox textBoxCronExpression;
        private System.Windows.Forms.CheckBox checkBoxActive;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBoxBusinessDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartDate;
        private System.Windows.Forms.Label labelEndDate;
        private System.Windows.Forms.Label labelStartDate;
        private System.Windows.Forms.CheckBox checkBoxUseEndDate;
        private System.Windows.Forms.CheckBox checkBoxUseStartDate;
    }
} 