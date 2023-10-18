using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using OZ.UserApi.Services.Auth.Models;

namespace OZ.UserApi.Services.Auth.Helpers
{
    public interface IHashGenerator
    {
        string GetPasswordHash(string password);
    }

    public class HashGenerator : IHashGenerator
    {
        private readonly HashGenerationSetting _hashGenerationSetting;

        public HashGenerator(IOptions<HashGenerationSetting> configurationFactory)
        {
            _hashGenerationSetting = configurationFactory.Value;
        }

        /// <summary>
        /// Create password hash.
        /// </summary>
        /// <param name="password">Password</param>
        /// <returns>Password hash</returns>
        public string GetPasswordHash(string password)
        {
            var saltBytes = Convert.FromBase64String(_hashGenerationSetting.Salt);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: _hashGenerationSetting.IterationCount,
                numBytesRequested: _hashGenerationSetting.BytesNumber));

            return hashed;
        }
    }
}
