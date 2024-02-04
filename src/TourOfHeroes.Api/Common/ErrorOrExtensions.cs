using ErrorOr;

namespace TourOfHeroes.Api.Common
{
    public static class ErrorOrExtensions
    {
        /// <summary>
        /// Creates a new <see cref="object"/> from a given <see cref="Error"/> where the property <see cref="Error.Metadata"/> removed if its value is null.
        /// </summary>
        /// <param name="error">An instance of the class <see cref="Error"/>.</param>
        /// <returns>An instance of the class <see cref="object"/>.</returns>
        public static object RemoveMetadataPropertyIfNull(this Error error)
        {
            dynamic result = error.Metadata is null
                ? new { error.Code, error.Description }
                : new { error.Code, error.Description, error.Metadata };

            return result;
        }
    }
}