//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shenam.API.Models.Foundation.Homes;
using Shenam.API.Models.Foundation.Homes.Exceptions;
using System;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xeptions;

namespace Shenam.API.Services.Foundations.Homes
{
    public partial class HomeService
    {
        private delegate ValueTask<Home> ReturnningHomeFunction();

        private async ValueTask<Home> TryCatch(ReturnningHomeFunction returnningHomeFunction)
        {
            try
            {
                return await returnningHomeFunction();
            }
            catch (NullHomeException nullHomeException)
            {
                throw CreateAndLogValidationException(nullHomeException);
            }
            catch (InvalidHomeException invalidHomeException)
            {
                throw CreateAndLogValidationException(invalidHomeException);
            }
            catch (NotFoundHomeException notFoundHomeException)
            {
                throw CreateAndLogValidationException(notFoundHomeException);
            }
            catch (SqlException sqlException)
            {
                var failedHomeStorageException =
                    new FailedHomeStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedHomeStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsHomeException =
                    new AlreadyExistsHomeException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistsHomeException);
            }
            catch(LockedHomeException lockedHomeException)
            {
                var homeDependencyValidationException =
                    new HomeDependencyValidationException(lockedHomeException);

                this.loggingBroker.LogError(homeDependencyValidationException);

                throw homeDependencyValidationException;
            }
            catch(Exception exception)
            {
                var failedHomeServiceException =
                    new FailedHomeServiceException(exception);

                throw CreateAndLogServiceException(failedHomeServiceException);
            }
        }


        private HomeValidationException CreateAndLogValidationException(Xeption exception)
        {
            var homeValidationException =
                new HomeValidationException(exception);

            this.loggingBroker.LogError(homeValidationException);

            return homeValidationException;
        }

        private HomeDependencyException CreateAndLogCriticalDependencyException (Xeption exception)
        {
            var homeDependencyException =
                new HomeDependencyException(exception);

            this.loggingBroker.LogCritical(homeDependencyException);

            return homeDependencyException;
        }

        private HomeDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var homeDependencyValidationException =
                new HomeDependencyValidationException(exception);

            this.loggingBroker.LogError(homeDependencyValidationException);

            return homeDependencyValidationException;
        }

        private HomeServiceException CreateAndLogServiceException(Xeption exception)
        {
            var homeServiceException =
                new HomeServiceException(exception);

            this.loggingBroker.LogError(homeServiceException);

            return homeServiceException;
        }
    }
}
