//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System.Threading.Tasks;
using Shenam.API.Models.Foundation.Hosts;

namespace Shenam.API.Services.Foundations.Hosts
{
    public interface IHostEntityService
    {
        ValueTask<HostEntity> AddHostEntityAsync(HostEntity host);
    }
}
