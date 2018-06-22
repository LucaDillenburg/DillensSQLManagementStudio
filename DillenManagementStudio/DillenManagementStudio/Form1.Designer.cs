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
            this.grvSelect = new System.Windows.Forms.DataGridView();
            this.btnExecute = new System.Windows.Forms.Button();
            this.rchtxtCode = new System.Windows.Forms.RichTextBox();
            this.rdSelect = new System.Windows.Forms.RadioButton();
            this.rdNonQuery = new System.Windows.Forms.RadioButton();
            this.btnChangeDtBs = new System.Windows.Forms.Button();
            this.cbxChsDtBs = new System.Windows.Forms.ComboBox();
            this.rdAutomatic = new System.Windows.Forms.RadioButton();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnOtherDtBs = new System.Windows.Forms.Button();
            this.lbTitle = new System.Windows.Forms.Label();
            this.btnBiggerFont = new System.Windows.Forms.Button();
            this.btnSmallerFont = new System.Windows.Forms.Button();
            this.btnAllTables = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grvSelect)).BeginInit();
            this.SuspendLayout();
            // 
            // grvSelect
            // 
            this.grvSelect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvSelect.Location = new System.Drawing.Point(780, 86);
            this.grvSelect.Name = "grvSelect";
            this.grvSelect.ReadOnly = true;
            this.grvSelect.Size = new System.Drawing.Size(578, 609);
            this.grvSelect.TabIndex = 20;
            // 
            // btnExecute
            // 
            this.btnExecute.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecute.Location = new System.Drawing.Point(294, 635);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(161, 48);
            this.btnExecute.TabIndex = 19;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // rchtxtCode
            // 
            this.rchtxtCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rchtxtCode.Location = new System.Drawing.Point(12, 113);
            this.rchtxtCode.Name = "rchtxtCode";
            this.rchtxtCode.Size = new System.Drawing.Size(749, 491);
            this.rchtxtCode.TabIndex = 18;
            this.rchtxtCode.Text = "Write down your SQL code in here...";
            this.rchtxtCode.TextChanged += new System.EventHandler(this.rchtxtCode_TextChanged);
            this.rchtxtCode.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.rchtxtCode_PreviewKeyDown);
            // 
            // rdSelect
            // 
            this.rdSelect.AutoSize = true;
            this.rdSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSelect.Location = new System.Drawing.Point(91, 642);
            this.rdSelect.Name = "rdSelect";
            this.rdSelect.Size = new System.Drawing.Size(78, 24);
            this.rdSelect.TabIndex = 17;
            this.rdSelect.TabStop = true;
            this.rdSelect.Text = "Select";
            this.rdSelect.UseVisualStyleBackColor = true;
            // 
            // rdNonQuery
            // 
            this.rdNonQuery.AutoSize = true;
            this.rdNonQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdNonQuery.Location = new System.Drawing.Point(91, 621);
            this.rdNonQuery.Name = "rdNonQuery";
            this.rdNonQuery.Size = new System.Drawing.Size(106, 24);
            this.rdNonQuery.TabIndex = 16;
            this.rdNonQuery.TabStop = true;
            this.rdNonQuery.Text = "NonQuery";
            this.rdNonQuery.UseVisualStyleBackColor = true;
            // 
            // btnChangeDtBs
            // 
            this.btnChangeDtBs.Location = new System.Drawing.Point(280, 83);
            this.btnChangeDtBs.Name = "btnChangeDtBs";
            this.btnChangeDtBs.Size = new System.Drawing.Size(113, 23);
            this.btnChangeDtBs.TabIndex = 15;
            this.btnChangeDtBs.Text = "Select database";
            this.btnChangeDtBs.UseVisualStyleBackColor = true;
            this.btnChangeDtBs.Click += new System.EventHandler(this.btnChangeDtBs_Click);
            // 
            // cbxChsDtBs
            // 
            this.cbxChsDtBs.FormattingEnabled = true;
            this.cbxChsDtBs.Items.AddRange(new object[] {
            "My - BD17188",
            "Prática Prof. - BDPRII17188"});
            this.cbxChsDtBs.Location = new System.Drawing.Point(12, 85);
            this.cbxChsDtBs.Name = "cbxChsDtBs";
            this.cbxChsDtBs.Size = new System.Drawing.Size(247, 21);
            this.cbxChsDtBs.TabIndex = 14;
            // 
            // rdAutomatic
            // 
            this.rdAutomatic.AutoSize = true;
            this.rdAutomatic.Checked = true;
            this.rdAutomatic.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdAutomatic.Location = new System.Drawing.Point(91, 663);
            this.rdAutomatic.Name = "rdAutomatic";
            this.rdAutomatic.Size = new System.Drawing.Size(108, 24);
            this.rdAutomatic.TabIndex = 21;
            this.rdAutomatic.TabStop = true;
            this.rdAutomatic.Text = "Automatic";
            this.rdAutomatic.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(565, 653);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(86, 30);
            this.btnSave.TabIndex = 22;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnOtherDtBs
            // 
            this.btnOtherDtBs.Location = new System.Drawing.Point(416, 83);
            this.btnOtherDtBs.Name = "btnOtherDtBs";
            this.btnOtherDtBs.Size = new System.Drawing.Size(75, 23);
            this.btnOtherDtBs.TabIndex = 23;
            this.btnOtherDtBs.Text = "Other";
            this.btnOtherDtBs.UseVisualStyleBackColor = true;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Modern No. 20", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.Location = new System.Drawing.Point(322, 9);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(703, 50);
            this.lbTitle.TabIndex = 24;
            this.lbTitle.Text = "Dillen\'s SQL Management Studio";
            // 
            // btnBiggerFont
            // 
            this.btnBiggerFont.Location = new System.Drawing.Point(724, 85);
            this.btnBiggerFont.Name = "btnBiggerFont";
            this.btnBiggerFont.Size = new System.Drawing.Size(29, 23);
            this.btnBiggerFont.TabIndex = 25;
            this.btnBiggerFont.Text = "+";
            this.btnBiggerFont.UseVisualStyleBackColor = true;
            // 
            // btnSmallerFont
            // 
            this.btnSmallerFont.Location = new System.Drawing.Point(689, 85);
            this.btnSmallerFont.Name = "btnSmallerFont";
            this.btnSmallerFont.Size = new System.Drawing.Size(29, 23);
            this.btnSmallerFont.TabIndex = 26;
            this.btnSmallerFont.Text = "-";
            this.btnSmallerFont.UseVisualStyleBackColor = true;
            // 
            // btnAllTables
            // 
            this.btnAllTables.Location = new System.Drawing.Point(1280, 57);
            this.btnAllTables.Name = "btnAllTables";
            this.btnAllTables.Size = new System.Drawing.Size(66, 23);
            this.btnAllTables.TabIndex = 28;
            this.btnAllTables.Text = "All tables";
            this.btnAllTables.UseVisualStyleBackColor = true;
            this.btnAllTables.Click += new System.EventHandler(this.btnAllTables_Click);
            // 
            // FrmDillenSQLManagementStudio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 703);
            this.Controls.Add(this.btnAllTables);
            this.Controls.Add(this.btnSmallerFont);
            this.Controls.Add(this.btnBiggerFont);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.btnOtherDtBs);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.rdAutomatic);
            this.Controls.Add(this.grvSelect);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.rchtxtCode);
            this.Controls.Add(this.rdSelect);
            this.Controls.Add(this.rdNonQuery);
            this.Controls.Add(this.btnChangeDtBs);
            this.Controls.Add(this.cbxChsDtBs);
            this.Name = "FrmDillenSQLManagementStudio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dillenburg\'s Product";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grvSelect)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grvSelect;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.RichTextBox rchtxtCode;
        private System.Windows.Forms.RadioButton rdSelect;
        private System.Windows.Forms.RadioButton rdNonQuery;
        private System.Windows.Forms.Button btnChangeDtBs;
        private System.Windows.Forms.ComboBox cbxChsDtBs;
        private System.Windows.Forms.RadioButton rdAutomatic;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnOtherDtBs;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Button btnBiggerFont;
        private System.Windows.Forms.Button btnSmallerFont;
        private System.Windows.Forms.Button btnAllTables;
    }
}

