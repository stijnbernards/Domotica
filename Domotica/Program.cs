using System;
using System.Threading;
using Domotica.Hue;

namespace Domotica
{
    public static class Program
    {
        public static string IP = "192.168.178.25";
        public static string Username = "X3v1tPkd2szuFL9EXJ1LwYy9yZMr2EwZt3UUQbfQ";

        private static HueMain hueMain;

        private static void Main()
        {
            hueMain = new HueMain(IP, Username);
            hueMain.IndexLights();

            hueMain.Lights.ForEach(x =>
            {
                x.TurnOn();
            });

            Brightness();

            Console.ReadLine();
        }

        private static void Brightness()
        {
            while (true)
            {
                Thread.Sleep(50);

                hueMain.Lights[2].Brightness(25);
                hueMain.Lights[2].Sat(251);
                hueMain.Lights[2].Hue(6291);
                hueMain.Lights[2].Execute();
            }
        }
    }
}
