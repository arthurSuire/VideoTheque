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
        public async Task<List<FilmViewModel>> GetFilms() =>
            (await _filmsBusiness.GetFilms()).Adapt<List<FilmViewModel>>();
        
        [HttpGet("{idPartenaire}")]
        public async Task<List<FilmViewModel>> GetFilmsPartenaire(int idPartenaire) =>
            (await _filmsBusiness.GetFilmsPartenaire(idPartenaire)).Adapt<List<FilmViewModel>>();

        [HttpGet("{id}")]
        public async Task<FilmViewModel> GetFilm([FromRoute] int id) => 
            (await _filmsBusiness.GetFilm(id)).Adapt<FilmViewModel>();
        
        [HttpGet("{idPartenaire, idFilmPartenaire}")]
        public async Task<List<FilmViewModel>> GetFilmPartenaire(int idPartenaire, int idFilmPartenaire) =>
            (await _filmsBusiness.GetFilmPartenaire(idPartenaire, idFilmPartenaire)).Adapt<List<FilmViewModel>>();
        
        [HttpPost]
        public async Task<IResult> InsertFilm([FromBody] FilmViewModel filmVm)
        {
            var toInsert = filmVm.Adapt<FilmDto>();
            toInsert.FirstActor = new PersonneDto { FirstName = filmVm.FirstActor.Split(' ')[1], LastName = filmVm.FirstActor.Split(' ')[0] };
            toInsert.Scenarist = new PersonneDto { FirstName = filmVm.Scenarist.Split(' ')[1], LastName = filmVm.Scenarist.Split(' ')[0] };
            toInsert.Director = new PersonneDto { FirstName = filmVm.Director.Split(' ')[1], LastName = filmVm.Director.Split(' ')[0] };
            var created = _filmsBusiness.InsertFilm(toInsert);
            return Results.Created($"/films/{created.Id}", created);
        }
        
        [HttpPut("{id}")]
        public async Task<IResult> UpdateFilm([FromRoute] int id, [FromBody] FilmViewModel film)
        {
            var toInsert = film.Adapt<FilmDto>();
            toInsert.FirstActor = new PersonneDto { FirstName = film.FirstActor.Split(' ')[1], LastName = film.FirstActor.Split(' ')[0] };
            toInsert.Scenarist = new PersonneDto { FirstName = film.Scenarist.Split(' ')[1], LastName = film.Scenarist.Split(' ')[0] };
            toInsert.Director = new PersonneDto { FirstName = film.Director.Split(' ')[1], LastName = film.Director.Split(' ')[0] };
            _filmsBusiness.UpdateFilm(id, toInsert);
            return Results.NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IResult> DeleteFilm([FromRoute] int id)
        {
            _filmsBusiness.DeleteFilm(id);
            return Results.Ok();
        }
    }
}