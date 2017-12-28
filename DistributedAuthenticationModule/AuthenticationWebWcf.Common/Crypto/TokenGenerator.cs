using System;
using System.Linq;

namespace AuthenticationWebWcf.Common.Crypto
{
    public static class TokenGenerator
    {
        public static string New()
        {
            var guid = Guid.NewGuid().ToString().ToUpperInvariant();
            var checksum = GetCheckSum(guid);
            return guid + "-" + checksum;
        }

        public static bool IsValid(string token)
        {
            try
            {
                var index = token.LastIndexOf("-", StringComparison.Ordinal);
                var guidsection = token.Substring(0, index);
                var checkSum = token.Substring(index + 1);
                return IsGuid(guidsection) && GetCheckSum(guidsection) == checkSum;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool IsGuid(string guidsection)
        {
            Guid guid;
            return Guid.TryParse(guidsection, out guid);
        }

        private static string GetCheckSum(string guid)
        {
            return guid.Sum(c => (int)c).ToString("X4");
        }
    }
}
