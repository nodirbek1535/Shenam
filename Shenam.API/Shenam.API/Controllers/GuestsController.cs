//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shenam.API.Models.Foundation.Guests;
using Shenam.API.Services.Foundations.Guests;

namespace Shenam.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestsController : Controller
    {
        private readonly IGuestService guestService;
        public GuestsController(IGuestService guestService)
        {
            this.guestService = guestService;
        }

        [HttpPost]
        public async Task<IActionResult> PostGuest(Guest guest)
        {
            return Ok(this.guestService.AddGuestAsync(guest));
        }
    }
}
