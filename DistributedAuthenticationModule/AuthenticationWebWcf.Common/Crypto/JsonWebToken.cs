using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using AuthenticationWebWcf.Common.Exceptions;
using Newtonsoft.Json;

namespace AuthenticationWebWcf.Common.Crypto
{
    /// <summary>
    /// Provides methods for encoding and decoding JSON Web Tokens.
    /// </summary>
    public class JsonWebToken : IJsonWebToken
    {
        /// <summary>
        /// Creates a JWT given a payload, the signing key (as a string that will be decoded with UTF8), and the algorithm to use.
        /// </summary>
        /// <param name="payload">An arbitrary payload (must be serializable to JSON via <see cref="JsonConvert"/>).</param>
        /// <param name="key">The key used to sign the token.</param>
        /// <param name="algorithm">The hash algorithm to use.</param>
        /// <returns>The generated JWT.</returns>
        public string Encode(string payload, string key, JwtHashAlgorithm algorithm)
        {
            return Encode(payload, Encoding.UTF8.GetBytes(key), algorithm);
        }

        /// <summary>
        /// Creates a JWT given a payload, the signing key, and the algorithm to use.
        /// </summary>
        /// <param name="payload">An arbitrary payload (must be serializable to JSON via <see cref="JsonConvert"/>).</param>
        /// <param name="key">The key used to sign the token.</param>
        /// <param name="algorithm">The hash algorithm to use.</param>
        /// <returns>The generated JWT.</returns>
        public string Encode(string payload, byte[] key, JwtHashAlgorithm algorithm)
        {
            var segments = new List<string>();
            var header = new { typ = "JWT", alg = algorithm.ToString() };

            var headerBytes = Encoding.UTF8.GetBytes(header.ToJson());
            var payloadBytes = Encoding.UTF8.GetBytes(payload);

            segments.Add(Base64UrlEncode(headerBytes));
            segments.Add(Base64UrlEncode(payloadBytes));

            var stringToSign = string.Join(".", segments.ToArray());

            var bytesToSign = Encoding.UTF8.GetBytes(stringToSign);

            var signature = GetSignature(algorithm, key, bytesToSign);
            segments.Add(Base64UrlEncode(signature));

            return string.Join(".", segments.ToArray());
        }

        public string Decode(string token, string key)
        {
            var keyBytes = key != null ? Encoding.UTF8.GetBytes(key) : null;
            return Decode(token, keyBytes);
        }

        /// <summary>
        /// Given a JWT, decode it and return the JSON payload.
        /// </summary>
        /// <param name="token">The JWT.</param>
        /// <param name="keyBytes">The key that was used to sign the JWT (string that will be decoded with UTF8).</param>
        /// <param name="verify">Whether to verify the signature (default is true).</param>
        /// <returns>A string containing the JSON payload.</returns>
        /// <exception cref="SignatureVerificationException">Thrown if the verify parameter was true and the signature was NOT valid or if the JWT was signed with an unsupported algorithm.</exception>
        public string Decode(string token, byte[] keyBytes, bool verify = true)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            var parts = token.Split('.');

            if (parts.Length != 3)
            {
                throw new SignatureVerificationException(string.Format("Invalid signature split. Point count {0}", parts.Length));
            }

            string payloadJson;
            string decodedCrypto;
            string decodedSignature;

            try
            {
                var header = parts[0];
                var payload = parts[1];
                var crypto = Base64UrlDecode(parts[2]);

                var headerJson = Encoding.UTF8.GetString(Base64UrlDecode(header));
                var headerData = JsonConvert.DeserializeObject<Dictionary<string, object>>(headerJson);
                payloadJson = Encoding.UTF8.GetString(Base64UrlDecode(payload));

                if (!verify)
                {
                    return payloadJson;
                }

                var bytesToSign = Encoding.UTF8.GetBytes(string.Concat(header, ".", payload));
                var algorithm = (string)headerData["alg"];

                var algorithmEnum = GetHashAlgorithm(algorithm);
                var signature = GetSignature(algorithmEnum, keyBytes, bytesToSign);
                decodedCrypto = Convert.ToBase64String(crypto);
                decodedSignature = Convert.ToBase64String(signature);
            }
            catch (Exception ex)
            {
                throw new SerializationException("Error decoding", ex);
            }

            if (decodedCrypto != decodedSignature)
            {
                throw new SignatureVerificationException(string.Format("Invalid signature. Expected {0} got {1}", decodedCrypto, decodedSignature));
            }

            return payloadJson;
        }

        private byte[] GetSignature(JwtHashAlgorithm algorithm, byte[] keyBytes, byte[] bytesToSign)
        {
            using (var hashAlgoritm = GetHashAlgoritm(algorithm, keyBytes))
            {
                return hashAlgoritm.ComputeHash(bytesToSign);
            }
        }

        private HashAlgorithm GetHashAlgoritm(JwtHashAlgorithm algoritm, byte[] keyBytes)
        {
            try
            {
                switch (algoritm)
                {
                    case JwtHashAlgorithm.Hs256:
                        return new HMACSHA256(keyBytes);
                    case JwtHashAlgorithm.Hs384:
                        return new HMACSHA384(keyBytes);
                    case JwtHashAlgorithm.Hs512:
                        return new HMACSHA512(keyBytes);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(algoritm), algoritm, null);
                }
            }
            catch (Exception ex)
            {
                throw new SignatureVerificationException(string.Format("Failed to instance HashAlgoritm with key {0}", keyBytes) , ex);
            }
        }

        private static JwtHashAlgorithm GetHashAlgorithm(string algorithm)
        {
            if (algorithm == JwtHashAlgorithm.Hs256.ToString())
            {
                return JwtHashAlgorithm.Hs256;
            }

            if (algorithm == JwtHashAlgorithm.Hs384.ToString())
            {
                return JwtHashAlgorithm.Hs384;
            }

            if (algorithm == JwtHashAlgorithm.Hs512.ToString())
            {
                return JwtHashAlgorithm.Hs512;
            }

            throw new SignatureVerificationException("Algorithm not supported.");
        }

        // from JWT spec
        private static string Base64UrlEncode(byte[] input)
        {
            var output = Convert.ToBase64String(input);
            output = output.Split('=')[0]; // Remove any trailing '='s
            output = output.Replace('+', '-'); // 62nd char of encoding
            output = output.Replace('/', '_'); // 63rd char of encoding
            return output;
        }

        // from JWT spec
        private static byte[] Base64UrlDecode(string input)
        {
            var output = input;
            output = output.Replace('-', '+'); // 62nd char of encoding
            output = output.Replace('_', '/'); // 63rd char of encoding

            // Pad with trailing '='s
            switch (output.Length%4)
            {
                case 0:
                    break; // No pad chars in this case
                case 2:
                    output += "==";
                    break; // Two pad chars
                case 3:
                    output += "=";
                    break; // One pad char
                default:
                    throw new Exception("Illegal base64url string!");
            }

            var converted = Convert.FromBase64String(output); // Standard base64 decoder
            return converted;
        }
    }
}
