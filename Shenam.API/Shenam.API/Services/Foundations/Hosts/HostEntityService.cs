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

                var hostEntityDependencyException =
                    new HostEntityDependencyException(failedHostEntityStorageException);

                this.loggingBroker.LogCritical(hostEntityDependencyException);

                throw hostEntityDependencyException;
            }
            catch (Exception exception)
            {
                var failedHostEntityServiceException =
                    new FailedHostEntityServiceException(exception);

                var hostEntityServiceException =
                    new HostEntityServiceException(failedHostEntityServiceException);

                this.loggingBroker.LogError(hostEntityServiceException);

                throw new HostEntityServiceException(failedHostEntityServiceException);
            }
        }
        
        public async ValueTask<HostEntity> ModifyHostEntityAsync(HostEntity hostEntity)
        {
            if(hostEntity == null)
            {
                var nullHostEntityException = new NullHostEntityException();

                var hostEntityValidationException =
                    new HostEntityValidationException(nullHostEntityException);

                this.loggingBroker.LogError(hostEntityValidationException);

                throw hostEntityValidationException;
            }
            if(hostEntity.Id == Guid.Empty)
            {
                var invalidHostEntityException = new InvalidHostEntityException();

                invalidHostEntityException.AddData(
                    key: nameof(HostEntity.Id),
                    values: "Id is required");

                var hostEntityValidationException =
                    new HostEntityValidationException(invalidHostEntityException);

                this.loggingBroker.LogError(hostEntityValidationException);

                throw hostEntityValidationException;
            }

            HostEntity maybeHostEntity =
                await this.storageBroker.SelectHostEntityByIdAsync(hostEntity.Id);

            if (maybeHostEntity is null)
            {
                var notFoundHostEntityException =
                    new NotFoundHostEntityException(hostEntity.Id);

                var hostEntityValidationException =
                    new HostEntityValidationException(notFoundHostEntityException);

                this.loggingBroker.LogError(hostEntityValidationException);

                throw hostEntityValidationException;
            }

            HostEntity updateHostEntity =
                await this.storageBroker.UpdateHostEntityAsync(hostEntity);

            return updateHostEntity;
        }
    }
}