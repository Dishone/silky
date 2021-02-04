using System;
using Lms.Core.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Lms.Core.Serialization
{
    public class DefaultJsonSerializer : IJsonSerializer, ITransientDependency
    {
        public string Serialize(object instance, bool camelCase = true, bool indented = false)
        {
            var settings = new JsonSerializerSettings();

            if (camelCase)
            {
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }
            else
            {
                settings.ContractResolver = new DefaultContractResolver();
            }

            if (indented)
            {
                settings.Formatting = Formatting.Indented;
            }

            return JsonConvert.SerializeObject(instance, settings);
        }

        public T Deserialize<T>(string jsonString, bool camelCase = true)
        {
            var settings = new JsonSerializerSettings();

            if (camelCase)
            {
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }
            else
            {
                settings.ContractResolver = new DefaultContractResolver();
            }

            return JsonConvert.DeserializeObject<T>(jsonString, settings);
        }

        public object Deserialize(Type type, string jsonString, bool camelCase = true)
        {
            var settings = new JsonSerializerSettings();

            if (camelCase)
            {
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }
            else
            {
                settings.ContractResolver = new DefaultContractResolver();
            }

            return JsonConvert.DeserializeObject(jsonString, type, settings);
        }
    }
}