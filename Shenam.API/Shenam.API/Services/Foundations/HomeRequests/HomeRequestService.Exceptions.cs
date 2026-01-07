//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Microsoft.Data.SqlClient;
using Shenam.API.Models.Foundation.HomeRequests;
using Shenam.API.Models.Foundation.HomeRequests.Exceptions;
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
            catch(InvalidHomeRequestException invalidHomeRequestException)
            {
                throw CreateAndLogValidationException(invalidHomeRequestException);
            }
            catch(SqlException sqlException)
            {
                var failedHomeRequestStorageException = new FailedHomeRequestStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedHomeRequestStorageException);
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
    }
}
