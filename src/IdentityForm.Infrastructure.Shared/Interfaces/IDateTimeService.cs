using System;

namespace IdentityForm.Infrastructure.Shared.Interfaces
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}