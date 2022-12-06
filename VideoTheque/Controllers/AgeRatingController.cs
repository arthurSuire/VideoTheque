namespace VideoTheque.Controllers
{
    using Mapster;
    using Microsoft.AspNetCore.Mvc;
    using VideoTheque.DTOs;
    using VideoTheque.ViewModels;
    using VideoTheque.Businesses.AgeRating;
    
    [ApiController]
    [Route("age-ratings")]
    public class AgeRatingController: ControllerBase
    {
        private readonly IAgeRatingBusiness _ageRatingBusiness;
        protected readonly ILogger<AgeRatingController> _logger;

        public AgeRatingController(ILogger<AgeRatingController> logger, IAgeRatingBusiness ageRatingBusiness)
        {
            _logger = logger;
            _ageRatingBusiness = ageRatingBusiness;
        }

        [HttpGet]
        public async Task<List<AgeRatingViewModel>> GetAgeRating() => (await _ageRatingBusiness.GetAgeRating()).Adapt<List<AgeRatingViewModel>>();

        [HttpGet("{id}")]
        public async Task<AgeRatingViewModel> GetAgeRating([FromRoute] int id) => _ageRatingBusiness.GetAgeRating(id).Adapt<AgeRatingViewModel>();

        [HttpPost]
        public async Task<IResult> InsentAgeRating([FromBody] AgeRatingViewModel ageRatingVM)
        {
            var created = _ageRatingBusiness.InsertAgeRating(ageRatingVM.Adapt<AgeRatingDto>());
            return Results.Created($"/ageRating/{created.Id}", created);
        }

        [HttpPut("{id}")]
        public async Task<IResult> UpdateAgeRating([FromRoute] int id, [FromBody] AgeRatingViewModel ageRatingVM)
        {
            _ageRatingBusiness.UpdateAgeRating(id, ageRatingVM.Adapt<AgeRatingDto>());
            return Results.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteAgeRating([FromRoute] int id)
        {
            _ageRatingBusiness.DeleteAgeRating(id);
            return Results.Ok();
        }
    }
}