﻿using System.ComponentModel.DataAnnotations;
using TourOfHeroes.Domain.Common;

namespace TourOfHeroes.Domain.Users
{
    public sealed class User : Entity
    {
        [MinLength(2), MaxLength(20)]
        public string FirstName { get; set; } = null!;

        [MinLength(2), MaxLength(50)]
        public string LastName { get; set; } = null!;

        [MaxLength(30)]
        public string Email { get; set; } = null!;

        [MinLength(10), MaxLength(20)]
        public string Password { get; set; } = null!;

        public User()
        {
        }
    }
}