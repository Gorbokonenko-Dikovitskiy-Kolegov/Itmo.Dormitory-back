using System.Linq;
using FluentValidation;

namespace Itmo.Dormitory.Core
{
    public static class CustomValidations
    {
        public static IRuleBuilderOptions<T, TProperty> InRange<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder, TProperty[] options)
        {
            return ruleBuilder
                .Must(options.Contains)
                .WithMessage("'{PropertyName}' must be one of: " + $"{string.Join(", ", options)}");
        }
    }
}