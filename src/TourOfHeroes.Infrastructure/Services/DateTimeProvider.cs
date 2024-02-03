using TourOfHeroes.Application.Common.Services;

namespace TourOfHeroes.Infrastructure.Services
{
    internal sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
