//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Shenam.API.Models.Foundation.Hosts;
using Shenam.API.Models.Foundation.Hosts.Exceptions;
using Shenam.API.Services.Foundations.Hosts;
using System;
using System.Threading.Tasks;

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

        [HttpGet("{hostEntityId}")]
        public async ValueTask<ActionResult<HostEntity>> GetHostEntityByIdAsync(Guid hostEntityId)
        {
            try
            {
                HostEntity hostEntity =
                    await this.hostEntityService.RetrieveHostEntityByIdAsync(hostEntityId);

                return Ok(hostEntity);
            }
            catch (HostEntityValidationException hostEntityValidationException)
            {
                return BadRequest(hostEntityValidationException.InnerException);
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
