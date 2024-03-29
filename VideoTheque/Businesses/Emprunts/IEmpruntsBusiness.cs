using Microsoft.AspNetCore.Mvc;
using VideoTheque.DTOs;

namespace VideoTheque.Businesses.Emprunts
{
    public interface IEmpruntsBusiness
    {
        Task<List<FilmDto>> GetEmprunts();

        Task<FilmDto> InsertEmprunt(int id);

        void DeleteEmprunt(string name); 
    }                                                                                                                       
}