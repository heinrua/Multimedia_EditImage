using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DPT_BTL
{
    public partial class Find_LBP : Form
    {
        private const int STANDARD_WIDTH = 150;
        private const int STANDARD_HEIGHT = 150;
        private const int GRID_SIZE = 8; // Chia ảnh thành lưới 8x8
        private double threshold = 0.65;
        private const int MAX_RESULTS = 5;
        private Bitmap selectedImage;
        public Find_LBP()
        {
            InitializeComponent();
        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Chọn ảnh";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (selectedImage != null)
                        {
                            selectedImage.Dispose();
                        }
                        selectedImage = new Bitmap(openFileDialog.FileName);
                        pictureBoxOriginal.Image = selectedImage;
                        pictureBoxOriginal.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi mở ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private double[] ExtractLBPFeatures(Bitmap image)
        {
            // Chuyển sang ảnh xám
            Bitmap grayImage = ConvertToGrayscale(image);

            // Tính LBP cho toàn bộ ảnh
            int[,] lbpValues = new int[image.Width, image.Height];
            for (int x = 1; x < image.Width - 1; x++)
            {
                for (int y = 1; y < image.Height - 1; y++)
                {
                    lbpValues[x, y] = CalculateLBP(grayImage, x, y);
                }
            }

            // Chia ảnh thành các cell và tính histogram cho từng cell
            List<double> features = new List<double>();
            int cellWidth = image.Width / GRID_SIZE;
            int cellHeight = image.Height / GRID_SIZE;

            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    var cellHistogram = CalculateCellHistogram(lbpValues, i * cellWidth, j * cellHeight,
                                                             cellWidth, cellHeight);
                    features.AddRange(cellHistogram);
                }
            }

            // Chuẩn hóa vector đặc trưng
            NormalizeFeatures(features);
            return features.ToArray();
        }

        private double[] CalculateCellHistogram(int[,] lbpValues, int startX, int startY,
                                              int width, int height)
        {
            double[] histogram = new double[256];

            for (int x = startX; x < startX + width && x < lbpValues.GetLength(0) - 1; x++)
            {
                for (int y = startY; y < startY + height && y < lbpValues.GetLength(1) - 1; y++)
                {
                    int lbpValue = lbpValues[x, y];
                    // Chỉ sử dụng uniform patterns
                    if (IsUniformPattern(lbpValue))
                    {
                        histogram[lbpValue]++;
                    }
                }
            }

            return histogram;
        }

        private bool IsUniformPattern(int lbpValue)
        {
            int transitions = 0;
            int lastBit = lbpValue & 1;
            int pattern = lbpValue | (lbpValue << 8);

            for (int i = 0; i < 8; i++)
            {
                int bit = (pattern >> i) & 1;
                if (bit != lastBit)
                {
                    transitions++;
                }
                lastBit = bit;
            }

            return transitions <= 2;
        }

        private int CalculateLBP(Bitmap image, int x, int y)
        {
            int centerPixel = image.GetPixel(x, y).R;
            int lbpValue = 0;

            // Tính LBP 8-bit
            int[] dx = { -1, -1, -1, 0, 1, 1, 1, 0 };
            int[] dy = { -1, 0, 1, 1, 1, 0, -1, -1 };

            for (int i = 0; i < 8; i++)
            {
                int neighborPixel = image.GetPixel(x + dx[i], y + dy[i]).R;
                if (neighborPixel >= centerPixel)
                {
                    lbpValue |= (1 << i);
                }
            }

            return lbpValue;
        }

        private void NormalizeFeatures(List<double> features)
        {
            double sum = features.Sum();
            if (sum > 0)
            {
                for (int i = 0; i < features.Count; i++)
                {
                    features[i] /= sum;
                }
            }
        }

        private Bitmap ConvertToGrayscale(Bitmap image)
        {
            Bitmap grayscale = new Bitmap(image.Width, image.Height);

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color c = image.GetPixel(x, y);
                    int gray = (int)((c.R * 0.299) + (c.G * 0.587) + (c.B * 0.114));
                    grayscale.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            }

            return grayscale;
        }

        private double CompareFeatures(double[] features1, double[] features2)
        {
            double similarity = 0;
            double chiSquareDistance = 0;

            for (int i = 0; i < features1.Length; i++)
            {
                if (features1[i] + features2[i] > 0)
                {
                    chiSquareDistance += Math.Pow(features1[i] - features2[i], 2) /
                                       (features1[i] + features2[i]);
                }
            }

            similarity = Math.Exp(-chiSquareDistance / 2);
            return similarity;
        }

        private Bitmap ResizeImage(Bitmap image)
        {
            Bitmap resized = new Bitmap(STANDARD_WIDTH, STANDARD_HEIGHT);
            using (Graphics g = Graphics.FromImage(resized))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, 0, 0, STANDARD_WIDTH, STANDARD_HEIGHT);
            }
            return resized;
        }

        private void btnFindSimilarImages_Click(object sender, EventArgs e)
        {
            if (selectedImage == null)
            {
                MessageBox.Show("Vui lòng chọn ảnh trước khi tìm kiếm.", "Thông báo",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (Bitmap resizedSelected = ResizeImage(selectedImage))
            {
                var selectedFeatures = ExtractLBPFeatures(resizedSelected);
                var results = new List<(string path, double similarity)>();

                string directoryPath = @"D:\CuoiKy_DPT\data-pic";
                string[] imageFiles = Directory.GetFiles(directoryPath, "*.jpg");

                foreach (var imagePath in imageFiles)
                {
                    try
                    {
                        using (Bitmap originalSample = new Bitmap(imagePath))
                        using (Bitmap resizedSample = ResizeImage(originalSample))
                        {
                            var sampleFeatures = ExtractLBPFeatures(resizedSample);
                            double similarity = CompareFeatures(selectedFeatures, sampleFeatures);

                            if (similarity >= threshold)
                            {
                                results.Add((imagePath, similarity));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi xử lý ảnh {imagePath}: {ex.Message}");
                    }
                }

                DisplayResults(results.OrderByDescending(r => r.similarity)
                                   .Take(MAX_RESULTS).ToList());
            }
        }
       


        private void DisplayResults(List<(string path, double similarity)> results)
        {
            flowLayoutPanelResults.Controls.Clear();

            foreach (var result in results)
            {
                Panel resultPanel = new Panel
                {
                    Width = 120,
                    Height = 140,
                    Margin = new Padding(5)
                };

                using (Bitmap originalImage = new Bitmap(result.path))
                {
                    PictureBox resultPictureBox = new PictureBox
                    {
                        Width = 100,
                        Height = 100,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Image = new Bitmap(originalImage),
                        Location = new Point(10, 0)
                    };

                    Label similarityLabel = new Label
                    {
                        Text = $"Similarity: {result.similarity:F3}",
                        AutoSize = true,
                        Location = new Point(10, 105)
                    };

                    resultPanel.Controls.Add(resultPictureBox);
                    resultPanel.Controls.Add(similarityLabel);
                    flowLayoutPanelResults.Controls.Add(resultPanel);
                }
            }
        }
        }
}
