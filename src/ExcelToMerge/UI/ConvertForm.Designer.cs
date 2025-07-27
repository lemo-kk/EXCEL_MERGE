namespace ExcelToMerge.UI
{
    partial class ConvertForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConvertForm));
            this.panelLeft = new System.Windows.Forms.Panel();
            this.groupBoxTables = new System.Windows.Forms.GroupBox();
            this.treeViewDatabase = new System.Windows.Forms.TreeView();
            this.imageListTreeView = new System.Windows.Forms.ImageList(this.components);
            this.labelDatabaseTitle = new System.Windows.Forms.Label();
            this.panelRight = new System.Windows.Forms.Panel();
            this.panelRightBottom = new System.Windows.Forms.Panel();
            this.labelStatus = new System.Windows.Forms.Label();
            this.progressBarExecution = new System.Windows.Forms.ProgressBar();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.groupBoxOutput = new System.Windows.Forms.GroupBox();
            this.radioButtonCsv = new System.Windows.Forms.RadioButton();
            this.radioButtonExcel = new System.Windows.Forms.RadioButton();
            this.labelOutputPath = new System.Windows.Forms.Label();
            this.textBoxOutputPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnSqlTemplate = new System.Windows.Forms.Button();
            this.btnBatchExecute = new System.Windows.Forms.Button();
            this.panelRightTop = new System.Windows.Forms.Panel();
            this.groupBoxTasks = new System.Windows.Forms.GroupBox();
            this.listViewTasks = new System.Windows.Forms.ListView();
            this.columnHeaderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCreated = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.groupBoxResult = new System.Windows.Forms.GroupBox();
            this.dataGridViewResult = new System.Windows.Forms.DataGridView();
            this.splitterMain = new System.Windows.Forms.Splitter();
            this.groupBoxSql = new System.Windows.Forms.GroupBox();
            this.textBoxTaskDescription = new System.Windows.Forms.TextBox();
            this.labelTaskDescription = new System.Windows.Forms.Label();
            this.labelTaskName = new System.Windows.Forms.Label();
            this.textBoxTaskName = new System.Windows.Forms.TextBox();
            this.richTextBoxSql = new System.Windows.Forms.RichTextBox();
            this.panelLeft.SuspendLayout();
            this.groupBoxTables.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelRightBottom.SuspendLayout();
            this.groupBoxOutput.SuspendLayout();
            this.panelRightTop.SuspendLayout();
            this.groupBoxTasks.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.groupBoxResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResult)).BeginInit();
            this.groupBoxSql.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.groupBoxTables);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Padding = new System.Windows.Forms.Padding(5);
            this.panelLeft.Size = new System.Drawing.Size(200, 510);
            this.panelLeft.TabIndex = 0;
            // 
            // groupBoxTables
            // 
            this.groupBoxTables.Controls.Add(this.treeViewDatabase);
            this.groupBoxTables.Controls.Add(this.labelDatabaseTitle);
            this.groupBoxTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxTables.Location = new System.Drawing.Point(5, 5);
            this.groupBoxTables.Name = "groupBoxTables";
            this.groupBoxTables.Size = new System.Drawing.Size(190, 500);
            this.groupBoxTables.TabIndex = 0;
            this.groupBoxTables.TabStop = false;
            this.groupBoxTables.Text = "数据库浏览";
            // 
            // treeViewDatabase
            // 
            this.treeViewDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            // 移除ImageList的引用
            // this.treeViewDatabase.ImageList = this.imageListTreeView;
            this.treeViewDatabase.Location = new System.Drawing.Point(3, 35);
            this.treeViewDatabase.Name = "treeViewDatabase";
            this.treeViewDatabase.Size = new System.Drawing.Size(184, 462);
            this.treeViewDatabase.TabIndex = 1;
            this.treeViewDatabase.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewDatabase_NodeMouseClick);
            // 
            // imageListTreeView
            // 
            // 移除ImageStream的设置
            // this.imageListTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTreeView.ImageStream")));
            this.imageListTreeView.TransparentColor = System.Drawing.Color.Transparent;
            // 移除Images.Add调用
            // 
            // labelDatabaseTitle
            // 
            this.labelDatabaseTitle.AutoSize = true;
            this.labelDatabaseTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelDatabaseTitle.Location = new System.Drawing.Point(3, 17);
            this.labelDatabaseTitle.Name = "labelDatabaseTitle";
            this.labelDatabaseTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.labelDatabaseTitle.Size = new System.Drawing.Size(101, 17);
            this.labelDatabaseTitle.TabIndex = 0;
            this.labelDatabaseTitle.Text = "本地SQLite数据库";
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.panelRightBottom);
            this.panelRight.Controls.Add(this.panelRightTop);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Location = new System.Drawing.Point(735, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Padding = new System.Windows.Forms.Padding(5);
            this.panelRight.Size = new System.Drawing.Size(200, 510);
            this.panelRight.TabIndex = 1;
            // 
            // panelRightBottom
            // 
            this.panelRightBottom.Controls.Add(this.labelStatus);
            this.panelRightBottom.Controls.Add(this.progressBarExecution);
            this.panelRightBottom.Controls.Add(this.btnExecute);
            this.panelRightBottom.Controls.Add(this.btnSave);
            this.panelRightBottom.Controls.Add(this.btnExport);
            this.panelRightBottom.Controls.Add(this.btnSqlTemplate);
            this.panelRightBottom.Controls.Add(this.btnBatchExecute);
            this.panelRightBottom.Controls.Add(this.groupBoxOutput);
            this.panelRightBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRightBottom.Location = new System.Drawing.Point(5, 255);
            this.panelRightBottom.Name = "panelRightBottom";
            this.panelRightBottom.Size = new System.Drawing.Size(190, 250);
            this.panelRightBottom.TabIndex = 1;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(7, 238);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(29, 12);
            this.labelStatus.TabIndex = 5;
            this.labelStatus.Text = "就绪";
            // 
            // progressBarExecution
            // 
            this.progressBarExecution.Location = new System.Drawing.Point(3, 225);
            this.progressBarExecution.Name = "progressBarExecution";
            this.progressBarExecution.Size = new System.Drawing.Size(184, 10);
            this.progressBarExecution.TabIndex = 4;
            this.progressBarExecution.Visible = false;
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(3, 153);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(184, 30);
            this.btnExecute.TabIndex = 3;
            this.btnExecute.Text = "执行查询";
            this.btnExecute.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(3, 189);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存任务";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(99, 189);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(88, 30);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "导出数据";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // groupBoxOutput
            // 
            this.groupBoxOutput.Controls.Add(this.radioButtonCsv);
            this.groupBoxOutput.Controls.Add(this.radioButtonExcel);
            this.groupBoxOutput.Controls.Add(this.labelOutputPath);
            this.groupBoxOutput.Controls.Add(this.textBoxOutputPath);
            this.groupBoxOutput.Controls.Add(this.btnBrowse);
            this.groupBoxOutput.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxOutput.Location = new System.Drawing.Point(0, 0);
            this.groupBoxOutput.Name = "groupBoxOutput";
            this.groupBoxOutput.Size = new System.Drawing.Size(190, 147);
            this.groupBoxOutput.TabIndex = 0;
            this.groupBoxOutput.TabStop = false;
            this.groupBoxOutput.Text = "输出设置";
            // 
            // radioButtonCsv
            // 
            this.radioButtonCsv.AutoSize = true;
            this.radioButtonCsv.Location = new System.Drawing.Point(104, 20);
            this.radioButtonCsv.Name = "radioButtonCsv";
            this.radioButtonCsv.Size = new System.Drawing.Size(41, 16);
            this.radioButtonCsv.TabIndex = 4;
            this.radioButtonCsv.Text = "CSV";
            this.radioButtonCsv.UseVisualStyleBackColor = true;
            // 
            // radioButtonExcel
            // 
            this.radioButtonExcel.AutoSize = true;
            this.radioButtonExcel.Checked = true;
            this.radioButtonExcel.Location = new System.Drawing.Point(9, 20);
            this.radioButtonExcel.Name = "radioButtonExcel";
            this.radioButtonExcel.Size = new System.Drawing.Size(53, 16);
            this.radioButtonExcel.TabIndex = 3;
            this.radioButtonExcel.TabStop = true;
            this.radioButtonExcel.Text = "Excel";
            this.radioButtonExcel.UseVisualStyleBackColor = true;
            // 
            // labelOutputPath
            // 
            this.labelOutputPath.AutoSize = true;
            this.labelOutputPath.Location = new System.Drawing.Point(7, 49);
            this.labelOutputPath.Name = "labelOutputPath";
            this.labelOutputPath.Size = new System.Drawing.Size(65, 12);
            this.labelOutputPath.TabIndex = 2;
            this.labelOutputPath.Text = "输出路径：";
            // 
            // textBoxOutputPath
            // 
            this.textBoxOutputPath.Location = new System.Drawing.Point(9, 70);
            this.textBoxOutputPath.Name = "textBoxOutputPath";
            this.textBoxOutputPath.Size = new System.Drawing.Size(172, 21);
            this.textBoxOutputPath.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(9, 97);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(172, 30);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "浏览...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // btnSqlTemplate
            // 
            this.btnSqlTemplate.Location = new System.Drawing.Point(3, 117);
            this.btnSqlTemplate.Name = "btnSqlTemplate";
            this.btnSqlTemplate.Size = new System.Drawing.Size(90, 30);
            this.btnSqlTemplate.TabIndex = 6;
            this.btnSqlTemplate.Text = "SQL模板";
            this.btnSqlTemplate.UseVisualStyleBackColor = true;
            this.btnSqlTemplate.Click += new System.EventHandler(this.btnSqlTemplate_Click);
            // 
            // btnBatchExecute
            // 
            this.btnBatchExecute.Location = new System.Drawing.Point(99, 117);
            this.btnBatchExecute.Name = "btnBatchExecute";
            this.btnBatchExecute.Size = new System.Drawing.Size(88, 30);
            this.btnBatchExecute.TabIndex = 7;
            this.btnBatchExecute.Text = "批量执行";
            this.btnBatchExecute.UseVisualStyleBackColor = true;
            this.btnBatchExecute.Click += new System.EventHandler(this.btnBatchExecute_Click);
            // 
            // panelRightTop
            // 
            this.panelRightTop.Controls.Add(this.groupBoxTasks);
            this.panelRightTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRightTop.Location = new System.Drawing.Point(5, 5);
            this.panelRightTop.Name = "panelRightTop";
            this.panelRightTop.Size = new System.Drawing.Size(190, 250);
            this.panelRightTop.TabIndex = 0;
            // 
            // groupBoxTasks
            // 
            this.groupBoxTasks.Controls.Add(this.listViewTasks);
            this.groupBoxTasks.Controls.Add(this.btnNew);
            this.groupBoxTasks.Controls.Add(this.btnEdit);
            this.groupBoxTasks.Controls.Add(this.btnDelete);
            this.groupBoxTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxTasks.Location = new System.Drawing.Point(0, 0);
            this.groupBoxTasks.Name = "groupBoxTasks";
            this.groupBoxTasks.Size = new System.Drawing.Size(190, 250);
            this.groupBoxTasks.TabIndex = 0;
            this.groupBoxTasks.TabStop = false;
            this.groupBoxTasks.Text = "转换任务";
            // 
            // listViewTasks
            // 
            this.listViewTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderId,
            this.columnHeaderName,
            this.columnHeaderDescription,
            this.columnHeaderCreated});
            this.listViewTasks.FullRowSelect = true;
            this.listViewTasks.HideSelection = false;
            this.listViewTasks.Location = new System.Drawing.Point(3, 17);
            this.listViewTasks.MultiSelect = false;
            this.listViewTasks.Name = "listViewTasks";
            this.listViewTasks.Size = new System.Drawing.Size(184, 191);
            this.listViewTasks.TabIndex = 3;
            this.listViewTasks.UseCompatibleStateImageBehavior = false;
            this.listViewTasks.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderId
            // 
            this.columnHeaderId.Text = "ID";
            this.columnHeaderId.Width = 30;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "名称";
            this.columnHeaderName.Width = 100;
            // 
            // columnHeaderDescription
            // 
            this.columnHeaderDescription.Text = "描述";
            this.columnHeaderDescription.Width = 150;
            // 
            // columnHeaderCreated
            // 
            this.columnHeaderCreated.Text = "创建时间";
            this.columnHeaderCreated.Width = 120;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(3, 214);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(56, 30);
            this.btnNew.TabIndex = 2;
            this.btnNew.Text = "新建";
            this.btnNew.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(65, 214);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(56, 30);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "编辑";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(127, 214);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(56, 30);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.groupBoxResult);
            this.panelMain.Controls.Add(this.splitterMain);
            this.panelMain.Controls.Add(this.groupBoxSql);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(200, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(5);
            this.panelMain.Size = new System.Drawing.Size(535, 510);
            this.panelMain.TabIndex = 2;
            // 
            // groupBoxResult
            // 
            this.groupBoxResult.Controls.Add(this.dataGridViewResult);
            this.groupBoxResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxResult.Location = new System.Drawing.Point(5, 210);
            this.groupBoxResult.Name = "groupBoxResult";
            this.groupBoxResult.Size = new System.Drawing.Size(525, 295);
            this.groupBoxResult.TabIndex = 2;
            this.groupBoxResult.TabStop = false;
            this.groupBoxResult.Text = "查询结果";
            // 
            // dataGridViewResult
            // 
            this.dataGridViewResult.AllowUserToAddRows = false;
            this.dataGridViewResult.AllowUserToDeleteRows = false;
            this.dataGridViewResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewResult.Location = new System.Drawing.Point(3, 17);
            this.dataGridViewResult.Name = "dataGridViewResult";
            this.dataGridViewResult.ReadOnly = true;
            this.dataGridViewResult.RowTemplate.Height = 23;
            this.dataGridViewResult.Size = new System.Drawing.Size(519, 275);
            this.dataGridViewResult.TabIndex = 0;
            // 
            // splitterMain
            // 
            this.splitterMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterMain.Location = new System.Drawing.Point(5, 205);
            this.splitterMain.Name = "splitterMain";
            this.splitterMain.Size = new System.Drawing.Size(525, 5);
            this.splitterMain.TabIndex = 1;
            this.splitterMain.TabStop = false;
            // 
            // groupBoxSql
            // 
            this.groupBoxSql.Controls.Add(this.textBoxTaskDescription);
            this.groupBoxSql.Controls.Add(this.labelTaskDescription);
            this.groupBoxSql.Controls.Add(this.labelTaskName);
            this.groupBoxSql.Controls.Add(this.textBoxTaskName);
            this.groupBoxSql.Controls.Add(this.richTextBoxSql);
            this.groupBoxSql.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxSql.Location = new System.Drawing.Point(5, 5);
            this.groupBoxSql.Name = "groupBoxSql";
            this.groupBoxSql.Size = new System.Drawing.Size(525, 200);
            this.groupBoxSql.TabIndex = 0;
            this.groupBoxSql.TabStop = false;
            this.groupBoxSql.Text = "SQL转换脚本";
            // 
            // textBoxTaskDescription
            // 
            this.textBoxTaskDescription.Location = new System.Drawing.Point(77, 47);
            this.textBoxTaskDescription.Name = "textBoxTaskDescription";
            this.textBoxTaskDescription.Size = new System.Drawing.Size(442, 21);
            this.textBoxTaskDescription.TabIndex = 4;
            // 
            // labelTaskDescription
            // 
            this.labelTaskDescription.AutoSize = true;
            this.labelTaskDescription.Location = new System.Drawing.Point(6, 50);
            this.labelTaskDescription.Name = "labelTaskDescription";
            this.labelTaskDescription.Size = new System.Drawing.Size(65, 12);
            this.labelTaskDescription.TabIndex = 3;
            this.labelTaskDescription.Text = "任务描述：";
            // 
            // labelTaskName
            // 
            this.labelTaskName.AutoSize = true;
            this.labelTaskName.Location = new System.Drawing.Point(6, 23);
            this.labelTaskName.Name = "labelTaskName";
            this.labelTaskName.Size = new System.Drawing.Size(65, 12);
            this.labelTaskName.TabIndex = 2;
            this.labelTaskName.Text = "任务名称：";
            // 
            // textBoxTaskName
            // 
            this.textBoxTaskName.Location = new System.Drawing.Point(77, 20);
            this.textBoxTaskName.Name = "textBoxTaskName";
            this.textBoxTaskName.Size = new System.Drawing.Size(442, 21);
            this.textBoxTaskName.TabIndex = 1;
            // 
            // richTextBoxSql
            // 
            this.richTextBoxSql.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxSql.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxSql.Location = new System.Drawing.Point(6, 74);
            this.richTextBoxSql.Name = "richTextBoxSql";
            this.richTextBoxSql.Size = new System.Drawing.Size(513, 120);
            this.richTextBoxSql.TabIndex = 0;
            this.richTextBoxSql.Text = "";
            // 
            // ConvertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 510);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ConvertForm";
            this.Text = "转换";
            this.Load += new System.EventHandler(this.ConvertForm_Load);
            this.panelLeft.ResumeLayout(false);
            this.groupBoxTables.ResumeLayout(false);
            this.groupBoxTables.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.panelRightBottom.ResumeLayout(false);
            this.panelRightBottom.PerformLayout();
            this.groupBoxOutput.ResumeLayout(false);
            this.groupBoxOutput.PerformLayout();
            this.panelRightTop.ResumeLayout(false);
            this.groupBoxTasks.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.groupBoxSql.ResumeLayout(false);
            this.groupBoxSql.PerformLayout();
            this.groupBoxResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.GroupBox groupBoxTables;
        private System.Windows.Forms.TreeView treeViewDatabase;
        private System.Windows.Forms.Label labelDatabaseTitle;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelRightBottom;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ProgressBar progressBarExecution;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.GroupBox groupBoxOutput;
        private System.Windows.Forms.RadioButton radioButtonCsv;
        private System.Windows.Forms.RadioButton radioButtonExcel;
        private System.Windows.Forms.Label labelOutputPath;
        private System.Windows.Forms.TextBox textBoxOutputPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnSqlTemplate;
        private System.Windows.Forms.Button btnBatchExecute;
        private System.Windows.Forms.Panel panelRightTop;
        private System.Windows.Forms.GroupBox groupBoxTasks;
        private System.Windows.Forms.ListView listViewTasks;
        private System.Windows.Forms.ColumnHeader columnHeaderId;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderDescription;
        private System.Windows.Forms.ColumnHeader columnHeaderCreated;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.GroupBox groupBoxResult;
        private System.Windows.Forms.DataGridView dataGridViewResult;
        private System.Windows.Forms.Splitter splitterMain;
        private System.Windows.Forms.GroupBox groupBoxSql;
        private System.Windows.Forms.TextBox textBoxTaskDescription;
        private System.Windows.Forms.Label labelTaskDescription;
        private System.Windows.Forms.Label labelTaskName;
        private System.Windows.Forms.TextBox textBoxTaskName;
        private System.Windows.Forms.RichTextBox richTextBoxSql;
        private System.Windows.Forms.ImageList imageListTreeView;
    }
} 