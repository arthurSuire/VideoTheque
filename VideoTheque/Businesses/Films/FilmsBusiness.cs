using VideoTheque.DTOs;
using VideoTheque.Repositories.Films;

namespace VideoTheque.Businesses.Films
{
    public class FilmsBusiness : IFilmsBusiness
    {
        private readonly IBluRaysRepository _bluRayDao;

        public FilmsBusiness(IBluRaysRepository bluRayRepository)
        {
            _bluRayDao = bluRayRepository;
        }
        
        public async Task<List<FilmDto>> GetFilms()
        {
            
            var blurays = await _bluRayDao.GetBluRays();
            var films = new List<FilmDto>();

            foreach (var elts in blurays)
            {
                films.Add(new FilmDto(elts));
            }

            return films;
        }

        public FilmDto GetFilm(int id)
        {
            throw new NotImplementedException();
        }

        public FilmDto insertFilm(FilmDto filmDto)
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