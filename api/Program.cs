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

//builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddDbContextFactory<ApplicationDbContext>();

builder.Services.AddAutoMapper(typeof(ApplicationMappingProfile));


builder.Services.AddScoped<DBQueryRunner<Player>, DBQueryRunner<Player>>();
builder.Services.AddScoped<DBCommandRunner<Player>, DBCommandRunner<Player>>();


builder.Services.AddScoped<IPlayersRepository, PlayersRepository>();
builder.Services.AddScoped<ITeamsRepository, TeamsRepository>();
builder.Services.AddScoped<MatchesRepository, MatchesRepository>();



builder.Services.AddScoped<IPlayersService, PlayersService>();
builder.Services.AddScoped<ITeamsService, TeamsService>();
builder.Services.AddScoped<MatchesService, MatchesService>();



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