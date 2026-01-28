//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Shenam.API.Models.Foundation.Hosts;

namespace Shenam.API.Services.Foundations.Hosts
{
    public interface IHostEntityService
    {
        ValueTask<HostEntity> AddHostEntityAsync(HostEntity host);
        ValueTask<HostEntity> RetrieveHostEntityByIdAsync(Guid hostEntityId);
        IQueryable<HostEntity> RetrieveAllHostEntities();
        ValueTask<HostEntity> ModifyHostEntityAsync(HostEntity hostEntity);
    }
}
