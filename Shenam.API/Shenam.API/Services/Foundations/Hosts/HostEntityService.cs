//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using System.Threading.Tasks;
using Shenam.API.Brokers.loggings;
using Shenam.API.Brokers.Storages;
using Shenam.API.Models.Foundation.Guests;
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
    }
}