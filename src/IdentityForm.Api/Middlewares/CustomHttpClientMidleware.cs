using SendGrid.Helpers.Mail;
using Serilog.Sinks.Grafana.Loki.HttpClients;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IdentityForm.Api.Middlewares
{
    public class GrafanaLokiHttpClient : BaseLokiHttpClient
    {
        public GrafanaLokiHttpClient(HttpClient httpClient = null)
        : base(httpClient)
        {
        }
        public async override Task<HttpResponseMessage> PostAsync(string requestUri, Stream contentStream)
        {
            using (var memoryStream = new MemoryStream())
            {
                await contentStream.CopyToAsync(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                using var content = new StreamContent(memoryStream);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                var result = await HttpClient.PostAsync(requestUri, content);
                string contentr = await result.Content.ReadAsStringAsync();
                return result;
            }
        }

    }
}
