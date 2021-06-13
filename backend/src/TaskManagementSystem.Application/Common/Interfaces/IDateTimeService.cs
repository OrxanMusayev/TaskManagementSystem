using System;

namespace TaskManagementSystem.Application.Common.Interfaces
{
    public interface IDateTimeService
    {
        DateTime Now();
        DateTime UtcNow();
    }
}
