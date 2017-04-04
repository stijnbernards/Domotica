using System;
using Newtonsoft.Json;
using RestSharp;

namespace Domotica.Hue.Lights
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

            stateRequest.AddJsonBody(new { on = State.On });
        }

        public void TurnOn()
        {
            State.On = true;

            stateRequest.AddJsonBody(new { on = State.On });
        }

        public void Brightness(int brightness)
        {
            State.Bri = brightness;

            stateRequest.AddJsonBody(new { bri = State.Bri });
        }

        public void Hue(int hue)
        {
            State.Hue = hue;

            stateRequest.AddJsonBody(new { hue = State.Hue });
        }

        public void Saturation(int sat)
        {
            State.Sat = sat;

            stateRequest.AddJsonBody(new { sat = State.Sat });
        }

        public void Apply()
        {
            HueMain.HueClient.Execute(currentRequest);
            currentRequest = null;
        }
    }
}
