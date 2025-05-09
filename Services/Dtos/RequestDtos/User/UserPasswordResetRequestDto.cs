using NATS.Services.Interfaces;
using NATS.Services.Extensions;

namespace NATS.Services.Dtos.RequestDtos;

public class UserPasswordResetRequestDto : IRequestDto
{
    public string NewPassword { get; set; }
    public string ConfirmationPassword { get; set; }

    public void TransformValues()
    {
        NewPassword = NewPassword?.ToNullIfEmpty();
        ConfirmationPassword = ConfirmationPassword?.ToNullIfEmpty();
    }
}