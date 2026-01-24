//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Shenam.API.Brokers.loggings;
using Shenam.API.Brokers.Storages;
using Shenam.API.Models.Foundation.Hosts;
using Shenam.API.Models.Foundation.Hosts.Exceptions;

namespace Shenam.API.Services.Foundations.Hosts
{
    public partial class HostEntityService : IHostEntityService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public HostEntityService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<HostEntity> AddHostEntityAsync(HostEntity host) =>
        TryCatch(async () =>
        {
            ValidateHostEntityOnAdd(host);

            return await this.storageBroker.InsertHostEntityAsync(host);
        });

        public ValueTask<HostEntity> RetrieveHostEntityByIdAsync(System.Guid hostEntityId) =>
        TryCatch(async () =>
        {
            ValidateHostEntityId(hostEntityId);

            HostEntity maybeHostEntity =
                await this.storageBroker.SelectHostEntityByIdAsync(hostEntityId);

            ValidateStorageHostEntity(maybeHostEntity, hostEntityId);

            return maybeHostEntity;
        });

        public IQueryable<HostEntity> RetrieveAllHostEntities()
        {
            try
            {
                return this.storageBroker.SelectAllHostEntities();
            }
            catch (SqlException sqlException)
            {
                var failedHostEntityStorageException =
                    new FailedHostEntityStorageException(sqlException);

                throw new HostEntityDependencyException(failedHostEntityStorageException);
            }
            catch (Exception exception)
            {
                var failedHostEntityServiceException =
                    new FailedHostEntityServiceException(exception);

                throw new HostEntityServiceException(failedHostEntityServiceException);
            }
        }
        

    }
}