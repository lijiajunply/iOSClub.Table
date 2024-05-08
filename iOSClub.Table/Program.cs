using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using iOSClub.Share;
using iOSClub.Share.Data;
using iOSClub.Table.Controllers;
using iOSClub.Table.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// 将服务添加到容器
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAntDesign();
builder.Services.AddControllers();

// 身份验证
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, Provider>();
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
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!)), //SecurityKey
            ValidateLifetime = true, //是否验证失效时间
            ClockSkew = TimeSpan.FromSeconds(30), //过期时间容错值，解决服务器端时间不同步问题（秒）
            RequireExpirationTime = true,
        };
    });

builder.Services.AddSingleton(new JwtHelper(builder.Configuration));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<TokenActionFilter>();

// 数据库
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContextFactory<SignContext>(opt =>
        opt.UseSqlite("Data Source=Data.db",
            o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
}
else if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContextFactory<SignContext>(opt =>
        opt.UseNpgsql(Environment.GetEnvironmentVariable("SQL",EnvironmentVariableTarget.Process),
            o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
}

// 跨域
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy => { policy.WithOrigins("https://qm.qq.com"); });
});
builder.Services.Configure<WebEncoderOptions>(options =>
    options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

// 初始化数据库
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SignContext>();
    if (!context.Staffs.Any())
    {
        var model = new StaffModel
        {
            UserId = "1906020412",
            Identity = "Founder",
            Name = "韩晨超"
        };
        context.Staffs.Add(model);
    }

    context.SaveChanges();
    context.Dispose();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapControllers();

app.Run();