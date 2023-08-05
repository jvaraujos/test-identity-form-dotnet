using IdentityForm.Shared.Constants.Localization;
using IdentityForm.Shared.Settings;
using System.Linq;

namespace IdentityForm.Api.Settings
{
    public record ServerPreference : IPreference
    {
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "pt-BR";
    }
}