namespace DillenManagementStudio
{
    partial class FrmSplash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSplash));
            this.label1 = new System.Windows.Forms.Label();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.lbStage = new System.Windows.Forms.Label();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(96, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(424, 100);
            this.label1.TabIndex = 1;
            this.label1.Text = "     Dillen\'s SQL\r\nManagement Studio";
            // 
            // tmr
            // 
            this.tmr.Enabled = true;
            this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
            // 
            // lbStage
            // 
            this.lbStage.AutoSize = true;
            this.lbStage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(45)))));
            this.lbStage.Font = new System.Drawing.Font("Lucida Bright", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStage.ForeColor = System.Drawing.Color.Transparent;
            this.lbStage.Location = new System.Drawing.Point(3, 8);
            this.lbStage.Name = "lbStage";
            this.lbStage.Size = new System.Drawing.Size(194, 14);
            this.lbStage.TabIndex = 2;
            this.lbStage.Text = "Trying to connect to Unicamp VPN";
            // 
            // picLoading
            // 
            this.picLoading.BackColor = System.Drawing.Color.Transparent;
            this.picLoading.Image = global::DillenManagementStudio.Properties.Resources.Loading_icon1;
            this.picLoading.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.picLoading.Location = new System.Drawing.Point(202, 5);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(18, 18);
            this.picLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLoading.TabIndex = 38;
            this.picLoading.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(45)))));
            this.panel1.Controls.Add(this.lbStage);
            this.panel1.Controls.Add(this.picLoading);
            this.panel1.Location = new System.Drawing.Point(-1, 193);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(620, 28);
            this.panel1.TabIndex = 39;
            // 
            // FrmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(618, 221);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSplash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSplash";
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer tmr;
        private System.Windows.Forms.Label lbStage;
        private System.Windows.Forms.PictureBox picLoading;
        private System.Windows.Forms.Panel panel1;
    }
}