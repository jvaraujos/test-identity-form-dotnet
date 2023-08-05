using IdentityForm.Infrastructure.Shared.Interfaces;
using System;

namespace IdentityForm.Infrastructure.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.Now.ToUniversalTime();
    }
}