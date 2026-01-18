//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Shenam.API.Models.Foundation.Homes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shenam.API.Services.Foundations.Homes
{
    public interface IHomeService
    {
        ValueTask<Home> AddHomeAsync(Home home);
        ValueTask<Home> RetrieveHomeByIdAsync(Guid invalidHomeId);
        IQueryable<Home> RetrieveAllHomes();
        ValueTask<Home> ModifyHomeAsync(Home home);
    }
}
