using VideoTheque.DTOs;

namespace VideoTheque.Businesses.Films
{
    public interface IFilmsBusiness
    {
        Task<List<FilmDto>> GetFilms();

        Task<List<FilmDto>> GetFilmsPartenaire(int idPartenaire);

        Task<FilmDto> GetFilm(int id);
        
        Task<FilmDto> GetFilmPartenaire(int idPartenaire, int idFilmPartenaire);

        Task<FilmDto> InsertFilm(FilmDto filmDto);

        void UpdateFilm(int id, FilmDto filmDto);

        void DeleteFilm(int id);
    }
}