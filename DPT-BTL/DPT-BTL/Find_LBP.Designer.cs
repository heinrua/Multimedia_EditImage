namespace DPT_BTL
{
    partial class Find_LBP
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
            pictureBoxOriginal = new PictureBox();
            btnChooseImage = new Button();
            btnFindSimilarImages = new Button();
            flowLayoutPanelResults = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOriginal).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxOriginal
            // 
            pictureBoxOriginal.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxOriginal.Location = new Point(40, 110);
            pictureBoxOriginal.Name = "pictureBoxOriginal";
            pictureBoxOriginal.Size = new Size(175, 217);
            pictureBoxOriginal.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxOriginal.TabIndex = 0;
            pictureBoxOriginal.TabStop = false;
            // 
            // btnChooseImage
            // 
            btnChooseImage.Location = new Point(79, 54);
            btnChooseImage.Name = "btnChooseImage";
            btnChooseImage.Size = new Size(94, 29);
            btnChooseImage.TabIndex = 1;
            btnChooseImage.Text = "Chọn ảnh";
            btnChooseImage.UseVisualStyleBackColor = true;
            btnChooseImage.Click += btnChooseImage_Click;
            // 
            // btnFindSimilarImages
            // 
            btnFindSimilarImages.Location = new Point(346, 54);
            btnFindSimilarImages.Name = "btnFindSimilarImages";
            btnFindSimilarImages.Size = new Size(94, 29);
            btnFindSimilarImages.TabIndex = 2;
            btnFindSimilarImages.Text = "Tìm ảnh";
            btnFindSimilarImages.UseVisualStyleBackColor = true;
            btnFindSimilarImages.Click += btnFindSimilarImages_Click;
            // 
            // flowLayoutPanelResults
            // 
            flowLayoutPanelResults.AutoScroll = true;
            flowLayoutPanelResults.BorderStyle = BorderStyle.FixedSingle;
            flowLayoutPanelResults.Location = new Point(303, 106);
            flowLayoutPanelResults.Name = "flowLayoutPanelResults";
            flowLayoutPanelResults.Size = new Size(629, 325);
            flowLayoutPanelResults.TabIndex = 3;
            // 
            // Find_LBP
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1031, 496);
            Controls.Add(flowLayoutPanelResults);
            Controls.Add(btnFindSimilarImages);
            Controls.Add(btnChooseImage);
            Controls.Add(pictureBoxOriginal);
            Name = "Find_LBP";
            Text = "Find_LBP";
            ((System.ComponentModel.ISupportInitialize)pictureBoxOriginal).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBoxOriginal;
        private Button btnChooseImage;
        private Button btnFindSimilarImages;
        private FlowLayoutPanel flowLayoutPanelResults;
    }
}