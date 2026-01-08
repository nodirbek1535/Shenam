//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Shenam.API.Models.Foundation.Hosts;
using Shenam.API.Models.Foundation.Hosts.Exceptions;
using Shenam.API.Services.Foundations.Hosts;

namespace Shenam.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HostEntityController : RESTFulController
    {
        private readonly IHostEntityService hostEntityService;

        public HostEntityController(IHostEntityService hostEntityService)
        {
            this.hostEntityService = hostEntityService;
        }

        [HttpPost]
        public async ValueTask<ActionResult> PostHostEntityAsync(HostEntity hostEntity)
        {
            try
            {
                HostEntity postedHostEntity = await this.hostEntityService.AddHostEntityAsync(hostEntity);

                return Created(postedHostEntity);
            }
            catch (HostEntityValidationException hostEntityValidationException)
            {
                return BadRequest(hostEntityValidationException.InnerException);
            }
            catch (HostEntityDependencyValidationException hostEntityDependencyValidationException)
                when (hostEntityDependencyValidationException.InnerException is AlreadyExistsHostEntityException)
            {
                return Conflict(hostEntityDependencyValidationException.InnerException);
            }
            catch (HostEntityDependencyValidationException hostEntityDependencyValidationException)
            {
                return BadRequest(hostEntityDependencyValidationException.InnerException);
            }
            catch (HostEntityDependencyException hostEntityDependencyException)
            {
                return InternalServerError(hostEntityDependencyException.InnerException);
            }
            catch (HostEntityServiceException hostEntityServiceException)
            {
                return InternalServerError(hostEntityServiceException.InnerException);
            }
        }
    }
}
