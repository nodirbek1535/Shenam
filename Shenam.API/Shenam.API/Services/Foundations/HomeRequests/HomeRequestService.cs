//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Shenam.API.Brokers.loggings;
using Shenam.API.Brokers.Storages;
using Shenam.API.Models.Foundation.HomeRequests;
using Shenam.API.Models.Foundation.HomeRequests.Exceptions;
using System.Threading.Tasks;

namespace Shenam.API.Services.Foundations.HomeRequests
{
    public partial class HomeRequestService:IHomeRequestService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public HomeRequestService(
            IStorageBroker storageBroker, 
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<HomeRequest> AddHomeRequestAsync(HomeRequest homeRequest)
        {
            try
            {
                ValidateHomeRequestOnAdd(homeRequest);

                return await this.storageBroker.InsertHomeRequestAsync(homeRequest);
            }
            catch(NullHomeRequestException nullHomeRequestException)
            {
                var homeRequestValidationException =
                    new HomeRequestValidationException(nullHomeRequestException);

                this.loggingBroker.LogError(homeRequestValidationException);

                throw homeRequestValidationException;
            }
            catch(InvalidHomeRequestException invalidHomeRequestException)
            {
                var homeRequestValidationException =
                    new HomeRequestValidationException(invalidHomeRequestException);

                this.loggingBroker.LogError(homeRequestValidationException);

                throw homeRequestValidationException;
            }
        }
    }
}
