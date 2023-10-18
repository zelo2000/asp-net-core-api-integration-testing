namespace OZ.UserApi.Services.Users.Models
{
    public record User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get => $"{FirstName} {LastName}"; }

        public string? ImageUrl { get; set; }

        public string Email { get; set; }
    }
}
