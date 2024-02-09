using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TourOfHeroes.Domain.Heroes.Events;

namespace TourOfHeroes.Application.Heroes.Events
{
    public class DummyHandler : INotificationHandler<HeroCreated>
    {
        public Task Handle(HeroCreated notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
