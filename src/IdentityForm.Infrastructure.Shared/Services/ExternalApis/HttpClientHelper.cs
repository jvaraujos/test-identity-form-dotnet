using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IdentityForm.Infrastructure.Shared.Services.ExternalApis
{
    public class HttpClientHelper
    {
        private readonly string _baseUrl;
        private string _bearerToken;

        public HttpClientHelper(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task<T> CallAsync<T>(HttpMethod httpMethod, string path, object body = null, int segundosTimeOut = 100)
        {
            var response = await ExecuteAsync(httpMethod, path, body, segundosTimeOut);
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public void SetBearerToken(string bearerToken) => _bearerToken = bearerToken;

        async Task<HttpResponseMessage> ExecuteAsync(HttpMethod httpMethod, string path, object body, int segundosTimeOut)
        {
            HttpClient client = CreatetHttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", _bearerToken == string.Empty ? "" : $"Bearer {_bearerToken}");
            client.Timeout = TimeSpan.FromSeconds(segundosTimeOut);
            var request = new HttpRequestMessage(httpMethod, GetUrl(path));

            if (body != null)
            {
                request.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("text/plain"));
                request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8,
                    "application/json");
            }

            var response = await client.SendAsync(request);
            VerificarStatusHttp(response);
            return response;

        }

        private HttpClient CreatetHttpClient()
        {
            HttpClientHandler httpClientHandler;

            httpClientHandler = new HttpClientHandler();

            if (httpClientHandler.SupportsAutomaticDecompression)
            {
                httpClientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }

            return new HttpClient(httpClientHandler);
        }

        private Uri GetUrl(string path)
        {
            Uri retorno;
            string url = _baseUrl;
            string pathFormatted = path;

            if (_baseUrl != null && _baseUrl.EndsWith("/"))
                url = url.Remove(url.Length - 1);

            if (!string.IsNullOrEmpty(path) && path.StartsWith("/"))
                pathFormatted = path.Substring(1);

            if (!Uri.TryCreate($"{url}/{pathFormatted}", UriKind.Absolute, out retorno))
                throw new Exception($"Url de conexão da com servidor inválido. Verifique a configuração de URL no web.config. Url: {url}.");

            return retorno;
        }

        private void VerificarStatusHttp(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode) return;

            var statusHttp = response.StatusCode;
            var prefixoErro = $"Erro ao acessar servidor: {_baseUrl}. ";

            if (statusHttp == HttpStatusCode.BadRequest)
            {
                var textoResposta = JToken.Parse(response.Content.ReadAsStringAsync().Result).ToString();
                throw new Exception(prefixoErro + Environment.NewLine + textoResposta);
            }
            else if (statusHttp == HttpStatusCode.InternalServerError)
            {
                string textoResposta;
                string textoDescricao = null;
                try
                {
                    textoResposta = JToken.Parse(response.Content.ReadAsStringAsync().Result).ToString();
                    var resposta = JsonConvert.DeserializeObject<dynamic>(textoResposta);
                    textoDescricao = prefixoErro + resposta.Messages.ToString();
                }
                catch (Exception)
                {
                    textoResposta = prefixoErro + (int)statusHttp + " - " + statusHttp;
                }
                throw new Exception(textoDescricao ?? textoResposta);
            }
            else
                throw new Exception(prefixoErro + (int)statusHttp + " - " + statusHttp);
        }
    }
}
