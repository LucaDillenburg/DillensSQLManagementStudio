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
            this.lbDatabase = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.lbLoading = new System.Windows.Forms.Label();
            this.rchtxtCode = new System.Windows.Forms.RichTextBox();
            this.pnlLoading = new System.Windows.Forms.Panel();
            this.picLoading = new System.Windows.Forms.PictureBox();
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.vpnConfigurationToolStripMenuItem = new System.Windows.Forms.Button();
            this.executeToolStripMenuItem = new System.Windows.Forms.Button();
            this.picExecute = new System.Windows.Forms.PictureBox();
            this.largerRchtxtFontToolStripMenuItem = new System.Windows.Forms.Button();
            this.smallerRchtxtFontToolStripMenuItem = new System.Windows.Forms.Button();
            this.allowOrNotNotificationsToolStripMenuItem = new System.Windows.Forms.Button();
            this.allCommandsSyntaxToolStripMenuItem = new System.Windows.Forms.Button();
            this.changeDatabaseToolStripMenuItem = new System.Windows.Forms.Button();
            this.redoToolStripMenuItem = new System.Windows.Forms.Button();
            this.undoToolStripMenuItem = new System.Windows.Forms.Button();
            this.saveToolStripMenuItem2 = new System.Windows.Forms.Button();
            this.fileToolStripMenuItem = new System.Windows.Forms.Button();
            this.replaceToolStripMenuItem = new System.Windows.Forms.Button();
            this.findToolStripMenuItem = new System.Windows.Forms.Button();
            this.closeToolStripMenuItem = new System.Windows.Forms.Button();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.Button();
            this.saveToolStripMenuItem = new System.Windows.Forms.Button();
            this.openFileToolStripMenuItem = new System.Windows.Forms.Button();
            this.newFileToolStripMenuItem = new System.Windows.Forms.Button();
            this.queryToolStripMenuItem = new System.Windows.Forms.Button();
            this.executeNonQueryToolStripMenuItem = new System.Windows.Forms.Button();
            this.automaticToolStripMenuItem = new System.Windows.Forms.Button();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.executeAsToolStripMenuItem = new System.Windows.Forms.Button();
            this.picMenuMainSeparator = new System.Windows.Forms.PictureBox();
            this.editToolStripMenuItem = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.btnMinimize = new System.Windows.Forms.PictureBox();
            this.pnlEdit = new System.Windows.Forms.Panel();
            this.pnlFile = new System.Windows.Forms.Panel();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pnlVPNConfiguration = new System.Windows.Forms.Panel();
            this.tryToConnectToolStripMenuItem = new System.Windows.Forms.Button();
            this.stopOrBeginTryingToConnectToolStripMenuItem = new System.Windows.Forms.Button();
            this.pnlExecuteAs = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.grvSelect)).BeginInit();
            this.pnlLoading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.pnlSearch.SuspendLayout();
            this.executeToolStripMenuItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExecute)).BeginInit();
            this.pnlMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMenuMainSeparator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).BeginInit();
            this.pnlEdit.SuspendLayout();
            this.pnlFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.pnlVPNConfiguration.SuspendLayout();
            this.pnlExecuteAs.SuspendLayout();
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
            this.lbTitle.BackColor = System.Drawing.Color.Transparent;
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
            // picLoading
            // 
            this.picLoading.BackColor = System.Drawing.Color.Transparent;
            this.picLoading.Image = global::DillenManagementStudio.Properties.Resources.Loading_icon1;
            resources.ApplyResources(this.picLoading, "picLoading");
            this.picLoading.Name = "picLoading";
            this.picLoading.TabStop = false;
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
            this.btnCloseFindReplace.BackgroundImage = global::DillenManagementStudio.Properties.Resources.cancel_music;
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
            this.btnReplaceAll.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnReplaceAll.FlatAppearance.BorderSize = 0;
            this.btnReplaceAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnReplaceAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
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
            // vpnConfigurationToolStripMenuItem
            // 
            this.vpnConfigurationToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.vpnConfigurationToolStripMenuItem.FlatAppearance.BorderSize = 0;
            this.vpnConfigurationToolStripMenuItem.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(64)))), ((int)(((byte)(70)))));
            this.vpnConfigurationToolStripMenuItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(64)))), ((int)(((byte)(70)))));
            resources.ApplyResources(this.vpnConfigurationToolStripMenuItem, "vpnConfigurationToolStripMenuItem");
            this.vpnConfigurationToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.vpnConfigurationToolStripMenuItem.Name = "vpnConfigurationToolStripMenuItem";
            this.vpnConfigurationToolStripMenuItem.Tag = "4";
            this.toolTip1.SetToolTip(this.vpnConfigurationToolStripMenuItem, resources.GetString("vpnConfigurationToolStripMenuItem.ToolTip"));
            this.vpnConfigurationToolStripMenuItem.UseVisualStyleBackColor = false;
            this.vpnConfigurationToolStripMenuItem.Click += new System.EventHandler(this.showMoreToolStripMenuItem_Click);
            this.vpnConfigurationToolStripMenuItem.MouseEnter += new System.EventHandler(this.moreToShowToolStripMenuItem_MouseEnter);
            this.vpnConfigurationToolStripMenuItem.MouseLeave += new System.EventHandler(this.moreToShowToolStripMenuItem_MouseLeave);
            // 
            // executeToolStripMenuItem
            // 
            this.executeToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.executeToolStripMenuItem.Controls.Add(this.picExecute);
            this.executeToolStripMenuItem.FlatAppearance.BorderSize = 0;
            this.executeToolStripMenuItem.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(106)))), ((int)(((byte)(117)))));
            this.executeToolStripMenuItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(106)))), ((int)(((byte)(117)))));
            this.executeToolStripMenuItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(64)))), ((int)(((byte)(70)))));
            resources.ApplyResources(this.executeToolStripMenuItem, "executeToolStripMenuItem");
            this.executeToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.executeToolStripMenuItem.Name = "executeToolStripMenuItem";
            this.toolTip1.SetToolTip(this.executeToolStripMenuItem, resources.GetString("executeToolStripMenuItem.ToolTip"));
            this.executeToolStripMenuItem.UseVisualStyleBackColor = false;
            this.executeToolStripMenuItem.Click += new System.EventHandler(this.executeToolStripMenuItem_Click);
            this.executeToolStripMenuItem.MouseEnter += new System.EventHandler(this.notShowMoreToolStripMenuItem_MouseEnter);
            // 
            // picExecute
            // 
            this.picExecute.BackColor = System.Drawing.Color.Transparent;
            this.picExecute.BackgroundImage = global::DillenManagementStudio.Properties.Resources.play_btn;
            resources.ApplyResources(this.picExecute, "picExecute");
            this.picExecute.Name = "picExecute";
            this.picExecute.TabStop = false;
            this.picExecute.Click += new System.EventHandler(this.executeToolStripMenuItem_Click);
            this.picExecute.MouseEnter += new System.EventHandler(this.notShowMoreToolStripMenuItem_MouseEnter);
            // 
            // largerRchtxtFontToolStripMenuItem
            // 
            this.largerRchtxtFontToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.largerRchtxtFontToolStripMenuItem.FlatAppearance.BorderSize = 0;
            this.largerRchtxtFontToolStripMenuItem.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(106)))), ((int)(((byte)(117)))));
            this.largerRchtxtFontToolStripMenuItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(106)))), ((int)(((byte)(117)))));
            this.largerRchtxtFontToolStripMenuItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(64)))), ((int)(((byte)(70)))));
            resources.ApplyResources(this.largerRchtxtFontToolStripMenuItem, "largerRchtxtFontToolStripMenuItem");
            this.largerRchtxtFontToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.largerRchtxtFontToolStripMenuItem.Name = "largerRchtxtFontToolStripMenuItem";
            this.toolTip1.SetToolTip(this.largerRchtxtFontToolStripMenuItem, resources.GetString("largerRchtxtFontToolStripMenuItem.ToolTip"));
            this.largerRchtxtFontToolStripMenuItem.UseVisualStyleBackColor = false;
            this.largerRchtxtFontToolStripMenuItem.Click += new System.EventHandler(this.largerRchtxtFontToolStripMenuItem_Click);
            this.largerRchtxtFontToolStripMenuItem.MouseEnter += new System.EventHandler(this.notShowMoreToolStripMenuItem_MouseEnter);
            // 
            // smallerRchtxtFontToolStripMenuItem
            // 
            this.smallerRchtxtFontToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.smallerRchtxtFontToolStripMenuItem.FlatAppearance.BorderSize = 0;
            this.smallerRchtxtFontToolStripMenuItem.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(106)))), ((int)(((byte)(117)))));
            this.smallerRchtxtFontToolStripMenuItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(106)))), ((int)(((byte)(117)))));
            this.smallerRchtxtFontToolStripMenuItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(64)))), ((int)(((byte)(70)))));
            resources.ApplyResources(this.smallerRchtxtFontToolStripMenuItem, "smallerRchtxtFontToolStripMenuItem");
            this.smallerRchtxtFontToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.smallerRchtxtFontToolStripMenuItem.Name = "smallerRchtxtFontToolStripMenuItem";
            this.toolTip1.SetToolTip(this.smallerRchtxtFontToolStripMenuItem, resources.GetString("smallerRchtxtFontToolStripMenuItem.ToolTip"));
            this.smallerRchtxtFontToolStripMenuItem.UseVisualStyleBackColor = false;
            this.smallerRchtxtFontToolStripMenuItem.Click += new System.EventHandler(this.smallerRchtxtFontToolStripMenuItem_Click);
            this.smallerRchtxtFontToolStripMenuItem.MouseEnter += new System.EventHandler(this.notShowMoreToolStripMenuItem_MouseEnter);
            // 
            // allowOrNotNotificationsToolStripMenuItem
            // 
            this.allowOrNotNotificationsToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.allowOrNotNotificationsToolStripMenuItem.FlatAppearance.BorderSize = 0;
            this.allowOrNotNotificationsToolStripMenuItem.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(106)))), ((int)(((byte)(117)))));
            this.allowOrNotNotificationsToolStripMenuItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(106)))), ((int)(((byte)(117)))));
            this.allowOrNotNotificationsToolStripMenuItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(64)))), ((int)(((byte)(70)))));
            resources.ApplyResources(this.allowOrNotNotificationsToolStripMenuItem, "allowOrNotNotificationsToolStripMenuItem");
            this.allowOrNotNotificationsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.allowOrNotNotificationsToolStripMenuItem.Image = global::DillenManagementStudio.Properties.Resources.switch_on4;
            this.allowOrNotNotificationsToolStripMenuItem.Name = "allowOrNotNotificationsToolStripMenuItem";
            this.allowOrNotNotificationsToolStripMenuItem.Tag = "";
            this.toolTip1.SetToolTip(this.allowOrNotNotificationsToolStripMenuItem, resources.GetString("allowOrNotNotificationsToolStripMenuItem.ToolTip"));
            this.allowOrNotNotificationsToolStripMenuItem.UseVisualStyleBackColor = false;
            this.allowOrNotNotificationsToolStripMenuItem.Click += new System.EventHandler(this.allowToolStripMenuItem_Click);
            this.allowOrNotNotificationsToolStripMenuItem.MouseEnter += new System.EventHandler(this.notShowMoreToolStripMenuItem_MouseEnter);
            // 
            // allCommandsSyntaxToolStripMenuItem
            // 
            this.allCommandsSyntaxToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.allCommandsSyntaxToolStripMenuItem.FlatAppearance.BorderSize = 0;
            this.allCommandsSyntaxToolStripMenuItem.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(106)))), ((int)(((byte)(117)))));
            this.allCommandsSyntaxToolStripMenuItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(106)))), ((int)(((byte)(117)))));
            this.allCommandsSyntaxToolStripMenuItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(64)))), ((int)(((byte)(70)))));
            resources.ApplyResources(this.allCommandsSyntaxToolStripMenuItem, "allCommandsSyntaxToolStripMenuItem");
            this.allCommandsSyntaxToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.allCommandsSyntaxToolStripMenuItem.Name = "allCommandsSyntaxToolStripMenuItem";
            this.toolTip1.SetToolTip(this.allCommandsSyntaxToolStripMenuItem, resources.GetString("allCommandsSyntaxToolStripMenuItem.ToolTip"));
            this.allCommandsSyntaxToolStripMenuItem.UseVisualStyleBackColor = false;
            this.allCommandsSyntaxToolStripMenuItem.Click += new System.EventHandler(this.allCommandsSintaxToolStripMenuItem_Click);
            this.allCommandsSyntaxToolStripMenuItem.MouseEnter += new System.EventHandler(this.notShowMoreToolStripMenuItem_MouseEnter);
            // 
            // changeDatabaseToolStripMenuItem
            // 
            this.changeDatabaseToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.changeDatabaseToolStripMenuItem.FlatAppearance.BorderSize = 0;
            this.changeDatabaseToolStripMenuItem.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(106)))), ((int)(((byte)(117)))));
            this.changeDatabaseToolStripMenuItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(106)))), ((int)(((byte)(117)))));
            this.changeDatabaseToolStripMenuItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(64)))), ((int)(((byte)(70)))));
            resources.ApplyResources(this.changeDatabaseToolStripMenuItem, "changeDatabaseToolStripMenuItem");
            this.changeDatabaseToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.changeDatabaseToolStripMenuItem.Name = "changeDatabaseToolStripMenuItem";
            this.toolTip1.SetToolTip(this.changeDatabaseToolStripMenuItem, resources.GetString("changeDatabaseToolStripMenuItem.ToolTip"));
            this.changeDatabaseToolStripMenuItem.UseVisualStyleBackColor = false;
            this.changeDatabaseToolStripMenuItem.Click += new System.EventHandler(this.changeDatabaseToolStripMenuItem_Click);
            this.changeDatabaseToolStripMenuItem.MouseEnter += new System.EventHandler(this.notShowMoreToolStripMenuItem_MouseEnter);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.redoToolStripMenuItem.BackgroundImage = global::DillenManagementStudio.Properties.Resources.redo;
            resources.ApplyResources(this.redoToolStripMenuItem, "redoToolStripMenuItem");
            this.redoToolStripMenuItem.FlatAppearance.BorderSize = 0;
            this.redoToolStripMenuItem.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(106)))), ((int)(((byte)(117)))));
            this.redoToolStripMenuItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(106)))), ((int)(((byte)(117)))));
            this.redoToolStripMenuItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(64)))), ((int)(((byte)(70)))));
            this.redoToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.toolTip1.SetToolTip(this.redoToolStripMenuItem, resources.GetString("redoToolStripMenuItem.ToolTip"));
            this.redoToolStripMenuItem.UseVisualStyleBackColor = false;
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            this.redoToolStripMenuItem.MouseEnter += new System.EventHandler(this.notShowMoreToolStripMenuItem_MouseEnter);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.undoToolStripMenuItem.BackgroundImage = global::DillenManagementStudio.Properties.Resources.undo;
            resources.ApplyResources(this.undoToolStripMenuItem, "undoToolStripMenuItem");
            this.undoToolStripMenuItem.FlatAppearance.BorderSize = 0;
            this.undoToolStripMenuItem.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(106)))), ((int)(((byte)(117)))));
            this.undoToolStripMenuItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(106)))), ((int)(((byte)(117)))));
            this.undoToolStripMenuItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(64)))), ((int)(((byte)(70)))));
            this.undoToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.toolTip1.SetToolTip(this.undoToolStripMenuItem, resources.GetString("undoToolStripMenuItem.ToolTip"));
            this.undoToolStripMenuItem.UseVisualStyleBackColor = false;
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            this.undoToolStripMenuItem.MouseEnter += new System.EventHandler(this.notShowMoreToolStripMenuItem_MouseEnter);
            // 
            // saveToolStripMenuItem2
            // 
            this.saveToolStripMenuItem2.BackColor = System.Drawing.Color.Transparent;
            this.saveToolStripMenuItem2.BackgroundImage = global::DillenManagementStudio.Properties.Resources.saveIcon;
            resources.ApplyResources(this.saveToolStripMenuItem2, "saveToolStripMenuItem2");
            this.saveToolStripMenuItem2.FlatAppearance.BorderSize = 0;
            this.saveToolStripMenuItem2.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(106)))), ((int)(((byte)(117)))));
            this.saveToolStripMenuItem2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(106)))), ((int)(((byte)(117)))));
            this.saveToolStripMenuItem2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(64)))), ((int)(((byte)(70)))));
            this.saveToolStripMenuItem2.ForeColor = System.Drawing.SystemColors.Control;
            this.saveToolStripMenuItem2.Name = "saveToolStripMenuItem2";
            this.toolTip1.SetToolTip(this.saveToolStripMenuItem2, resources.GetString("saveToolStripMenuItem2.ToolTip"));
            this.saveToolStripMenuItem2.UseVisualStyleBackColor = false;
            this.saveToolStripMenuItem2.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            this.saveToolStripMenuItem2.MouseEnter += new System.EventHandler(this.notShowMoreToolStripMenuItem_MouseEnter);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.fileToolStripMenuItem.FlatAppearance.BorderSize = 0;
            this.fileToolStripMenuItem.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(64)))), ((int)(((byte)(70)))));
            this.fileToolStripMenuItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(64)))), ((int)(((byte)(70)))));
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Tag = "1";
            this.toolTip1.SetToolTip(this.fileToolStripMenuItem, resources.GetString("fileToolStripMenuItem.ToolTip"));
            this.fileToolStripMenuItem.UseVisualStyleBackColor = false;
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.showMoreToolStripMenuItem_Click);
            this.fileToolStripMenuItem.MouseEnter += new System.EventHandler(this.moreToShowToolStripMenuItem_MouseEnter);
            this.fileToolStripMenuItem.MouseLeave += new System.EventHandler(this.moreToShowToolStripMenuItem_MouseLeave);
            // 
            // replaceToolStripMenuItem
            // 
            this.replaceToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.replaceToolStripMenuItem.FlatAppearance.BorderSize = 0;
            this.replaceToolStripMenuItem.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(166)))), ((int)(((byte)(233)))));
            this.replaceToolStripMenuItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(166)))), ((int)(((byte)(233)))));
            this.replaceToolStripMenuItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this.replaceToolStripMenuItem, "replaceToolStripMenuItem");
            this.replaceToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
            this.toolTip1.SetToolTip(this.replaceToolStripMenuItem, resources.GetString("replaceToolStripMenuItem.ToolTip"));
            this.replaceToolStripMenuItem.UseVisualStyleBackColor = false;
            this.replaceToolStripMenuItem.Click += new System.EventHandler(this.replaceToolStripMenuItem_Click);
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.findToolStripMenuItem.FlatAppearance.BorderSize = 0;
            this.findToolStripMenuItem.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(166)))), ((int)(((byte)(233)))));
            this.findToolStripMenuItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(166)))), ((int)(((byte)(233)))));
            this.findToolStripMenuItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this.findToolStripMenuItem, "findToolStripMenuItem");
            this.findToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.toolTip1.SetToolTip(this.findToolStripMenuItem, resources.GetString("findToolStripMenuItem.ToolTip"));
            this.findToolStripMenuItem.UseVisualStyleBackColor = false;
            this.findToolStripMenuItem.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.closeToolStripMenuItem.FlatAppearance.BorderSize = 0;
            this.closeToolStripMenuItem.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(166)))), ((int)(((byte)(233)))));
            this.closeToolStripMenuItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(166)))), ((int)(((byte)(233)))));
            this.closeToolStripMenuItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this.closeToolStripMenuItem, "closeToolStripMenuItem");
            this.closeToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.toolTip1.SetToolTip(this.closeToolStripMenuItem, resources.GetString("closeToolStripMenuItem.ToolTip"));
            this.closeToolStripMenuItem.UseVisualStyleBackColor = false;
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.saveAsToolStripMenuItem.FlatAppearance.BorderSize = 0;
            this.saveAsToolStripMenuItem.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(166)))), ((int)(((byte)(233)))));
            this.saveAsToolStripMenuItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(166)))), ((int)(((byte)(233)))));
            this.saveAsToolStripMenuItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this.saveAsToolStripMenuItem, "saveAsToolStripMenuItem");
            this.saveAsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.toolTip1.SetToolTip(this.saveAsToolStripMenuItem, resources.GetString("saveAsToolStripMenuItem.ToolTip"));
            this.saveAsToolStripMenuItem.UseVisualStyleBackColor = false;
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.saveToolStripMenuItem.FlatAppearance.BorderSize = 0;
            this.saveToolStripMenuItem.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(166)))), ((int)(((byte)(233)))));
            this.saveToolStripMenuItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(166)))), ((int)(((byte)(233)))));
            this.saveToolStripMenuItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.toolTip1.SetToolTip(this.saveToolStripMenuItem, resources.GetString("saveToolStripMenuItem.ToolTip"));
            this.saveToolStripMenuItem.UseVisualStyleBackColor = false;
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.openFileToolStripMenuItem.FlatAppearance.BorderSize = 0;
            this.openFileToolStripMenuItem.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(166)))), ((int)(((byte)(233)))));
            this.openFileToolStripMenuItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(166)))), ((int)(((byte)(233)))));
            this.openFileToolStripMenuItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this.openFileToolStripMenuItem, "openFileToolStripMenuItem");
            this.openFileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.toolTip1.SetToolTip(this.openFileToolStripMenuItem, resources.GetString("openFileToolStripMenuItem.ToolTip"));
            this.openFileToolStripMenuItem.UseVisualStyleBackColor = false;
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // newFileToolStripMenuItem
            // 
            this.newFileToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.newFileToolStripMenuItem.FlatAppearance.BorderSize = 0;
            this.newFileToolStripMenuItem.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(166)))), ((int)(((byte)(233)))));
            this.newFileToolStripMenuItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(166)))), ((int)(((byte)(233)))));
            this.newFileToolStripMenuItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this.newFileToolStripMenuItem, "newFileToolStripMenuItem");
            this.newFileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.newFileToolStripMenuItem.Name = "newFileToolStripMenuItem";
            this.toolTip1.SetToolTip(this.newFileToolStripMenuItem, resources.GetString("newFileToolStripMenuItem.ToolTip"));
            this.newFileToolStripMenuItem.UseVisualStyleBackColor = false;
            this.newFileToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // queryToolStripMenuItem
            // 
            this.queryToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.queryToolStripMenuItem.FlatAppearance.BorderSize = 0;
            this.queryToolStripMenuItem.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(166)))), ((int)(((byte)(233)))));
            this.queryToolStripMenuItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(166)))), ((int)(((byte)(233)))));
            this.queryToolStripMenuItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this.queryToolStripMenuItem, "queryToolStripMenuItem");
            this.queryToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.queryToolStripMenuItem.Name = "queryToolStripMenuItem";
            this.toolTip1.SetToolTip(this.queryToolStripMenuItem, resources.GetString("queryToolStripMenuItem.ToolTip"));
            this.queryToolStripMenuItem.UseVisualStyleBackColor = false;
            this.queryToolStripMenuItem.Click += new System.EventHandler(this.queryToolStripMenuItem_Click);
            // 
            // executeNonQueryToolStripMenuItem
            // 
            this.executeNonQueryToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.executeNonQueryToolStripMenuItem.FlatAppearance.BorderSize = 0;
            this.executeNonQueryToolStripMenuItem.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(166)))), ((int)(((byte)(233)))));
            this.executeNonQueryToolStripMenuItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(166)))), ((int)(((byte)(233)))));
            this.executeNonQueryToolStripMenuItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this.executeNonQueryToolStripMenuItem, "executeNonQueryToolStripMenuItem");
            this.executeNonQueryToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.executeNonQueryToolStripMenuItem.Name = "executeNonQueryToolStripMenuItem";
            this.toolTip1.SetToolTip(this.executeNonQueryToolStripMenuItem, resources.GetString("executeNonQueryToolStripMenuItem.ToolTip"));
            this.executeNonQueryToolStripMenuItem.UseVisualStyleBackColor = false;
            this.executeNonQueryToolStripMenuItem.Click += new System.EventHandler(this.executeNonQueryToolStripMenuItem_Click);
            // 
            // automaticToolStripMenuItem
            // 
            this.automaticToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.automaticToolStripMenuItem.FlatAppearance.BorderSize = 0;
            this.automaticToolStripMenuItem.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(166)))), ((int)(((byte)(233)))));
            this.automaticToolStripMenuItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(166)))), ((int)(((byte)(233)))));
            this.automaticToolStripMenuItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this.automaticToolStripMenuItem, "automaticToolStripMenuItem");
            this.automaticToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.automaticToolStripMenuItem.Name = "automaticToolStripMenuItem";
            this.toolTip1.SetToolTip(this.automaticToolStripMenuItem, resources.GetString("automaticToolStripMenuItem.ToolTip"));
            this.automaticToolStripMenuItem.UseVisualStyleBackColor = false;
            this.automaticToolStripMenuItem.Click += new System.EventHandler(this.automaticToolStripMenuItem_Click);
            // 
            // pnlMenu
            // 
            this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(45)))));
            this.pnlMenu.Controls.Add(this.vpnConfigurationToolStripMenuItem);
            this.pnlMenu.Controls.Add(this.executeAsToolStripMenuItem);
            this.pnlMenu.Controls.Add(this.picMenuMainSeparator);
            this.pnlMenu.Controls.Add(this.executeToolStripMenuItem);
            this.pnlMenu.Controls.Add(this.largerRchtxtFontToolStripMenuItem);
            this.pnlMenu.Controls.Add(this.smallerRchtxtFontToolStripMenuItem);
            this.pnlMenu.Controls.Add(this.editToolStripMenuItem);
            this.pnlMenu.Controls.Add(this.allowOrNotNotificationsToolStripMenuItem);
            this.pnlMenu.Controls.Add(this.allCommandsSyntaxToolStripMenuItem);
            this.pnlMenu.Controls.Add(this.changeDatabaseToolStripMenuItem);
            this.pnlMenu.Controls.Add(this.redoToolStripMenuItem);
            this.pnlMenu.Controls.Add(this.undoToolStripMenuItem);
            this.pnlMenu.Controls.Add(this.saveToolStripMenuItem2);
            this.pnlMenu.Controls.Add(this.fileToolStripMenuItem);
            this.pnlMenu.Controls.Add(this.pictureBox1);
            this.pnlMenu.Controls.Add(this.btnClose);
            this.pnlMenu.Controls.Add(this.btnMinimize);
            this.pnlMenu.Controls.Add(this.lbTitle);
            this.pnlMenu.ForeColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.pnlMenu, "pnlMenu");
            this.pnlMenu.Name = "pnlMenu";
            // 
            // executeAsToolStripMenuItem
            // 
            this.executeAsToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.executeAsToolStripMenuItem.BackgroundImage = global::DillenManagementStudio.Properties.Resources.moreIcon;
            resources.ApplyResources(this.executeAsToolStripMenuItem, "executeAsToolStripMenuItem");
            this.executeAsToolStripMenuItem.FlatAppearance.BorderSize = 0;
            this.executeAsToolStripMenuItem.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(64)))), ((int)(((byte)(70)))));
            this.executeAsToolStripMenuItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(64)))), ((int)(((byte)(70)))));
            this.executeAsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.executeAsToolStripMenuItem.Name = "executeAsToolStripMenuItem";
            this.executeAsToolStripMenuItem.Tag = "3";
            this.executeAsToolStripMenuItem.UseVisualStyleBackColor = false;
            this.executeAsToolStripMenuItem.Click += new System.EventHandler(this.showMoreToolStripMenuItem_Click);
            this.executeAsToolStripMenuItem.MouseEnter += new System.EventHandler(this.moreToShowToolStripMenuItem_MouseEnter);
            this.executeAsToolStripMenuItem.MouseLeave += new System.EventHandler(this.moreToShowToolStripMenuItem_MouseLeave);
            // 
            // picMenuMainSeparator
            // 
            this.picMenuMainSeparator.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.picMenuMainSeparator, "picMenuMainSeparator");
            this.picMenuMainSeparator.Name = "picMenuMainSeparator";
            this.picMenuMainSeparator.TabStop = false;
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.editToolStripMenuItem.FlatAppearance.BorderSize = 0;
            this.editToolStripMenuItem.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(64)))), ((int)(((byte)(70)))));
            this.editToolStripMenuItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(64)))), ((int)(((byte)(70)))));
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            this.editToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Tag = "2";
            this.editToolStripMenuItem.UseVisualStyleBackColor = false;
            this.editToolStripMenuItem.Click += new System.EventHandler(this.showMoreToolStripMenuItem_Click);
            this.editToolStripMenuItem.MouseEnter += new System.EventHandler(this.moreToShowToolStripMenuItem_MouseEnter);
            this.editToolStripMenuItem.MouseLeave += new System.EventHandler(this.moreToShowToolStripMenuItem_MouseLeave);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
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
            // pnlEdit
            // 
            this.pnlEdit.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.pnlEdit, "pnlEdit");
            this.pnlEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEdit.Controls.Add(this.replaceToolStripMenuItem);
            this.pnlEdit.Controls.Add(this.findToolStripMenuItem);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Tag = "2";
            // 
            // pnlFile
            // 
            this.pnlFile.BackColor = System.Drawing.Color.White;
            this.pnlFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFile.Controls.Add(this.closeToolStripMenuItem);
            this.pnlFile.Controls.Add(this.pictureBox5);
            this.pnlFile.Controls.Add(this.saveAsToolStripMenuItem);
            this.pnlFile.Controls.Add(this.saveToolStripMenuItem);
            this.pnlFile.Controls.Add(this.pictureBox4);
            this.pnlFile.Controls.Add(this.openFileToolStripMenuItem);
            this.pnlFile.Controls.Add(this.newFileToolStripMenuItem);
            resources.ApplyResources(this.pnlFile, "pnlFile");
            this.pnlFile.Name = "pnlFile";
            this.pnlFile.Tag = "1";
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.pictureBox5, "pictureBox5");
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.pictureBox4, "pictureBox4");
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.TabStop = false;
            // 
            // pnlVPNConfiguration
            // 
            this.pnlVPNConfiguration.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.pnlVPNConfiguration, "pnlVPNConfiguration");
            this.pnlVPNConfiguration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlVPNConfiguration.Controls.Add(this.tryToConnectToolStripMenuItem);
            this.pnlVPNConfiguration.Controls.Add(this.stopOrBeginTryingToConnectToolStripMenuItem);
            this.pnlVPNConfiguration.Name = "pnlVPNConfiguration";
            this.pnlVPNConfiguration.Tag = "4";
            // 
            // tryToConnectToolStripMenuItem
            // 
            this.tryToConnectToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.tryToConnectToolStripMenuItem.FlatAppearance.BorderSize = 0;
            this.tryToConnectToolStripMenuItem.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(166)))), ((int)(((byte)(233)))));
            this.tryToConnectToolStripMenuItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(166)))), ((int)(((byte)(233)))));
            this.tryToConnectToolStripMenuItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this.tryToConnectToolStripMenuItem, "tryToConnectToolStripMenuItem");
            this.tryToConnectToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tryToConnectToolStripMenuItem.Name = "tryToConnectToolStripMenuItem";
            this.tryToConnectToolStripMenuItem.UseVisualStyleBackColor = false;
            this.tryToConnectToolStripMenuItem.Click += new System.EventHandler(this.tryToConnectToolStripMenuItem_Click);
            // 
            // stopOrBeginTryingToConnectToolStripMenuItem
            // 
            this.stopOrBeginTryingToConnectToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.stopOrBeginTryingToConnectToolStripMenuItem.FlatAppearance.BorderSize = 0;
            this.stopOrBeginTryingToConnectToolStripMenuItem.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(166)))), ((int)(((byte)(233)))));
            this.stopOrBeginTryingToConnectToolStripMenuItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(166)))), ((int)(((byte)(233)))));
            this.stopOrBeginTryingToConnectToolStripMenuItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this.stopOrBeginTryingToConnectToolStripMenuItem, "stopOrBeginTryingToConnectToolStripMenuItem");
            this.stopOrBeginTryingToConnectToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.stopOrBeginTryingToConnectToolStripMenuItem.Name = "stopOrBeginTryingToConnectToolStripMenuItem";
            this.stopOrBeginTryingToConnectToolStripMenuItem.UseVisualStyleBackColor = false;
            this.stopOrBeginTryingToConnectToolStripMenuItem.Click += new System.EventHandler(this.stopOrBeginTryingToConnectToolStripMenuItem_Click);
            // 
            // pnlExecuteAs
            // 
            this.pnlExecuteAs.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.pnlExecuteAs, "pnlExecuteAs");
            this.pnlExecuteAs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlExecuteAs.Controls.Add(this.queryToolStripMenuItem);
            this.pnlExecuteAs.Controls.Add(this.executeNonQueryToolStripMenuItem);
            this.pnlExecuteAs.Controls.Add(this.automaticToolStripMenuItem);
            this.pnlExecuteAs.Name = "pnlExecuteAs";
            this.pnlExecuteAs.Tag = "3";
            // 
            // FrmDillenSQLManagementStudio
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pnlExecuteAs);
            this.Controls.Add(this.pnlVPNConfiguration);
            this.Controls.Add(this.pnlEdit);
            this.Controls.Add(this.pnlFile);
            this.Controls.Add(this.pnlMenu);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.btnAllTables);
            this.Controls.Add(this.btnAllProcFunc);
            this.Controls.Add(this.lbTableName);
            this.Controls.Add(this.pnlLoading);
            this.Controls.Add(this.lbDatabase);
            this.Controls.Add(this.lbExecutionResult);
            this.Controls.Add(this.grvSelect);
            this.Controls.Add(this.rchtxtAux);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmDillenSQLManagementStudio";
            this.ShowInTaskbar = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.FrmDillenSQLManagementStudio_Load);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.FrmDillenSQLManagementStudio_PreviewKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grvSelect)).EndInit();
            this.pnlLoading.ResumeLayout(false);
            this.pnlLoading.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.executeToolStripMenuItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picExecute)).EndInit();
            this.pnlMenu.ResumeLayout(false);
            this.pnlMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMenuMainSeparator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).EndInit();
            this.pnlEdit.ResumeLayout(false);
            this.pnlFile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.pnlVPNConfiguration.ResumeLayout(false);
            this.pnlExecuteAs.ResumeLayout(false);
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
        private System.Windows.Forms.PictureBox picMenuMainSeparator;
        private System.Windows.Forms.Label lbDatabase;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.PictureBox picLoading;
        private System.Windows.Forms.Label lbLoading;
        private System.Windows.Forms.RichTextBox rchtxtCode;
        private System.Windows.Forms.Panel pnlLoading;
        private System.Windows.Forms.RichTextBox rchtxtAux;
        private Timer tmrCheckVPNConn;
        private Label lbTableName;
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
        private ToolTip toolTip1;
        private PictureBox btnClose;
        private PictureBox btnMinimize;
        private Panel pnlMenu;
        private Button allCommandsSyntaxToolStripMenuItem;
        private Button changeDatabaseToolStripMenuItem;
        private Button redoToolStripMenuItem;
        private Button undoToolStripMenuItem;
        private Button saveToolStripMenuItem2;
        private Button fileToolStripMenuItem;
        private Button executeToolStripMenuItem;
        private Button largerRchtxtFontToolStripMenuItem;
        private Button smallerRchtxtFontToolStripMenuItem;
        private Button editToolStripMenuItem;
        private Button allowOrNotNotificationsToolStripMenuItem;
        private PictureBox picExecute;
        private Button vpnConfigurationToolStripMenuItem;
        private Button executeAsToolStripMenuItem;
        private Panel pnlFile;
        private Button closeToolStripMenuItem;
        private PictureBox pictureBox5;
        private Button saveAsToolStripMenuItem;
        private Button saveToolStripMenuItem;
        private PictureBox pictureBox4;
        private Button openFileToolStripMenuItem;
        private Button newFileToolStripMenuItem;
        private Panel pnlEdit;
        private Button replaceToolStripMenuItem;
        private Button findToolStripMenuItem;
        private Panel pnlVPNConfiguration;
        private Button tryToConnectToolStripMenuItem;
        private Button stopOrBeginTryingToConnectToolStripMenuItem;
        private Panel pnlExecuteAs;
        private Button queryToolStripMenuItem;
        private Button executeNonQueryToolStripMenuItem;
        private Button automaticToolStripMenuItem;

        protected OpaquePanel opaquePanel;
    }
}

