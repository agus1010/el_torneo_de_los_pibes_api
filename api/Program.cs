using api.Data;
using api.Models.Entities;
using api.Models.Mappers;
using api.Repositories;
using api.Repositories.Interfaces;
using api.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>();

builder.Services.AddAutoMapper(typeof(PlayerMapper));
builder.Services.AddScoped<IBaseCRUDRepository<Player>, BaseCRUDRepository<Player>>();
builder.Services.AddScoped<PlayersService, PlayersService>();

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
