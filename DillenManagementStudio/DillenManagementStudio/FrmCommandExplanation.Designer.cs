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
            this.lbTitle = new System.Windows.Forms.Label();
            this.lbExplanation = new System.Windows.Forms.Label();
            this.rchtxtTryCode = new System.Windows.Forms.RichTextBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.grvSelectTry = new System.Windows.Forms.DataGridView();
            this.lbExecutionResult = new System.Windows.Forms.Label();
            this.btnHelp = new System.Windows.Forms.Button();
            this.rchtxtAux = new System.Windows.Forms.RichTextBox();
            this.picLoading = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.grvSelectTry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.Location = new System.Drawing.Point(135, 9);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(400, 31);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "Part of Command Explanation";
            // 
            // lbExplanation
            // 
            this.lbExplanation.AutoSize = true;
            this.lbExplanation.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbExplanation.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbExplanation.Location = new System.Drawing.Point(8, 49);
            this.lbExplanation.Name = "lbExplanation";
            this.lbExplanation.Size = new System.Drawing.Size(658, 234);
            this.lbExplanation.TabIndex = 1;
            this.lbExplanation.Text = resources.GetString("lbExplanation.Text");
            // 
            // rchtxtTryCode
            // 
            this.rchtxtTryCode.Location = new System.Drawing.Point(24, 281);
            this.rchtxtTryCode.Name = "rchtxtTryCode";
            this.rchtxtTryCode.Size = new System.Drawing.Size(249, 161);
            this.rchtxtTryCode.TabIndex = 2;
            this.rchtxtTryCode.Text = "";
            // 
            // btnExecute
            // 
            this.btnExecute.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecute.Location = new System.Drawing.Point(295, 334);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(78, 50);
            this.btnExecute.TabIndex = 3;
            this.btnExecute.Text = "Execute >>";
            this.btnExecute.UseVisualStyleBackColor = true;
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(12, 462);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 23);
            this.btnPrevious.TabIndex = 4;
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(584, 462);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // grvSelectTry
            // 
            this.grvSelectTry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvSelectTry.Location = new System.Drawing.Point(405, 297);
            this.grvSelectTry.Name = "grvSelectTry";
            this.grvSelectTry.Size = new System.Drawing.Size(240, 145);
            this.grvSelectTry.TabIndex = 6;
            // 
            // lbExecutionResult
            // 
            this.lbExecutionResult.AutoSize = true;
            this.lbExecutionResult.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbExecutionResult.ForeColor = System.Drawing.Color.Green;
            this.lbExecutionResult.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbExecutionResult.Location = new System.Drawing.Point(411, 279);
            this.lbExecutionResult.Name = "lbExecutionResult";
            this.lbExecutionResult.Size = new System.Drawing.Size(138, 15);
            this.lbExecutionResult.TabIndex = 31;
            this.lbExecutionResult.Text = "Succesfully executed!";
            this.lbExecutionResult.Visible = false;
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(279, 281);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(26, 26);
            this.btnHelp.TabIndex = 32;
            this.btnHelp.Text = "?";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // rchtxtAux
            // 
            this.rchtxtAux.Location = new System.Drawing.Point(24, 281);
            this.rchtxtAux.Name = "rchtxtAux";
            this.rchtxtAux.Size = new System.Drawing.Size(249, 161);
            this.rchtxtAux.TabIndex = 33;
            this.rchtxtAux.Text = "";
            // 
            // picLoading
            // 
            this.picLoading.BackColor = System.Drawing.Color.Transparent;
            this.picLoading.Image = global::DillenManagementStudio.Properties.Resources.Loading_icon1;
            this.picLoading.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.picLoading.Location = new System.Drawing.Point(116, 329);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(66, 66);
            this.picLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLoading.TabIndex = 38;
            this.picLoading.TabStop = false;
            // 
            // FrmCommandExplanation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 496);
            this.Controls.Add(this.rchtxtTryCode);
            this.Controls.Add(this.picLoading);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.lbExecutionResult);
            this.Controls.Add(this.grvSelectTry);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.lbExplanation);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.rchtxtAux);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmCommandExplanation";
            this.Text = "Command Name";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmCommandExplanation_FormClosed);
            this.Shown += new System.EventHandler(this.FrmCommandExplanation_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.grvSelectTry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label lbExplanation;
        private System.Windows.Forms.RichTextBox rchtxtTryCode;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.DataGridView grvSelectTry;
        private System.Windows.Forms.Label lbExecutionResult;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.RichTextBox rchtxtAux;
        private System.Windows.Forms.PictureBox picLoading;
    }
}