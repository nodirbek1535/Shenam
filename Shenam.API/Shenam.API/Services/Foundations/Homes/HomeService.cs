//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Shenam.API.Brokers.loggings;
using Shenam.API.Brokers.Storages;
using Shenam.API.Models.Foundation.Homes;
using Shenam.API.Models.Foundation.Homes.Exceptions;
using System;
using System.Threading.Tasks;

namespace Shenam.API.Services.Foundations.Homes
{
    public partial class HomeService:IHomeService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public HomeService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Home> AddHomeAsync(Home home)
        {
            try
            {
                if (home is null)
                {
                    throw new NullHomeException();
                }

                return await this.storageBroker.InsertHomeAsync(home);
            }
            catch(NullHomeException nullHomeException)
            {
                var homeValidationException =
                    new HomeValidationException(nullHomeException);

                this.loggingBroker.LogError(homeValidationException);

                throw homeValidationException;
            }
        }
    }
}
