using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HueLibrary.Hue;
using ColorMode = HueLibrary.Hue.Lights.ColorMode;

namespace screenColors.Screen
{
    public class ScreenHelper
    {
        private BitmapData bitmapData;
        private int thumbnailSize = 128;

        HueMain hueMain = new HueMain("192.168.178.25", "X3v1tPkd2szuFL9EXJ1LwYy9yZMr2EwZt3UUQbfQ");

        public ScreenHelper()
        {
            while (true)
            {
                Thread.Sleep(500);
                Bitmap bitmap = new Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width,
                    System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height,
                    PixelFormat.Format32bppArgb);

                Graphics gfxScreenshot = Graphics.FromImage(bitmap);

                gfxScreenshot.CopyFromScreen(System.Windows.Forms.Screen.PrimaryScreen.Bounds.X,
                    System.Windows.Forms.Screen.PrimaryScreen.Bounds.Y,
                    0,
                    0,
                    System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size,
                    CopyPixelOperation.SourceCopy);

                Bitmap thumbBitmap = new Bitmap(bitmap, new Size(thumbnailSize, thumbnailSize));

                bitmapData = thumbBitmap.LockBits(
                    new Rectangle(0, 0, thumbBitmap.Width, thumbBitmap.Height),
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);

                GetDominantColor();

                gfxScreenshot.Dispose();
                thumbBitmap.Dispose();
                bitmap.Dispose();
            }
        }

        public void GetDominantColor()
        {
            int stride = bitmapData.Stride;

            IntPtr Scan0 = bitmapData.Scan0;

            long[] totals = new long[] { 0, 0, 0 };

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                for (int y = 0; y < thumbnailSize; y++)
                {
                    for (int x = 0; x < thumbnailSize; x++)
                    {
                        for (int color = 0; color < 3; color++)
                        {
                            int idx = (y * stride) + x * 4 + color;

                            totals[color] += p[idx];
                        }
                    }
                }
            }

            float avgB = (float)totals[0] / (thumbnailSize * thumbnailSize);
            float avgG = (float)totals[1] / (thumbnailSize * thumbnailSize);
            float avgR = (float)totals[2] / (thumbnailSize * thumbnailSize);

            double brightness;

            double[] xy = HueHelpers.RGBToXY(avgR, avgG, avgB, out brightness);

            hueMain.IndexLights();

            hueMain.Lights[1].TurnOn();
            hueMain.Lights[1].SetXY(xy);
            hueMain.Lights[1].Brightness((int)brightness * 6);
            hueMain.Lights[1].SetColorMode(ColorMode.XY);
            hueMain.Lights[1].Apply();
        }
    }
}
