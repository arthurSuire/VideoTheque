using VideoTheque.Businesses.Hosts;

namespace VideoTheque.Controllers
{
    using Mapster;
    using Microsoft.AspNetCore.Mvc;
    using VideoTheque.DTOs;
    using VideoTheque.ViewModels;

    [ApiController]
    [Route("hosts")]
    public class HostsController : ControllerBase
    {
        private readonly IHostsBusiness _hostBusiness;
        protected readonly ILogger<HostsController> _logger;

        public HostsController(ILogger<HostsController> logger, IHostsBusiness hostBusiness)
        {
            _logger = logger;
            _hostBusiness = hostBusiness;
        }

        [HttpGet]
        public async Task<List<HostViewModel>> GetHosts() => (await _hostBusiness.GetHosts()).Adapt<List<HostViewModel>>();

        [HttpGet("{id}")]
        public async Task<HostViewModel> GetHost([FromRoute] int id) => _hostBusiness.GetHost(id).Adapt<HostViewModel>();

        [HttpPost]
        public async Task<IResult> InsertHost([FromBody] HostViewModel hostVM)
        {
            var created = _hostBusiness.InsertHost(hostVM.Adapt<HostDto>());
            return Results.Created($"/hosts/{created.Id}", created);
        }

        [HttpPut("{id}")]
        public async Task<IResult> UpdateHost([FromRoute] int id, [FromBody] HostViewModel hostVM)
        {
            _hostBusiness.UpdateHost(id, hostVM.Adapt<HostDto>());
            return Results.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteHost([FromRoute] int id)
        {
            _hostBusiness.DeleteHost(id);
            return Results.Ok();
        }
    }
}