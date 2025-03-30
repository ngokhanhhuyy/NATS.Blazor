using NATS.Services.Enums;
using NATS.Services.Interfaces;

namespace NATS.Services.Dtos.RequestDtos;

public class CatalogItemListRequestDto : IRequestDto
{
    public CatalogItemType? Type { get; set; }
}