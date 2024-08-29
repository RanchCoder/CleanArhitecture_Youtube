using BuberDinner.Application.common.Interfaces.Services;

namespace BuberDinner.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UTCNow => DateTime.UtcNow;
}