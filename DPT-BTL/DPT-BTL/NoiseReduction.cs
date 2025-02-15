using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPT_BTL
{
    internal class NoiseReduction
    {
        public Bitmap ApplyMeanFilter(Bitmap image)
        {
            Bitmap result = new Bitmap(image.Width, image.Height);

            for (int x = 1; x < image.Width - 1; x++)
            {
                for (int y = 1; y < image.Height - 1; y++)
                {
                    int r = 0, g = 0, b = 0;

                    // Duyệt qua các pixel lân cận (3x3)
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            Color neighbor = image.GetPixel(x + i, y + j);
                            r += neighbor.R;
                            g += neighbor.G;
                            b += neighbor.B;
                        }
                    }

                    // Lấy trung bình
                    r /= 9; g /= 9; b /= 9;

                    result.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            return result;
        }
    }
}
