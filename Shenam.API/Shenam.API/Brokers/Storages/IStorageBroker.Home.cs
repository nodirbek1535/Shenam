//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

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
    }
}
