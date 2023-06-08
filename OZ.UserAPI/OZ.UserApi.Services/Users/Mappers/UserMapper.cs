using OZ.UserApi.Data.Users;
using OZ.UserApi.Services.Users.Models;

namespace OZ.UserApi.Services.Users.Mappers
{
    public static class UserMapper
    {
        public static User ToDomain(this UserEntity entity)
        {
            return new User
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email
            };
        }

        public static UserEntity ToEntity(this UserPayload entity)
        {
            var model = new UserEntity
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email
            };

            if (entity.Id.HasValue)
            {
                model.Id = entity.Id.Value;
            }

            return model;
        }
    }
}
