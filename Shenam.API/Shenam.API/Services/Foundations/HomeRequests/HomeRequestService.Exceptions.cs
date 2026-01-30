//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shenam.API.Models.Foundation.HomeRequests;
using Shenam.API.Models.Foundation.HomeRequests.Exceptions;
using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Xeptions;

namespace Shenam.API.Services.Foundations.HomeRequests
{
    public partial class HomeRequestService
    {

        private delegate ValueTask<HomeRequest> ReturnningHomeRequestFunction();

        private async ValueTask<HomeRequest> TryCatch(ReturnningHomeRequestFunction returnningHomeRequestFunction)
        {
            try
            {
                return await returnningHomeRequestFunction();
            }
            catch (NullHomeRequestException nullHomeRequestException)
            {
                throw CreateAndLogValidationException(nullHomeRequestException);
            }
            catch (InvalidHomeRequestException invalidHomeRequestException)
            {
                throw CreateAndLogValidationException(invalidHomeRequestException);
            }
            catch (NotFoundHomeRequestException notFoundHomeRequestException)
            {
                throw CreateAndLogValidationException(notFoundHomeRequestException);
            }
            catch (SqlException sqlException)
            {
                var failedHomeRequestStorageException = new FailedHomeRequestStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedHomeRequestStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsHomeRequestException =
                    new AlreadyExistsHomeRequestException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistsHomeRequestException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedHomeRequestException =
                    new LockedHomeRequestException(dbUpdateConcurrencyException);

                throw CreateAndLogDependencyValidationException(lockedHomeRequestException);
            }
            catch (Exception exception)
            {
                var failedHomeRequestServiceException =
                    new FailedHomeRequestServiceException(exception);

                throw CreateAndLogServiceException(failedHomeRequestServiceException);
            }
        }

        private HomeRequestValidationException CreateAndLogValidationException(Xeption exception)
        {
            var homeRequestValidationException =
                new HomeRequestValidationException(exception);

            this.loggingBroker.LogError(homeRequestValidationException);

            return homeRequestValidationException;
        }

        private HomeRequestDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var homeRequestDependencyException =
                new HomeRequestDependencyException(exception);

            this.loggingBroker.LogCritical(homeRequestDependencyException);

            return homeRequestDependencyException;
        }

        private HomeRequestDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var homeRequestDependencyValidationException =
                new HomeRequestDependencyValidationException(exception);

            this.loggingBroker.LogError(homeRequestDependencyValidationException);

            return homeRequestDependencyValidationException;
        }

        private HomeRequestServiceException CreateAndLogServiceException(Xeption exception)
        {
            var homeRequestServiceException =
                new HomeRequestServiceException(exception);

            this.loggingBroker.LogError(homeRequestServiceException);

            return homeRequestServiceException;
        }
    }
}
