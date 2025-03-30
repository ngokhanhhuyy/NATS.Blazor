using NATS.Services.Entities;

namespace NATS.Services.Dtos.ResponseDtos;

public class CatalogItemDetailPhotoResponseDto
{
    public int Id { get; set; }
    public string Url { get; set; }

    public CatalogItemDetailPhotoResponseDto(CatalogItemPhoto photo)
    {
        Id = photo.Id;
        Url = photo.Url;
    }
}