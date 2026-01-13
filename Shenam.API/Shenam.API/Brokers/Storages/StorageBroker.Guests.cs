//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shenam.API.Models.Foundation.Guests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shenam.API.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Guest> Guests { get; set; }

        public async ValueTask<Guest> InsertGuestAsync(Guest guest)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<Guest> guestEntityEntry =
                await broker.Guests.AddAsync(guest);
            await broker.SaveChangesAsync();

            return guestEntityEntry.Entity;
        }

        public async ValueTask<Guest> SelectGuestByIdAsync(Guid guestId)
        {
            using var broker = new StorageBroker(this.configuration);

            return await broker.Guests
                .FirstOrDefaultAsync(guest => guest.Id == guestId);
        }

        public IQueryable<Guest> SelectAllGuests() =>
            SelectAll<Guest>();


    }
}
