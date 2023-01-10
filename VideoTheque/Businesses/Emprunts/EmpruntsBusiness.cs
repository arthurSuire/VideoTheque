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
                if (elts.IsAvailable == true)
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
            var film = (await _bluRaysRepository.GetBluRay(id));
            var bluray = film.Adapt<FilmDto>();
            if (_bluRayDao.InsertBluRay(film).IsFaulted)
            {
                throw new InternalErrorException($"Erreur lors de l'insertion du film avec comme titre {film.Title}");
            }

            return bluray;
        }

        public void DeleteEmprunt(string name)
        {
            if (_bluRayDao.DeleteBluRayName(name).IsFaulted)
            {
                throw new InternalErrorException($"Erreur lors de la suppression du film d'identifiant {name}");
            }
        }
    }
}