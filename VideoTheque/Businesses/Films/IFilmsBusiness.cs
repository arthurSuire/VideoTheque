using VideoTheque.DTOs;

namespace VideoTheque.Businesses.Films
{
    public interface IFilmsBusiness
    {
        Task<List<FilmDto>> GetFilms();

        Task<FilmDto> GetFilm(int id);

        FilmDto InsertFilm(FilmDto filmDto);

        void UpdateFilm(int id, FilmDto filmDto);

        void DeleteFilm(int id);
    }
}