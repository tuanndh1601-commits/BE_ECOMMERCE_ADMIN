using ECOM.APPLICATION.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECOM.API.Controllers
{
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpPost]
        [Route("GetList")]
        public async Task<IActionResult> GetMenuGroupAsync()
        {
            var data = await _menuService.GetMenuGroupAsync();
            return Ok(data);
        }
    }
}
