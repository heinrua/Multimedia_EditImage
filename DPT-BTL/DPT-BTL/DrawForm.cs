using Emgu.CV.Structure;
using Emgu.CV;
using System.Diagnostics;
using System.Windows.Forms;
using Emgu.CV.Features2D;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using DPT_BTL;


namespace DPT_BTL
{

    public partial class DrawForm : Form
    {
        private Draw drawTool;
        private List<string> imagePaths = new List<string>();
        private string bestMatchPath;
        public DrawForm()
        {
            InitializeComponent();

            drawTool = new Draw(draw_panel);
            draw_panel.MouseDown += (sender, e) => drawTool.StartDrawing(e);
            draw_panel.MouseMove += (sender, e) => drawTool.DrawLine(e);
            draw_panel.MouseUp += (sender, e) => drawTool.StopDrawing();

        }

        private void ShowSelectedImageInEdit(Image selectedImage)
        {
            pictureBox1.Image = selectedImage;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btn_searchInDB_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy hình ảnh từ panel phác họa
                Bitmap sketchBitmap = new Bitmap(draw_panel.Width, draw_panel.Height);
                draw_panel.DrawToBitmap(sketchBitmap, new Rectangle(0, 0, draw_panel.Width, draw_panel.Height));

                using (var sketchImage = sketchBitmap.ToImage<Gray, byte>())
                {
                    double bestSimilarity = 0;
                    bestMatchPath = null;

                    // So sánh với các ảnh trong thư viện
                    foreach (string imagePath in imagePaths)
                    {
                        // Gọi Python để so sánh ảnh
                        double similarity = CompareImagesWithPython(imagePath, sketchBitmap);

                        if (similarity > bestSimilarity)
                        {
                            bestSimilarity = similarity;
                            bestMatchPath = imagePath;
                        }
                    }

                    // Hiển thị kết quả
                    if (bestMatchPath != null)
                    {
                        pictureBox1.Image = Image.FromFile(bestMatchPath);
                        MessageBox.Show($"Tìm thấy ảnh phù hợp nhất (độ tương đồng: {bestSimilarity:P2})");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy ảnh phù hợp.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
            }

        }
        private double CompareImagesWithPython(string imagePath, Bitmap sketchBitmap)
        {
            // Save the sketch bitmap to a temporary file
            string sketchPath = Path.Combine(Path.GetTempPath(), "sketch.png");
            sketchBitmap.Save(sketchPath, System.Drawing.Imaging.ImageFormat.Png);

            using (Process process = new Process())
            {
                process.StartInfo.FileName = "python"; // Đường dẫn đến Python
                process.StartInfo.Arguments = $"testkt.py \"{imagePath}\" \"{sketchPath}\"";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode == 0 && double.TryParse(output, out double similarityScore))
                {
                    return similarityScore; // Return the similarity score
                }
                else
                {
                    MessageBox.Show($"Error: {process.StandardError.ReadToEnd()}");
                    return 0; // Return 0 in case of error
                }
            }
        }

        private void LoadImageFromDirectory(string imageDirectory, FlowLayoutPanel flowLayoutPanel_img)
        {
            try
            {
                flowLayoutPanel_img.Controls.Clear();
                // Lấy danh sách file ảnh
                string[] imageFiles = Directory.GetFiles(imageDirectory)
                    .Where(file =>
                    {
                        string ext = Path.GetExtension(file).ToLower();
                        return ext == ".png" || ext == ".jpg" || ext == ".jpeg" || ext == ".bmp";
                    }).ToArray();

                if (imageFiles.Length == 0)
                {
                    MessageBox.Show("Không có ảnh trong thư mục. Hãy thêm ảnh vào thư mục Image.");
                    return;
                }
                // Lưu danh sách ảnh vào biến toàn cục
                imagePaths = imageFiles.ToList();

                foreach (string filePath in imageFiles)
                {
                    try
                    {
                        using (var original = Image.FromFile(filePath))
                        {
                            // Tạo bản copy của ảnh
                            var clone = new Bitmap(original);

                            var pictureBox = new PictureBox
                            {
                                Image = clone,
                                SizeMode = PictureBoxSizeMode.StretchImage,
                                Width = 100,
                                Height = 100,
                                Margin = new Padding(5),
                                Tag = filePath // Lưu đường dẫn file để dùng sau này nếu cần
                            };

                            pictureBox.Click += (s, e) =>
                            {
                                try
                                {
                                    using (var img = Image.FromFile((string)pictureBox.Tag))
                                    {
                                        ShowSelectedImageInEdit(new Bitmap(img));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Không thể mở ảnh: {ex.Message}");
                                }
                            };

                            flowLayoutPanel_img.Controls.Add(pictureBox);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Bỏ qua file lỗi và tiếp tục với file khác
                        Debug.WriteLine($"Lỗi khi tải ảnh {filePath}: {ex.Message}");
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thư mục ảnh: {ex.Message}");
            }
        }

        private void btn_ChooseFolder_Click(object sender, EventArgs e)
        {
            // Tạo đối tượng FolderBrowserDialog
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();

            // Nếu người dùng chọn một thư mục
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn thư mục đã chọn
                string selectedPath = folderDialog.SelectedPath;
                LoadImageFromDirectory(selectedPath, flowLayoutPanel_img);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Tag = bestMatchPath; // Trả về ảnh đã cắt
            this.Close();
        }
    }
}
