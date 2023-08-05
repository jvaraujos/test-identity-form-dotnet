using Microsoft.Extensions.Localization;

namespace IdentityForm.Api.Managers.Preferences
{
    public class ServerPreferenceManager : IServerPreferenceManager
    {
        private readonly IStringLocalizer<ServerPreferenceManager> _localizer;

        public ServerPreferenceManager(
            IStringLocalizer<ServerPreferenceManager> localizer)
        {
            _localizer = localizer;
        }
    }
}