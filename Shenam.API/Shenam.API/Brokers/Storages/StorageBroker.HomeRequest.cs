//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shenam.API.Models.Foundation.HomeRequests;

namespace Shenam.API.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<HomeRequest> HomeRequests { get; set; }

        public async ValueTask<HomeRequest> InsertHomeRequestAsync(HomeRequest homeRequest)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<HomeRequest> homeRequestEntityEntry =
                await broker.HomeRequests.AddAsync(homeRequest);
            await broker.SaveChangesAsync();

            return homeRequestEntityEntry.Entity;
        }

        public async ValueTask<HomeRequest> SelectHomeRequestByIdAsync(Guid homeRequestId)
        {
            using var broker = new StorageBroker(this.configuration);

            return await broker.HomeRequests
                .FirstOrDefaultAsync(homeRequest => homeRequest.Id == homeRequestId);
        }

        public IQueryable<HomeRequest> SelectAllHomeRequests() =>
            SelectAll<HomeRequest>();
    }
}
