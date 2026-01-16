//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using Xeptions;

namespace Shenam.API.Models.Foundation.Guests.Exceptions
{ 
    public class LockedGuestException: Xeption
    {
        public LockedGuestException(Exception innerException)
            : base(message: "Guest is locked and cannot be modified. Please try again later", innerException)
        { }
    }
}
