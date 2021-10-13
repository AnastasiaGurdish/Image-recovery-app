
namespace ImageRecoveryApp
{
    partial class ImageRecoveryImage
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.UploadPhotoButton = new System.Windows.Forms.Button();
            this.UploadedImage = new System.Windows.Forms.PictureBox();
            this.SpoilImage = new System.Windows.Forms.Button();
            this.RecoverImage = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.SpoiledImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.UploadedImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpoiledImage)).BeginInit();
            this.SuspendLayout();
            // 
            // UploadPhotoButton
            // 
            this.UploadPhotoButton.BackColor = System.Drawing.Color.RosyBrown;
            this.UploadPhotoButton.FlatAppearance.BorderSize = 0;
            this.UploadPhotoButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.OrangeRed;
            this.UploadPhotoButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.OrangeRed;
            this.UploadPhotoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UploadPhotoButton.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UploadPhotoButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.UploadPhotoButton.Location = new System.Drawing.Point(28, 26);
            this.UploadPhotoButton.Name = "UploadPhotoButton";
            this.UploadPhotoButton.Size = new System.Drawing.Size(366, 48);
            this.UploadPhotoButton.TabIndex = 0;
            this.UploadPhotoButton.Text = "Загрузить фото";
            this.UploadPhotoButton.UseVisualStyleBackColor = false;
            this.UploadPhotoButton.Click += new System.EventHandler(this.UploadPhotoButton_Click);
            // 
            // UploadedImage
            // 
            this.UploadedImage.BackColor = System.Drawing.Color.Snow;
            this.UploadedImage.Location = new System.Drawing.Point(422, 26);
            this.UploadedImage.Name = "UploadedImage";
            this.UploadedImage.Size = new System.Drawing.Size(450, 450);
            this.UploadedImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.UploadedImage.TabIndex = 1;
            this.UploadedImage.TabStop = false;
            // 
            // SpoilImage
            // 
            this.SpoilImage.BackColor = System.Drawing.Color.RosyBrown;
            this.SpoilImage.FlatAppearance.BorderSize = 0;
            this.SpoilImage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.OrangeRed;
            this.SpoilImage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.OrangeRed;
            this.SpoilImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SpoilImage.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SpoilImage.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SpoilImage.Location = new System.Drawing.Point(28, 101);
            this.SpoilImage.Name = "SpoilImage";
            this.SpoilImage.Size = new System.Drawing.Size(366, 48);
            this.SpoilImage.TabIndex = 5;
            this.SpoilImage.Text = "Наложить искажение";
            this.SpoilImage.UseVisualStyleBackColor = false;
            this.SpoilImage.Click += new System.EventHandler(this.SpoilImage_Click);
            // 
            // RecoverImage
            // 
            this.RecoverImage.BackColor = System.Drawing.Color.RosyBrown;
            this.RecoverImage.FlatAppearance.BorderSize = 0;
            this.RecoverImage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.OrangeRed;
            this.RecoverImage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.OrangeRed;
            this.RecoverImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RecoverImage.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RecoverImage.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.RecoverImage.Location = new System.Drawing.Point(28, 179);
            this.RecoverImage.Name = "RecoverImage";
            this.RecoverImage.Size = new System.Drawing.Size(366, 48);
            this.RecoverImage.TabIndex = 6;
            this.RecoverImage.Text = "Восстановить изображение";
            this.RecoverImage.UseVisualStyleBackColor = false;
            this.RecoverImage.Click += new System.EventHandler(this.RecoverImage_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.BackColor = System.Drawing.Color.RosyBrown;
            this.SaveButton.FlatAppearance.BorderSize = 0;
            this.SaveButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.OrangeRed;
            this.SaveButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.OrangeRed;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SaveButton.Location = new System.Drawing.Point(28, 255);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(366, 48);
            this.SaveButton.TabIndex = 7;
            this.SaveButton.Text = "Сохранить";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.pictureBox1.Location = new System.Drawing.Point(422, 503);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(450, 290);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.pictureBox2.Location = new System.Drawing.Point(898, 503);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(450, 290);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // SpoiledImage
            // 
            this.SpoiledImage.BackColor = System.Drawing.Color.Snow;
            this.SpoiledImage.Location = new System.Drawing.Point(898, 26);
            this.SpoiledImage.Name = "SpoiledImage";
            this.SpoiledImage.Size = new System.Drawing.Size(450, 450);
            this.SpoiledImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.SpoiledImage.TabIndex = 11;
            this.SpoiledImage.TabStop = false;
            // 
            // ImageRecoveryImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(1382, 823);
            this.Controls.Add(this.SpoiledImage);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.RecoverImage);
            this.Controls.Add(this.SpoilImage);
            this.Controls.Add(this.UploadedImage);
            this.Controls.Add(this.UploadPhotoButton);
            this.Name = "ImageRecoveryImage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Реставратор фото";
            ((System.ComponentModel.ISupportInitialize)(this.UploadedImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpoiledImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button UploadPhotoButton;
        private System.Windows.Forms.PictureBox UploadedImage;
        private System.Windows.Forms.Button SpoilImage;
        private System.Windows.Forms.Button RecoverImage;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox SpoiledImage;
    }
}

