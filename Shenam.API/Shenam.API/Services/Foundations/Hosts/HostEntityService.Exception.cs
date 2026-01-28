//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shenam.API.Models.Foundation.Homes.Exceptions;
using Shenam.API.Models.Foundation.Hosts;
using Shenam.API.Models.Foundation.Hosts.Exceptions;
using System;
using System.Threading.Tasks;
using Xeptions;

namespace Shenam.API.Services.Foundations.Hosts
{
    public partial class HostEntityService
    {
        private delegate ValueTask<HostEntity> ReturningHostEntityFunction();

        private async ValueTask<HostEntity> TryCatch(
            ReturningHostEntityFunction returningHostEntityFunction)
        {
            try
            {
                return await returningHostEntityFunction();
            }
            catch (NullHostEntityException nullHostEntityException)
            {
                throw CreateAndLogValidationException(nullHostEntityException);
            }
            catch (InvalidHostEntityException invalidHostEntityException)
            {
                throw CreateAndLogValidationException(invalidHostEntityException);
            }
            catch(NotFoundHostEntityException notFoundHostEntityException)
            {
                throw CreateAndLogValidationException(notFoundHostEntityException);
            }
            catch (SqlException sqlException)
            {
                var failedHostEntityStorageException =
                    new FailedHostEntityStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(
                    failedHostEntityStorageException);
            }
            catch(DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedHostEntityException =
                    new LockedHostEntityException(dbUpdateConcurrencyException);

                throw CreateAndLogDependencyValidationException(lockedHostEntityException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                var failedHostEntityStorageException =
                    new FailedHostEntityStorageException(dbUpdateException);

                throw CreateandLogDepedndencyException(failedHostEntityStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsHostEntityException =
                    new AlreadyExistsHostEntityException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(
                    alreadyExistsHostEntityException);
            }
            catch (Exception exception)
            {
                var failedHostEntityServiceException =
                    new FailedHostEntityServiceException(exception);

                throw CreateAndLogServiceException(
                    failedHostEntityServiceException);
            }
        }

        private HostEntityValidationException CreateAndLogValidationException(Xeption exception)
        {
            var hostEntityValidationException =
                new HostEntityValidationException(exception);

            this.loggingBroker.LogError(hostEntityValidationException);

            return hostEntityValidationException;
        }

        private HostEntityDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var hostEntityDependencyException =
                new HostEntityDependencyException(exception);

            this.loggingBroker.LogCritical(hostEntityDependencyException);

            return hostEntityDependencyException;
        }

        private HostEntityDependencyValidationException CreateAndLogDependencyValidationException(
            Xeption exception)
        {
            var hostEntityDependencyValidationException =
                new HostEntityDependencyValidationException(exception);

            this.loggingBroker.LogError(hostEntityDependencyValidationException);
            return hostEntityDependencyValidationException;
        }

        private HostEntityServiceException CreateAndLogServiceException(Xeption exception)
        {
            var hostEntityServiceException =
                new HostEntityServiceException(exception);

            this.loggingBroker.LogError(hostEntityServiceException);
            return hostEntityServiceException;
        }

        private HostEntityDependencyException CreateandLogDepedndencyException(Xeption exception)
        {
            var hostEntityDependencyExcpetion =
                new HostEntityDependencyException(exception);

            this.loggingBroker.LogError(hostEntityDependencyExcpetion);

            return hostEntityDependencyExcpetion;
        }
    }
}
