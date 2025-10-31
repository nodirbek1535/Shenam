//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using Xeptions;

namespace Shenam.API.Models.Foundation.Guests.Exceptions
{
    public class FailedGuestServiceException : Xeption
    {
        public FailedGuestServiceException(Exception innerException)
            : base(message: "Guest service error occurred, contact support",
                  innerException)
        { }

    }
}
