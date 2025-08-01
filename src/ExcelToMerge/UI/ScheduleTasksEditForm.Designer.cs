namespace ExcelToMerge.UI
{
    partial class ScheduleTasksEditForm
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
            this.listViewTasks = new System.Windows.Forms.ListView();
            this.columnHeaderSequence = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderFormat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAddTask = new System.Windows.Forms.Button();
            this.btnRemoveTask = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.labelTasks = new System.Windows.Forms.Label();
            this.labelHelp = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listViewTasks
            // 
            this.listViewTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewTasks.CheckBoxes = true;
            this.listViewTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderSequence,
            this.columnHeaderId,
            this.columnHeaderName,
            this.columnHeaderDescription,
            this.columnHeaderFormat});
            this.listViewTasks.FullRowSelect = true;
            this.listViewTasks.HideSelection = false;
            this.listViewTasks.Location = new System.Drawing.Point(12, 50);
            this.listViewTasks.Name = "listViewTasks";
            this.listViewTasks.Size = new System.Drawing.Size(676, 300);
            this.listViewTasks.TabIndex = 0;
            this.listViewTasks.UseCompatibleStateImageBehavior = false;
            this.listViewTasks.View = System.Windows.Forms.View.Details;
            this.listViewTasks.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewTasks_ItemChecked);
            // 
            // columnHeaderSequence
            // 
            this.columnHeaderSequence.Text = "序号";
            this.columnHeaderSequence.Width = 50;
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
            this.columnHeaderDescription.Width = 300;
            // 
            // columnHeaderFormat
            // 
            this.columnHeaderFormat.Text = "格式";
            this.columnHeaderFormat.Width = 70;
            // 
            // btnAddTask
            // 
            this.btnAddTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddTask.Location = new System.Drawing.Point(694, 50);
            this.btnAddTask.Name = "btnAddTask";
            this.btnAddTask.Size = new System.Drawing.Size(94, 30);
            this.btnAddTask.TabIndex = 1;
            this.btnAddTask.Text = "添加任务";
            this.btnAddTask.UseVisualStyleBackColor = true;
            this.btnAddTask.Click += new System.EventHandler(this.btnAddTask_Click);
            // 
            // btnRemoveTask
            // 
            this.btnRemoveTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveTask.Location = new System.Drawing.Point(694, 86);
            this.btnRemoveTask.Name = "btnRemoveTask";
            this.btnRemoveTask.Size = new System.Drawing.Size(94, 30);
            this.btnRemoveTask.TabIndex = 2;
            this.btnRemoveTask.Text = "移除任务";
            this.btnRemoveTask.UseVisualStyleBackColor = true;
            this.btnRemoveTask.Click += new System.EventHandler(this.btnRemoveTask_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveUp.Location = new System.Drawing.Point(694, 140);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(94, 30);
            this.btnMoveUp.TabIndex = 3;
            this.btnMoveUp.Text = "上移";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveDown.Location = new System.Drawing.Point(694, 176);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(94, 30);
            this.btnMoveDown.TabIndex = 4;
            this.btnMoveDown.Text = "下移";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(594, 358);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 30);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(694, 358);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelTasks
            // 
            this.labelTasks.AutoSize = true;
            this.labelTasks.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTasks.Location = new System.Drawing.Point(12, 15);
            this.labelTasks.Name = "labelTasks";
            this.labelTasks.Size = new System.Drawing.Size(88, 25);
            this.labelTasks.TabIndex = 7;
            this.labelTasks.Text = "任务列表";
            // 
            // labelHelp
            // 
            this.labelHelp.AutoSize = true;
            this.labelHelp.Location = new System.Drawing.Point(106, 21);
            this.labelHelp.Name = "labelHelp";
            this.labelHelp.Size = new System.Drawing.Size(337, 15);
            this.labelHelp.TabIndex = 8;
            this.labelHelp.Text = "（勾选表示启用任务，任务将按照序号顺序执行）";
            // 
            // ScheduleTasksEditForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.Controls.Add(this.labelHelp);
            this.Controls.Add(this.labelTasks);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.btnRemoveTask);
            this.Controls.Add(this.btnAddTask);
            this.Controls.Add(this.listViewTasks);
            this.MinimumSize = new System.Drawing.Size(600, 300);
            this.Name = "ScheduleTasksEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "编辑任务列表";
            this.Load += new System.EventHandler(this.ScheduleTasksEditForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewTasks;
        private System.Windows.Forms.ColumnHeader columnHeaderSequence;
        private System.Windows.Forms.ColumnHeader columnHeaderId;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderDescription;
        private System.Windows.Forms.ColumnHeader columnHeaderFormat;
        private System.Windows.Forms.Button btnAddTask;
        private System.Windows.Forms.Button btnRemoveTask;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label labelTasks;
        private System.Windows.Forms.Label labelHelp;
    }
} 