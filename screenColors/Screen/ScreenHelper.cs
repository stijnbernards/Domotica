using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HueLibrary.Hue;
using ColorMode = HueLibrary.Hue.Lights.ColorMode;

namespace screenColors.Screen
{
    public class ScreenHelper
    {
        private Bitmap bitmap;
        private BitmapData bitmapData;
        private int thumbnailSize = 32; 

        public ScreenHelper()
        {
            bitmap = new Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width,
                                           System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height,
                                           PixelFormat.Format32bppArgb);

            Graphics gfxScreenshot = Graphics.FromImage(bitmap);

            gfxScreenshot.CopyFromScreen(System.Windows.Forms.Screen.PrimaryScreen.Bounds.X,
                                        System.Windows.Forms.Screen.PrimaryScreen.Bounds.Y,
                                        0,
                                        0,
                                        System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size,
                                        CopyPixelOperation.SourceCopy);

            bitmap = new Bitmap(bitmap, new Size(thumbnailSize, thumbnailSize));

            bitmap.Save("test.png", ImageFormat.Png);

            bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            GetDominantColor();
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

            double[] xy = HueHelpers.RGBToXY(avgR, avgG, avgB);

            HueMain hueMain = new HueMain("192.168.178.25", "X3v1tPkd2szuFL9EXJ1LwYy9yZMr2EwZt3UUQbfQ");
            hueMain.IndexLights();

            hueMain.Lights[0].TurnOn();
            hueMain.Lights[0].SetXY(xy);
            hueMain.Lights[0].SetColorMode(ColorMode.XY);
            hueMain.Lights[0].Apply();
        }
    }
}
