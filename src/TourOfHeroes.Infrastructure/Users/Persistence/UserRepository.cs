using Microsoft.EntityFrameworkCore;
using TourOfHeroes.Application.Users.Persistence;
using TourOfHeroes.Domain.Users;
using TourOfHeroes.Infrastructure.Common.Persistence;

namespace TourOfHeroes.Infrastructure.Users.Persistence
{
    public sealed class UserRepository(TourOfHeroesDbContext _dbContext) : IUserRepository
    {
        public async Task CreateUser(User user, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetUser(string email, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(user => user.Email == email, cancellationToken);
        }
    }
}
