using Corale.Colore.Core;
using Corale.Colore.Razer.Mouse;

namespace RazerRestService
{
    public class MouseMain
    {
        private Color defaultColor = Color.FromRgb(0x00FFFF);

        public MouseMain()
        {
            Mouse.Instance.SetAll(Color.FromRgb(0xFF000000));
        }

        public void Reset()
        {
            Mouse.Instance.SetLed(Led.All, defaultColor);
        }
    }
}
