using NATS.Services.Dtos.RequestDtos;
using NATS.Services.Localization;
using FluentValidation;

namespace NATS.Validation.Validators;

[JetBrains.Annotations.UsedImplicitly]
public class GeneralSettingsUpdateValidator : Validator<GeneralSettingsUpdateRequestDto>
{
    public GeneralSettingsUpdateValidator()
    {
        RuleFor(dto => dto.ApplicationName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100)
            .WithName(dto => DisplayNames.Get(nameof(dto.ApplicationName)));
        RuleFor(dto => dto.ApplicationShortName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100)
            .WithName(dto => DisplayNames.Get(nameof(dto.ApplicationName)));
        RuleFor(dto => dto.FavIconFile)
            .Must(IsValidImage).WithMessage(ErrorMessages.Invalid)
            .When(dto => dto.FavIconFile != null)
            .WithName(dto => DisplayNames.Get(nameof(dto.ApplicationShortName)));
    }
}
