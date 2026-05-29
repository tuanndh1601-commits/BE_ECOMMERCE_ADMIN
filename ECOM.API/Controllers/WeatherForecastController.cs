using ECOM.INFRASTRUCTURE.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ECOM.API.Controllers
{
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMenuService _menuService;
        public WeatherForecastController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<IActionResult> Get()
        {
            var data = await _menuService.GetDataListAsync();
            return Ok(data);
        }
    }
}
