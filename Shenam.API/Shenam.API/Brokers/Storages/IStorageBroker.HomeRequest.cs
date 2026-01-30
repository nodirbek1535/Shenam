//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Shenam.API.Models.Foundation.HomeRequests;

namespace Shenam.API.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        //CREATE
        //READ
        //UPDATE
        //DELETE
        ValueTask<HomeRequest> InsertHomeRequestAsync(HomeRequest homeRequest);

        ValueTask<HomeRequest> SelectHomeRequestByIdAsync(Guid homeRequestId);

        IQueryable<HomeRequest> SelectAllHomeRequests();

        ValueTask<HomeRequest> UpdateHomeRequestAsync(HomeRequest homeRequest);

        ValueTask<HomeRequest> DeleteHomeRequestAsync(HomeRequest homeRequest);
    }
}
