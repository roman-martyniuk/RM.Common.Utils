namespace RM.Common.Utils
{
    /// <summary>
    /// Represents declension extension methods.
    /// </summary>
    public static class DeclensionExtensions
    {
        /// <summary>
        /// Returns the word declension depending on the given number.
        /// NOTE: This overload version is usefull for russian-like languages.
        /// </summary>
        /// <param name="nominative">Nominative (Именительный падеж слова). e.g. "день".</param>
        /// <param name="number">The number from which the selected word depends.</param>
        /// <param name="plural">Plural (Множественное число слова). e.g. "дней".</param>
        /// <param name="genitive">Genitive (Родительный падеж слова). e.g. "дня".</param>
        /// <returns>Returns the word declension depending on the given number.</returns>
        public static string Decline(this string nominative, long number, string plural, string genitive)
        {
            return Decline(number, nominative, plural, genitive);
        }

        /// <summary>
        /// Returns the word declension depending on the given number.
        /// NOTE: This overload version is usefull for russian-like languages.
        /// </summary>
        /// <param name="number">The number from which the selected word depends.</param>
        /// <param name="nominative">Nominative (Именительный падеж слова). e.g. "день".</param>
        /// <param name="plural">Plural (Множественное число слова). e.g. "дней".</param>
        /// <param name="genitive">Genetive (Родительный падеж слова). e.g. "дня".</param>
        /// <returns>Returns the word declension depending on the given number.</returns>
        public static string Decline(this long number, string nominative, string plural, string genitive)
        {
            number = number%100;
            if (11 <= number && number <= 19) return plural;

            var lastDigit = number%10;

            if (lastDigit == 1) return nominative;
            if (2 <= lastDigit && lastDigit <= 4) return genitive;
            return plural;
        }

        /// <summary>
        /// Returns the word declension depending on the given number.
        /// NOTE: This overload version is usefull for russian-like languages.
        /// </summary>
        /// <param name="number">The number from which the selected word depends.</param>
        /// <param name="nominative">Nominative (Именительный падеж слова). e.g. "день".</param>
        /// <param name="plural">Plural (Множественное число слова). e.g. "дней".</param>
        /// <param name="genitive">Genetive (Родительный падеж слова). e.g. "дня".</param>
        /// <returns>Returns the word declension depending on the given number.</returns>
        public static string Decline(this int number, string nominative, string plural, string genitive) =>
            Decline((long)number, nominative, plural, genitive);

        /// <summary>
        /// Returns the word declension depending on the given number.
        /// NOTE: This overload version is usefull for english-like languages.
        /// </summary>
        /// <param name="singular">Singular. e.g. "day".</param>
        /// <param name="number">The number from which the selected word depends.</param>
        /// <param name="plural">Plural. e.g. "days".</param>
        /// <returns>Returns the word declension depending on the given number.</returns>
        public static string Decline(this string singular, long number, string plural) => Decline(number, singular, plural);

        /// <summary>
        /// Returns the word declension depending on the given number.
        /// NOTE: This overload version is usefull for english-like languages.
        /// </summary>
        /// <param name="number">The number from which the selected word depends.</param>
        /// <param name="singular">Singular. e.g. "day".</param>
        /// <param name="plural">Plural. e.g. "days".</param>
        /// <returns>Returns the word declension depending on the given number.</returns>
        public static string Decline(this long number, string singular, string plural) => number == 1 ? singular : plural;

        /// <summary>
        /// Returns the word declension depending on the given number.
        /// NOTE: This overload version is usefull for english-like languages.
        /// </summary>
        /// <param name="number">The number from which the selected word depends.</param>
        /// <param name="singular">Singular. e.g. "day".</param>
        /// <param name="plural">Plural. e.g. "days".</param>
        /// <returns>Returns the word declension depending on the given number.</returns>
        public static string Decline(this int number, string singular, string plural) => number == 1 ? singular : plural;
    }
}