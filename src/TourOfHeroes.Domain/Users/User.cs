using System.ComponentModel.DataAnnotations;
using TourOfHeroes.Domain.Common.Models;
using TourOfHeroes.Domain.Users.ValueObjects;

namespace TourOfHeroes.Domain.Users
{
    public sealed class User : AggregateRoot<UserId, Guid>
    {
        [MinLength(2), MaxLength(20)]
        public string FirstName { get; set; } = null!;

        [MinLength(2), MaxLength(50)]
        public string LastName { get; set; } = null!;

        [MaxLength(30)]
        public string Email { get; set; } = null!;

        [MinLength(10), MaxLength(20)]
        public string Password { get; set; } = null!;

        private User(
            UserId userId,
            string firstName,
            string lastName,
            string email,
            string password)
            : base(userId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        public static User Create(
            string firstName, 
            string lastName,
            string email, 
            string password)
        {
            return new(
                UserId.CreateUnique(),
                firstName,
                lastName,
                email,
                password);
        }

        private User()
        {
        }
    }
}
