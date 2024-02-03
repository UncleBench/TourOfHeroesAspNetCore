using ErrorOr;
using System.Threading;
using System.Threading.Tasks;

namespace TourOfHeroes.Application.Security.Authentication
{
    public interface IAuthenticationService
    {
        public Task<ErrorOr<AuthenticationResult>> Login(
            string email,
            string password,
            CancellationToken cancellationToken);

        public Task<ErrorOr<AuthenticationResult>> Register(
            string firstName,
            string lastName,
            string email,
            string password,
            CancellationToken cancellationToken);
    }
}
