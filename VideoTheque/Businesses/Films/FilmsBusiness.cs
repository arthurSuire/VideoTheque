using System.Text.Json;
using Mapster;
using VideoTheque.Businesses.Hosts;
using VideoTheque.Core;
using VideoTheque.DTOs;
using VideoTheque.Repositories.AgeRating;
using VideoTheque.Repositories.Films;
using VideoTheque.Repositories.Genres;
using VideoTheque.Repositories.Personnes;

namespace VideoTheque.Businesses.Films
{
    public class FilmsBusiness : IFilmsBusiness
    {
        private readonly IBluRaysRepository _bluRayDao;
        private readonly IPersonnesRepository _personnesRepository;
        private readonly IAgeRatingRepository _ageRatingRepository;
        private readonly IGenresRepository _genresRepository;
        private readonly IHostsBusiness _hostsBusiness;
        private HttpClient client = new HttpClient();

        public FilmsBusiness(IBluRaysRepository bluRayRepository, IPersonnesRepository personnesRepository,
            IAgeRatingRepository ageRatingRepository, IGenresRepository genresRepository, IHostsBusiness hostsBusiness)
        {
            _bluRayDao = bluRayRepository;
            _personnesRepository = personnesRepository;
            _ageRatingRepository = ageRatingRepository;
            _genresRepository = genresRepository;
            _hostsBusiness = hostsBusiness;
        }

        public async Task<List<FilmDto>> GetFilms()
        {
            var blurays = await _bluRayDao.GetBluRays();
            var films = new List<FilmDto>();

            foreach (var elts in blurays)
            {
                var film = new FilmDto(elts);
                film.FirstActor = _personnesRepository.GetPersonne(film.FirstActor.Id).Result;
                film.Director = _personnesRepository.GetPersonne(film.Director.Id).Result;
                film.FirstActor = _personnesRepository.GetPersonne(film.FirstActor.Id).Result;
                film.Scenarist = _personnesRepository.GetPersonne(film.Scenarist.Id).Result;
                film.AgeRating = _ageRatingRepository.GetAgeRating(film.AgeRating.Id).Result;
                film.Genre = _genresRepository.GetGenre(film.Genre.Id).Result;
                films.Add(film);
            }

            return films;
        }

        public async Task<FilmDto> GetFilm(int id)
        {
            var bluray = await _bluRayDao.GetBluRay(id);
            var film = new FilmDto(bluray);
            film.FirstActor = _personnesRepository.GetPersonne(film.FirstActor.Id).Result;
            film.Director = _personnesRepository.GetPersonne(film.Director.Id).Result;
            film.FirstActor = _personnesRepository.GetPersonne(film.FirstActor.Id).Result;
            film.Scenarist = _personnesRepository.GetPersonne(film.Scenarist.Id).Result;
            film.AgeRating = _ageRatingRepository.GetAgeRating(film.AgeRating.Id).Result;
            film.Genre = _genresRepository.GetGenre(film.Genre.Id).Result;
            return film;
        }

        public async Task<FilmDto> InsertFilm(FilmDto filmDto)
        {
            var personnes = (await _personnesRepository.GetPersonnes());
            var genres = (await _genresRepository.GetGenres());
            var ageRatings = (await _ageRatingRepository.GetAgeRating());
            var bluray = filmDto.Adapt<BluRayDto>();
            bluray.IdDirector = personnes.First(p => p.FirstName == filmDto.Director.FirstName && p.LastName == filmDto.Director.LastName).Id;
            bluray.IdScenarist = personnes.First(p => p.FirstName == filmDto.Scenarist.FirstName && p.LastName == filmDto.Scenarist.LastName).Id;
            bluray.IdFirstActor = personnes.First(p => p.FirstName == filmDto.FirstActor.FirstName && p.LastName == filmDto.FirstActor.LastName).Id;
            bluray.IdGenre = genres.First(g => g.Name == filmDto.Genre.Name).Id;
            bluray.IdAgeRating = ageRatings.First(ag => ag.Name == filmDto.AgeRating.Name).Id;
            bluray.IsAvailable = filmDto.IsAvailable;
            if (_bluRayDao.InsertBluRay(bluray).IsFaulted)
            {
                throw new InternalErrorException($"Erreur lors de l'insertion du film avec comme titre {bluray.Title}");
            }
            return filmDto;
        }

        public async void UpdateFilm(int id, FilmDto filmDto)
        {
            var personnes = (await _personnesRepository.GetPersonnes());
            var genres = (await _genresRepository.GetGenres());
            var ageRatings = (await _ageRatingRepository.GetAgeRating());
            var bluray = filmDto.Adapt<BluRayDto>();
            bluray.Id = id;
            bluray.IdDirector = personnes.First(p => p.FirstName == filmDto.Director.FirstName && p.LastName == filmDto.Director.LastName).Id;
            bluray.IdScenarist = personnes.First(p => p.FirstName == filmDto.Scenarist.FirstName && p.LastName == filmDto.Scenarist.LastName).Id;
            bluray.IdFirstActor = personnes.First(p => p.FirstName == filmDto.FirstActor.FirstName && p.LastName == filmDto.FirstActor.LastName).Id;
            bluray.IdGenre = genres.First(g => g.Name == filmDto.Genre.Name).Id;
            bluray.IdAgeRating = ageRatings.First(ag => ag.Name == filmDto.AgeRating.Name).Id;
            if (_bluRayDao.UpdateBluRay(bluray).IsFaulted)
            {
                throw new InternalErrorException($"Erreur lors de la modification du film avec comme titre {bluray.Title}");
            }
        }

        public async void DeleteFilm(int id)
        {
            var bluray = await _bluRayDao.GetBluRay(id);
            var film = new FilmDto(bluray);
            if (bluray.IdOwner is null && bluray.IsAvailable == true)
            {
                if (_bluRayDao.DeleteBluRay(id).IsFaulted)
                {
                    throw new InternalErrorException($"Erreur lors de la suppression du film d'identifiant {id}");
                }
            }
            else
            {
                HostDto host = _hostsBusiness.GetHost(bluray.IdOwner.Value);
                string url = host.Url + "/emprunts/"+bluray.Title;
                try
                {
                    using HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    FilmDto jsonToFilmDto = JsonSerializer.Deserialize<FilmDto>(responseBody);
                    if (_bluRayDao.DeleteBluRay(id).IsFaulted)
                    {
                        throw new InternalErrorException($"Erreur lors de la suppression du film d'identifiant {id}");
                    }
                }
                catch (HttpRequestException e)
                {
                    throw new InternalErrorException(e.Message);
                } 
            }
        }
    }
}