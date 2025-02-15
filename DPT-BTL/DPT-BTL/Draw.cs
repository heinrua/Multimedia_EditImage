using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPT_BTL
{
    internal class Draw
    {
        private bool isDrawing = false;
        private Point lastPoint;
        private Panel drawingPanel;
        private Bitmap canvasBitmap; // Bitmap để lưu các nét vẽ
        private Graphics graphics;

        public Draw(Panel panel)
        {
            drawingPanel = panel;

            // Khởi tạo Bitmap với kích thước của Panel
            canvasBitmap = new Bitmap(panel.Width, panel.Height);

            // Gắn Bitmap vào Panel (hiển thị)
            panel.BackgroundImage = canvasBitmap;
            panel.BackgroundImageLayout = ImageLayout.None;

            // Lấy Graphics từ Bitmap
            graphics = Graphics.FromImage(canvasBitmap);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // Làm mịn nét vẽ
        }

        public void StartDrawing(MouseEventArgs e)
        {
            isDrawing = true;
            lastPoint = e.Location;
        }

        public void DrawLine(MouseEventArgs e)
        {
            if (isDrawing)
            {
                // Vẽ lên Bitmap
                graphics.DrawLine(Pens.Black, lastPoint, e.Location);
                lastPoint = e.Location;

                // Yêu cầu Panel vẽ lại để hiển thị nét vẽ
                drawingPanel.Invalidate();
            }
        }

        public void StopDrawing()
        {
            isDrawing = false;
        }

        public Bitmap GetBitmap()
        {
            // Trả về Bitmap hiện tại
            return canvasBitmap;
        }
    }
}
