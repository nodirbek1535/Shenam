//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Shenam.API.Brokers.Storages;
using Shenam.API.Models.Foundation.HomeRequests;
using System;
using System.Threading.Tasks;

namespace Shenam.API.Services.Foundations.HomeRequests
{
    public partial class HomeRequestService:IHomeRequestService
    {
        private readonly IStorageBroker storageBroker;
        public HomeRequestService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public ValueTask<HomeRequest> AddHomeRequestAsync(HomeRequest homeRequest) =>
            throw new NotImplementedException();
    }
}
