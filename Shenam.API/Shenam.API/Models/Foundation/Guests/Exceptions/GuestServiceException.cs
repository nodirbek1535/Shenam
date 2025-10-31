//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using Xeptions;

namespace Shenam.API.Models.Foundation.Guests.Exceptions
{
    public class GuestServiceException : Xeption
    {
        public GuestServiceException(Exception innerexcception)
            : base(message: "Guest service error occurred, contact support",
                  innerexcception)
        { }
    }
}
