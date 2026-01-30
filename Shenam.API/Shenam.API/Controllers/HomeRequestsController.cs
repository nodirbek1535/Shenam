//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Shenam.API.Models.Foundation.HomeRequests;
using Shenam.API.Models.Foundation.HomeRequests.Exceptions;
using Shenam.API.Services.Foundations.HomeRequests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shenam.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeRequestsController : RESTFulController
    {
        private readonly IHomeRequestService homeRequestService;

        public HomeRequestsController(IHomeRequestService homeRequestService)
        {
            this.homeRequestService = homeRequestService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<HomeRequest>> PostHomeRequestAsync(HomeRequest homeRequest)
        {
            try
            {
                HomeRequest postedHomeRequest = await this.homeRequestService.AddHomeRequestAsync(homeRequest);

                return Created(postedHomeRequest);
            }
            catch(HomeRequestValidationException homeRequestValidationException)
            {
                return BadRequest(homeRequestValidationException.InnerException);
            }
            catch (HomeRequestDependencyValidationException homeRequestDependencyValidationException)
                when (homeRequestDependencyValidationException.InnerException is AlreadyExistsHomeRequestException)
            {
                return Conflict(homeRequestDependencyValidationException.InnerException);
            }
            catch (HomeRequestDependencyException homeRequestDependencyException)
            {
                return InternalServerError(homeRequestDependencyException.InnerException);
            }
            catch (HomeRequestServiceException homeRequestServiceException)
            {
                return InternalServerError(homeRequestServiceException.InnerException);
            }
        }

        [HttpGet("{homeRequestId}")]
        public async ValueTask<ActionResult<HomeRequest>> GetHomeRequestByIdAsync(Guid homeRequestId)
        {
            try
            {
                HomeRequest homeRequest =
                    await this.homeRequestService.RetrieveHomeRequestByIdAsync(homeRequestId);

                return Ok(homeRequest);
            }
            catch (HomeRequestValidationException homeRequestValidationException)
            {
                return BadRequest(homeRequestValidationException.InnerException);
            }
            catch (HomeRequestDependencyException homeRequestDependencyException)
            {
                return InternalServerError(homeRequestDependencyException.InnerException);
            }
            catch (HomeRequestServiceException homeRequestServiceException)
            {
                return InternalServerError(homeRequestServiceException.InnerException);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<HomeRequest>> GetAllHomeRequests()
        {
            try
            {
                IQueryable<HomeRequest> homeRequests =
                    this.homeRequestService.RetrieveAllHomeRequests();

                return Ok(homeRequests);
            }
            catch (HomeRequestDependencyException homeRequestDependencyException)
            {
                return InternalServerError(
                    homeRequestDependencyException.InnerException);
            }
            catch (HomeRequestServiceException homeRequestServiceException)
            {
                return InternalServerError(
                    homeRequestServiceException.InnerException);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<HomeRequest>> PutHomeRequestAsync(HomeRequest homeRequest)
        {
            try
            {
                HomeRequest modifiedHomeRequest =
                    await this.homeRequestService.ModifyHomeRequestAsync(homeRequest);

                return Ok(modifiedHomeRequest);
            }
            catch (HomeRequestValidationException homeRequestValidationException)
            {
                return BadRequest(homeRequestValidationException.InnerException);
            }
            catch (HomeRequestDependencyValidationException homeRequestDependencyValidationException)
                when (homeRequestDependencyValidationException.InnerException is LockedHomeRequestException)
            {
                return Locked(homeRequestDependencyValidationException.InnerException);
            }
            catch (HomeRequestDependencyValidationException homeRequestDependencyValidationException)
            {
                return BadRequest(homeRequestDependencyValidationException.InnerException);
            }
            catch (HomeRequestDependencyException homeRequestDependencyException)
            {
                return InternalServerError(homeRequestDependencyException.InnerException);
            }
            catch (HomeRequestServiceException homeRequestServiceException)
            {
                return InternalServerError(homeRequestServiceException.InnerException);
            }
        }

        [HttpDelete("{homeRequestId}")]
        public async ValueTask<ActionResult<HomeRequest>> DeleteHomeRequestByIdAsync(Guid homeRequestId)
        {
            try
            {
                HomeRequest deletedHomeRequest =
                    await this.homeRequestService.RemoveHomeRequestByIdAsync(homeRequestId);

                return Ok(deletedHomeRequest);
            }
            catch (HomeRequestValidationException homeRequestValidationException)
            {
                return BadRequest(homeRequestValidationException.InnerException);
            }
            catch (HomeRequestDependencyValidationException homeRequestDependencyValidationException)
                when (homeRequestDependencyValidationException.InnerException is LockedHomeRequestException)
            {
                return Locked(homeRequestDependencyValidationException.InnerException);
            }
            catch (HomeRequestDependencyValidationException homeRequestDependencyValidationException)
            {
                return BadRequest(homeRequestDependencyValidationException.InnerException);
            }
            catch (HomeRequestDependencyException homeRequestDependencyException)
            {
                return InternalServerError(homeRequestDependencyException.InnerException);
            }
            catch (HomeRequestServiceException homeRequestServiceException)
            {
                return InternalServerError(homeRequestServiceException.InnerException);
            }
        }
    }
}
