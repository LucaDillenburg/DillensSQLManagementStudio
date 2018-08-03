namespace DillenManagementStudio
{
    partial class FrmCommandExplanation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCommandExplanation));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbTitle = new System.Windows.Forms.Label();
            this.rchtxtTryCode = new System.Windows.Forms.RichTextBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.grvSelectTry = new System.Windows.Forms.DataGridView();
            this.lbExecutionResult = new System.Windows.Forms.Label();
            this.btnHelp = new System.Windows.Forms.Button();
            this.rchtxtAux = new System.Windows.Forms.RichTextBox();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.pnlTitle = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.grvSelectTry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            resources.ApplyResources(this.lbTitle, "lbTitle");
            this.lbTitle.BackColor = System.Drawing.Color.Transparent;
            this.lbTitle.ForeColor = System.Drawing.SystemColors.Control;
            this.lbTitle.Name = "lbTitle";
            // 
            // rchtxtTryCode
            // 
            resources.ApplyResources(this.rchtxtTryCode, "rchtxtTryCode");
            this.rchtxtTryCode.HideSelection = false;
            this.rchtxtTryCode.Name = "rchtxtTryCode";
            this.rchtxtTryCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCommandExplanation_KeyDown);
            // 
            // btnExecute
            // 
            resources.ApplyResources(this.btnExecute, "btnExecute");
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnPrevious
            // 
            resources.ApplyResources(this.btnPrevious, "btnPrevious");
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            resources.ApplyResources(this.btnNext, "btnNext");
            this.btnNext.Name = "btnNext";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // grvSelectTry
            // 
            this.grvSelectTry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grvSelectTry.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.grvSelectTry, "grvSelectTry");
            this.grvSelectTry.Name = "grvSelectTry";
            this.grvSelectTry.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grvSelectTry.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            // 
            // lbExecutionResult
            // 
            resources.ApplyResources(this.lbExecutionResult, "lbExecutionResult");
            this.lbExecutionResult.ForeColor = System.Drawing.Color.Green;
            this.lbExecutionResult.Name = "lbExecutionResult";
            // 
            // btnHelp
            // 
            resources.ApplyResources(this.btnHelp, "btnHelp");
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // rchtxtAux
            // 
            resources.ApplyResources(this.rchtxtAux, "rchtxtAux");
            this.rchtxtAux.Name = "rchtxtAux";
            // 
            // picLoading
            // 
            this.picLoading.BackColor = System.Drawing.Color.White;
            this.picLoading.Image = global::DillenManagementStudio.Properties.Resources.Loading_icon1;
            resources.ApplyResources(this.picLoading, "picLoading");
            this.picLoading.Name = "picLoading";
            this.picLoading.TabStop = false;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(45)))));
            this.pnlTitle.Controls.Add(this.lbTitle);
            resources.ApplyResources(this.pnlTitle, "pnlTitle");
            this.pnlTitle.Name = "pnlTitle";
            // 
            // FrmCommandExplanation
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.picLoading);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.lbExecutionResult);
            this.Controls.Add(this.grvSelectTry);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.rchtxtAux);
            this.Name = "FrmCommandExplanation";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmCommandExplanation_FormClosed);
            this.Shown += new System.EventHandler(this.FrmCommandExplanation_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCommandExplanation_KeyDown);
            this.Resize += new System.EventHandler(this.FrmCommandExplanation_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.grvSelectTry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.RichTextBox rchtxtTryCode;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.DataGridView grvSelectTry;
        private System.Windows.Forms.Label lbExecutionResult;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.RichTextBox rchtxtAux;
        private System.Windows.Forms.PictureBox picLoading;
        private System.Windows.Forms.Panel pnlTitle;
    }
}