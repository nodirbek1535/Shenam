//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using Xeptions;

namespace Shenam.API.Models.Foundation.Guests.Exceptions
{
    public class AlreadyExistsGuestException : Xeption
    {
        public AlreadyExistsGuestException(Exception innerException)
            : base(message: "Guest  already exists", innerException)
        { }
    }
}
