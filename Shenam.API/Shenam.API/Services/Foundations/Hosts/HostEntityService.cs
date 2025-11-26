//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using System.Threading.Tasks;
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

        public async ValueTask<HostEntity> AddHostEntityAsync(HostEntity host)
        {

            try
            {
                ValidateHostEntityOnAdd(host);


                return await this.storageBroker.InsertHostEntityAsync(host);
            }
            catch(NullHostEntityException nullHostEntityException)
            {
                var hostEntityValidationException =
                    new HostEntityValidationException(nullHostEntityException);

                this.loggingBroker.LogError(hostEntityValidationException);

                throw hostEntityValidationException;
            }
            catch(InvalidHostEntityException invalidHostEntityException)
            {
                var hostEntityValidationException =
                    new HostEntityValidationException(invalidHostEntityException);

                this.loggingBroker.LogError(hostEntityValidationException);

                throw hostEntityValidationException;
            }
        }

    }
}