namespace BrandInspector.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnScanFonts;
        private System.Windows.Forms.Button btnScanColors;
        private System.Windows.Forms.Button btnScanSizes;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.ListBox lstErrors;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblTotals;
        private System.Windows.Forms.ToolStripStatusLabel lblNonCompliant;
        private System.Windows.Forms.ToolStripProgressBar progressBar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnScanFonts = new System.Windows.Forms.Button();
            this.btnScanColors = new System.Windows.Forms.Button();
            this.btnScanSizes = new System.Windows.Forms.Button();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.lstErrors = new System.Windows.Forms.ListBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblTotals = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNonCompliant = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(12, 12);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(600, 22);
            this.txtFilePath.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(620, 10);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(90, 26);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnScanFonts
            // 
            this.btnScanFonts.Location = new System.Drawing.Point(12, 45);
            this.btnScanFonts.Name = "btnScanFonts";
            this.btnScanFonts.Size = new System.Drawing.Size(120, 30);
            this.btnScanFonts.TabIndex = 2;
            this.btnScanFonts.Text = "Scan Fonts";
            this.btnScanFonts.UseVisualStyleBackColor = true;
            this.btnScanFonts.Click += new System.EventHandler(this.btnScanFonts_Click);
            // 
            // btnScanColors
            // 
            this.btnScanColors.Location = new System.Drawing.Point(138, 45);
            this.btnScanColors.Name = "btnScanColors";
            this.btnScanColors.Size = new System.Drawing.Size(120, 30);
            this.btnScanColors.TabIndex = 3;
            this.btnScanColors.Text = "Scan Colors";
            this.btnScanColors.UseVisualStyleBackColor = true;
            this.btnScanColors.Click += new System.EventHandler(this.btnScanColors_Click);
            // 
            // btnScanSizes
            // 
            this.btnScanSizes.Location = new System.Drawing.Point(264, 45);
            this.btnScanSizes.Name = "btnScanSizes";
            this.btnScanSizes.Size = new System.Drawing.Size(120, 30);
            this.btnScanSizes.TabIndex = 4;
            this.btnScanSizes.Text = "Scan Sizes";
            this.btnScanSizes.UseVisualStyleBackColor = true;
            this.btnScanSizes.Click += new System.EventHandler(this.btnScanSizes_Click);
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Location = new System.Drawing.Point(200, 90);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.RowHeadersWidth = 51;
            this.dgvResults.RowTemplate.Height = 24;
            this.dgvResults.Size = new System.Drawing.Size(730, 430);
            this.dgvResults.TabIndex = 5;
            // 
            // lstErrors
            // 
            this.lstErrors.FormattingEnabled = true;
            this.lstErrors.ItemHeight = 16;
            this.lstErrors.Location = new System.Drawing.Point(12, 90);
            this.lstErrors.Name = "lstErrors";
            this.lstErrors.Size = new System.Drawing.Size(180, 420);
            this.lstErrors.TabIndex = 6;
            this.lstErrors.SelectedIndexChanged += new System.EventHandler(this.lstErrors_SelectedIndexChanged);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTotals,
            this.lblNonCompliant,
            this.progressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 530);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(950, 24);
            this.statusStrip.TabIndex = 7;
            this.statusStrip.Text = "statusStrip";
            // 
            // lblTotals
            // 
            this.lblTotals.Name = "lblTotals";
            this.lblTotals.Size = new System.Drawing.Size(60, 19);
            this.lblTotals.Text = "Total: 0";
            // 
            // lblNonCompliant
            // 
            this.lblNonCompliant.Margin = new System.Windows.Forms.Padding(15, 3, 0, 2);
            this.lblNonCompliant.Name = "lblNonCompliant";
            this.lblNonCompliant.Size = new System.Drawing.Size(120, 19);
            this.lblNonCompliant.Text = "Non-Compliant: 0";
            // 
            // progressBar
            // 
            this.progressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(200, 16);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 554);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnScanFonts);
            this.Controls.Add(this.btnScanColors);
            this.Controls.Add(this.btnScanSizes);
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.lstErrors);
            this.Controls.Add(this.statusStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Brand Inspector";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
