namespace IdentityForm.Shared.Constants.Application
{
    public static class ApplicationConstants
    {
        public static class SignalR
        {
            public const string HubUrl = "/signalRHub";
        }
        public static class Cache
        {

            public const string GetAllDocumentIdentitysKey = "all-DocumentIdentitys";

            public static string GetAllEntityExtendedAttributesCacheKey(string entityFullName)
            {
                return $"all-{entityFullName}-extended-attributes";
            }

            public static string GetAllEntityExtendedAttributesByEntityIdCacheKey<TEntityId>(string entityFullName, TEntityId entityId)
            {
                return $"all-{entityFullName}-extended-attributes-{entityId}";
            }

            public static string GetProcurationCertifiedCacheKey<TEntityId>(TEntityId entityId)
            {
                return $"procuration-certified-{entityId}";
            }
            public static string GetCustomerCertifiedCacheKey<TEntityId>(TEntityId entityId)
            {
                return $"customer-certified-{entityId}";
            }
        }
        public static class Redis
        {
            public const string CertifiedsFolder = "certifieds";

            public static string GetCertifiedPath(long customerId)
            {
                return $"{CertifiedsFolder}:{customerId}";
            }

        }
        public static class MimeTypes
        {
            public const string OpenXml = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            public const string Pdf = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            public const string Xml = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        }

    }
}