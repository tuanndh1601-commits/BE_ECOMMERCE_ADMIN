using ECOM.INFRASTRUCTURE.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ECOM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMenuService _menuService;
        public WeatherForecastController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _menuService.GetDataListAsync();
            return Ok(data);
        }
    }
}
