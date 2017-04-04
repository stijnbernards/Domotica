using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domotica.Hue.Lights;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Domotica.Hue
{
    public class HueMain
    {
        public static RestClient HueClient;
        public List<Light> Lights;

        public HueMain(string ip, string username)
        {
            HueClient = new RestClient($"http://{ip}/api/{username}");
        }

        public void IndexLights()
        {
            IRestResponse resp = HueCommand(new RestRequest("lights"));

            Lights = ResponseToList<Light>(resp);
        }

        public IRestResponse HueCommand(RestRequest request)
        {
            return HueClient.Execute(request);
        }

        public IRestResponse<T> HueCommand<T>(RestRequest request) where T : new()
        {
            return HueClient.Execute<T>(request);
        }

        public List<T> ResponseToList<T>(IRestResponse response) where T : IHueItem
        {
            JObject responseObject = JObject.Parse(response.Content);
            List<T> result = new List<T>();

            foreach (KeyValuePair<string, JToken> kvp in responseObject)
            {
                T item = kvp.Value.ToObject<T>();
                item.ID = kvp.Key;
                result.Add(item);
            }

            return result;
        }
    }
}
