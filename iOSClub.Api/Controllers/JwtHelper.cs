﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using iOSClub.Share.Data;
using Microsoft.IdentityModel.Tokens;

namespace iOSClub.Api.Controllers;

public class JwtHelper
{
    private readonly IConfiguration _configuration;

    public JwtHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetMemberToken(PermissionsModel model)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, model.Name),
            new Claim(ClaimTypes.Role, model.Identity),
            new Claim(ClaimTypes.PrimarySid,model.UserId)
        };

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
        const string algorithm = SecurityAlgorithms.HmacSha256;
        var signingCredentials = new SigningCredentials(secretKey, algorithm);

        var securityToken = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.Now, //notBefore
            expires: DateTime.Now.AddSeconds(30), //expires
            signingCredentials: signingCredentials
        );
        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return token;
    }
    
    public string GetMemberToken(MemberModel model)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, model.UserName),
            new Claim(ClaimTypes.Role, model.Identity),
            new Claim(ClaimTypes.PrimarySid,model.UserId)
        };

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
        const string algorithm = SecurityAlgorithms.HmacSha256;
        var signingCredentials = new SigningCredentials(secretKey, algorithm);

        var securityToken = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.Now, //notBefore
            expires: DateTime.Now.AddSeconds(30), //expires
            signingCredentials: signingCredentials
        );
        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return token;
    }
}