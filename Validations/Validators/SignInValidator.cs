using NATS.Services.Dtos.RequestDtos;
using NATS.Services.Localization;
using FluentValidation;

namespace NATS.Validation.Validators;

[JetBrains.Annotations.UsedImplicitly]
public class SignInValidator : Validator<SignInRequestDto>
{
    public SignInValidator()
    {
        RuleFor(dto => dto.UserName)
            .NotNull()
            .NotEmpty()
            .WithName(DisplayNames.UserName);
        RuleFor(dto => dto.Password)
            .NotNull()
            .NotEmpty()
            .WithName(DisplayNames.Password);
    }
}