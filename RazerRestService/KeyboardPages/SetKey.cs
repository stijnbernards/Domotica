using System;
using System.Net;
using Corale.Colore.Core;
using Corale.Colore.Razer.Keyboard;
using RazerRestService.Helpers;
using Rest.Web;

namespace RazerRestService.KeyboardPages
{
    public class SetKey : Page
    {
        public override void Init(HttpListenerContext ctx = null)
        {
            if (this._POST.ContainsKey("KEY") && this._POST.ContainsKey("COLOR"))
            {
                Key key = _POST["KEY"].ToEnum<Key>();

                Keyboard.Instance.SetKey(key, Color.FromRgb(Convert.ToUInt32(_POST["COLOR"], 16)));
            }

            response = Rest.Web.Constants.STATUS_TRUE;
            ContentType = Rest.Web.Constants.CONTENT_JSON;
        }
    }
}
