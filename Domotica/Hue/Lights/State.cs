namespace Domotica.Hue.Lights
{
    public class State
    {
        public bool On { get; set; }
        public int Bri { get; set; }
        public int Hue { get; set; }
        public int Sat { get; set; }
        public string Effect { get; set; }
        public double[] Xy { get; set; }
        public int Ct { get; set; }
        public string Alert { get; set; }
        public string Colormode { get; set; }
        public bool Reachable { get; set; }
    }
}
