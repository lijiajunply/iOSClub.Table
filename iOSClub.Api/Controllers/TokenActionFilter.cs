﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using iOSClub.Share.Data;
using Microsoft.AspNetCore.Mvc.Filters;

namespace iOSClub.Api.Controllers;

public class TokenActionFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var test = context.HttpContext.Request.Path;
        string? bearer = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        if (string.IsNullOrEmpty(bearer) || !bearer.Contains("Bearer")) return;
        string[] jwt = bearer.Split(' ');
        var tokenObj = new JwtSecurityToken(jwt[1]);

        var claimsIdentity = new ClaimsIdentity(tokenObj.Claims);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        context.HttpContext.User = claimsPrincipal;
    }
}

public static class TokenHelper
{
    public static MemberModel? GetUser(this ClaimsPrincipal claimsPrincipal)
    {
        try
        {
            var claimId = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid);
            var claimName = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            var claimRole = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (claimId.IsNull() || claimName.IsNull() || claimRole.IsNull())
            {
                return null;
            }

            return new MemberModel()
            {
                UserName = claimName!.Value,
                UserId = claimId!.Value,
                Identity = claimRole!.Value
            };
        }
        catch
        {
            return null;
        }
    }

    private static bool IsNull(this Claim? claim)
        => claim == null || !string.IsNullOrEmpty(claim.Value);
}