namespace OZ.UserApi.Services.Users.Models
{
    public record UserPayload
    {
        public Guid? Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
