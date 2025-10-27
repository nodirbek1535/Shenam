//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Microsoft.EntityFrameworkCore.ChangeTracking;
using Xeptions;

namespace Shenam.API.Models.Foundation.Guests.Exceptions
{
    public class GuestDependencyException : Xeption
    {
        public GuestDependencyException(Xeption innerException) 
            :base(message: "Guest dependency error occurred, contact support",
                 innerException)
        { }
    }
}
