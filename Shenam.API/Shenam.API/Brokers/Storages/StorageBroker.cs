//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using EFxceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shenam.API.Models.Foundation.Guests;
using Shenam.API.Models.Foundation.HomeRequests;
using Shenam.API.Models.Foundation.Homes;
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

        protected IQueryable<T> SelectAll<T>() where T : class =>
            this.Set<T>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString =
                this.configuration.GetConnectionString(name: "DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }

        public override void Dispose() { }

        // Guests
        async ValueTask<Guest> IStorageBroker.InsertGuestAsync(Guest guest)
        {
            var broker = new StorageBroker(this.configuration);
            broker.Entry(guest).State = EntityState.Added;
            await broker.SaveChangesAsync();

            return guest;
        }

        async ValueTask<Guest> IStorageBroker.SelectGuestByIdAsync(Guid guestId)
        {
            var broker = new StorageBroker(this.configuration);
           
            return await broker.Guests
                .FirstOrDefaultAsync(guest => guest.Id == guestId);
        }

        async ValueTask<HostEntity> IStorageBroker.InsertHostEntityAsync(HostEntity hostEntity)
        {
            var broker = new StorageBroker(this.configuration);
            broker.Entry(hostEntity).State = EntityState.Added;
            await broker.SaveChangesAsync();

            return hostEntity;
         }
         
        async ValueTask<Home> IStorageBroker.InsertHomeAsync(Home home)
        {
            var broker = new StorageBroker(this.configuration);
            broker.Entry(home).State = EntityState.Added;
            await broker.SaveChangesAsync();

            return home;
        }

        async ValueTask<HomeRequest> IStorageBroker.InsertHomeRequestAsync(HomeRequest homeRequest)
        {
            var broker = new StorageBroker(this.configuration);
            broker.Entry(homeRequest).State = EntityState.Added;
            await broker.SaveChangesAsync();

            return homeRequest;
        }
    }
}
