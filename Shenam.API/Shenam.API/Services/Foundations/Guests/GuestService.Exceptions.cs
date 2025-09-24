//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System.Threading.Tasks;
using Shenam.API.Models.Foundation.Guests;
using Shenam.API.Models.Foundation.Guests.Exceptions;
using Xeptions;

namespace Shenam.API.Services.Foundations.Guests
{
    public partial class GuestService
    {
        private delegate ValueTask<Guest> ReturnningGuestFunction();

        private async ValueTask<Guest> TryCatch(ReturnningGuestFunction returnningGuestFunction)
        {
            try
            {
                return await returnningGuestFunction();
            }
            catch (NullGuestException nullGuestException)
            {
                throw CreateAndLogValidationException(nullGuestException);
            }
            catch(InvalidGuestException invalidGuestException)
            {
                throw CreateAndLogValidationException(invalidGuestException);
            }
        }

        private GuestValidationException CreateAndLogValidationException(Xeption exception)
        {
            var guestValidationException =
                new GuestValidationException(exception);

            this.loggingBroker.LogError(guestValidationException);

            return guestValidationException;
        }
    }
}
