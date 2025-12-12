//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System.Threading.Tasks;
using EFxceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shenam.API.Models.Foundation.Guests;
using Shenam.API.Models.Foundation.Hosts;

namespace Shenam.API.Brokers.Storages
{
    public partial class StorageBroker : EFxceptionsContext, IStorageBroker
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Database.Migrate();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString =
                this.configuration.GetConnectionString(name: "DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }

        public override void Dispose() { }

        async ValueTask<Guest> IStorageBroker.InsertGuestAsync(Guest guest)
        {
            var broker = new StorageBroker(this.configuration);
            broker.Entry(guest).State = EntityState.Added;
            await broker.SaveChangesAsync();

            return guest;
        }

        async ValueTask<HostEntity> IStorageBroker.InsertHostEntityAsync(HostEntity hostEntity)
        {
            var broker = new StorageBroker(this.configuration);
            broker.Entry(hostEntity).State = EntityState.Added;
            await broker.SaveChangesAsync();

            return hostEntity;
        }
    }
}
