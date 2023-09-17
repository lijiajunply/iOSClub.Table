using iOSClub.Table.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAntDesign();
builder.Services.AddDbContextFactory<SignContext>(opt =>
    opt.UseSqlite($"Data Source={nameof(SignContext.Students)}.db"));
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy => { policy.WithOrigins("http://qm.qq.com"); });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();