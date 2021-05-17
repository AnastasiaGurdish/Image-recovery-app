
namespace FourierTransformFFT
{
    partial class FFT
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transformToFreqencyDomainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transformToSpatialDomainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.image_frame = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.image_frame)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1264, 30);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 26);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transformToFreqencyDomainToolStripMenuItem,
            this.transformToSpatialDomainToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(49, 26);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // transformToFreqencyDomainToolStripMenuItem
            // 
            this.transformToFreqencyDomainToolStripMenuItem.Name = "transformToFreqencyDomainToolStripMenuItem";
            this.transformToFreqencyDomainToolStripMenuItem.Size = new System.Drawing.Size(292, 26);
            this.transformToFreqencyDomainToolStripMenuItem.Text = "Transform to freqency domain";
            this.transformToFreqencyDomainToolStripMenuItem.Click += new System.EventHandler(this.transformToFreqencyDomainToolStripMenuItem_Click);
            // 
            // transformToSpatialDomainToolStripMenuItem
            // 
            this.transformToSpatialDomainToolStripMenuItem.Name = "transformToSpatialDomainToolStripMenuItem";
            this.transformToSpatialDomainToolStripMenuItem.Size = new System.Drawing.Size(292, 26);
            this.transformToSpatialDomainToolStripMenuItem.Text = "Transform to spatial domain";
            this.transformToSpatialDomainToolStripMenuItem.Click += new System.EventHandler(this.transformToSpatialDomainToolStripMenuItem_Click);
            // 
            // image_frame
            // 
            this.image_frame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.image_frame.Location = new System.Drawing.Point(0, 30);
            this.image_frame.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.image_frame.Name = "image_frame";
            this.image_frame.Size = new System.Drawing.Size(1264, 701);
            this.image_frame.TabIndex = 1;
            this.image_frame.TabStop = false;
            // 
            // FFT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 731);
            this.Controls.Add(this.image_frame);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FFT";
            this.Text = "Fourier Transform Demonstration";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.image_frame)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transformToFreqencyDomainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transformToSpatialDomainToolStripMenuItem;
        private System.Windows.Forms.PictureBox image_frame;
    }
}

