//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using System.Threading.Tasks;
using Shenam.API.Brokers.Storages;
using Shenam.API.Models.Foundation.Guests;

namespace Shenam.API.Services.Foundations.Guests
{
    public class GuestService : IGuestService
    {
        private readonly IStorageBroker storageBroker;

        public GuestService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<Guest> AddGuestAsync(Guest guest) =>
            await this.storageBroker.InsertGuestAsync(guest);
 
    }
}
