//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Shenam.API.Models.Foundation.Homes;

namespace Shenam.API.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        //CREATE
        //READ
        //UPDATE
        //DELETE
        ValueTask<Home> InsertHomeAsync(Home home);

        ValueTask<Home> SelectHomeByIdAsync(Guid homeId);

        IQueryable<Home> SelectAllHomes();

        ValueTask<Home> UpdateHomeAsync(Home home);
    }
}
