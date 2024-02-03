using System.Threading;
using System.Threading.Tasks;
using TourOfHeroes.Domain.Users;

namespace TourOfHeroes.Application.Users.Persistence
{
    public interface IUserRepository
    {
        Task<User?> GetUser(string email, CancellationToken cancellationToken);
        Task CreateUser(User user, CancellationToken cancellationToken);
    }
}
