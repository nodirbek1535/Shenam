//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Shenam.API.Models.Foundation.Guests.Exceptions;
using Shenam.API.Models.Foundation.Hosts;
using Shenam.API.Models.Foundation.Hosts.Exceptions;
using Shenam.API.Services.Foundations.Hosts;
using System;
using System.Linq;
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

        [HttpGet]
        public ActionResult<IQueryable<HostEntity>> GetAllHostEntities()
        {
            try
            {
                IQueryable<HostEntity> hostEntitys =
                    this.hostEntityService.RetrieveAllHostEntities();

                return Ok(hostEntitys);
            }
            catch (HostEntityDependencyException hostEntityDependencyException)
            {
                return InternalServerError(
                    hostEntityDependencyException.InnerException);
            }
            catch (GuestServiceException guestServiceException)
            {
                return InternalServerError(
                    guestServiceException.InnerException);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<HostEntity>> PutHostEntityAsync(HostEntity hostEntity)
        {
            try
            {
                HostEntity modifiedHostEntity =
                    await this.hostEntityService.ModifyHostEntityAsync(hostEntity);

                return Ok(modifiedHostEntity);
            }
            catch (HostEntityValidationException hostEntityValidationException)
            {
                return BadRequest(hostEntityValidationException.InnerException);
            }
            catch (HostEntityDependencyValidationException hostEntityDependencyValidationException)
                when (hostEntityDependencyValidationException.InnerException is LockedHostEntityException)
            {
                return Locked(hostEntityDependencyValidationException.InnerException);
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

        [HttpDelete("{hostEntityId}")]
        public async ValueTask<ActionResult<HostEntity>> DeleteHostEntityByIdAsync(Guid hostEntityId)
        {
            try
            {
                HostEntity deletedHostEntity =
                    await this.hostEntityService.RemoveHostEntityByIdAsync(hostEntityId);

                return Ok(deletedHostEntity);
            }
            catch (HostEntityValidationException hostEntityValidationException)
            {
                return BadRequest(hostEntityValidationException.InnerException);
            }
            catch (HostEntityDependencyValidationException hostEntityDependencyValidationException)
                when (hostEntityDependencyValidationException.InnerException is LockedHostEntityException)
            {
                return Locked(hostEntityDependencyValidationException.InnerException);
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