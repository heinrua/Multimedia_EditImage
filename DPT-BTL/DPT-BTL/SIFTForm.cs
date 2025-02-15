using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace DPT_BTL
{
    public partial class SIFTForm : Form
    {
        public SIFTForm()
        {
            InitializeComponent();
        }

        private void btnChooseImg_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = ofd.FileName;
                    pictureBox.Image = Image.FromFile(ofd.FileName);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }

        // Hàm trích xuất đặc trưng SIFT từ ảnh
        private Tuple<VectorOfKeyPoint, Mat> ExtractSIFTFeatures(string imagePath)
        {
            var img = CvInvoke.Imread(imagePath, ImreadModes.Color);
            var grayImg = new Mat();
            CvInvoke.CvtColor(img, grayImg, ColorConversion.Bgr2Gray);

            var sift = new SIFT();

            var keyPoints = new VectorOfKeyPoint();
            var descriptors = new Mat();

            // Phát hiện keypoints và tính toán descriptors trên ảnh xám
            sift.DetectAndCompute(grayImg, null, keyPoints, descriptors, false);

            return Tuple.Create(keyPoints, descriptors);
        }

        // Hàm tính độ tương đồng giữa hai ảnh
        private double ComputeImageSimilarity(Mat queryDescriptors, Mat imageDescriptors)
        {
            if (queryDescriptors.Rows == 0 || imageDescriptors.Rows == 0)
                return 0;

            // Sử dụng BFMatcher với norm L2
            var matcher = new BFMatcher(DistanceType.L2);
            var matches = new VectorOfVectorOfDMatch();

            // Tìm 2 matches tốt nhất cho mỗi descriptor
            matcher.KnnMatch(queryDescriptors, imageDescriptors, matches, 2);

            // Áp dụng ratio test để lọc matches tốt với ngưỡng chặt chẽ hơn
            var goodMatches = new List<MDMatch>();
            float ratioThreshold = 0.7f;

            for (int i = 0; i < matches.Size; i++)
            {
                if (matches[i].Size > 1)
                {
                    var m = matches[i][0];
                    var n = matches[i][1];
                    if (m.Distance < ratioThreshold * n.Distance)
                    {
                        goodMatches.Add(m);
                    }
                }
            }

            // Tính phần trăm độ tương đồng dựa trên số lượng matches tốt
            double totalPossibleMatches = Math.Min(queryDescriptors.Rows, imageDescriptors.Rows);
            double similarityPercentage = (goodMatches.Count / totalPossibleMatches) * 100;

            return Math.Min(100, Math.Max(0, similarityPercentage));
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPath.Text))
            {
                MessageBox.Show("Vui lòng chọn ảnh để tìm kiếm!", "Thông báo");
                return;
            }

            // Lấy đặc trưng của ảnh query
            var queryFeatures = ExtractSIFTFeatures(txtPath.Text);
            var queryDescriptors = queryFeatures.Item2;

            string folderPath = "D:\\CuoiKy_DPT\\data-pic";
            var results = new List<Tuple<string, double>>();

            // Tìm kiếm trong thư mục ảnh
            var imageFiles = Directory.GetFiles(folderPath, "*.*")
                .Where(file => file.ToLower().EndsWith(".jpg") ||
                             file.ToLower().EndsWith(".jpeg") ||
                             file.ToLower().EndsWith(".png") ||
                             file.ToLower().EndsWith(".bmp"));

            foreach (var imagePath in imageFiles)
            {
                try
                {
                    var features = ExtractSIFTFeatures(imagePath);
                    var descriptors = features.Item2;

                    // Tính độ tương đồng
                    double similarity = ComputeImageSimilarity(queryDescriptors, descriptors);

                    // Chỉ lấy những ảnh có độ tương đồng từ 20% trở lên
                    if (similarity >= 20)
                    {
                        results.Add(Tuple.Create(imagePath, similarity));
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }

            // Sắp xếp kết quả 
            var topResults = results.OrderByDescending(r => r.Item2)
                                  //     .Take(5)
                                  .ToList();

            // Hiển thị kết quả tìm kiếm
            MessageBox.Show($"Số lượng ảnh tìm thấy: {topResults.Count}");

            // Hiển thị kết quả
            flowLayout.Controls.Clear();

            if (topResults.Any())
            {
                foreach (var result in topResults)
                {
                    try
                    {
                        // Tạo Panel để chứa PictureBox và Label
                        var panel = new Panel
                        {
                            Width = 120,
                            Height = 140,
                            Margin = new Padding(5)
                        };

                        // Tạo PictureBox cho ảnh
                        var pb = new PictureBox
                        {
                            Image = Image.FromFile(result.Item1),
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Width = 100,
                            Height = 100,
                            Cursor = Cursors.Hand,
                            Tag = result.Item1,
                            Location = new Point(10, 0) // Căn giữa trong panel
                        };

                        // Gắn sự kiện Click
                        pb.Click += (s, ev) =>
                        {
                            string selectedImagePath = (s as PictureBox).Tag.ToString();

                            DialogResult dialogResult = MessageBox.Show(
                                "Bạn có muốn sửa ảnh này không?",
                                "Xác nhận chỉnh sửa",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question
                            );

                            if (dialogResult == DialogResult.Yes)
                            {
                                Form1 form = new Form1(selectedImagePath);
                                this.Hide();
                                form.ShowDialog();
                            }
                        };

                        // Tạo Label cho phần trăm tương đồng
                        var lbl = new Label
                        {
                            Text = $"{result.Item2:F1}%",
                            Width = 100,
                            TextAlign = ContentAlignment.MiddleCenter,
                            Location = new Point(10, 105) // Đặt ngay dưới PictureBox
                        };

                        panel.Controls.Add(pb);
                        panel.Controls.Add(lbl);
                        flowLayout.Controls.Add(panel);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy ảnh tương tự!", "Thông báo");
            }
        }
    }
}