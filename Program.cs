/*
    Date 2 Mar 2026

    ESL Pro League S23 Stage 1, Stage 2,
        Legacy vs AURORA,
    Game of Thrones (3 run) S1 at S3,
*/

using CSTV_v1.Data;
using CSTV_v1.Services.Player;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
