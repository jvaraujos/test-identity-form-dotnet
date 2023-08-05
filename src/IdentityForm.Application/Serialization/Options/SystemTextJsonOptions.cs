using IdentityForm.Application.Interfaces.Serialization.Options;
using System.Text.Json;

namespace IdentityForm.Application.Serialization.Options
{
    public class SystemTextJsonOptions : IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();
    }
}