using System.Text.RegularExpressions;

namespace SistemaTaxiMobil.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return true; // Opcional

            var regex = new Regex(@"^\d{10}$");
            return regex.IsMatch(phone);
        }

        public static bool IsValidPassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && password.Length >= 6;
        }

        public static bool IsValidCreditCard(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                return false;

            // Eliminar espacios
            cardNumber = cardNumber.Replace(" ", "");

            // Debe tener 16 dígitos
            if (cardNumber.Length != 16 || !cardNumber.All(char.IsDigit))
                return false;

            // Algoritmo de Luhn para validar tarjeta
            return IsValidLuhn(cardNumber);
        }

        private static bool IsValidLuhn(string cardNumber)
        {
            int sum = 0;
            bool alternate = false;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int digit = int.Parse(cardNumber[i].ToString());

                if (alternate)
                {
                    digit *= 2;
                    if (digit > 9)
                        digit -= 9;
                }

                sum += digit;
                alternate = !alternate;
            }

            return sum % 10 == 0;
        }

        public static bool IsValidCVV(string cvv)
        {
            if (string.IsNullOrWhiteSpace(cvv))
                return false;

            return (cvv.Length == 3 || cvv.Length == 4) && cvv.All(char.IsDigit);
        }

        public static bool IsValidExpiryDate(string expiryDate)
        {
            if (string.IsNullOrWhiteSpace(expiryDate))
                return false;

            var parts = expiryDate.Split('/');
            if (parts.Length != 2)
                return false;

            if (!int.TryParse(parts[0], out int month) || !int.TryParse(parts[1], out int year))
                return false;

            if (month < 1 || month > 12)
                return false;

            // Convertir año de 2 dígitos a 4 dígitos
            if (year < 100)
                year += 2000;

            var expiryDateTime = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            return expiryDateTime >= DateTime.Now;
        }
    }
}