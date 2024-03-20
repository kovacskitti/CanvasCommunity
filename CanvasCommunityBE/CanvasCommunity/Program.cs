using CanvasCommunity.Context;
using CanvasCommunity.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

AddServices();

builder.Services.AddControllers();
AddDbContext();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void AddServices()
{
    builder.Services.AddHttpClient();
    builder.Services.AddScoped<IArtsyTokenManager,ArtsyTokenManager>();
}

void AddDbContext()
{
    
    builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
}