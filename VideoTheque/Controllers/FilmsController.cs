using System.Text.Json;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using VideoTheque.Businesses.Films;
using VideoTheque.Businesses.Hosts;
using VideoTheque.Core;
using VideoTheque.DTOs;
using VideoTheque.ViewModels;

namespace VideoTheque.Controllers
{
    [ApiController]
    [Route("films")]
    public class FilmsController
    {
        private readonly IFilmsBusiness _filmsBusiness;
        private readonly IHostsBusiness _hostsBusiness;
        protected readonly ILogger<FilmsController> _logger;
        private HttpClient client = new HttpClient();

        public FilmsController(ILogger<FilmsController> logger, IFilmsBusiness filmsBusiness, IHostsBusiness hostsBusiness)
        {
            _logger = logger;
            _filmsBusiness = filmsBusiness;
            _hostsBusiness = hostsBusiness;
        }

        [HttpGet]
        public async Task<List<FilmDto>> GetFilms([FromQuery] int? partenaire) 
        { 
            if(partenaire == null)
            {
                return (await _filmsBusiness.GetFilms()).Adapt<List<FilmDto>>();
            }
            else 
            {
                HostDto host = _hostsBusiness.GetHost(partenaire.Value);
                string url = host.Url + "/emprunts";
                try
                {
                    using HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    List<FilmDto>? jsonToFilmDto = JsonSerializer.Deserialize<List<FilmDto>>(responseBody);
                    return jsonToFilmDto;
                }
                catch (HttpRequestException e)
                {
                    throw new InternalErrorException(e.Message);
                } 
            }
        }

        [HttpGet("{id}")]
        public async Task<FilmDto> GetFilm([FromRoute] int id, [FromQuery] int? partenaire){ 
            if(partenaire == null)
            {
                return (await _filmsBusiness.GetFilm(id)).Adapt<FilmDto>();
            }
            else 
            {
                HostDto host = _hostsBusiness.GetHost(partenaire.Value);
                string url = host.Url + "/emprunts/"+id;
                try
                {
                    using HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    FilmDto jsonToFilmDto = JsonSerializer.Deserialize<FilmDto>(responseBody);
                    await _filmsBusiness.InsertFilm(jsonToFilmDto);
                    return jsonToFilmDto;
                }
                catch (HttpRequestException e)
                {
                    throw new InternalErrorException(e.Message);
                } 
            }
        }
        
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
        public async Task DeleteFilm([FromRoute] int id)
        {
            _filmsBusiness.DeleteFilm(id);
        }
    }
}