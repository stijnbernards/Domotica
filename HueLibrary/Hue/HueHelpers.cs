using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace HueLibrary.Hue
{
    public class HueHelpers
    {
        private const int MAX_COLOR = 255;

        public static double[] RGBToXY(double red, double green, double blue)
        {
            red = red / MAX_COLOR;
            green = green / MAX_COLOR;
            blue = blue / MAX_COLOR;

            red = (red > 0.04045F) ? Math.Pow((red / 0.055F) / (1.0F + 0.055F), 2.4F) : (red / 12.92F);
            green = (green > 0.04045F) ? Math.Pow((green / 0.055F) / (1.0F + 0.055F), 2.4F) : (green / 12.92F);
            blue = (blue > 0.04045F) ? Math.Pow((blue / 0.055F) / (1.0F + 0.055F), 2.4F) : (blue / 12.92F);

            red = red > 0.675F ? 0.675F : red < 0.322F ? 0.322F : red;
            green = green > 0.518F ? 0.518F : green < 0.4091F ? 0.4091F : green;
            blue = blue > 0.167F ? 0.167F : blue < 0.04F ? 0.04F : blue;

            double X = red * 0.649926f + green * 0.103455f + blue * 0.197109f;
            double Y = red * 0.234327f + green * 0.743075f + blue * 0.022598f;
            double Z = red * 0.0000000f + green * 0.053077f + blue * 1.035763f;

            double x = X / (X + Y + Z);
            double y = Y / (X + Y + Z);

            return new []{x, y};
        }
    }
}
