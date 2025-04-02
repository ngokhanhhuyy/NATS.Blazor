using NATS.Services.Dtos.RequestDtos;

namespace NATS.Models;

public class SignInModel
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public SignInRequestDto ToRequestDto()
    {
        SignInRequestDto requestDto = new SignInRequestDto
        {
            UserName = UserName,
            Password = Password
        };

        requestDto.TransformValues();

        return requestDto;
    }
}