using NATS.Services.Dtos.ResponseDtos;
using NATS.Services.Interfaces;

namespace NATS.Middlewares;

public class UnderMaintainanceMiddleware
{
    private readonly RequestDelegate _next;

    public UnderMaintainanceMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IGeneralSettingsService service)
    {
        bool isAuthenticated = context.User.Identity!.IsAuthenticated;
        bool isLoginRequest = context.Request.Path.StartsWithSegments("/Login");
        bool isUnderMaintainanceRequest = context.Request.Path.StartsWithSegments("/bao-tri");
        GeneralSettingsResponseDto generalSettingsResponseDto;
        generalSettingsResponseDto = await service.GetAsync();
        bool isUnderMaintainance = generalSettingsResponseDto.UnderMaintainance;

        if (!isUnderMaintainance && isUnderMaintainanceRequest)
        {
            context.Response.Redirect("/");
        }

        if (!isAuthenticated && !isLoginRequest && isUnderMaintainance)
        {
            if (!isUnderMaintainanceRequest)
            {
                context.Response.Redirect("/bao-tri");
            }
        }

        await _next(context);
    }
}