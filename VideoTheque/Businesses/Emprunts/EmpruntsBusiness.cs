using Mapster;
using VideoTheque.Businesses.Films;
using VideoTheque.Businesses.Hosts;
using VideoTheque.Core;
using VideoTheque.DTOs;
using VideoTheque.Repositories.AgeRating;
using VideoTheque.Repositories.Films;
using VideoTheque.Repositories.Genres;
using VideoTheque.Repositories.Hosts;
using VideoTheque.Repositories.Personnes;

namespace VideoTheque.Businesses.Emprunts
{
    public class EmpruntsBusiness : IEmpruntsBusiness 
    {
        private readonly IBluRaysRepository _bluRayDao;
        private readonly IPersonnesRepository _personnesRepository;
        private readonly IAgeRatingRepository _ageRatingRepository;
        private readonly IGenresRepository _genresRepository;
        private readonly IHostsRepository _hostsRepository;
        private readonly IBluRaysRepository _bluRaysRepository;
        
        public EmpruntsBusiness(IBluRaysRepository bluRayRepository, IPersonnesRepository personnesRepository,
            IAgeRatingRepository ageRatingRepository, IGenresRepository genresRepository, IHostsRepository hostsRepository, IBluRaysRepository bluRaysRepository)
        {
            _bluRayDao = bluRayRepository;
            _personnesRepository = personnesRepository;
            _ageRatingRepository = ageRatingRepository;
            _genresRepository = genresRepository;
            _hostsRepository = hostsRepository;
            _bluRaysRepository = bluRayRepository;
        }
        public async Task<List<FilmDto>> GetEmprunts()
        {
            var blurays = await _bluRayDao.GetBluRays();
            var films = new List<FilmDto>();

            foreach (var elts in blurays)
            {
                if(elts is {IsAvailable:true, IdOwner:null})
                {
                    var film = new FilmDto(elts);
                    film.FirstActor = _personnesRepository.GetPersonne(film.FirstActor.Id).Result;
                    film.Director = _personnesRepository.GetPersonne(film.Director.Id).Result;
                    film.FirstActor = _personnesRepository.GetPersonne(film.FirstActor.Id).Result;
                    film.Scenarist = _personnesRepository.GetPersonne(film.Scenarist.Id).Result;
                    film.AgeRating = _ageRatingRepository.GetAgeRating(film.AgeRating.Id).Result;
                    film.Genre = _genresRepository.GetGenre(film.Genre.Id).Result;
                    film.IsAvailable = elts.IsAvailable;
                    film.IdOwner = elts.IdOwner;
                    films.Add(film);
                }
            }
            return films;
        }

        public async Task<FilmDto> InsertEmprunt(int id)
        {
            var bluray = (await _bluRaysRepository.GetBluRay(id));
            var film = new FilmDto(bluray);
            film.FirstActor = _personnesRepository.GetPersonne(film.FirstActor.Id).Result;
            film.Director = _personnesRepository.GetPersonne(film.Director.Id).Result;
            film.FirstActor = _personnesRepository.GetPersonne(film.FirstActor.Id).Result;
            film.Scenarist = _personnesRepository.GetPersonne(film.Scenarist.Id).Result;
            film.AgeRating = _ageRatingRepository.GetAgeRating(film.AgeRating.Id).Result;
            film.Genre = _genresRepository.GetGenre(film.Genre.Id).Result;
            film.IsAvailable = false;
            return film;
        }

        public async Task<FilmDto> DeleteEmprunt(string name)
        {
            var blurays = await _bluRayDao.GetBluRays();
            var filmARetourner = new FilmDto();

            foreach (var elts in blurays)
            {
                if(elts.Title == name)
                {
                    var film = new FilmDto(elts);
                    film.IsAvailable = true;
                    film.IdOwner = null;
                    filmARetourner = film;
                }
            }

            return filmARetourner;
        }
    }
}