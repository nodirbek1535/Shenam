 //===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Shenam.API.Models.Foundation.Homes;
using Shenam.API.Models.Foundation.Homes.Exceptions;
using System;
using System.Threading.Tasks;
using Xeptions;

namespace Shenam.API.Services.Foundations.Homes
{
    public partial class HomeService
    {
        private delegate ValueTask<Home> ReturnningHomeFunction();

        private async ValueTask<Home> TryCatch(ReturnningHomeFunction returnningHomeFunction)
        {
            try
            {
                return await returnningHomeFunction();
            }
            catch (NullHomeException nullHomeException)
            {
                throw CreateAndLogValidationException(nullHomeException);
            }
            catch(InvalidHomeException invalidHomeException)
            {
                throw CreateAndLogValidationException(invalidHomeException);
            }
        }

        private HomeValidationException CreateAndLogValidationException(Xeption exception)
        {
            var homeValidationException =
                new HomeValidationException(exception);

            this.loggingBroker.LogError(homeValidationException);

            return homeValidationException;
        }
    }
}
