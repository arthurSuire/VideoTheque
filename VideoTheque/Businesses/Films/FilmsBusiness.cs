using VideoTheque.DTOs;
using VideoTheque.Repositories.AgeRating;
using VideoTheque.Repositories.Films;
using VideoTheque.Repositories.Personnes;

namespace VideoTheque.Businesses.Films
{
    public class FilmsBusiness : IFilmsBusiness
    {
        private readonly IBluRaysRepository _bluRayDao;
        private readonly IPersonnesRepository _personnesRepository;
        private readonly IAgeRatingRepository _ageRatingRepository;
        
        public FilmsBusiness(IBluRaysRepository bluRayRepository, IPersonnesRepository personnesRepository, IAgeRatingRepository ageRatingRepository)
        {
            _bluRayDao = bluRayRepository;
            _personnesRepository = personnesRepository;
            _ageRatingRepository = ageRatingRepository;

        }
        
        public async Task<List<FilmDto>> GetFilms()
        {
            
            var blurays = await _bluRayDao.GetBluRays();
            var films = new List<FilmDto>();

            foreach(var elts in blurays)
            {
                var film = new FilmDto(elts);
                film.FirstActor = _personnesRepository.GetPersonne(film.FirstActor.Id).Result;
                film.Director = _personnesRepository.GetPersonne(film.Director.Id).Result;
                film.FirstActor = _personnesRepository.GetPersonne(film.FirstActor.Id).Result;
                film.Scenarist = _personnesRepository.GetPersonne(film.Scenarist.Id).Result;
                film.AgeRating = _ageRatingRepository.GetAgeRating(film.AgeRating.Id).Result;

            }
            return films;
        }

        public FilmDto GetFilm(int id)
        {
            throw new NotImplementedException();
        }

        public FilmDto InsertFilm(FilmDto filmDto)
        {
            throw new NotImplementedException();
        }

        public void UpdateFilm(int id, FilmDto filmDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteFilm(int id)
        {
            throw new NotImplementedException();
        }
    }
}