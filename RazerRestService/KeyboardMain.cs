using Corale.Colore.Core;

namespace RazerRestService
{
    public class KeyboardMain
    {
        private Color defaultColor = Color.FromRgb(0x00FFFF);

        public KeyboardMain()
        {
            Keyboard.Instance.SetAll(defaultColor);
        }

        public void Reset()
        {
            Keyboard.Instance.SetAll(defaultColor);
        }
    }
}
