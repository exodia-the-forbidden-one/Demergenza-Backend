using System.Text;
using Demergenza.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Demergenza.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpsRedirection(options => { options.HttpsPort = 5133; });
builder.WebHost.UseUrls("https://*:5133;http://*:5134");
if (!builder.Environment.IsDevelopment())
{
    builder.Configuration.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "configuration"))
        .AddJsonFile("appsettings.json");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Audience"],
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]!)),
        ClockSkew = TimeSpan.Zero
    };
});
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("default", corsPolicyBuilder =>
    {
        corsPolicyBuilder.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});
builder.Services.AddPersistenceServices();
builder.Services.AddApplicationServices();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    DemergenzaDbContext dbContext = scope.ServiceProvider.GetRequiredService<DemergenzaDbContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseCors("default");
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data-images")),
    RequestPath = "/data-images"
});

app.MapControllers();

app.Run();