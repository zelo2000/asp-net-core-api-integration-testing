﻿using OZ.UserApi.Data.Base;

namespace OZ.UserApi.Data.Users
{
    public class UserEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? ImageUrl { get; set; }

        public string PasswordHash { get; set; }
        public DateTime LastLogIn { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
