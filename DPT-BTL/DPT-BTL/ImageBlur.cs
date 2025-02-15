using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPT_BTL
{
    internal class ImageBlur
    {
        private static readonly double[,] GaussianKernel = {
        { 1, 2, 1 },
        { 2, 4, 2 },
        { 1, 2, 1 }
        };

        public Bitmap ApplyGaussianBlur(Bitmap image)
        {
            Bitmap result = new Bitmap(image.Width, image.Height);

            for (int x = 1; x < image.Width - 1; x++)
            {
                for (int y = 1; y < image.Height - 1; y++)
                {
                    double r = 0, g = 0, b = 0;

                    // Áp dụng ma trận Gaussian Kernel (3x3)
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            Color neighbor = image.GetPixel(x + i, y + j);
                            double weight = GaussianKernel[i + 1, j + 1];

                            r += neighbor.R * weight;
                            g += neighbor.G * weight;
                            b += neighbor.B * weight;
                        }
                    }

                    // Chia tổng trọng số
                    r /= 16; g /= 16; b /= 16;

                    result.SetPixel(x, y, Color.FromArgb((int)r, (int)g, (int)b));
                }
            }

            return result;
        }
    }
}
