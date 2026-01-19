//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Shenam.API.Brokers.loggings;
using Shenam.API.Brokers.Storages;
using Shenam.API.Models.Foundation.Guests;
using Shenam.API.Models.Foundation.Homes;
using Shenam.API.Models.Foundation.Homes.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shenam.API.Services.Foundations.Homes
{
    public partial class HomeService : IHomeService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public HomeService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Home> AddHomeAsync(Home home) =>
        TryCatch(async () =>
        {
            ValidateHomeOnAdd(home);

            return await this.storageBroker.InsertHomeAsync(home);
        });

        public ValueTask<Home> RetrieveHomeByIdAsync(Guid homeId) =>
        TryCatch(async () =>
        {
            ValidateHomeId(homeId);

            Home maybeHome =
                await this.storageBroker.SelectHomeByIdAsync(homeId);

            ValidateStorageHome(maybeHome, homeId);

            return maybeHome;
        });

        public IQueryable<Home> RetrieveAllHomes()
        {
            try
            {
                return this.storageBroker.SelectAllHomes();
            }
            catch (SqlException sqlException)
            {
                var failedHomeStorageException =
                    new FailedHomeStorageException(sqlException);

                var homeDependencyException =
                    new HomeDependencyException(failedHomeStorageException);

                this.loggingBroker.LogCritical(homeDependencyException);

                throw homeDependencyException;
            }
            catch (Exception exception)
            {
                var failedHomeServiceException =
                    new FailedHomeServiceException(exception);  

                var homeServiceException =
                    new HomeServiceException(failedHomeServiceException);

                this.loggingBroker.LogError(homeServiceException);

                throw homeServiceException;
            }
        }
        
        public async ValueTask<Home> ModifyHomeAsync(Home home)
        {
            if(home is null)
            {
                var nullHomeException = new NullHomeException();

                var homeValidationException =
                    new HomeValidationException(nullHomeException);

                this.loggingBroker.LogError(homeValidationException);

                throw homeValidationException;
            }

            return home;
        }
    }
}
