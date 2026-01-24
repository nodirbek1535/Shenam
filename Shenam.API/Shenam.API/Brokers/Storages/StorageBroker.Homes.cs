//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shenam.API.Models.Foundation.Homes;

namespace Shenam.API.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Home> Homes { get; set; }

        public async ValueTask<Home> InsertHomeAsync(Home home)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<Home> homeEntityEntry =
                await broker.Homes.AddAsync(home);
            await broker.SaveChangesAsync();

            return homeEntityEntry.Entity;
        }

        public async ValueTask<Home> SelectHomeByIdAsync(Guid homeId)
        {
            using var broker = new StorageBroker(this.configuration);

            return await broker.Homes
                .FirstOrDefaultAsync(home => home.Id == homeId);
        }

        public IQueryable<Home> SelectAllHomes() =>
            SelectAll<Home>();

        public async ValueTask<Home> UpdateHomeAsync(Home home)
        {
            using var broker = new StorageBroker(this.configuration);

            broker.Homes.Update(home);
            await broker.SaveChangesAsync();

            return home;
        }

        public async ValueTask<Home> DeleteHomeAsync(Home home)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<Home> homeEntityEntry =
                broker.Homes.Remove(home);

            await broker.SaveChangesAsync();

            return homeEntityEntry.Entity;
        }
    }
}
