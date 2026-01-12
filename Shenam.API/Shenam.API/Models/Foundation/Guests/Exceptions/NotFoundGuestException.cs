//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using Xeptions;

namespace Shenam.API.Models.Foundation.Guests.Exceptions
{
    public class NotFoundGuestException:Xeption
    {
        public NotFoundGuestException(Guid guestId)
            : base(message: $"Guest with id: {guestId} is not found")
        { }
    }
}
