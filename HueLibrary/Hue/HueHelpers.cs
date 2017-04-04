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

        public static double[] RGBToXY(double red, double green, double blue, out double brightness)
        {
            red = red / MAX_COLOR;
            green = green / MAX_COLOR;
            blue = blue / MAX_COLOR;

            red = (red > 0.04045) ? Math.Pow((red + 0.055) / (1.0 + 0.055), 2.4) : (red / 12.92);
            green = (green > 0.04045) ? Math.Pow((green + 0.055) / (1.0 + 0.055), 2.4) : (green / 12.92);
            blue = (blue > 0.04045) ? Math.Pow((blue + 0.055) / (1.0 + 0.055), 2.4) : (blue / 12.92);

            double X = red * 0.664511 + green * 0.154324 + blue * 0.162028;
            double Y = red * 0.283881 + green * 0.668433 + blue * 0.047685;
            double Z = red * 0.000088 + green * 0.072310 + blue * 0.986039;

            double fx = X / (X + Y + Z);
            double fy = Y / (X + Y + Z);

            if (!CheckDouble(fx))
            {
                fx = 0.0f;
            }

            if (!CheckDouble(fy))
            {
                fy = 0.0f;
            }

            brightness = Math.Round(Y * 1000);

            return new[] {Math.Round(fx, 4), Math.Round(fy, 4)};
        }

        private static bool CheckDouble(double number)
        {
            return !Double.IsNaN(number) && !Double.IsInfinity(number);
        }
    }
}
