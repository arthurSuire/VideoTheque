using VideoTheque.DTOs;

namespace VideoTheque.Businesses.Films
{
    public interface IFilmsBusiness
    {
        Task<List<FilmDto>> GetFilms();

        FilmDto GetFilm(int id);

        FilmDto insertFilm(FilmDto filmDto);

        void UpdateFilm(int id, FilmDto filmDto);

        void DeleteFilm(int id);
    }
}