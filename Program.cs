/*
    Date 2 Mar 2026

    ESL Pro League S23 Stage 1, Stage 2,
        Legacy vs AURORA, Legacy vs NAVI, FURIA vs PAIN, FURIA vs Astralis (sonho estranho)
    Game of Thrones (3 run) S1 at S7,
    fernandemiguels
        The Boys S1 at S3, 
*/

using CSLA.Data;
using CSLA.Services.Player;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
*/

// implementações
builder.Services.AddScoped<IPlayerInterface, PlayerService>();

builder.Services.AddDbContext<AppDbContext>( options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
