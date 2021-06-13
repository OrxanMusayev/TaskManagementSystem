using TaskManagementSystem.Application.Common.Interfaces;
using System;

namespace TaskManagementSystem.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now() => DateTime.Now;

        public DateTime UtcNow() => DateTime.UtcNow;
    }
}
