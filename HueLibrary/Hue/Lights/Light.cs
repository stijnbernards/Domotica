using System;
using System.Linq;
using RestSharp;

namespace HueLibrary.Hue.Lights
{
    public class Light : IHueItem
    {
        public State State { get; set; }
        public string ID { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string ModelID { get; set; }
        public string ManufacturerName { get; set; }
        public string UniqueID { get; set; }
        public string SwVersion { get; set; }

        private RestRequest stateRequest => currentRequest ?? (currentRequest = new RestRequest($"lights/{ID}/state", Method.PUT));
        private RestRequest currentRequest;

        public void TurnOff()
        {
            State.On = false;
        }

        public void TurnOn()
        {
            State.On = true;
        }

        public void Brightness(int brightness)
        {
            State.Bri = brightness;
        }

        public void Hue(int hue)
        {
            State.Hue = hue;
        }

        public void Saturation(int sat)
        {
            State.Sat = sat;
        }

        public void SetXY(double[] xy)
        {
            State.Xy = xy;
        }

        public void SetColorMode(string mode)
        {
            State.Colormode = mode;
        }

        public void Apply()
        {
            stateRequest.AddJsonBody(new
            {
                on = State.On,
                xy = State.Xy,
                ColorMode = State.Colormode,
                hue = State.Hue,
                sat = State.Sat,
                bri = State.Bri
            });
            HueMain.HueClient.Execute(currentRequest);
            currentRequest = null;
        }
    }
}
