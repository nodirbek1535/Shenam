//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Shenam.API.Brokers.Storages;
using Shenam.API.Models.Foundation.Homes;
using System;
using System.Threading.Tasks;

namespace Shenam.API.Services.Foundations.Homes
{
    public partial class HomeService:IHomeService
    {
        private readonly IStorageBroker storageBroker;

        public HomeService(IStorageBroker storageBroker) => 
            this.storageBroker = storageBroker;

        public ValueTask<Home> AddHomeAsync(Home home) =>
            throw new NotImplementedException();
    }
}
