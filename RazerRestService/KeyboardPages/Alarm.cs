using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Corale.Colore.Core;
using Rest.Web;

namespace RazerRestService.KeyboardPages
{
    public class Alarm : Page
    {
        public override void Init(HttpListenerContext ctx = null)
        {
            Blink();

            response = Rest.Web.Constants.STATUS_TRUE;
            ContentType = Rest.Web.Constants.CONTENT_JSON;
        }

        private async void Blink()
        {
            for (int i = 0; i < 5; i++)
            {
                Keyboard.Instance.SetAll(Color.FromRgb(0xFF0000));
                await Task.Delay(500);

                Program.Keyboard.Reset();
                await Task.Delay(500);
            }
        }
    }
}
