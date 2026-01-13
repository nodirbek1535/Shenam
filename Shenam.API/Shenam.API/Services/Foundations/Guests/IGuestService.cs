//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Shenam.API.Models.Foundation.Guests;

namespace Shenam.API.Services.Foundations.Guests
{
    public interface IGuestService
    {
        ValueTask<Guest> AddGuestAsync(Guest guest);
        ValueTask<Guest> RetrieveGuestByIdAsync(Guid invalidGuestId);
        IQueryable<Guest> RetrieveAllGuests();
    }
}
