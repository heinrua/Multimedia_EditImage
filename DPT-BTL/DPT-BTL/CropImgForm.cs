using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DPT_BTL
{
    public partial class CropImgForm : Form
    {
        private Bitmap originalImage;
        private Rectangle cropArea = new Rectangle();
        private Point startPoint;
        public CropImgForm(Bitmap image)
        {
            InitializeComponent();
            originalImage = image;
            ptb_Crop.Paint += ptb_Crop_Paint;

            ptb_Crop.Image = originalImage;
            ptb_Crop.MouseDown += PictureBox_MouseDown;
            ptb_Crop.MouseMove += PictureBox_MouseMove;
            ptb_Crop.MouseUp += PictureBox_MouseUp;
        }
        private Point ConvertToImageCoordinates(Point pt)
        {
            if (ptb_Crop.Image == null) return Point.Empty;

            float xRatio = (float)originalImage.Width / ptb_Crop.Width;
            float yRatio = (float)originalImage.Height / ptb_Crop.Height;

            return new Point((int)(pt.X * xRatio), (int)(pt.Y * yRatio));
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startPoint = ConvertToImageCoordinates(e.Location);
                cropArea.Location = startPoint;
                cropArea.Size = Size.Empty;
            }
        }


        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point endPoint = ConvertToImageCoordinates(e.Location);
                cropArea.Width = Math.Abs(endPoint.X - cropArea.X);
                cropArea.Height = Math.Abs(endPoint.Y - cropArea.Y);
                cropArea.X = Math.Min(startPoint.X, endPoint.X);
                cropArea.Y = Math.Min(startPoint.Y, endPoint.Y);
                ptb_Crop.Invalidate(); // Yêu cầu vẽ lại PictureBox
            }
        }


        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ptb_Crop.Invalidate();
            }
        }

        private void ptb_Crop_Paint(object sender, PaintEventArgs e)
        {
            if (cropArea.Width > 0 && cropArea.Height > 0)
            {
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    e.Graphics.DrawRectangle(pen, cropArea);
                }
            }
        }

        private void btn_cancelCrop_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        
        private void btn_okCrop_Click(object sender, EventArgs e)
        {
            if (cropArea.Width <= 0 || cropArea.Height <= 0)
            {
                MessageBox.Show("Vui lòng chọn một vùng cắt hợp lệ.");
                return;
            }

            // Chuyển đổi cropArea sang tọa độ của ảnh gốc
            Rectangle adjustedCropArea = new Rectangle(
                (int)(cropArea.X * originalImage.Width / ptb_Crop.Width),
                (int)(cropArea.Y * originalImage.Height / ptb_Crop.Height),
                (int)(cropArea.Width * originalImage.Width / ptb_Crop.Width),
                (int)(cropArea.Height * originalImage.Height / ptb_Crop.Height));

            // Cắt ảnh
            Bitmap croppedImage = new Bitmap(adjustedCropArea.Width, adjustedCropArea.Height);
            using (Graphics g = Graphics.FromImage(croppedImage))
            {
                g.DrawImage(originalImage, new Rectangle(0, 0, croppedImage.Width, croppedImage.Height),
                    adjustedCropArea, GraphicsUnit.Pixel);
            }

            ptb_Crop.Image = croppedImage;
            ptb_Crop.SizeMode = PictureBoxSizeMode.StretchImage;

            this.DialogResult = DialogResult.OK;
            this.Tag = croppedImage; // Trả về ảnh đã cắt
            this.Close();
        }
    }
}
