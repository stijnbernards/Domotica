using System;
using System.Drawing;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Windows.Forms;
using RazerRestService.KeyboardPages;
using Rest;

namespace RazerRestService
{
    public class Program
    {
        public static KeyboardMain Keyboard;
        public static MouseMain Mouse;

        private static RestServer rest;
        private static string baseUri = GetLocalIPAddress();

        private static NotifyIcon icon;
        private static ContextMenu menu;
        private static MenuItem menuItem;
        private static IContainer components;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            BuildMenu();

            Keyboard = new KeyboardMain();
            Mouse = new MouseMain();

            rest = new RestServer("http://" + baseUri + "/", "razer", 0, true);

            rest.SetNoAuth();

            rest.AddRouting<SetKey>("keyboard/setKey");
            rest.AddRouting<BlinkAll>("keyboard/blink");
            rest.AddRouting<Reset>("keyboard/reset");
            rest.AddRouting<Alarm>("keyboard/alarm");

            rest.Start();

            Application.Run();
        }

        private static string GetLocalIPAddress()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    Console.WriteLine(ip);
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        private static void BuildMenu()
        {
            components = new Container();
            menu = new ContextMenu();
            menuItem = new MenuItem();

            menu.MenuItems.AddRange(
                    new []
                    {
                        menuItem
                    }
                );

            menuItem.Index = 0;
            menuItem.Text = @"Exit";
            menuItem.Click += MenuItemClick;

            icon = new NotifyIcon(components)
            {
                Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location),
                ContextMenu = menu,
                Text = @"RazerRest API",
                Visible = true
            };
        }

        private static void MenuItemClick(object sender, EventArgs e)
        {
            icon.Visible = false;
            rest.Stop();
            Application.Exit();
        }
    }
}
