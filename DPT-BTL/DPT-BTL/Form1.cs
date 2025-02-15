using System.Diagnostics;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace DPT_BTL
{
    public partial class Form1 : Form
    {
        private string imagePath;
        private float scale = 1.0f;
        private Image adjustedImage;
        public Form1()
        {
            InitializeComponent();
            // Cấu hình TrackBar
            trackBarSize.Minimum = 10;  // Giá trị tối thiểu là 10% (thu nhỏ 90%)
            trackBarSize.Maximum = 200; // Giá trị tối đa là 200% (phóng to gấp đôi)
            trackBarSize.Value = 100;   // Giá trị mặc định là 100% (kích thước gốc)

            // Gán sự kiện
            this.Paint += ResizeImageForm_Paint;
            trackBarSize.Scroll += trackBarSize_Scroll;
        }


        public Form1(string selectedImagePath)
        {
            InitializeComponent();

            imagePath = selectedImagePath;

            pictureBox.Image = Image.FromFile(imagePath);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }




        private void btnUpLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.jfif;*.jjif";
                ofd.Title = "Chọn ảnh để tìm kiếm";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    imagePath = ofd.FileName;

                    pictureBox.Image = Image.FromFile(imagePath);
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }
        private void trackBarSize_Scroll(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                try
                {
                    // Tính tỷ lệ phóng to/thu nhỏ từ TrackBar
                    scale = trackBarSize.Value / 100.0f;

                    // Yêu cầu Form vẽ lại
                    this.Invalidate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thay đổi kích thước ảnh: " + ex.Message);
                }
            }
        }

        private void ResizeImageForm_Paint(object sender, PaintEventArgs e)
        {
            if (pictureBox.Image != null)
            {
                // Tính toán kích thước mới của ảnh
                int newWidth = (int)(pictureBox.Image.Width * scale);
                int newHeight = (int)(pictureBox.Image.Height * scale);

                // Tính toán vị trí để ảnh được căn giữa Form
                int x = (this.ClientSize.Width - newWidth) / 2;
                int y = (this.ClientSize.Height - newHeight) / 2;

                // Vẽ ảnh trên Form
                e.Graphics.DrawImage(pictureBox.Image, x, y, newWidth, newHeight);
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Lưu ảnh";
                saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox.Image.Save(saveFileDialog.FileName);
                    MessageBox.Show("Ảnh đã được lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        //ĐỘ SÁNG
        private void TrackBarBrightness_Scroll(object sender, EventArgs e)
        {
            if (imagePath != null)
            {
                try
                {
                    float brightness = trackBarBrightness.Value / 100f + 1;
                    using (Image originalImage = Image.FromFile(imagePath))
                    {
                        // Tạo color matrix để điều chỉnh độ sáng
                        var cm = new ColorMatrix(new float[][] {
                    new float[] {brightness, 0, 0, 0, 0},
                    new float[] {0, brightness, 0, 0, 0},
                    new float[] {0, 0, brightness, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1},
                });

                        var attributes = new ImageAttributes();
                        attributes.SetColorMatrix(cm);

                        // Tạo và vẽ ảnh mới với độ sáng đã điều chỉnh
                        var bm = new Bitmap(originalImage.Width, originalImage.Height);
                        using (var gr = Graphics.FromImage(bm))
                        {
                            gr.DrawImage(originalImage,
                                new Rectangle(0, 0, bm.Width, bm.Height),
                                0, 0, originalImage.Width, originalImage.Height,
                                GraphicsUnit.Pixel, attributes);
                        }
                        pictureBox.Image = bm;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi điều chỉnh độ sáng: {ex.Message}", "Lỗi");
                }
            }
        }

        //XOAY ẢNH
        private void btnLeft_Click(object sender, EventArgs e)
        {
            RotateImage(RotateFlipType.Rotate270FlipNone);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            RotateImage(RotateFlipType.Rotate90FlipNone);
        }

        private void RotateImage(RotateFlipType rotateFlipType)
        {
            if (pictureBox.Image != null)
            {
                try
                {
                    // Lấy ảnh hiện tại
                    Image img = pictureBox.Image;

                    // Xoay ảnh
                    img.RotateFlip(rotateFlipType);

                    // Cập nhật ảnh đã xoay vào PictureBox
                    pictureBox.Image = img;

                    // Làm mới PictureBox để hiển thị ảnh đã xoay
                    pictureBox.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xoay ảnh: {ex.Message}", "Lỗi");
                }
            }
            else
            {
                MessageBox.Show("Không có ảnh để xoay!", "Thông báo");
            }
        }

        private void btnFindSIFT_Click(object sender, EventArgs e)
        {
            SIFTForm siftForm = new SIFTForm();
            this.Hide();
            siftForm.Show();
        }


        // CẮT ẢNH
        private void btnCropImg_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                using (CropImgForm cropForm = new CropImgForm(new Bitmap(pictureBox.Image)))
                {
                    if (cropForm.ShowDialog() == DialogResult.OK)
                    {
                        // Lấy ảnh đã cắt và hiển thị trong PictureBox
                        pictureBox.Image = (Bitmap)cropForm.Tag;
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một bức ảnh trước khi cắt.");
            }
        }

        // XÓA PHÔNG
        private void btnRemoveBG_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                // Lưu ảnh hiện tại vào file tạm thời
                string debugDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
                string tempInputPath = Path.Combine(debugDirectory, Guid.NewGuid().ToString() + ".png");
                string outputPath = Path.Combine(debugDirectory, Guid.NewGuid().ToString() + ".png");

                pictureBox.Image.Save(tempInputPath, System.Drawing.Imaging.ImageFormat.Png);

                // Thực thi script Python
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "python"; // Đường dẫn đến Python
                    process.StartInfo.Arguments = $"removeBg.py \"{tempInputPath}\" \"{outputPath}\""; // Truyền tham số
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.CreateNoWindow = true;

                    // Bắt đầu quá trình
                    process.Start();
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    // Kiểm tra lỗi
                    if (process.ExitCode == 0)
                    {
                        // Tải ảnh đã tách nền
                        pictureBox.Image = Image.FromFile(outputPath);
                    }
                    else
                    {
                        MessageBox.Show($"Lỗi: {error}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một bức ảnh trước khi tách nền.");
            }
        }

        private void btnFindDraw_Click(object sender, EventArgs e)
        {
            using (DrawForm drawForm = new DrawForm())
            {
                if (drawForm.ShowDialog() == DialogResult.OK)
                {
                    // Lấy ảnh đã cắt và hiển thị trong PictureBox
                    pictureBox.Image = Image.FromFile((string)drawForm.Tag);
                }
            }

        }




        private Bitmap originalImage;
        private List<int> compressedData;
        private void btnReduceNoise_Click(object sender, EventArgs e)
        {
            originalImage = new Bitmap(pictureBox.Image);
            if (originalImage == null)
            {
                MessageBox.Show("Vui lòng chọn ảnh trước!");
                return;
            }
            // Gọi hàm khử nhiễu (thay bằng lớp xử lý thực tế)
            NoiseReduction noiseReduction = new NoiseReduction();
            Bitmap processedImage = noiseReduction.ApplyMeanFilter(originalImage);
            pictureBox.Image = processedImage;
        }

        private void btnBlur_Click(object sender, EventArgs e)
        {
            originalImage = new Bitmap(pictureBox.Image);
            if (originalImage == null)
            {
                MessageBox.Show("Vui lòng chọn ảnh trước!");
                return;
            }

            // Gọi hàm làm mờ (thay bằng lớp xử lý thực tế)
            ImageBlur blur = new ImageBlur();
            Bitmap processedImage = blur.ApplyGaussianBlur(originalImage);
            pictureBox.Image = processedImage;
        }
        private byte[] ImageToByteArray(Bitmap image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
        private Bitmap ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return new Bitmap(ms);
            }
        }
        private void btnCompress_Click(object sender, EventArgs e)
        {
            originalImage = new Bitmap(pictureBox.Image);
            if (originalImage == null)
            {
                MessageBox.Show("Vui lòng chọn ảnh trước!");
                return;
            }

            try
            {
                // Chuyển ảnh thành byte[]
                byte[] imageBytes = ImageToByteArray(originalImage);

                // Nén dữ liệu bằng thuật toán LZW
                LZW lzw = new LZW();
                compressedData = lzw.Compress(imageBytes);

                MessageBox.Show("Nén ảnh thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi nén ảnh: {ex.Message}");
            }
        }

        private void btnSaveCompressed_Click(object sender, EventArgs e)
        {
            if (compressedData == null || compressedData.Count == 0)
            {
                MessageBox.Show("Vui lòng nén ảnh trước!");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Compressed File|*.lzw",
                Title = "Lưu tệp nén"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllBytes(saveFileDialog.FileName, compressedData.SelectMany(BitConverter.GetBytes).ToArray());
                    MessageBox.Show("Lưu tệp nén thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể lưu tệp nén: {ex.Message}");
                }
            }
        }


        private void btnDecompress_Click(object sender, EventArgs e)
        {
            if (compressedData == null || compressedData.Count == 0)
            {
                MessageBox.Show("Vui lòng tải hoặc nén dữ liệu trước!");
                return;
            }

            try
            {
                // Giải nén dữ liệu bằng thuật toán LZW
                LZW lzw = new LZW();
                byte[] decompressedData = lzw.Decompress(compressedData);

                // Chuyển byte[] thành ảnh
                Bitmap decompressedImage = ByteArrayToImage(decompressedData);
                pictureBox.Image = decompressedImage;

                MessageBox.Show("Giải nén ảnh thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi giải nén ảnh: {ex.Message}");
            }
        }

        private void btnHuffman_Click(object sender, EventArgs e)
        {
            originalImage = new Bitmap(pictureBox.Image);
            if (originalImage == null)
            {
                MessageBox.Show("Vui lòng chọn ảnh trước!");
                return;
            }

            try
            {
                // Chuyển ảnh gốc thành mảng byte
                byte[] imageBytes = ImageToByteArray(originalImage);

                // Áp dụng thuật toán nén (ví dụ Huffman hoặc LZW)
                HuffmanCompression huffman = new HuffmanCompression();
                string compressedData = huffman.Compress(Convert.ToBase64String(imageBytes));

                // Lưu dữ liệu nén vào file .txt
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Text Files|*.txt",
                    Title = "Lưu file nén"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, compressedData);
                    MessageBox.Show("Nén ảnh và lưu file thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi nén ảnh: {ex.Message}");
            }
        }

        private void btnFindLBP_Click(object sender, EventArgs e)
        {
            Find_LBP findImageForm = new Find_LBP();
            findImageForm.Show();
        }

        private void trackBarContrast_Scroll_1(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                try
                {
                    float contrastValue = trackBarContrast.Value / 100.0f + 1.0f;

                    using (Bitmap originalBitmap = new Bitmap(imagePath))
                    {
                        Bitmap adjustedBitmap = AdjustContrast(originalBitmap, contrastValue);
                        pictureBox.Image = adjustedBitmap;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi điều chỉnh độ tương phản: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một bức ảnh trước khi điều chỉnh độ tương phản.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private Bitmap AdjustContrast(Bitmap originalBitmap, float contrast)
        {
            Bitmap adjustedBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height);
            float adjustedContrast = contrast - 1.0f;

            // Tạo ma trận điều chỉnh độ tương phản
            float[][] contrastMatrix = {
        new float[] {contrast, 0, 0, 0, 0},
        new float[] {0, contrast, 0, 0, 0},
        new float[] {0, 0, contrast, 0, 0},
        new float[] {0, 0, 0, 1, 0},
        new float[] {adjustedContrast, adjustedContrast, adjustedContrast, 0, 1}
    };

            var cm = new ColorMatrix(contrastMatrix);
            var attributes = new ImageAttributes();
            attributes.SetColorMatrix(cm);

            // Áp dụng ma trận lên ảnh
            using (Graphics g = Graphics.FromImage(adjustedBitmap))
            {
                g.DrawImage(originalBitmap, new Rectangle(0, 0, adjustedBitmap.Width, adjustedBitmap.Height),
                    0, 0, originalBitmap.Width, originalBitmap.Height, GraphicsUnit.Pixel, attributes);
            }

            return adjustedBitmap;
        }

        private void btnLoadCompressed_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Compressed Files|*.lzw;*.txt",
                Title = "Tải tệp nén"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    byte[] fileBytes = File.ReadAllBytes(openFileDialog.FileName);
                    compressedData = new List<int>();

                    for (int i = 0; i < fileBytes.Length; i += sizeof(int))
                    {
                        compressedData.Add(BitConverter.ToInt32(fileBytes, i));
                    }

                    MessageBox.Show("Tệp nén đã được tải!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể tải tệp nén: {ex.Message}");
                }
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }
    }
}

