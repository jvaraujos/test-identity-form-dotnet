using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace IdentityForm.Infrastructure.Shared.Helpers
{
    public static class ExceptionExtensions
    {
        public static string ListExeption(this Exception exception, object obj)
        {
            if (exception.InnerException != null)
                exception = exception.InnerException;

            var dictionary = new Dictionary<string, string>
            {
                { "Mensagem", exception?.Message.RemoveChars() ?? "Message" },
                { "Projeto", exception?.Source.RemoveChars() ?? "Source" },

                { "Dados da Requisição", JsonConvert.SerializeObject(obj).RemoveChars() },
            };

            var body = exception.ListExeptionBody();

            dictionary.Add("Pilha de erro", body);

            return JsonConvert.SerializeObject(dictionary);
        }

        private static string RemoveChars(this string value)
        {
            if (!string.IsNullOrEmpty(value))
                return value.Replace("\\", "").Replace("\"", "");

            return value;
        }

        public static string ListExeptionBody(this Exception exception)
        {
            var trace = new List<string>();

            do
            {
                trace.Add(exception.Message.RemoveChars());

                if (!string.IsNullOrWhiteSpace(exception.StackTrace))
                {
                    foreach (var item in exception.StackTrace.Split('\n'))
                    {
                        trace.Add(item.RemoveChars());
                    }
                }

                exception = exception.InnerException;

            } while (exception != null);

            return JsonConvert.SerializeObject(trace);
        }
    }
}
