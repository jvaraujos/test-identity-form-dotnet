using IdentityForm.Application.Interfaces.Serialization.Settings;
using Newtonsoft.Json;

namespace IdentityForm.Application.Serialization.Settings
{
    public class NewtonsoftJsonSettings : IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}