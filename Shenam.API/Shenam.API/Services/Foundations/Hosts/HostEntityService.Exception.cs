//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System.Threading.Tasks;
using Shenam.API.Models.Foundation.Guests.Exceptions;
using Shenam.API.Models.Foundation.Hosts;
using Shenam.API.Models.Foundation.Hosts.Exceptions;
using Xeptions;

namespace Shenam.API.Services.Foundations.Hosts
{
    public partial class HostEntityService
    {
        private delegate ValueTask<HostEntity> ReturningHostEntityFunction(); 

        private async ValueTask<HostEntity> TryCatch(
            ReturningHostEntityFunction returningHostEntityFunction)
        {
            try
            {
                return await returningHostEntityFunction();
            }
            catch (NullHostEntityException nullHostEntityException)
            {
                throw CreateAndLogValidationException(nullHostEntityException);
            }
            catch (InvalidHostEntityException invalidHostEntityException)
            {
                throw CreateAndLogValidationException(invalidHostEntityException);
            }
        }

        private HostEntityValidationException CreateAndLogValidationException(Xeption exception)
        {
            var hostEntityValidationException =
                new HostEntityValidationException(exception);

            this.loggingBroker.LogError(hostEntityValidationException);

            return hostEntityValidationException;
        }
    }
}
