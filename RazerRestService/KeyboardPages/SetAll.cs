using System;
using System.Net;
using Corale.Colore.Core;
using Rest.Web;

namespace RazerRestService.KeyboardPages
{
    class SetAll : Page
    {
        public override void Init(HttpListenerContext ctx = null)
        {
            if (this._POST.ContainsKey("COLOR"))
            {
                Keyboard.Instance.SetAll(Color.FromRgb(Convert.ToUInt32(_POST["COLOR"], 16)));
            }

            response = Constants.STATUS_TRUE;
            ContentType = Constants.CONTENT_JSON;
        }
    }
}
