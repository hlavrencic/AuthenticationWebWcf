using AuthenticationWebWcf.Common.Exceptions;
using Newtonsoft.Json;

namespace AuthenticationWebWcf.Common.Crypto
{
    public interface IJsonWebToken
    {
        /// <summary>
        /// Creates a JWT given a payload, the signing key (as a string that will be decoded with UTF8), and the algorithm to use.
        /// </summary>
        /// <param name="payload">An arbitrary payload (must be serializable to JSON via <see cref="JsonConvert"/>).</param>
        /// <param name="key">The key used to sign the token.</param>
        /// <param name="algorithm">The hash algorithm to use.</param>
        /// <returns>The generated JWT.</returns>
        string Encode(string payload, string key, JwtHashAlgorithm algorithm);

        /// <summary>
        /// Creates a JWT given a payload, the signing key, and the algorithm to use.
        /// </summary>
        /// <param name="payload">An arbitrary payload (must be serializable to JSON via <see cref="JsonConvert"/>).</param>
        /// <param name="key">The key used to sign the token.</param>
        /// <param name="algorithm">The hash algorithm to use.</param>
        /// <returns>The generated JWT.</returns>
        string Encode(string payload, byte[] key, JwtHashAlgorithm algorithm);

        string Decode(string token, string key);

        /// <summary>
        /// Given a JWT, decode it and return the JSON payload.
        /// </summary>
        /// <param name="token">The JWT.</param>
        /// <param name="key">The key that was used to sign the JWT (string that will be decoded with UTF8).</param>
        /// <param name="verify">Whether to verify the signature (default is true).</param>
        /// <returns>A string containing the JSON payload.</returns>
        /// <exception cref="SignatureVerificationException">Thrown if the verify parameter was true and the signature was NOT valid or if the JWT was signed with an unsupported algorithm.</exception>
        string Decode(string token, byte[] key, bool verify = true);
    }
}