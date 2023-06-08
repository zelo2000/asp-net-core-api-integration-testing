namespace OZ.UserApi.IntegrationTests.Users.Base
{
    public static class UserEmail
    {
        public const string Default = "test@test.com";

        private static readonly Random _random = new();

        public static string GenerateEmail()
        {
            var name = RandomString(5);
            var surname = RandomString(8);
            return $"{name}.{surname}@test.com";
        }

        private static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}
