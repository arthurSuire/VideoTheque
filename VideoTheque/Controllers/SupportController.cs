using Mapster;
using Microsoft.AspNetCore.Mvc;
using VideoTheque.Businesses.Hosts;
using VideoTheque.Businesses.Support;
using VideoTheque.ViewModels;

namespace VideoTheque.Controllers
{
    [ApiController]
    [Route("supports")]
    public class SupportController: ControllerBase
    {
        private readonly ISupportBusiness _supportBusiness;
        protected readonly ILogger<SupportController> _logger;

        public SupportController(ILogger<SupportController> logger, ISupportBusiness supportBusiness)
        {
            _logger = logger;
            _supportBusiness = supportBusiness;
        }

        [HttpGet]
        public List<SupportsViewModel> GetSupports() => _supportBusiness.GetSupports().Adapt<List<SupportsViewModel>>();

        [HttpGet("{id}")]
        public SupportsViewModel GetSupport([FromRoute] int id) => _supportBusiness.GetSupport(id).Adapt<SupportsViewModel>();
    }
}