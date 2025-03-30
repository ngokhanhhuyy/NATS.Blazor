using NATS.Services.Dtos.RequestDtos;
using NATS.Services.Localization;
using FluentValidation;

namespace NATS.Validation.Validators;

[JetBrains.Annotations.UsedImplicitly]
public class UserPasswordChangeValidator : Validator<UserPasswordChangeRequestDto>
{
    public UserPasswordChangeValidator()
    {
        RuleFor(dto => dto.CurrentPassword)
            .NotEmpty()
            .WithName(dto => DisplayNames.Get(nameof(dto.CurrentPassword)));
        RuleFor(dto => dto.NewPassword)
            .NotEmpty()
            .Length(8, 20)
            .WithName(dto => DisplayNames.Get(nameof(dto.NewPassword)));
        RuleFor(dto => dto.ConfirmationPassword)
            .NotEmpty()
            .Must((dto, confirmationPassword) => confirmationPassword == dto.NewPassword)
            .WithMessage(dto => ErrorMessages.MismatchedWith
                .Replace("{ComparisonPropertyName}", DisplayNames.Get(nameof(dto.NewPassword))))
            .WithName(dto => DisplayNames.Get(nameof(dto.ConfirmationPassword)));
    }
}