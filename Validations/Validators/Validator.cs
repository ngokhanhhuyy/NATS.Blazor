using System.Text.RegularExpressions;
using NATS.Services.Interfaces;
using FluentValidation;
using ImageMagick;

namespace NATS.Validation.Validators;

public class Validator<TRequestDto> : AbstractValidator<TRequestDto>
        where TRequestDto : IRequestDto {
    public Validator()
    {
        ClassLevelCascadeMode = CascadeMode.Continue;
        RuleLevelCascadeMode = CascadeMode.Stop;
    }

    protected virtual bool EqualOrEarlierThanToday(DateTime value)
    {
        return value <= DateTime.UtcNow.Date;
    }

    protected virtual bool EqualOrEarlierThanToday(DateTime? value)
    {
        if (value.HasValue) {
            return EqualOrEarlierThanToday(value.Value);
        }
        
        return true;
    }

    protected virtual bool ValidPhoneNumber(string value)
    {
        if (value != null) {
            Regex regex = new Regex(@"[0-9]");
            return regex.Matches(value).Any();
        }
        
        return true;
    }

    protected virtual bool ValidIdCardNumber(string value)
    {
        if (value != null)
        {
            Regex regex = new Regex(@"[0-9]");
            return regex.Matches(value).Count > 0;
        }
        return true;
    }

    protected virtual bool IsEnumElementName<TEnum>(string name) where TEnum : Enum
    {
        return name != null && Enum.GetNames(typeof(TEnum)).ToList().Contains(name);
    }

    protected virtual bool IsValidImage(byte[] imageAsBytes)
    {
        try
        {
            MagickImage _ = new MagickImage(imageAsBytes);
            return true;
        }
        catch (MagickMissingDelegateErrorException)
        {
            return false;
        }
    }
}