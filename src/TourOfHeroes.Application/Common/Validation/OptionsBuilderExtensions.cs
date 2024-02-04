using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace TourOfHeroes.Application.Common.Validation
{
    public static class OptionsBuilderExtensions
    {
        public static OptionsBuilder<TOptions> ValidateFluently<TOptions>(this OptionsBuilder<TOptions> optionsBuilder) where TOptions : class
        {
            optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>>(
                provider => new FluentValidationOptions<TOptions>(
                    optionsBuilder.Name, 
                    provider.GetRequiredService<IValidator<TOptions>>()));

            return optionsBuilder;
        }
    }

    internal class FluentValidationOptions<TOptions>(
        string? name, 
        IValidator<TOptions> validator) 
        : IValidateOptions<TOptions> where TOptions : class
    {
        private readonly IValidator<TOptions> _validator = validator;

        /// <summary>
        /// The options name.
        /// </summary>
        public string? Name { get; } = name;

        public ValidateOptionsResult Validate(string? name, TOptions options)
        {
            // Null name is used to configure all named options.
            if (Name != null && Name != name)
            {
                // Ignored if not validating this instance.
                return ValidateOptionsResult.Skip;
            }

            // Ensure options are provided to validate against
            ArgumentNullException.ThrowIfNull(options);

            var validationResult = _validator.Validate(options);

            if (validationResult.IsValid)
            {
                return ValidateOptionsResult.Success;
            }

            var errors = validationResult.Errors.Select(e =>
            $"Option validation failed for '{e.PropertyName}' with '{e.ErrorMessage}'.");

            return ValidateOptionsResult.Fail(errors);
        }
    }
}
