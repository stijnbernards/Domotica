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
    class BlinkAll : Page
    {
        public override void Init(HttpListenerContext ctx = null)
        {
            if (this._POST.ContainsKey("COLOR") && this._POST.ContainsKey("TIME") && this._POST.ContainsKey("AMOUNT"))
            {
                Blink();
            }

            response = Rest.Web.Constants.STATUS_TRUE;
            ContentType = Rest.Web.Constants.CONTENT_JSON;
        }

        private async void Blink()
        {
            for (int i = 0; i < Convert.ToInt16(_POST["AMOUNT"]); i++)
            {
                Keyboard.Instance.SetAll(Color.FromRgb(Convert.ToUInt32(_POST["COLOR"], 16)));
                await Task.Delay(Convert.ToInt16(_POST["TIME"]));

                Program.Keyboard.Reset();
                await Task.Delay(Convert.ToInt16(_POST["TIME"]));
            }
        }
    }
}
