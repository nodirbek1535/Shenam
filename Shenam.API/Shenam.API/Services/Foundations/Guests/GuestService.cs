//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Microsoft.Data.SqlClient;
using Shenam.API.Brokers.loggings;
using Shenam.API.Brokers.Storages;
using Shenam.API.Models.Foundation.Guests;
using Shenam.API.Models.Foundation.Guests.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shenam.API.Services.Foundations.Guests
{
    public partial class GuestService : IGuestService
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

        public ValueTask<Guest> AddGuestAsync(Guest guest) =>
        TryCatch(async () =>
        {
            ValidateGuestOnAdd(guest);

            return await this.storageBroker.InsertGuestAsync(guest);
        });

        public ValueTask<Guest> RetrieveGuestByIdAsync(Guid guestId) =>
        TryCatch(async () =>
        {
            ValidateGuestId(guestId);

            Guest maybeGuest =
                await this.storageBroker.SelectGuestByIdAsync(guestId);

            ValidateStorageGuest(maybeGuest, guestId);

            return maybeGuest;
        });

        public IQueryable<Guest> RetrieveAllGuests()
        {
            try
            {
                return this.storageBroker.SelectAllGuests();
            }
            catch (SqlException sqlException)
            {
                var failedStorageException =
                    new FailedGuestStorageException(sqlException);

                throw new GuestDependencyException(failedStorageException);
            }
            catch (Exception exception)
            {
                var failedServiceException =
                    new FailedGuestServiceException(exception);

                throw new GuestServiceException(failedServiceException);
            }
        }


    }
}
