//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Shenam.API.Models.Foundation.Homes;
using Shenam.API.Models.Foundation.Homes.Exceptions;
using Shenam.API.Services.Foundations.Homes;
using System;
using System.Threading.Tasks;

namespace Shenam.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : RESTFulController
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Home>> PostHomeAsync(Home home)
        {
            try
            {
                Home postedHome = await this.homeService.AddHomeAsync(home);

                return Created(postedHome);
            }
            catch (HomeValidationException homeValidationException)
            {
                return BadRequest(homeValidationException.InnerException);
            }
            catch (HomeDependencyValidationException homeDependencyValidationException)
                when (homeDependencyValidationException.InnerException is AlreadyExistsHomeException)
            {
                return Conflict(homeDependencyValidationException.InnerException);
            }
            catch (HomeDependencyException homeDependencyException)
            {
                return InternalServerError(homeDependencyException.InnerException);
            }
            catch (HomeServiceException homeServiceException)
            {
                return InternalServerError(homeServiceException.InnerException);
            }
        }

        [HttpGet("{homeId}")]
        public async ValueTask<ActionResult<Home>> GetHomeByIdAsync(Guid homeId)
        {
            try
            {
                Home home =
                    await this.homeService.RetrieveHomeByIdAsync(homeId);

                return Ok(home);
            }
            catch (HomeValidationException homeValidationException)
            {
                return BadRequest(homeValidationException.InnerException);
            }
            catch (HomeDependencyException homeDependencyException)
            {
                return InternalServerError(homeDependencyException.InnerException);
            }
            catch (HomeServiceException homeServiceException)
            {
                return InternalServerError(homeServiceException.InnerException);
            }
        }
    }
}
