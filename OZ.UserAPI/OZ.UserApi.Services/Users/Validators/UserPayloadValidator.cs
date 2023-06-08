using FluentValidation;
using OZ.UserApi.Services.Users.Models;

namespace OZ.UserApi.Services.Users.Validators
{
    public class UserPayloadValidator : AbstractValidator<UserPayload>
    {
        public UserPayloadValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
