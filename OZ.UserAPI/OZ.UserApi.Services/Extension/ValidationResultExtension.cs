using FluentValidation.Results;

namespace OZ.UserApi.Services.Extension
{
    public static class ValidationResultExtension
    {
        public static string BuildMessage(this ValidationResult validationResult)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).Where(e => !string.IsNullOrEmpty(e));
            return string.Join(", ", errors);
        }
    }
}
