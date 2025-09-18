//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using System.Threading.Tasks;
using Shenam.API.Brokers.loggings;
using Shenam.API.Brokers.Storages;
using Shenam.API.Models.Foundation.Guests;
using Shenam.API.Models.Foundation.Guests.Exceptions;

namespace Shenam.API.Services.Foundations.Guests
{
    public class GuestService : IGuestService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public GuestService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Guest> AddGuestAsync(Guest guest)
        {
            try
            {
                if (guest is null)
                {
                    throw new NullGuestException();
                }


                return await this.storageBroker.InsertGuestAsync(guest);
            }
            catch (NullGuestException nullGuestException)
            {
                var guestValidationException =
                    new GuestValidationException(nullGuestException);

                this.loggingBroker.LogError(guestValidationException);

                throw guestValidationException;
            }
        }
 
    }
}
