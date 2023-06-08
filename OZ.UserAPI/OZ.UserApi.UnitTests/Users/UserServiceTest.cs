using Moq;
using OZ.UserApi.Data.Users;
using OZ.UserApi.Services.Users;
using OZ.UserApi.Services.Users.Models;
using Shouldly;

namespace OZ.UserApi.UnitTests.Users
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _repository;
        private readonly IUserService _service;

        private readonly UserEntity _userEntity = new()
        {
            Id = Guid.Empty,
            FirstName = "FirstName",
            LastName = "LastName",
            Email = "FirstName.LastName@domain.test",
            CreatedAt = DateTime.MinValue
        };

        public UserServiceTest()
        {
            _repository = new Mock<IUserRepository>();
            _service = new UserService(_repository.Object);
        }

        [Fact]
        public async Task GetUser_UserList()
        {
            var userEntities = new List<UserEntity> { _userEntity };
            _repository.Setup(r => r.GetAllAsync()).ReturnsAsync(userEntities);

            var users = await _service.GetUsers();

            users.ShouldNotBeEmpty();
            users.Count().ShouldBe(1);
            var user = users.FirstOrDefault();
            user?.Id.ShouldBe(_userEntity.Id);
            user?.FullName.ShouldBe($"{_userEntity.FirstName} {_userEntity.LastName}");
        }

        [Fact]
        public async Task GetUserById_UserAsync()
        {
            _repository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(_userEntity);

            var user = await _service.GetUserById(_userEntity.Id);

            user.ShouldNotBeNull();
            user?.Id.ShouldBe(_userEntity.Id);
            user?.FullName.ShouldBe($"{_userEntity.FirstName} {_userEntity.LastName}");
        }

        [Fact]
        public async Task CreateUser_User()
        {
            _repository.Setup(r => r.AddAsync(It.IsAny<UserEntity>())).ReturnsAsync(_userEntity);

            var user = await _service.CreateUser(new UserPayload());

            user.ShouldNotBeNull();
            user?.Id.ShouldBe(_userEntity.Id);
        }

        [Fact]
        public async Task UpdateUser_User()
        {
            _repository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(_userEntity);
            _repository.Setup(r => r.UpdateAsync(It.IsAny<UserEntity>())).ReturnsAsync(_userEntity);

            var user = await _service.UpdateUser(new UserPayload { Id = _userEntity.Id });

            user.ShouldNotBeNull();
            user?.Id.ShouldBe(_userEntity.Id);
        }

        [Fact]
        public void UpdateUser_Exception_UserIdIsNull()
        {
            _repository.Setup(r => r.UpdateAsync(It.IsAny<UserEntity>())).ReturnsAsync(_userEntity);

            var exception = Should.Throw<ApplicationException>(async () => await _service.UpdateUser(new UserPayload { Id = null }));
            exception.Message.ShouldBe("User id is null");
        }

        [Fact]
        public async Task DeleteUser_Task()
        {
            _repository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(_userEntity);
            _repository.Setup(r => r.DeleteAsync(It.IsAny<UserEntity>())).Returns(Task.CompletedTask);

            await _service.DeleteUser(_userEntity.Id);

            _repository.Verify(m => m.DeleteAsync(_userEntity), Times.Once);
        }
    }
}