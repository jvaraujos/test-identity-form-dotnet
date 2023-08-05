using System;
using System.Globalization;
using System.Linq;

namespace IdentityForm.Shared.Helpers
{
    public static class StringExtensions
    {
        public static string HashPassword(this string password) => BCrypt.Net.BCrypt.HashPassword(password);

        public static bool VerifyPassword(this string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
        public static string GetNumbers(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return new string(input.Where(c => char.IsDigit(c)).ToArray());
            }
            return string.Empty;
        }

        public static string ToInt(this string input)
        {
            if (!string.IsNullOrEmpty(input)) return new string(input.Where(c => char.IsDigit(c)).ToArray());
            return string.Empty;
        }

        public static string ToCNPJ(this string CNPJ)
        {
            return Convert.ToUInt64(CNPJ).ToString(@"00\.000\.000\/0000\-00");
        }
        public static string ToCPF(this string cpf)
        {
            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }

        public static decimal ConvertBrlToDecimal(this string valueBrl)
        {
            return Convert.ToDecimal(valueBrl.Replace("R$", "").Replace(".", "").Replace(",", ".").Trim(), CultureInfo.InvariantCulture);
        }
    }
}
