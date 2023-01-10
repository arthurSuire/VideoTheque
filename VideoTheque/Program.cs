using Mapster;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.OpenApi.Models;
using VideoTheque.Businesses.AgeRating;
using VideoTheque.Businesses.Emprunts;
using VideoTheque.Businesses.Films;
using VideoTheque.Businesses.Genres;
using VideoTheque.Businesses.Hosts;
using VideoTheque.Businesses.Personnes;
using VideoTheque.Businesses.Support;
using VideoTheque.Config;
using VideoTheque.Context;
using VideoTheque.Core;
using VideoTheque.Repositories.AgeRating;
using VideoTheque.Repositories.Films;
using VideoTheque.Repositories.Genres;
using VideoTheque.Repositories.Hosts;
using VideoTheque.Repositories.Personnes;
using VideoTheque.Repositories.Supports;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Videotheque") ?? "Data Source=Videotheque.db";

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddSqlite<VideothequeDb>(connectionString);

builder.Services.AddScoped(typeof(IGenresRepository), typeof(GenresRepository));
builder.Services.AddScoped(typeof(IGenresBusiness), typeof(GenresBusiness));
builder.Services.AddScoped(typeof(IAgeRatingRepository), typeof(AgeRatingRepository));
builder.Services.AddScoped(typeof(IAgeRatingBusiness), typeof(AgeRatingBusiness));
builder.Services.AddScoped(typeof(IPersonnesRepository), typeof(PersonnesRepository));
builder.Services.AddScoped(typeof(IPersonnesBusiness), typeof(PersonnesBusiness));
builder.Services.AddScoped(typeof(ISupportsRepository), typeof(SupportsRepository));
builder.Services.AddScoped(typeof(ISupportBusiness), typeof(SupportBusiness));
builder.Services.AddScoped(typeof(IHostsRepository), typeof(HostsRepository));
builder.Services.AddScoped(typeof(IHostsBusiness), typeof(HostsBusiness));
builder.Services.AddScoped(typeof(IBluRaysRepository), typeof(BluRaysRepository));
builder.Services.AddScoped(typeof(IFilmsBusiness), typeof(FilmsBusiness));
builder.Services.AddScoped(typeof(IEmpruntsBusiness), typeof(EmpruntsBusiness));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "VidéoThèque API",
        Description = "Gestion de sa collection de film.",
        Version = "v1"
    });
});

builder.Services.AddCors(option => option
    .AddDefaultPolicy(builder => builder
        .SetIsOriginAllowed(_=> true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()));

builder.Services.configMapsterFilmRegister();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "VidéoThèque API V1");
    });
}

app.UseRouting();

//app.UseCors(builder => builder
//    .SetIsOriginAllowed(_ => true)
//    .AllowAnyMethod()
//    .AllowAnyHeader()
//    .AllowCredentials()
//    );

app.UseCors();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();
