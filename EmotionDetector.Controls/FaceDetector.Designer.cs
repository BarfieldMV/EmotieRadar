﻿namespace EmotionDetector.Controls
{
    partial class FaceDetectorForm
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
            this.selectPhotoButton = new MetroFramework.Controls.MetroButton();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.facePictureBox = new System.Windows.Forms.PictureBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.ComboSelectors = new MetroFramework.Controls.MetroComboBox();
            this.ComboDetectors = new MetroFramework.Controls.MetroComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.facePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // selectPhotoButton
            // 
            this.selectPhotoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectPhotoButton.Location = new System.Drawing.Point(23, 404);
            this.selectPhotoButton.Name = "selectPhotoButton";
            this.selectPhotoButton.Size = new System.Drawing.Size(113, 42);
            this.selectPhotoButton.TabIndex = 1;
            this.selectPhotoButton.Text = "Kies een foto";
            this.selectPhotoButton.Click += new System.EventHandler(this.selectPhotoButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // facePictureBox
            // 
            this.facePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.facePictureBox.Location = new System.Drawing.Point(24, 64);
            this.facePictureBox.Name = "facePictureBox";
            this.facePictureBox.Size = new System.Drawing.Size(440, 334);
            this.facePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.facePictureBox.TabIndex = 2;
            this.facePictureBox.TabStop = false;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid1.CommandsDisabledLinkColor = System.Drawing.Color.Black;
            this.propertyGrid1.CommandsVisibleIfAvailable = false;
            this.propertyGrid1.HelpVisible = false;
            this.propertyGrid1.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propertyGrid1.Location = new System.Drawing.Point(470, 64);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(305, 334);
            this.propertyGrid1.TabIndex = 3;
            this.propertyGrid1.ToolbarVisible = false;
            // 
            // ComboSelectors
            // 
            this.ComboSelectors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboSelectors.FormattingEnabled = true;
            this.ComboSelectors.ItemHeight = 23;
            this.ComboSelectors.Location = new System.Drawing.Point(299, 405);
            this.ComboSelectors.Name = "ComboSelectors";
            this.ComboSelectors.Size = new System.Drawing.Size(476, 29);
            this.ComboSelectors.TabIndex = 4;
            this.ComboSelectors.SelectedIndexChanged += new System.EventHandler(this.ComboFaces_SelectedIndexChanged);
            // 
            // ComboDetectors
            // 
            this.ComboDetectors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboDetectors.FormattingEnabled = true;
            this.ComboDetectors.ItemHeight = 23;
            this.ComboDetectors.Location = new System.Drawing.Point(299, 29);
            this.ComboDetectors.Name = "ComboDetectors";
            this.ComboDetectors.Size = new System.Drawing.Size(476, 29);
            this.ComboDetectors.TabIndex = 5;
            // 
            // FaceDetectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 469);
            this.Controls.Add(this.ComboDetectors);
            this.Controls.Add(this.ComboSelectors);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.facePictureBox);
            this.Controls.Add(this.selectPhotoButton);
            this.Name = "FaceDetectorForm";
            this.Text = "Detector";
            ((System.ComponentModel.ISupportInitialize)(this.facePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroButton selectPhotoButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.PictureBox facePictureBox;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private MetroFramework.Controls.MetroComboBox ComboSelectors;
        private MetroFramework.Controls.MetroComboBox ComboDetectors;
    }
}

