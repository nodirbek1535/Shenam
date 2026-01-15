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

                var guestDependencyException =
                    new GuestDependencyException(failedStorageException);

                this.loggingBroker.LogCritical(guestDependencyException);

                throw guestDependencyException;
            }
            catch (Exception exception)
            {
                var failedServiceException =
                    new FailedGuestServiceException(exception);

                var guestServiceException =
                    new GuestServiceException(failedServiceException);

                this.loggingBroker.LogError(guestServiceException); 

                throw new GuestServiceException(failedServiceException);
            }
        }

        public async ValueTask<Guest> ModifyGuestAsync(Guest guest)
        {
            if(guest is null)
            {
                var nullGuestException = new NullGuestException();

                var guestValidationException =
                    new GuestValidationException(nullGuestException);

                this.loggingBroker.LogError(guestValidationException);

                throw guestValidationException;
            }
            if(guest.Id == Guid.Empty)
            {
                var invalidGuestException = new InvalidGuestException();

                invalidGuestException.AddData(
                    key: nameof(Guest.Id),
                    values: "Id is required");

                var guestValidationException =
                    new GuestValidationException(invalidGuestException);

                this.loggingBroker.LogError(guestValidationException);

                throw guestValidationException;
            }

            return guest;
        }
    }
}
