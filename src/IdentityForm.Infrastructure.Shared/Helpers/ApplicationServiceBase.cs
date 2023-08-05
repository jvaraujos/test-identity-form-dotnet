using IdentityForm.Shared.Helpers;
using IdentityForm.Shared.Wrapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Refit;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IdentityForm.Infrastructure.Shared.Helpers
{
    public abstract class ApplicationServiceBase
    {
        public Task<ActionResult> ReturnOk(object data, string message = "Operação realizada com sucesso")
        {
            return Task.FromResult(IdentityFormServiceResponseResult.GetResponse(true, message, data, StatusCodes.Status200OK));
        }

        public Task<ActionResult> ReturnNo(object data = null, string message = null, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return Task.FromResult(IdentityFormServiceResponseResult.GetResponse(false, new string[] { message }, data, (int)statusCode));
        }

        public Task<ActionResult> ReturnNo(object data = null, List<string> message = null, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return Task.FromResult(IdentityFormServiceResponseResult.GetResponse(false, new string[] { message.FirstOrDefault() }, data, (int)statusCode));
        }

        public async Task<dynamic> TratamentoExceptionResponse(ApiException ex)
        {
            if (ex.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                var response = new Result
                {
                };
                response.Messages.Add(ex.Message.ToString());
                return response;
            }
            var errors = await ex.GetContentAsAsync<dynamic>();

            if (string.IsNullOrEmpty(errors.ToString()))
            {
                var response = new Result();
                response.Messages.Add(ex.Message.ToString());
                return response;
            }
            var returno = JsonConvert.DeserializeObject<Result>(errors.ToString());
            returno.StatusCode = ex.StatusCode;

            return returno;
        }
    }
}
