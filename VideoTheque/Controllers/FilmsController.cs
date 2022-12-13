using Mapster;
using Microsoft.AspNetCore.Mvc;
using VideoTheque.Businesses.Films;
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
    }
}