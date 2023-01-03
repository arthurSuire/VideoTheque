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
        
        public FilmsBusiness(IBluRaysRepository bluRayRepository, IPersonnesRepository personnesRepository,
            IAgeRatingRepository ageRatingRepository, IGenresRepository genresRepository)
        {
            _bluRayDao = bluRayRepository;
            _personnesRepository = personnesRepository;
            _ageRatingRepository = ageRatingRepository;
            _genresRepository = genresRepository;
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
                film.Genre = _genresRepository.GetGenre(film.Genre.Id).Result;
                films.Add(film);
            }
            return films;
        }

        public async Task<FilmDto> GetFilm(int id)
        {
            var bluray = await _bluRayDao.GetBluRay(id);
            var film = new FilmDto(bluray);
            Console.WriteLine("Je passe dans le bussinnes");
            film.FirstActor = _personnesRepository.GetPersonne(film.FirstActor.Id).Result;
            film.Director = _personnesRepository.GetPersonne(film.Director.Id).Result;
            film.FirstActor = _personnesRepository.GetPersonne(film.FirstActor.Id).Result;
            film.Scenarist = _personnesRepository.GetPersonne(film.Scenarist.Id).Result;
            film.AgeRating = _ageRatingRepository.GetAgeRating(film.AgeRating.Id).Result;
            film.Genre = _genresRepository.GetGenre(film.Genre.Id).Result;
            return film;
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