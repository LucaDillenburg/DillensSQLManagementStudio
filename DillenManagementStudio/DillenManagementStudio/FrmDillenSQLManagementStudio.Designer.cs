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
            this.changeDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allCommandsSintaxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smallerRchtxtFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largerRchtxtFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopTryingToConnectWithUnicampVPNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopOrBeginTryingToConnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tryToConnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbDatabase = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.lbLoading = new System.Windows.Forms.Label();
            this.rchtxtCode = new System.Windows.Forms.RichTextBox();
            this.pnlLoading = new System.Windows.Forms.Panel();
            this.rchtxtAux = new System.Windows.Forms.RichTextBox();
            this.tmrCheckVPNConn = new System.Windows.Forms.Timer(this.components);
            this.lbTableName = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.btnNotSeeReplace = new System.Windows.Forms.Button();
            this.btnSeeReplace = new System.Windows.Forms.Button();
            this.btnReplaceAll = new System.Windows.Forms.Button();
            this.chxIgnoreCase = new System.Windows.Forms.CheckBox();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.txtReplace = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnMinimize = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.btnCloseFindReplace = new System.Windows.Forms.Button();
            this.btnReplaceCurr = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.saveToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.automaticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeNonQueryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.grvSelect)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.pnlLoading.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.saveToolStripMenuItem2,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.changeDatabaseToolStripMenuItem,
            this.allCommandsSintaxToolStripMenuItem,
            this.allowToolStripMenuItem,
            this.editToolStripMenuItem,
            this.smallerRchtxtFontToolStripMenuItem,
            this.largerRchtxtFontToolStripMenuItem,
            this.executeToolStripMenuItem,
            this.executeAsToolStripMenuItem,
            this.stopTryingToConnectWithUnicampVPNToolStripMenuItem});
            this.menuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.ShowItemToolTips = true;
            this.menuStrip.TabStop = true;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.AutoToolTip = true;
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
            // changeDatabaseToolStripMenuItem
            // 
            this.changeDatabaseToolStripMenuItem.AutoToolTip = true;
            resources.ApplyResources(this.changeDatabaseToolStripMenuItem, "changeDatabaseToolStripMenuItem");
            this.changeDatabaseToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.changeDatabaseToolStripMenuItem.Name = "changeDatabaseToolStripMenuItem";
            this.changeDatabaseToolStripMenuItem.Click += new System.EventHandler(this.changeDatabaseToolStripMenuItem_Click);
            // 
            // allCommandsSintaxToolStripMenuItem
            // 
            this.allCommandsSintaxToolStripMenuItem.AutoToolTip = true;
            this.allCommandsSintaxToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(45)))));
            this.allCommandsSintaxToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.allCommandsSintaxToolStripMenuItem.Name = "allCommandsSintaxToolStripMenuItem";
            resources.ApplyResources(this.allCommandsSintaxToolStripMenuItem, "allCommandsSintaxToolStripMenuItem");
            this.allCommandsSintaxToolStripMenuItem.Click += new System.EventHandler(this.allCommandsSintaxToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.AutoToolTip = true;
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
            // smallerRchtxtFontToolStripMenuItem
            // 
            resources.ApplyResources(this.smallerRchtxtFontToolStripMenuItem, "smallerRchtxtFontToolStripMenuItem");
            this.smallerRchtxtFontToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.smallerRchtxtFontToolStripMenuItem.Name = "smallerRchtxtFontToolStripMenuItem";
            this.smallerRchtxtFontToolStripMenuItem.Click += new System.EventHandler(this.smallerRchtxtFontToolStripMenuItem_Click);
            // 
            // largerRchtxtFontToolStripMenuItem
            // 
            resources.ApplyResources(this.largerRchtxtFontToolStripMenuItem, "largerRchtxtFontToolStripMenuItem");
            this.largerRchtxtFontToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.largerRchtxtFontToolStripMenuItem.Name = "largerRchtxtFontToolStripMenuItem";
            this.largerRchtxtFontToolStripMenuItem.Click += new System.EventHandler(this.largerRchtxtFontToolStripMenuItem_Click);
            // 
            // stopTryingToConnectWithUnicampVPNToolStripMenuItem
            // 
            this.stopTryingToConnectWithUnicampVPNToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.stopTryingToConnectWithUnicampVPNToolStripMenuItem.AutoToolTip = true;
            this.stopTryingToConnectWithUnicampVPNToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stopOrBeginTryingToConnectToolStripMenuItem,
            this.tryToConnectToolStripMenuItem});
            this.stopTryingToConnectWithUnicampVPNToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.stopTryingToConnectWithUnicampVPNToolStripMenuItem.Name = "stopTryingToConnectWithUnicampVPNToolStripMenuItem";
            resources.ApplyResources(this.stopTryingToConnectWithUnicampVPNToolStripMenuItem, "stopTryingToConnectWithUnicampVPNToolStripMenuItem");
            // 
            // stopOrBeginTryingToConnectToolStripMenuItem
            // 
            this.stopOrBeginTryingToConnectToolStripMenuItem.Name = "stopOrBeginTryingToConnectToolStripMenuItem";
            resources.ApplyResources(this.stopOrBeginTryingToConnectToolStripMenuItem, "stopOrBeginTryingToConnectToolStripMenuItem");
            this.stopOrBeginTryingToConnectToolStripMenuItem.Click += new System.EventHandler(this.stopOrBeginTryingToConnectToolStripMenuItem_Click);
            // 
            // tryToConnectToolStripMenuItem
            // 
            this.tryToConnectToolStripMenuItem.Name = "tryToConnectToolStripMenuItem";
            resources.ApplyResources(this.tryToConnectToolStripMenuItem, "tryToConnectToolStripMenuItem");
            this.tryToConnectToolStripMenuItem.Click += new System.EventHandler(this.tryToConnectToolStripMenuItem_Click);
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
            // rchtxtCode
            // 
            resources.ApplyResources(this.rchtxtCode, "rchtxtCode");
            this.rchtxtCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.rchtxtCode.HideSelection = false;
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
            this.btnReplaceAll.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnReplaceAll.FlatAppearance.BorderSize = 2;
            this.btnReplaceAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnReplaceAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lime;
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
            this.chxIgnoreCase.ForeColor = System.Drawing.Color.Black;
            this.chxIgnoreCase.Name = "chxIgnoreCase";
            this.chxIgnoreCase.UseVisualStyleBackColor = true;
            // 
            // txtFind
            // 
            resources.ApplyResources(this.txtFind, "txtFind");
            this.txtFind.Name = "txtFind";
            this.txtFind.TextChanged += new System.EventHandler(this.txtFind_TextChanged);
            this.txtFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFind_KeyDown);
            this.txtFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFind_KeyPress);
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
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Name = "label1";
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnMinimize, "btnMinimize");
            this.btnMinimize.Image = global::DillenManagementStudio.Properties.Resources.btnMinimize;
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.TabStop = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            this.btnMinimize.MouseEnter += new System.EventHandler(this.btnMinimize_MouseEnter);
            this.btnMinimize.MouseLeave += new System.EventHandler(this.btnMinimize_MouseLeave);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Image = global::DillenManagementStudio.Properties.Resources.btnClose__1_;
            this.btnClose.Name = "btnClose";
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // btnCloseFindReplace
            // 
            this.btnCloseFindReplace.BackgroundImage = global::DillenManagementStudio.Properties.Resources.cancel_music;
            resources.ApplyResources(this.btnCloseFindReplace, "btnCloseFindReplace");
            this.btnCloseFindReplace.Name = "btnCloseFindReplace";
            this.btnCloseFindReplace.UseVisualStyleBackColor = true;
            this.btnCloseFindReplace.Click += new System.EventHandler(this.btnCloseFindReplace_Click);
            // 
            // btnReplaceCurr
            // 
            this.btnReplaceCurr.BackgroundImage = global::DillenManagementStudio.Properties.Resources.arrow_angle_pointing_down;
            resources.ApplyResources(this.btnReplaceCurr, "btnReplaceCurr");
            this.btnReplaceCurr.Name = "btnReplaceCurr";
            this.toolTip1.SetToolTip(this.btnReplaceCurr, resources.GetString("btnReplaceCurr.ToolTip"));
            this.btnReplaceCurr.UseVisualStyleBackColor = true;
            this.btnReplaceCurr.Click += new System.EventHandler(this.btnReplaceCurr_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackgroundImage = global::DillenManagementStudio.Properties.Resources.next;
            resources.ApplyResources(this.btnNext, "btnNext");
            this.btnNext.Name = "btnNext";
            this.toolTip1.SetToolTip(this.btnNext, resources.GetString("btnNext.ToolTip"));
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
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
            // saveToolStripMenuItem2
            // 
            this.saveToolStripMenuItem2.ForeColor = System.Drawing.SystemColors.Control;
            this.saveToolStripMenuItem2.Image = global::DillenManagementStudio.Properties.Resources.saveIcon;
            this.saveToolStripMenuItem2.Name = "saveToolStripMenuItem2";
            resources.ApplyResources(this.saveToolStripMenuItem2, "saveToolStripMenuItem2");
            this.saveToolStripMenuItem2.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.undoToolStripMenuItem.Image = global::DillenManagementStudio.Properties.Resources.undo;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            resources.ApplyResources(this.undoToolStripMenuItem, "undoToolStripMenuItem");
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.redoToolStripMenuItem.Image = global::DillenManagementStudio.Properties.Resources.redo;
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            resources.ApplyResources(this.redoToolStripMenuItem, "redoToolStripMenuItem");
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // allowToolStripMenuItem
            // 
            this.allowToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.allowToolStripMenuItem.Image = global::DillenManagementStudio.Properties.Resources.switch_on3;
            resources.ApplyResources(this.allowToolStripMenuItem, "allowToolStripMenuItem");
            this.allowToolStripMenuItem.Name = "allowToolStripMenuItem";
            this.allowToolStripMenuItem.Click += new System.EventHandler(this.allowToolStripMenuItem_Click);
            // 
            // executeToolStripMenuItem
            // 
            this.executeToolStripMenuItem.AutoToolTip = true;
            resources.ApplyResources(this.executeToolStripMenuItem, "executeToolStripMenuItem");
            this.executeToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.executeToolStripMenuItem.Image = global::DillenManagementStudio.Properties.Resources.play_btn;
            this.executeToolStripMenuItem.Name = "executeToolStripMenuItem";
            this.executeToolStripMenuItem.Click += new System.EventHandler(this.executeToolStripMenuItem_Click);
            // 
            // executeAsToolStripMenuItem
            // 
            this.executeAsToolStripMenuItem.Checked = true;
            this.executeAsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.executeAsToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.executeAsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.automaticToolStripMenuItem,
            this.executeNonQueryToolStripMenuItem,
            this.queryToolStripMenuItem});
            this.executeAsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.executeAsToolStripMenuItem.Image = global::DillenManagementStudio.Properties.Resources.moreIcon;
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
            // FrmDillenSQLManagementStudio
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.btnAllTables);
            this.Controls.Add(this.btnAllProcFunc);
            this.Controls.Add(this.lbTableName);
            this.Controls.Add(this.pnlLoading);
            this.Controls.Add(this.lbDatabase);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbExecutionResult);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.grvSelect);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.rchtxtAux);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmDillenSQLManagementStudio";
            this.ShowInTaskbar = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.FrmDillenSQLManagementStudio_Load);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.FrmDillenSQLManagementStudio_PreviewKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grvSelect)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.pnlLoading.ResumeLayout(false);
            this.pnlLoading.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem changeDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem largerRchtxtFontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smallerRchtxtFontToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem2;
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
        private ToolStripMenuItem stopTryingToConnectWithUnicampVPNToolStripMenuItem;
        private ToolStripMenuItem stopOrBeginTryingToConnectToolStripMenuItem;
        private ToolStripMenuItem tryToConnectToolStripMenuItem;
        private ToolTip toolTip1;
        private PictureBox btnClose;
        private PictureBox btnMinimize;
    }
}

