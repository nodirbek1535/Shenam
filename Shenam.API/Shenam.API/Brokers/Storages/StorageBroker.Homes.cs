//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

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

        public async ValueTask<Home> SelectHomeByIdAsync(System.Guid homeId)
        {
            using var broker = new StorageBroker(this.configuration);

            return await broker.Homes
                .FirstOrDefaultAsync(home => home.Id == homeId);
        }
    }
}
