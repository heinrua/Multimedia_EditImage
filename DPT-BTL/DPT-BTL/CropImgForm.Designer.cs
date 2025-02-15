namespace DPT_BTL
{
    partial class CropImgForm
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
            btnCancel = new Button();
            btnOK = new Button();
            ptb_Crop = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)ptb_Crop).BeginInit();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(212, 391);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btn_cancelCrop_Click;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(446, 391);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(94, 29);
            btnOK.TabIndex = 2;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btn_okCrop_Click;
            // 
            // ptb_Crop
            // 
            ptb_Crop.BorderStyle = BorderStyle.FixedSingle;
            ptb_Crop.Cursor = Cursors.Cross;
            ptb_Crop.Location = new Point(45, 12);
            ptb_Crop.Name = "ptb_Crop";
            ptb_Crop.Size = new Size(727, 348);
            ptb_Crop.SizeMode = PictureBoxSizeMode.StretchImage;
            ptb_Crop.TabIndex = 3;
            ptb_Crop.TabStop = false;
            ptb_Crop.MouseDown += PictureBox_MouseDown;
            ptb_Crop.MouseMove += PictureBox_MouseMove;
            ptb_Crop.MouseUp += PictureBox_MouseUp;
            // 
            // CropImgForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ptb_Crop);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            Name = "CropImgForm";
            Text = "CropImgForm";
            ((System.ComponentModel.ISupportInitialize)ptb_Crop).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button btnCancel;
        private Button btnOK;
        private PictureBox ptb_Crop;
    }
}