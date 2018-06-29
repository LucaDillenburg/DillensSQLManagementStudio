namespace DillenManagementStudio
{
    partial class FrmDillenSQLManagementStudio
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDillenSQLManagementStudio));
            this.grvSelect = new System.Windows.Forms.DataGridView();
            this.btnExecute = new System.Windows.Forms.Button();
            this.rdSelect = new System.Windows.Forms.RadioButton();
            this.rdNonQuery = new System.Windows.Forms.RadioButton();
            this.btnChangeDtBs = new System.Windows.Forms.Button();
            this.cbxChsDtBs = new System.Windows.Forms.ComboBox();
            this.rdAutomatic = new System.Windows.Forms.RadioButton();
            this.btnOtherDtBs = new System.Windows.Forms.Button();
            this.lbTitle = new System.Windows.Forms.Label();
            this.btnBiggerFont = new System.Windows.Forms.Button();
            this.btnSmallerFont = new System.Windows.Forms.Button();
            this.btnAllTables = new System.Windows.Forms.Button();
            this.btnAllProcFunc = new System.Windows.Forms.Button();
            this.lbExecutionResult = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allCommandsSintaxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rchtxtCode = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.grvSelect)).BeginInit();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // grvSelect
            // 
            this.grvSelect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.grvSelect, "grvSelect");
            this.grvSelect.Name = "grvSelect";
            this.grvSelect.ReadOnly = true;
            // 
            // btnExecute
            // 
            resources.ApplyResources(this.btnExecute, "btnExecute");
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // rdSelect
            // 
            resources.ApplyResources(this.rdSelect, "rdSelect");
            this.rdSelect.Name = "rdSelect";
            this.rdSelect.TabStop = true;
            this.rdSelect.UseVisualStyleBackColor = true;
            this.rdSelect.CheckedChanged += new System.EventHandler(this.rdSelect_CheckedChanged);
            // 
            // rdNonQuery
            // 
            resources.ApplyResources(this.rdNonQuery, "rdNonQuery");
            this.rdNonQuery.Name = "rdNonQuery";
            this.rdNonQuery.TabStop = true;
            this.rdNonQuery.UseVisualStyleBackColor = true;
            this.rdNonQuery.CheckedChanged += new System.EventHandler(this.rdSelect_CheckedChanged);
            // 
            // btnChangeDtBs
            // 
            resources.ApplyResources(this.btnChangeDtBs, "btnChangeDtBs");
            this.btnChangeDtBs.Name = "btnChangeDtBs";
            this.btnChangeDtBs.UseVisualStyleBackColor = true;
            this.btnChangeDtBs.Click += new System.EventHandler(this.btnChangeDtBs_Click);
            // 
            // cbxChsDtBs
            // 
            this.cbxChsDtBs.FormattingEnabled = true;
            this.cbxChsDtBs.Items.AddRange(new object[] {
            resources.GetString("cbxChsDtBs.Items"),
            resources.GetString("cbxChsDtBs.Items1")});
            resources.ApplyResources(this.cbxChsDtBs, "cbxChsDtBs");
            this.cbxChsDtBs.Name = "cbxChsDtBs";
            // 
            // rdAutomatic
            // 
            resources.ApplyResources(this.rdAutomatic, "rdAutomatic");
            this.rdAutomatic.Checked = true;
            this.rdAutomatic.Name = "rdAutomatic";
            this.rdAutomatic.TabStop = true;
            this.rdAutomatic.UseVisualStyleBackColor = true;
            this.rdAutomatic.CheckedChanged += new System.EventHandler(this.rdSelect_CheckedChanged);
            // 
            // btnOtherDtBs
            // 
            resources.ApplyResources(this.btnOtherDtBs, "btnOtherDtBs");
            this.btnOtherDtBs.Name = "btnOtherDtBs";
            this.btnOtherDtBs.UseVisualStyleBackColor = true;
            // 
            // lbTitle
            // 
            resources.ApplyResources(this.lbTitle, "lbTitle");
            this.lbTitle.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lbTitle.Name = "lbTitle";
            // 
            // btnBiggerFont
            // 
            resources.ApplyResources(this.btnBiggerFont, "btnBiggerFont");
            this.btnBiggerFont.Name = "btnBiggerFont";
            this.btnBiggerFont.UseVisualStyleBackColor = true;
            // 
            // btnSmallerFont
            // 
            this.btnSmallerFont.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnSmallerFont, "btnSmallerFont");
            this.btnSmallerFont.Name = "btnSmallerFont";
            this.btnSmallerFont.UseVisualStyleBackColor = true;
            // 
            // btnAllTables
            // 
            resources.ApplyResources(this.btnAllTables, "btnAllTables");
            this.btnAllTables.Name = "btnAllTables";
            this.btnAllTables.UseVisualStyleBackColor = true;
            this.btnAllTables.Click += new System.EventHandler(this.btnAllTables_Click);
            // 
            // btnAllProcFunc
            // 
            resources.ApplyResources(this.btnAllProcFunc, "btnAllProcFunc");
            this.btnAllProcFunc.Name = "btnAllProcFunc";
            this.btnAllProcFunc.UseVisualStyleBackColor = true;
            this.btnAllProcFunc.Click += new System.EventHandler(this.btnAllProcFunc_Click);
            // 
            // lbExecutionResult
            // 
            resources.ApplyResources(this.lbExecutionResult, "lbExecutionResult");
            this.lbExecutionResult.ForeColor = System.Drawing.Color.Green;
            this.lbExecutionResult.Name = "lbExecutionResult";
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.CornflowerBlue;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.allCommandsSintaxToolStripMenuItem});
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Name = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            resources.ApplyResources(this.newToolStripMenuItem, "newToolStripMenuItem");
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            resources.ApplyResources(this.saveAsToolStripMenuItem, "saveAsToolStripMenuItem");
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            resources.ApplyResources(this.closeToolStripMenuItem, "closeToolStripMenuItem");
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // allCommandsSintaxToolStripMenuItem
            // 
            this.allCommandsSintaxToolStripMenuItem.Name = "allCommandsSintaxToolStripMenuItem";
            resources.ApplyResources(this.allCommandsSintaxToolStripMenuItem, "allCommandsSintaxToolStripMenuItem");
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Lavender;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackColor = System.Drawing.Color.Lavender;
            resources.ApplyResources(this.btnMinimize, "btnMinimize");
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DillenManagementStudio.Properties.Resources.wolf1;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // rchtxtCode
            // 
            this.rchtxtCode.AcceptsTab = true;
            resources.ApplyResources(this.rchtxtCode, "rchtxtCode");
            this.rchtxtCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.rchtxtCode.Name = "rchtxtCode";
            this.rchtxtCode.TextChanged += new System.EventHandler(this.rchtxtCode_TextChanged);
            this.rchtxtCode.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.rchtxtCode_PreviewKeyDown);
            // 
            // FrmDillenSQLManagementStudio
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbExecutionResult);
            this.Controls.Add(this.btnAllProcFunc);
            this.Controls.Add(this.btnAllTables);
            this.Controls.Add(this.btnSmallerFont);
            this.Controls.Add(this.btnBiggerFont);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.btnOtherDtBs);
            this.Controls.Add(this.rdAutomatic);
            this.Controls.Add(this.grvSelect);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.rchtxtCode);
            this.Controls.Add(this.rdSelect);
            this.Controls.Add(this.rdNonQuery);
            this.Controls.Add(this.btnChangeDtBs);
            this.Controls.Add(this.cbxChsDtBs);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmDillenSQLManagementStudio";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.FrmDillenSQLManagementStudio_Click);
            ((System.ComponentModel.ISupportInitialize)(this.grvSelect)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grvSelect;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.RadioButton rdSelect;
        private System.Windows.Forms.RadioButton rdNonQuery;
        private System.Windows.Forms.Button btnChangeDtBs;
        private System.Windows.Forms.ComboBox cbxChsDtBs;
        private System.Windows.Forms.RadioButton rdAutomatic;
        private System.Windows.Forms.Button btnOtherDtBs;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Button btnBiggerFont;
        private System.Windows.Forms.Button btnSmallerFont;
        private System.Windows.Forms.Button btnAllTables;
        private System.Windows.Forms.Button btnAllProcFunc;
        private System.Windows.Forms.Label lbExecutionResult;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allCommandsSintaxToolStripMenuItem;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.RichTextBox rchtxtCode;
    }
}

