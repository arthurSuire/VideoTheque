using Mapster;
using MapsterMapper;
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
                Duration = c.Duration,
                AgeRating = c.AgeRating.Name,
                Genre = c.Genre.Name,
                Support = c.Support.ToString(),
                Title = c.Title.ToString(),
                FirstActor = c.FirstActor.FullName,
                Director = c.Director.FullName,
                Scenarist = c.Scenarist.FullName
            }).ToList();

        [HttpGet("{id}")]
        public async Task<FilmViewModel> GetFilm([FromRoute] int id) => _filmsBusiness.GetFilm(id).Adapt<FilmViewModel>();
    }
}