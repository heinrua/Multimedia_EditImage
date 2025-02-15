namespace DPT_BTL
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox = new PictureBox();
            btnUpLoad = new Button();
            btnSave = new Button();
            trackBarBrightness = new TrackBar();
            btnLeft = new Button();
            label1 = new Label();
            btnRight = new Button();
            btnFindSIFT = new Button();
            btnCropImg = new Button();
            btnRemoveBG = new Button();
            btnFindDraw = new Button();
            btnFindLBP = new Button();
            btnReduceNoise = new Button();
            btnBlur = new Button();
            btnCompress = new Button();
            btnSaveCompressed = new Button();
            btnDecompress = new Button();
            btnHuffman = new Button();
            trackBarSize = new TrackBar();
            label2 = new Label();
            trackBarContrast = new TrackBar();
            label3 = new Label();
            btnLoadCompressed = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBrightness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarContrast).BeginInit();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.BackColor = SystemColors.Control;
            pictureBox.Location = new Point(58, 37);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(271, 349);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            pictureBox.Click += pictureBox_Click;
            // 
            // btnUpLoad
            // 
            btnUpLoad.Location = new Point(58, 411);
            btnUpLoad.Name = "btnUpLoad";
            btnUpLoad.Size = new Size(133, 29);
            btnUpLoad.TabIndex = 1;
            btnUpLoad.Text = "Tải ảnh";
            btnUpLoad.UseVisualStyleBackColor = true;
            btnUpLoad.Click += btnUpLoad_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(197, 411);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(133, 29);
            btnSave.TabIndex = 2;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // trackBarBrightness
            // 
            trackBarBrightness.Location = new Point(461, 243);
            trackBarBrightness.Name = "trackBarBrightness";
            trackBarBrightness.Size = new Size(370, 56);
            trackBarBrightness.TabIndex = 3;
            trackBarBrightness.Scroll += TrackBarBrightness_Scroll;
            // 
            // btnLeft
            // 
            btnLeft.Location = new Point(375, 348);
            btnLeft.Name = "btnLeft";
            btnLeft.Size = new Size(94, 29);
            btnLeft.TabIndex = 4;
            btnLeft.Text = "Xoay trái";
            btnLeft.UseVisualStyleBackColor = true;
            btnLeft.Click += btnLeft_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(375, 243);
            label1.Name = "label1";
            label1.Size = new Size(67, 20);
            label1.TabIndex = 5;
            label1.Text = "Độ sáng:";
            // 
            // btnRight
            // 
            btnRight.Location = new Point(497, 348);
            btnRight.Name = "btnRight";
            btnRight.Size = new Size(94, 29);
            btnRight.TabIndex = 6;
            btnRight.Text = "Xoay phải";
            btnRight.UseVisualStyleBackColor = true;
            btnRight.Click += btnRight_Click;
            // 
            // btnFindSIFT
            // 
            btnFindSIFT.Location = new Point(375, 88);
            btnFindSIFT.Name = "btnFindSIFT";
            btnFindSIFT.Size = new Size(456, 29);
            btnFindSIFT.TabIndex = 7;
            btnFindSIFT.Text = "Tìm ảnh tương tự sử dụng SIFT";
            btnFindSIFT.UseVisualStyleBackColor = true;
            btnFindSIFT.Click += btnFindSIFT_Click;
            // 
            // btnCropImg
            // 
            btnCropImg.Location = new Point(617, 348);
            btnCropImg.Name = "btnCropImg";
            btnCropImg.Size = new Size(94, 29);
            btnCropImg.TabIndex = 8;
            btnCropImg.Text = "Cắt";
            btnCropImg.UseVisualStyleBackColor = true;
            btnCropImg.Click += btnCropImg_Click;
            // 
            // btnRemoveBG
            // 
            btnRemoveBG.Location = new Point(617, 400);
            btnRemoveBG.Name = "btnRemoveBG";
            btnRemoveBG.Size = new Size(94, 29);
            btnRemoveBG.TabIndex = 9;
            btnRemoveBG.Text = "Xóa phông";
            btnRemoveBG.UseVisualStyleBackColor = true;
            btnRemoveBG.Click += btnRemoveBG_Click;
            // 
            // btnFindDraw
            // 
            btnFindDraw.Location = new Point(375, 37);
            btnFindDraw.Name = "btnFindDraw";
            btnFindDraw.Size = new Size(456, 29);
            btnFindDraw.TabIndex = 10;
            btnFindDraw.Text = "Tìm ảnh phác họa";
            btnFindDraw.UseVisualStyleBackColor = true;
            btnFindDraw.Click += btnFindDraw_Click;
            // 
            // btnFindLBP
            // 
            btnFindLBP.Location = new Point(375, 137);
            btnFindLBP.Name = "btnFindLBP";
            btnFindLBP.Size = new Size(456, 29);
            btnFindLBP.TabIndex = 11;
            btnFindLBP.Text = "Tìm ảnh tương tự sử dụng LBP";
            btnFindLBP.UseVisualStyleBackColor = true;
            btnFindLBP.Click += btnFindLBP_Click;
            // 
            // btnReduceNoise
            // 
            btnReduceNoise.Location = new Point(375, 400);
            btnReduceNoise.Name = "btnReduceNoise";
            btnReduceNoise.Size = new Size(94, 29);
            btnReduceNoise.TabIndex = 12;
            btnReduceNoise.Text = "Khử nhiễu";
            btnReduceNoise.UseVisualStyleBackColor = true;
            btnReduceNoise.Click += btnReduceNoise_Click;
            // 
            // btnBlur
            // 
            btnBlur.Location = new Point(497, 400);
            btnBlur.Name = "btnBlur";
            btnBlur.Size = new Size(94, 29);
            btnBlur.TabIndex = 13;
            btnBlur.Text = "Làm mờ";
            btnBlur.UseVisualStyleBackColor = true;
            btnBlur.Click += btnBlur_Click;
            // 
            // btnCompress
            // 
            btnCompress.Location = new Point(58, 471);
            btnCompress.Name = "btnCompress";
            btnCompress.Size = new Size(125, 29);
            btnCompress.TabIndex = 14;
            btnCompress.Text = "Nén ảnh LZW";
            btnCompress.UseVisualStyleBackColor = true;
            btnCompress.Click += btnCompress_Click;
            // 
            // btnSaveCompressed
            // 
            btnSaveCompressed.Location = new Point(721, 471);
            btnSaveCompressed.Name = "btnSaveCompressed";
            btnSaveCompressed.Size = new Size(94, 29);
            btnSaveCompressed.TabIndex = 15;
            btnSaveCompressed.Text = "Lưu file nén ";
            btnSaveCompressed.UseVisualStyleBackColor = true;
            btnSaveCompressed.Click += btnSaveCompressed_Click;
            // 
            // btnDecompress
            // 
            btnDecompress.Location = new Point(375, 471);
            btnDecompress.Name = "btnDecompress";
            btnDecompress.Size = new Size(134, 29);
            btnDecompress.TabIndex = 16;
            btnDecompress.Text = "Giải nén LZW";
            btnDecompress.UseVisualStyleBackColor = true;
            btnDecompress.Click += btnDecompress_Click;
            // 
            // btnHuffman
            // 
            btnHuffman.Location = new Point(197, 471);
            btnHuffman.Name = "btnHuffman";
            btnHuffman.Size = new Size(139, 29);
            btnHuffman.TabIndex = 17;
            btnHuffman.Text = "Nén ảnh Huffman";
            btnHuffman.UseVisualStyleBackColor = true;
            btnHuffman.Click += btnHuffman_Click;
            // 
            // trackBarSize
            // 
            trackBarSize.Location = new Point(461, 181);
            trackBarSize.Name = "trackBarSize";
            trackBarSize.Size = new Size(354, 56);
            trackBarSize.TabIndex = 18;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(375, 181);
            label2.Name = "label2";
            label2.Size = new Size(80, 20);
            label2.TabIndex = 19;
            label2.Text = "Thu phóng";
            // 
            // trackBarContrast
            // 
            trackBarContrast.Location = new Point(461, 285);
            trackBarContrast.Name = "trackBarContrast";
            trackBarContrast.Size = new Size(370, 56);
            trackBarContrast.TabIndex = 20;
            trackBarContrast.Scroll += trackBarContrast_Scroll_1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(375, 285);
            label3.Name = "label3";
            label3.Size = new Size(92, 20);
            label3.TabIndex = 21;
            label3.Text = "Tương phản:";
            // 
            // btnLoadCompressed
            // 
            btnLoadCompressed.Location = new Point(568, 471);
            btnLoadCompressed.Name = "btnLoadCompressed";
            btnLoadCompressed.Size = new Size(94, 29);
            btnLoadCompressed.TabIndex = 22;
            btnLoadCompressed.Text = "Tải file nén";
            btnLoadCompressed.UseVisualStyleBackColor = true;
            btnLoadCompressed.Click += btnLoadCompressed_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(904, 571);
            Controls.Add(btnLoadCompressed);
            Controls.Add(label3);
            Controls.Add(trackBarContrast);
            Controls.Add(label2);
            Controls.Add(trackBarSize);
            Controls.Add(btnHuffman);
            Controls.Add(btnDecompress);
            Controls.Add(btnSaveCompressed);
            Controls.Add(btnCompress);
            Controls.Add(btnBlur);
            Controls.Add(btnReduceNoise);
            Controls.Add(btnFindLBP);
            Controls.Add(btnFindDraw);
            Controls.Add(btnRemoveBG);
            Controls.Add(btnCropImg);
            Controls.Add(btnFindSIFT);
            Controls.Add(btnRight);
            Controls.Add(label1);
            Controls.Add(btnLeft);
            Controls.Add(trackBarBrightness);
            Controls.Add(btnSave);
            Controls.Add(btnUpLoad);
            Controls.Add(pictureBox);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBrightness).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarContrast).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox;
        private Button btnUpLoad;
        private Button btnSave;
        private TrackBar trackBarBrightness;
        private Button btnLeft;
        private Label label1;
        private Button btnRight;
        private Button btnFindSIFT;
        private Button btnCropImg;
        private Button btnRemoveBG;
        private Button btnFindDraw;
        private Button btnFindLBP;
        private Button btnReduceNoise;
        private Button btnBlur;
        private Button btnCompress;
        private Button btnSaveCompressed;
        private Button btnDecompress;
        private Button btnHuffman;
        private TrackBar trackBarSize;
        private Label label2;
        private TrackBar trackBarContrast;
        private Label label3;
        private Button btnLoadCompressed;
    }
}
