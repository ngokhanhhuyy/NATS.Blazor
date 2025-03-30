using NATS.Services.Dtos.RequestDtos;
using NATS.Services.Localization;
using FluentValidation;

namespace NATS.Validation.Validators;

[JetBrains.Annotations.UsedImplicitly]
public class CatalogItemUpsertPhotoValidator : Validator<CatalogItemUpsertPhotoRequestDto>
{
    public CatalogItemUpsertPhotoValidator()
    {
        RuleSet("Create", () =>
        {
            RuleFor(dto => dto.File)
                .NotEmpty()
                .Must(IsValidImage)
                .WithMessage(ErrorMessages.Invalid)
                .WithName(DisplayNames.PhotoFile);
        });
        
        RuleSet("Update", () =>
        {
            RuleFor(dto => dto.File)
                .Must(IsValidImage)
                .WithMessage(ErrorMessages.Invalid)
                .When(dto => dto.File != null)
                .WithName(DisplayNames.PhotoFile);
        });
    }
}
