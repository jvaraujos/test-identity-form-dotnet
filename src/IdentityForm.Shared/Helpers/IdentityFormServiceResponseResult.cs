using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace IdentityForm.Shared.Helpers
{
    public static class IdentityFormServiceResponseResult
    {
        public static ActionResult GetResponse<T>(bool sucess, IList<string> message, T data, int statusCode = 200)
        {
            object value = new object();
            object obj = new object();
            obj = !data.GetType().GetProperties().Any((p) => p.Name == "Data") ? data : data.GetType().GetProperty("Data")!.GetValue(data);
            switch (statusCode)
            {
                case 200:
                    value = new
                    {
                        Succeeded = sucess,
                        Messages = message,
                        Data = obj,
                        StatusCode = (HttpStatusCode)statusCode
                    };
                    break;
                case 400:
                    value = new
                    {
                        Succeeded = sucess,
                        Messages = message,
                        Data = obj,
                        StatusCode = (HttpStatusCode)statusCode
                    };
                    break;
                case 500:
                    throw new Exception(message.FirstOrDefault());
            }

            return new ObjectResult(value)
            {
                StatusCode = statusCode
            };
        }

        public static ActionResult GetResponse<T>(bool sucess, string message, T data, int statusCode = 200)
        {
            object value = new object();
            object obj = new object();
            obj = !data.GetType().GetProperties().Any((p) => p.Name == "Data") ? data : data.GetType().GetProperty("Data")!.GetValue(data);
            switch (statusCode)
            {
                case 200:
                    value = new
                    {
                        Succeeded = sucess,
                        Messages = new List<string> { message },
                        Data = obj,
                        StatusCode = (HttpStatusCode)statusCode
                    };
                    break;
                case 400:
                    value = new
                    {
                        Succeeded = sucess,
                        Messages = new List<string> { message },
                        Data = obj,
                        StatusCode = (HttpStatusCode)statusCode
                    };
                    break;
                case 500:
                    throw new Exception(message);
            }

            return new ObjectResult(value)
            {
                StatusCode = statusCode
            };
        }
    }
}
