namespace ExcelToMerge.UI
{
    partial class SqlTemplateForm
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.listViewTemplates = new System.Windows.Forms.ListView();
            this.columnHeaderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCategory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSystem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.panelDetails = new System.Windows.Forms.Panel();
            this.richTextBoxSql = new System.Windows.Forms.RichTextBox();
            this.labelSql = new System.Windows.Forms.Label();
            this.checkBoxSystem = new System.Windows.Forms.CheckBox();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.labelCategory = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.listViewTemplates);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panelButtons);
            this.splitContainer.Panel2.Controls.Add(this.panelDetails);
            this.splitContainer.Size = new System.Drawing.Size(800, 450);
            this.splitContainer.SplitterDistance = 350;
            this.splitContainer.TabIndex = 0;
            // 
            // listViewTemplates
            // 
            this.listViewTemplates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderId,
            this.columnHeaderName,
            this.columnHeaderCategory,
            this.columnHeaderDescription,
            this.columnHeaderSystem});
            this.listViewTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewTemplates.FullRowSelect = true;
            this.listViewTemplates.HideSelection = false;
            this.listViewTemplates.Location = new System.Drawing.Point(0, 0);
            this.listViewTemplates.MultiSelect = false;
            this.listViewTemplates.Name = "listViewTemplates";
            this.listViewTemplates.Size = new System.Drawing.Size(350, 450);
            this.listViewTemplates.TabIndex = 0;
            this.listViewTemplates.UseCompatibleStateImageBehavior = false;
            this.listViewTemplates.View = System.Windows.Forms.View.Details;
            this.listViewTemplates.SelectedIndexChanged += new System.EventHandler(this.listViewTemplates_SelectedIndexChanged);
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
            // columnHeaderCategory
            // 
            this.columnHeaderCategory.Text = "分类";
            this.columnHeaderCategory.Width = 80;
            // 
            // columnHeaderDescription
            // 
            this.columnHeaderDescription.Text = "描述";
            this.columnHeaderDescription.Width = 120;
            // 
            // columnHeaderSystem
            // 
            this.columnHeaderSystem.Text = "系统模板";
            this.columnHeaderSystem.Width = 60;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnApply);
            this.panelButtons.Controls.Add(this.btnCancel);
            this.panelButtons.Controls.Add(this.btnSave);
            this.panelButtons.Controls.Add(this.btnDelete);
            this.panelButtons.Controls.Add(this.btnEdit);
            this.panelButtons.Controls.Add(this.btnNew);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 410);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(446, 40);
            this.panelButtons.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(291, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(210, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(129, 8);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(86, 8);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(37, 25);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "编辑";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(5, 8);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 25);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "新建";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // panelDetails
            // 
            this.panelDetails.Controls.Add(this.richTextBoxSql);
            this.panelDetails.Controls.Add(this.labelSql);
            this.panelDetails.Controls.Add(this.checkBoxSystem);
            this.panelDetails.Controls.Add(this.comboBoxCategory);
            this.panelDetails.Controls.Add(this.labelCategory);
            this.panelDetails.Controls.Add(this.textBoxDescription);
            this.panelDetails.Controls.Add(this.labelDescription);
            this.panelDetails.Controls.Add(this.textBoxName);
            this.panelDetails.Controls.Add(this.labelName);
            this.panelDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetails.Location = new System.Drawing.Point(0, 0);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.Size = new System.Drawing.Size(446, 450);
            this.panelDetails.TabIndex = 0;
            // 
            // richTextBoxSql
            // 
            this.richTextBoxSql.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxSql.Enabled = false;
            this.richTextBoxSql.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxSql.Location = new System.Drawing.Point(5, 118);
            this.richTextBoxSql.Name = "richTextBoxSql";
            this.richTextBoxSql.Size = new System.Drawing.Size(436, 286);
            this.richTextBoxSql.TabIndex = 8;
            this.richTextBoxSql.Text = "";
            // 
            // labelSql
            // 
            this.labelSql.AutoSize = true;
            this.labelSql.Location = new System.Drawing.Point(3, 103);
            this.labelSql.Name = "labelSql";
            this.labelSql.Size = new System.Drawing.Size(65, 12);
            this.labelSql.TabIndex = 7;
            this.labelSql.Text = "SQL脚本：";
            // 
            // checkBoxSystem
            // 
            this.checkBoxSystem.AutoSize = true;
            this.checkBoxSystem.Enabled = false;
            this.checkBoxSystem.Location = new System.Drawing.Point(365, 78);
            this.checkBoxSystem.Name = "checkBoxSystem";
            this.checkBoxSystem.Size = new System.Drawing.Size(72, 16);
            this.checkBoxSystem.TabIndex = 6;
            this.checkBoxSystem.Text = "系统模板";
            this.checkBoxSystem.UseVisualStyleBackColor = true;
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.Enabled = false;
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new System.Drawing.Point(74, 76);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(285, 20);
            this.comboBoxCategory.TabIndex = 5;
            // 
            // labelCategory
            // 
            this.labelCategory.AutoSize = true;
            this.labelCategory.Location = new System.Drawing.Point(3, 79);
            this.labelCategory.Name = "labelCategory";
            this.labelCategory.Size = new System.Drawing.Size(65, 12);
            this.labelCategory.TabIndex = 4;
            this.labelCategory.Text = "分    类：";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Enabled = false;
            this.textBoxDescription.Location = new System.Drawing.Point(74, 42);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(367, 21);
            this.textBoxDescription.TabIndex = 3;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(3, 45);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(65, 12);
            this.labelDescription.TabIndex = 2;
            this.labelDescription.Text = "描    述：";
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Enabled = false;
            this.textBoxName.Location = new System.Drawing.Point(74, 12);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(367, 21);
            this.textBoxName.TabIndex = 1;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(3, 15);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(65, 12);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "名    称：";
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(372, 8);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(69, 25);
            this.btnApply.TabIndex = 5;
            this.btnApply.Text = "应用模板";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // SqlTemplateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer);
            this.Name = "SqlTemplateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SQL模板管理";
            this.Load += new System.EventHandler(this.SqlTemplateForm_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.panelDetails.ResumeLayout(false);
            this.panelDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ListView listViewTemplates;
        private System.Windows.Forms.ColumnHeader columnHeaderId;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderCategory;
        private System.Windows.Forms.ColumnHeader columnHeaderDescription;
        private System.Windows.Forms.ColumnHeader columnHeaderSystem;
        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.Label labelCategory;
        private System.Windows.Forms.CheckBox checkBoxSystem;
        private System.Windows.Forms.RichTextBox richTextBoxSql;
        private System.Windows.Forms.Label labelSql;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnApply;
    }
} 