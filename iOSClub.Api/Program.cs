using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using iOSClub.Api.Controllers;
using iOSClub.Share.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.WebEncoders;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddAuthentication(options => { options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false, //是否验证Issuer
            ValidateAudience = false, //是否验证Audience
            ValidateIssuerSigningKey = true, //是否验证SecurityKey
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!)), //SecurityKey
            ValidateLifetime = true, //是否验证失效时间
            ClockSkew = TimeSpan.FromSeconds(30), //过期时间容错值，解决服务器端时间不同步问题（秒）
            RequireExpirationTime = true,
        };
    });

builder.Services.AddSingleton(new JwtHelper(configuration));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<TokenActionFilter>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContextFactory<SignContext>(opt =>
        opt.UseSqlite(configuration.GetConnectionString("SQLite")));
}
else if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContextFactory<SignContext>(opt =>
        opt.UseMySQL(configuration.GetConnectionString("MySQL")!));
}

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy => { policy.WithOrigins("https://qm.qq.com"); });
});
builder.Services.Configure<WebEncoderOptions>(options =>
    options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SignContext>();
    try
    {
        context.Database.Migrate();
        context.Database.EnsureCreated();
    }
    catch
    {
        var databaseCreator = (RelationalDatabaseCreator)context.Database.GetService<IDatabaseCreator>();
        databaseCreator.CreateTables();
        context.Database.Migrate();
    }
    finally
    {
        if (!context.Staffs.Any())
        {
            var model = new PermissionsModel
            {
                UserId = "1906020412",
                Identity = "Founder",
                Name = "韩晨超"
            };
            context.Staffs.Add(model);
        }
    }

    context.SaveChanges();
    context.Dispose();
}


app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors();
app.MapControllers();

app.Run();