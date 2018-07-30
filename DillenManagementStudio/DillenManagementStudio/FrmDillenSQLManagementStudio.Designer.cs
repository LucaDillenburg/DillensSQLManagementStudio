using System.Windows.Forms;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDillenSQLManagementStudio));
            this.grvSelect = new System.Windows.Forms.DataGridView();
            this.lbTitle = new System.Windows.Forms.Label();
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
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.changeDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allCommandsSintaxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.allowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.smallerRchtxtFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largerRchtxtFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.executeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.automaticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeNonQueryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.lbDatabase = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.lbLoading = new System.Windows.Forms.Label();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rchtxtCode = new System.Windows.Forms.RichTextBox();
            this.pnlLoading = new System.Windows.Forms.Panel();
            this.rchtxtAux = new System.Windows.Forms.RichTextBox();
            this.tmrCheckVPNConn = new System.Windows.Forms.Timer(this.components);
            this.lbTableName = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.btnCloseFindReplace = new System.Windows.Forms.Button();
            this.btnNotSeeReplace = new System.Windows.Forms.Button();
            this.btnSeeReplace = new System.Windows.Forms.Button();
            this.btnReplaceAll = new System.Windows.Forms.Button();
            this.chxIgnoreCase = new System.Windows.Forms.CheckBox();
            this.btnReplaceCurr = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.txtReplace = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grvSelect)).BeginInit();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlLoading.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // grvSelect
            // 
            this.grvSelect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.grvSelect, "grvSelect");
            this.grvSelect.Name = "grvSelect";
            this.grvSelect.ReadOnly = true;
            this.grvSelect.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvSelect_CellContentDoubleClick);
            // 
            // lbTitle
            // 
            resources.ApplyResources(this.lbTitle, "lbTitle");
            this.lbTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(45)))));
            this.lbTitle.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbTitle.Name = "lbTitle";
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
            this.menuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(45)))));
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItem2,
            this.saveToolStripMenuItem1,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripMenuItem4,
            this.changeDatabaseToolStripMenuItem,
            this.allCommandsSintaxToolStripMenuItem,
            this.toolStripMenuItem1,
            this.allowToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolStripMenuItem5,
            this.smallerRchtxtFontToolStripMenuItem,
            this.largerRchtxtFontToolStripMenuItem,
            this.toolStripMenuItem6,
            this.executeToolStripMenuItem,
            this.executeAsToolStripMenuItem});
            this.menuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.ShowItemToolTips = true;
            this.menuStrip.TabStop = true;
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
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            resources.ApplyResources(this.newToolStripMenuItem, "newToolStripMenuItem");
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
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
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            resources.ApplyResources(this.saveAsToolStripMenuItem, "saveAsToolStripMenuItem");
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
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
            // toolStripMenuItem2
            // 
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            this.toolStripMenuItem2.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.ForeColor = System.Drawing.SystemColors.Control;
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            resources.ApplyResources(this.saveToolStripMenuItem1, "saveToolStripMenuItem1");
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            resources.ApplyResources(this.undoToolStripMenuItem, "undoToolStripMenuItem");
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            resources.ApplyResources(this.redoToolStripMenuItem, "redoToolStripMenuItem");
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
            this.toolStripMenuItem4.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            // 
            // changeDatabaseToolStripMenuItem
            // 
            resources.ApplyResources(this.changeDatabaseToolStripMenuItem, "changeDatabaseToolStripMenuItem");
            this.changeDatabaseToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.changeDatabaseToolStripMenuItem.Name = "changeDatabaseToolStripMenuItem";
            this.changeDatabaseToolStripMenuItem.Click += new System.EventHandler(this.changeDatabaseToolStripMenuItem_Click);
            // 
            // allCommandsSintaxToolStripMenuItem
            // 
            this.allCommandsSintaxToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(45)))));
            this.allCommandsSintaxToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.allCommandsSintaxToolStripMenuItem.Name = "allCommandsSintaxToolStripMenuItem";
            resources.ApplyResources(this.allCommandsSintaxToolStripMenuItem, "allCommandsSintaxToolStripMenuItem");
            // 
            // toolStripMenuItem1
            // 
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            // 
            // allowToolStripMenuItem
            // 
            this.allowToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.allowToolStripMenuItem.Name = "allowToolStripMenuItem";
            resources.ApplyResources(this.allowToolStripMenuItem, "allowToolStripMenuItem");
            this.allowToolStripMenuItem.Click += new System.EventHandler(this.allowToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findToolStripMenuItem,
            this.replaceToolStripMenuItem});
            this.editToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            resources.ApplyResources(this.findToolStripMenuItem, "findToolStripMenuItem");
            this.findToolStripMenuItem.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
            // 
            // replaceToolStripMenuItem
            // 
            this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
            resources.ApplyResources(this.replaceToolStripMenuItem, "replaceToolStripMenuItem");
            this.replaceToolStripMenuItem.Click += new System.EventHandler(this.replaceToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
            this.toolStripMenuItem5.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            // 
            // smallerRchtxtFontToolStripMenuItem
            // 
            this.smallerRchtxtFontToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.smallerRchtxtFontToolStripMenuItem.Name = "smallerRchtxtFontToolStripMenuItem";
            resources.ApplyResources(this.smallerRchtxtFontToolStripMenuItem, "smallerRchtxtFontToolStripMenuItem");
            this.smallerRchtxtFontToolStripMenuItem.Click += new System.EventHandler(this.smallerRchtxtFontToolStripMenuItem_Click);
            // 
            // largerRchtxtFontToolStripMenuItem
            // 
            this.largerRchtxtFontToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.largerRchtxtFontToolStripMenuItem.Name = "largerRchtxtFontToolStripMenuItem";
            resources.ApplyResources(this.largerRchtxtFontToolStripMenuItem, "largerRchtxtFontToolStripMenuItem");
            this.largerRchtxtFontToolStripMenuItem.Click += new System.EventHandler(this.largerRchtxtFontToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            resources.ApplyResources(this.toolStripMenuItem6, "toolStripMenuItem6");
            this.toolStripMenuItem6.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            // 
            // executeToolStripMenuItem
            // 
            resources.ApplyResources(this.executeToolStripMenuItem, "executeToolStripMenuItem");
            this.executeToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.executeToolStripMenuItem.Name = "executeToolStripMenuItem";
            this.executeToolStripMenuItem.Click += new System.EventHandler(this.executeToolStripMenuItem_Click);
            // 
            // executeAsToolStripMenuItem
            // 
            this.executeAsToolStripMenuItem.Checked = true;
            this.executeAsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.executeAsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.automaticToolStripMenuItem,
            this.executeNonQueryToolStripMenuItem,
            this.queryToolStripMenuItem});
            this.executeAsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.executeAsToolStripMenuItem.Name = "executeAsToolStripMenuItem";
            resources.ApplyResources(this.executeAsToolStripMenuItem, "executeAsToolStripMenuItem");
            this.executeAsToolStripMenuItem.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // automaticToolStripMenuItem
            // 
            this.automaticToolStripMenuItem.Name = "automaticToolStripMenuItem";
            resources.ApplyResources(this.automaticToolStripMenuItem, "automaticToolStripMenuItem");
            this.automaticToolStripMenuItem.Click += new System.EventHandler(this.automaticToolStripMenuItem_Click);
            // 
            // executeNonQueryToolStripMenuItem
            // 
            this.executeNonQueryToolStripMenuItem.Name = "executeNonQueryToolStripMenuItem";
            resources.ApplyResources(this.executeNonQueryToolStripMenuItem, "executeNonQueryToolStripMenuItem");
            this.executeNonQueryToolStripMenuItem.Click += new System.EventHandler(this.executeNonQueryToolStripMenuItem_Click);
            // 
            // queryToolStripMenuItem
            // 
            this.queryToolStripMenuItem.Name = "queryToolStripMenuItem";
            resources.ApplyResources(this.queryToolStripMenuItem, "queryToolStripMenuItem");
            this.queryToolStripMenuItem.Click += new System.EventHandler(this.queryToolStripMenuItem_Click);
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
            // lbDatabase
            // 
            resources.ApplyResources(this.lbDatabase, "lbDatabase");
            this.lbDatabase.Name = "lbDatabase";
            // 
            // openFileDialog
            // 
            resources.ApplyResources(this.openFileDialog, "openFileDialog");
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "sql";
            resources.ApplyResources(this.saveFileDialog, "saveFileDialog");
            // 
            // lbLoading
            // 
            resources.ApplyResources(this.lbLoading, "lbLoading");
            this.lbLoading.BackColor = System.Drawing.Color.Transparent;
            this.lbLoading.Name = "lbLoading";
            // 
            // picLoading
            // 
            this.picLoading.BackColor = System.Drawing.Color.Transparent;
            this.picLoading.Image = global::DillenManagementStudio.Properties.Resources.Loading_icon1;
            resources.ApplyResources(this.picLoading, "picLoading");
            this.picLoading.Name = "picLoading";
            this.picLoading.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // rchtxtCode
            // 
            this.rchtxtCode.EnableAutoDragDrop = true;
            resources.ApplyResources(this.rchtxtCode, "rchtxtCode");
            this.rchtxtCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.rchtxtCode.Name = "rchtxtCode";
            // 
            // pnlLoading
            // 
            this.pnlLoading.BackColor = System.Drawing.SystemColors.Control;
            this.pnlLoading.Controls.Add(this.picLoading);
            this.pnlLoading.Controls.Add(this.lbLoading);
            resources.ApplyResources(this.pnlLoading, "pnlLoading");
            this.pnlLoading.Name = "pnlLoading";
            // 
            // rchtxtAux
            // 
            this.rchtxtAux.AcceptsTab = true;
            this.rchtxtAux.EnableAutoDragDrop = true;
            resources.ApplyResources(this.rchtxtAux, "rchtxtAux");
            this.rchtxtAux.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.rchtxtAux.Name = "rchtxtAux";
            this.rchtxtAux.ReadOnly = true;
            this.rchtxtAux.TabStop = false;
            // 
            // tmrCheckVPNConn
            // 
            this.tmrCheckVPNConn.Interval = 30000;
            this.tmrCheckVPNConn.Tick += new System.EventHandler(this.tmrCheckVPNConn_Tick);
            // 
            // lbTableName
            // 
            resources.ApplyResources(this.lbTableName, "lbTableName");
            this.lbTableName.Name = "lbTableName";
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(180)))));
            this.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearch.Controls.Add(this.btnCloseFindReplace);
            this.pnlSearch.Controls.Add(this.btnNotSeeReplace);
            this.pnlSearch.Controls.Add(this.btnSeeReplace);
            this.pnlSearch.Controls.Add(this.btnReplaceAll);
            this.pnlSearch.Controls.Add(this.chxIgnoreCase);
            this.pnlSearch.Controls.Add(this.btnReplaceCurr);
            this.pnlSearch.Controls.Add(this.btnNext);
            this.pnlSearch.Controls.Add(this.txtFind);
            this.pnlSearch.Controls.Add(this.txtReplace);
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.label1);
            resources.ApplyResources(this.pnlSearch, "pnlSearch");
            this.pnlSearch.Name = "pnlSearch";
            // 
            // btnCloseFindReplace
            // 
            resources.ApplyResources(this.btnCloseFindReplace, "btnCloseFindReplace");
            this.btnCloseFindReplace.Name = "btnCloseFindReplace";
            this.btnCloseFindReplace.UseVisualStyleBackColor = true;
            this.btnCloseFindReplace.Click += new System.EventHandler(this.btnCloseFindReplace_Click);
            // 
            // btnNotSeeReplace
            // 
            resources.ApplyResources(this.btnNotSeeReplace, "btnNotSeeReplace");
            this.btnNotSeeReplace.Name = "btnNotSeeReplace";
            this.btnNotSeeReplace.UseVisualStyleBackColor = true;
            this.btnNotSeeReplace.Click += new System.EventHandler(this.btnNotSeeReplace_Click);
            // 
            // btnSeeReplace
            // 
            resources.ApplyResources(this.btnSeeReplace, "btnSeeReplace");
            this.btnSeeReplace.Name = "btnSeeReplace";
            this.btnSeeReplace.UseVisualStyleBackColor = true;
            this.btnSeeReplace.Click += new System.EventHandler(this.btnSeeReplace_Click);
            // 
            // btnReplaceAll
            // 
            resources.ApplyResources(this.btnReplaceAll, "btnReplaceAll");
            this.btnReplaceAll.Name = "btnReplaceAll";
            this.btnReplaceAll.UseVisualStyleBackColor = true;
            this.btnReplaceAll.Click += new System.EventHandler(this.btnReplaceAll_Click);
            // 
            // chxIgnoreCase
            // 
            resources.ApplyResources(this.chxIgnoreCase, "chxIgnoreCase");
            this.chxIgnoreCase.Checked = true;
            this.chxIgnoreCase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxIgnoreCase.Name = "chxIgnoreCase";
            this.chxIgnoreCase.UseVisualStyleBackColor = true;
            // 
            // btnReplaceCurr
            // 
            resources.ApplyResources(this.btnReplaceCurr, "btnReplaceCurr");
            this.btnReplaceCurr.Name = "btnReplaceCurr";
            this.btnReplaceCurr.UseVisualStyleBackColor = true;
            this.btnReplaceCurr.Click += new System.EventHandler(this.btnReplaceCurr_Click);
            // 
            // btnNext
            // 
            resources.ApplyResources(this.btnNext, "btnNext");
            this.btnNext.Name = "btnNext";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // txtFind
            // 
            resources.ApplyResources(this.txtFind, "txtFind");
            this.txtFind.Name = "txtFind";
            this.txtFind.TextChanged += new System.EventHandler(this.txtFind_TextChanged);
            this.txtFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFind_KeyDown);
            // 
            // txtReplace
            // 
            resources.ApplyResources(this.txtReplace, "txtReplace");
            this.txtReplace.Name = "txtReplace";
            this.txtReplace.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReplace_KeyDown);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // FrmDillenSQLManagementStudio
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.btnAllTables);
            this.Controls.Add(this.btnAllProcFunc);
            this.Controls.Add(this.lbTableName);
            this.Controls.Add(this.pnlLoading);
            this.Controls.Add(this.lbDatabase);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbExecutionResult);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.grvSelect);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.rchtxtAux);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmDillenSQLManagementStudio";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.FrmDillenSQLManagementStudio_Load);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.FrmDillenSQLManagementStudio_PreviewKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grvSelect)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlLoading.ResumeLayout(false);
            this.pnlLoading.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grvSelect;
        private System.Windows.Forms.Label lbTitle;
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
        private System.Windows.Forms.ToolStripMenuItem changeDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem largerRchtxtFontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smallerRchtxtFontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem executeToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lbDatabase;
        private System.Windows.Forms.ToolStripMenuItem allowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem executeAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem automaticToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem executeNonQueryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem queryToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.PictureBox picLoading;
        private System.Windows.Forms.Label lbLoading;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.RichTextBox rchtxtCode;
        private System.Windows.Forms.Panel pnlLoading;
        private System.Windows.Forms.RichTextBox rchtxtAux;
        private Timer tmrCheckVPNConn;
        private Label lbTableName;
        private ToolStripMenuItem findToolStripMenuItem;
        private ToolStripMenuItem replaceToolStripMenuItem;
        private Panel pnlSearch;
        private TextBox txtFind;
        private TextBox txtReplace;
        private Label label2;
        private Label label1;
        private CheckBox chxIgnoreCase;
        private Button btnReplaceCurr;
        private Button btnNext;
        private Button btnReplaceAll;
        private Button btnSeeReplace;
        private Button btnNotSeeReplace;
        private Button btnCloseFindReplace;
    }
}

