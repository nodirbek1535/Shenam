//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using System.Threading.Tasks;
using Shenam.API.Brokers.loggings;
using Shenam.API.Brokers.Storages;
using Shenam.API.Models.Foundation.Hosts;

namespace Shenam.API.Services.Foundations.Hosts
{
    public partial class HostEntityService : IHostEntityService
    {
        private readonly IStorageBroker storageBroker;

        public HostEntityService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public ValueTask<HostEntity> AddHostEntityAsync(HostEntity host)
        {
            ValidateHostEntityOnAdd(host);

            return this.storageBroker.InsertHostEntityAsync(host);
        }

    }
}
