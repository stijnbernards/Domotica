using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HueLibrary.Hue
{
    public class LowercaseContractResolver : DefaultContractResolver
    {
        public static JsonSerializerSettings Settings = new JsonSerializerSettings()
        {
            ContractResolver = new LowercaseContractResolver()
        };

        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }
}
