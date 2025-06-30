using System.Globalization;
using FluentValidation;
using FluentValidation.Validators;

namespace Application.Commons.Helpers;

public static class ValidatorHelper
{
    public static bool BeAValidDate(string date)
    {
        return DateTime.TryParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }

    public static bool BeAValidTime(string date)
    {
        return DateTime.TryParseExact(date, "HHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }

    public static bool BeValidNowDate(string date)
    {
        return DateTime.Now.ToString("yyyyMMdd").Equals(date);
    }
    
    public static IRuleBuilderOptions<T, string> MatchAlphaNumericShort<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new RegularExpressionValidator<T>("^[a-zA-Z0-9_-]*$"));
    }
    public static IRuleBuilderOptions<T, string> MatchAlphaNumericExtended<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new RegularExpressionValidator<T>("^[a-zA-Z0-9,. _'&!/()*+:?-]*$"));
    }
    public static IRuleBuilderOptions<T, string> MatchNumericRule<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new RegularExpressionValidator<T>("^[0-9]*$"));
    }
}