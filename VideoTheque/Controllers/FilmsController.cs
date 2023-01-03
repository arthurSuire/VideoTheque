using Mapster;
using Microsoft.AspNetCore.Mvc;
using VideoTheque.Businesses.Films;
using VideoTheque.DTOs;
using VideoTheque.ViewModels;

namespace VideoTheque.Controllers
{
    [ApiController]
    [Route("films")]
    public class FilmsController
    {
        private readonly IFilmsBusiness _filmsBusiness;
        protected readonly ILogger<FilmsController> _logger;

        public FilmsController(ILogger<FilmsController> logger, IFilmsBusiness filmsBusiness)
        {
            _logger = logger;
            _filmsBusiness = filmsBusiness;
        }

        [HttpGet]
        public async Task<List<FilmViewModel>> GetFilms() => (await _filmsBusiness.GetFilms()).Select(c =>
            new FilmViewModel
            {
                Id = c.Id,
                Duree = c.Duration,
                AgeRating = c.AgeRating.Name,
                Genre = c.Genre.Name,
                Support = c.Support.ToString(),
                Titre = c.Title.ToString(),
                PrincipalActor = c.FirstActor.FullName,
                RealFullName = c.Director.FullName,
                ScenarFullName = c.Scenarist.FullName
            }).ToList();

        [HttpGet("{id}")]
        public void GetFilm([FromRoute] int id)
        {
            TypeAdapterConfig<FilmDto, FilmViewModel>.NewConfig()
                .Map(filmView => filmView.Id, filmDto => filmDto.Id)
                .Map(filmView => filmView.RealFullName, filmDto => filmDto.Director)
                .Map(filmView => filmView.ScenarFullName, filmDto => filmDto.Scenarist)
                .Map(filmView => filmView.Duree, filmDto => filmDto.Duration)
                .Map(filmView => filmView.Support, filmDto => filmDto.Support)
                .Map(filmView => filmView.AgeRating, filmDto => filmDto.AgeRating)
                .Map(filmView => filmView.Genre, filmDto => filmDto.Genre)
                .Map(filmView => filmView.Titre, filmDto => filmDto.Title)
                .Map(filmView => filmView.PrincipalActor, filmDto => filmDto.FirstActor);
        }
    }
}