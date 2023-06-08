using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using OZ.UserApi.Data;
using OZ.UserApi.Data.Users;

namespace OZ.UserApi.IntegrationTests.Users.Base
{
    public class UserFactory
    {
        private readonly Fixture _fixture;
        private readonly IServiceProvider _services;

        public UserFactory(IServiceProvider services)
        {
            _services = services;
            _fixture = new Fixture();
        }

        public async Task<List<UserEntity>> CreateAndInsertUserList(int amount = 1)
        {
            using var serviceScope = _services.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<UserApiDbContext>()
                ?? throw new Exception("Fail to get database context");

            var users = new List<UserEntity>();
            for (int i = 0; i < amount; i++)
            {
                var user = _fixture.Build<UserEntity>()
                    .With(u => u.Email, UserEmail.GenerateEmail())
                    .Create();

                users.Add(user);
            }

            context.Set<UserEntity>().AddRange(users);
            await context.SaveChangesAsync();

            return users;
        }
    }
}
