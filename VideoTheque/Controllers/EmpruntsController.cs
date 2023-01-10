using Mapster;
using Microsoft.AspNetCore.Mvc;
using VideoTheque.Businesses.Emprunts;
using VideoTheque.DTOs;
using VideoTheque.ViewModels;

namespace VideoTheque.Controllers
{
    [ApiController]
    [Route("emprunts")]
    public class EmpruntsController
    {
        private readonly IEmpruntsBusiness _empruntsBusiness;
        protected readonly ILogger<EmpruntsController> _logger;

        public EmpruntsController(ILogger<EmpruntsController> logger, IEmpruntsBusiness empruntsBusiness)
        {
            _logger = logger;
            _empruntsBusiness = empruntsBusiness;
        }

        [HttpGet]
        public async Task<List<FilmViewModel>> GetEmprunts() => (await _empruntsBusiness.GetEmprunts()).Adapt<List<FilmViewModel>>();

        [HttpPost]
        public async Task<IResult> InsertEmprunt([FromBody] FilmViewModel filmVM)
        {
            var created = _empruntsBusiness.InsertEmprunt(filmVM.Id);
            return Results.Created($"/emprunts/{created.Id}", created);
        }

        [HttpDelete("{name}")]
        public async Task<IResult> DeleteEmprunt([FromRoute] string name)
        {
            _empruntsBusiness.DeleteEmprunt(name);
            return Results.Ok();
        }
    }
}