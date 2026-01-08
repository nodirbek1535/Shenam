//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Shenam.API.Models.Foundation.HomeRequests;
using System.Threading.Tasks;

namespace Shenam.API.Services.Foundations.HomeRequests
{
    public interface IHomeRequestService
    {
        ValueTask<HomeRequest> AddHomeRequestAsync(HomeRequest homeRequest);
    }
}
