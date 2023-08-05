namespace IdentityForm.Application.Responses.Payments
{
    public class MBNotificationDto
    {
        public string action { get; set; }
        public string api_version { get; set; }
        public string application_id { get; set; }
        public string date_created { get; set; }
        public string id { get; set; }
        public string live_mode { get; set; }
        public string type { get; set; }
        public string user_id { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string id { get; set; }
    }
}
