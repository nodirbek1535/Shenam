//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using Xeptions;

namespace Shenam.API.Models.Foundation.Guests.Exceptions
{
    public class NullGuestException:Xeption
    {
        public NullGuestException()
        :base(message:"Guest is null")
        {}
    }
}
