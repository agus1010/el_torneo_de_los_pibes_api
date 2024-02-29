using api.Data;
using api.Models.Entities;
using api.Profiles;
using api.Repositories;
using api.Repositories.Interfaces;
using api.Services;
using api.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>();

builder.Services.AddAutoMapper(typeof(PlayerMappingProfile));

builder.Services.AddScoped<IBaseCRUDRepository<Player>, PlayersRepository>();
builder.Services.AddScoped<PlayersRepository, PlayersRepository>();
builder.Services.AddScoped<TeamsRepository, TeamsRepository>();

builder.Services.AddScoped<IPlayersService, PlayersService>();
builder.Services.AddScoped<ITeamsService, TeamsService>();



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