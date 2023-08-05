namespace IdentityForm.Infrastructure.Shared.Configurations
{
    public class AwsConfiguration
    {
        public string KeyId { get; set; }
        public string SecretKey { get; set; }
        public string BucketName { get; set; }
        public string BucketNamePublic { get; set; }
        public string Region { get; set; }
    }
}
