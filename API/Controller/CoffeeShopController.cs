using API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CoffeeShopController : ControllerBase
    {
        private readonly ICoffeeShopService _service;

        public CoffeeShopController(ICoffeeShopService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var coffeeShops = await _service.List();

            return Ok(coffeeShops);
        }
    }
}
