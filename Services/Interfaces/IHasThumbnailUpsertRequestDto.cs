using NATS.Services.Interfaces;

namespace NATS.Services.Dtos;

public interface IHasThumbnailUpsertRequestDto : IRequestDto
{
    byte[] ThumbnailFile { get; set; }
    bool ThumbnailChanged { get; set; }
}