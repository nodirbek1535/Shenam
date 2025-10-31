﻿//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System.Threading.Tasks;
using Shenam.API.Brokers.loggings;
using Shenam.API.Brokers.Storages;
using Shenam.API.Models.Foundation.Guests;

namespace Shenam.API.Services.Foundations.Guests
{
    public partial class  GuestService : IGuestService
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
    }
}
