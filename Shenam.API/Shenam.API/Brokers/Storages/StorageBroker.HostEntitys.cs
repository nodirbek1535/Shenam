//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shenam.API.Models.Foundation.Hosts;

namespace Shenam.API.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<HostEntity> Hosts { get; set; }

        public async ValueTask<HostEntity> InsertHostAsync(HostEntity hostEntity)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<HostEntity> hostEntityEntry =
                await broker.Hosts.AddAsync(hostEntity);
            await broker.SaveChangesAsync();

            return hostEntityEntry.Entity;
        }

        public void InsertHostEntityAsync(HostEntity randomHostEntity)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<HostEntity> SelectHostEntityByIdAsync(Guid hostEntityId)
        {
            using var broker = new StorageBroker(this.configuration);

            return await broker.Hosts
                .FirstOrDefaultAsync(hostEntity => hostEntity.Id == hostEntityId);
        }

        public IQueryable<HostEntity> SelectAllHostEntities() =>
            SelectAll<HostEntity>();

        public async ValueTask<HostEntity> UpdateHostEntityAsync(HostEntity hostEntity)
        {
            using var broker = new StorageBroker(this.configuration);

            broker.Hosts.Update(hostEntity);
            await broker.SaveChangesAsync();

            return hostEntity;
        }
    }
}
