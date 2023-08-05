using System.Collections.Generic;

namespace IdentityForm.Shared.Wrapper
{
    public interface IResult
    {
        List<string> Messages { get; }
        bool Succeeded { get; }
    }
    public interface IResult<out T> : IResult
    {
        T Data { get; }
    }

}