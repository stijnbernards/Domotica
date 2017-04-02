using System.Net;
using Rest.Web;

namespace RazerRestService.KeyboardPages
{
    public class Reset : Page
    {
        public override void Init(HttpListenerContext ctx = null)
        {
            Program.Keyboard.Reset();

            response = Constants.STATUS_TRUE;
            ContentType = Constants.CONTENT_JSON;
        }
    }
}
