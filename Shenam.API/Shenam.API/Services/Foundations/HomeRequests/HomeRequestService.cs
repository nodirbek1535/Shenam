//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Microsoft.Data.SqlClient;
using Shenam.API.Brokers.loggings;
using Shenam.API.Brokers.Storages;
using Shenam.API.Models.Foundation.HomeRequests;
using Shenam.API.Models.Foundation.HomeRequests.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shenam.API.Services.Foundations.HomeRequests
{
    public partial class HomeRequestService:IHomeRequestService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public HomeRequestService(
            IStorageBroker storageBroker, 
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<HomeRequest> AddHomeRequestAsync(HomeRequest homeRequest) =>
        TryCatch(async () =>
        {
            ValidateHomeRequestOnAdd(homeRequest);

            return await this.storageBroker.InsertHomeRequestAsync(homeRequest);
        });

        public ValueTask<HomeRequest> RetrieveHomeRequestByIdAsync(Guid homeRequestId) =>
        TryCatch(async () =>
        {
            ValidateHomeRequestId(homeRequestId);

            HomeRequest maybeHomeRequest =
                await this.storageBroker.SelectHomeRequestByIdAsync(homeRequestId);

            ValidateStorageHomeRequest(maybeHomeRequest, homeRequestId);

            return maybeHomeRequest;
        });

        public IQueryable<HomeRequest> RetrieveAllHomeRequests()
        {
            try
            {
                return this.storageBroker.SelectAllHomeRequests();
            }
            catch (SqlException sqlException)
            {
                var failedHomeRequestStorageException =
                    new FailedHomeRequestStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedHomeRequestStorageException);
            }
            catch (Exception exception)
            {
                var failedHomeRequestServiceException =
                    new FailedHomeRequestServiceException(exception);
                throw CreateAndLogServiceException(failedHomeRequestServiceException);
            }
        }
    }
}
