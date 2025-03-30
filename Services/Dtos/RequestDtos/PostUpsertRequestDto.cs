using NATS.Services.Extensions;

namespace NATS.Services.Dtos.RequestDtos;

public class PostUpsertRequestDto : IHasThumbnailUpsertRequestDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsPinned { get; set; }
    public bool IsPublished { get; set; }
    public bool ThumbnailChanged { get; set; }
    public byte[] ThumbnailFile { get; set; }

    public void TransformValues()
    {
        Title = Title.ToNullIfEmpty();
        Content = Content.ToNullIfEmpty();
    }
}