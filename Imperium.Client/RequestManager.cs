using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NetData = System.Collections.Generic.Dictionary<string, object>;

namespace Imperium.Client
{
    public class RequestManager
    {
        public static JsonSerializer Serializer =
            JsonSerializer.Create(
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    TypeNameHandling = TypeNameHandling.Objects,
                    FloatParseHandling = FloatParseHandling.Decimal,
                });
        
        public string CreateRequest(string type, NetData args)
        {
            return new JObject
            {
                {"type", type},
                {"args", JToken.FromObject(args)}
            }.ToString();
        }

        public T DecodeResponse<T>(string response)
        {
            return JToken.Parse(response)["result"].ToObject<T>(Serializer);
        }
    }
}