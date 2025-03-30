using NATS.Services.Interfaces;
using NATS.Services.Extensions;

namespace NATS.Services.Dtos.RequestDtos;

public class SignInRequestDto : IRequestDto
{
    public string UserName { get; set; }
    public string Password { get; set; }

    public void TransformValues()
    {
        UserName = UserName.ToNullIfEmpty();
        Password = Password.ToNullIfEmpty();
    }
}