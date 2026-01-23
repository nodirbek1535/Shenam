//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using System.Threading.Tasks;
using Shenam.API.Models.Foundation.Hosts;

namespace Shenam.API.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        //CREATE
        //READ
        //UPDATE
        //DELETE
        ValueTask<HostEntity> InsertHostEntityAsync(HostEntity hostEntity);

        ValueTask<HostEntity> SelectHostEntityByIdAsync(Guid hostEntityId);
    }
}
