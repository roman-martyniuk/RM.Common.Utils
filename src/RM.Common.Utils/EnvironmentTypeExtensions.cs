namespace RM.Common.Utils
{
    /// <summary>
    /// Represents extension methods for <see cref="EnvironmentType"/>.
    /// </summary>
    public static class EnvironmentTypeExtensions
    {
        /// <summary>
        /// Checks whether specified <paramref name="environment"/> is <see cref="EnvironmentType.Dev"/>.
        /// </summary>
        public static bool IsDev(this EnvironmentType environment) => environment == EnvironmentType.Dev;

        /// <summary>
        /// Checks whether specified <paramref name="environment"/> is <see cref="EnvironmentType.QA"/>.
        /// </summary>
        public static bool IsQA(this EnvironmentType environment) => environment == EnvironmentType.QA;

        /// <summary>
        /// Checks whether specified <paramref name="environment"/> is <see cref="EnvironmentType.Dev"/> or <see cref="EnvironmentType.QA"/>.
        /// </summary>
        public static bool IsDevOrQA(this EnvironmentType environment) => environment == EnvironmentType.Dev || environment == EnvironmentType.QA;

        /// <summary>
        /// Checks whether specified <paramref name="environment"/> is <see cref="EnvironmentType.Production"/>.
        /// </summary>
        public static bool IsProduction(this EnvironmentType environment) => environment == EnvironmentType.Production;
    }
}