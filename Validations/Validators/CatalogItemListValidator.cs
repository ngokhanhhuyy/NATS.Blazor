using NATS.Services.Dtos.RequestDtos;
using NATS.Services.Localization;
using FluentValidation;

namespace NATS.Validation.Validators;

[JetBrains.Annotations.UsedImplicitly]
public class CatalogItemListValidator : Validator<CatalogItemListRequestDto>
{
    public CatalogItemListValidator()
    {
        RuleFor(dto => dto.Type)
            .IsInEnum()
            .WithName(DisplayNames.Type);
    }
}