
namespace DPT_BTL
{
    partial class SIFTForm
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
            pictureBox = new PictureBox();
            txtPath = new TextBox();
            btnSearch = new Button();
            btnClose = new Button();
            flowLayout = new FlowLayoutPanel();
            btnChooseImg = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.BackColor = SystemColors.ControlLightLight;
            pictureBox.Location = new Point(51, 54);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(220, 293);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // txtPath
            // 
            txtPath.BackColor = SystemColors.ControlLightLight;
            txtPath.Location = new Point(307, 54);
            txtPath.Name = "txtPath";
            txtPath.Size = new Size(259, 27);
            txtPath.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = SystemColors.ButtonFace;
            btnSearch.Location = new Point(584, 52);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 29);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Tìm";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnClose
            // 
            btnClose.BackColor = SystemColors.Control;
            btnClose.Location = new Point(684, 52);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(94, 29);
            btnClose.TabIndex = 3;
            btnClose.Text = "Đóng";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // flowLayout
            // 
            flowLayout.AutoScroll = true;
            flowLayout.BackColor = SystemColors.ControlLightLight;
            flowLayout.Location = new Point(307, 109);
            flowLayout.Name = "flowLayout";
            flowLayout.Size = new Size(471, 392);
            flowLayout.TabIndex = 4;
            // 
            // btnChooseImg
            // 
            btnChooseImg.BackColor = SystemColors.ButtonFace;
            btnChooseImg.Location = new Point(114, 358);
            btnChooseImg.Name = "btnChooseImg";
            btnChooseImg.Size = new Size(94, 29);
            btnChooseImg.TabIndex = 5;
            btnChooseImg.Text = "Chọn ảnh";
            btnChooseImg.UseVisualStyleBackColor = false;
            btnChooseImg.Click += btnChooseImg_Click;
            // 
            // SIFTForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(831, 532);
            Controls.Add(btnChooseImg);
            Controls.Add(flowLayout);
            Controls.Add(btnClose);
            Controls.Add(btnSearch);
            Controls.Add(txtPath);
            Controls.Add(pictureBox);
            Name = "SIFTForm";
            Text = "SIFT";
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private PictureBox pictureBox;
        private TextBox txtPath;
        private Button btnSearch;
        private Button btnClose;
        private FlowLayoutPanel flowLayout;
        private Button btnChooseImg;
    }
}