﻿using NATS.Services.Interfaces;
using NATS.Services.Extensions;

namespace NATS.Services.Dtos.RequestDtos;

public class UserPasswordChangeRequestDto : IRequestDto
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmationPassword { get; set; }

    public void TransformValues()
    {
        CurrentPassword = CurrentPassword?.ToNullIfEmpty();
        NewPassword = NewPassword?.ToNullIfEmpty();
        ConfirmationPassword = ConfirmationPassword?.ToNullIfEmpty();
    }
}