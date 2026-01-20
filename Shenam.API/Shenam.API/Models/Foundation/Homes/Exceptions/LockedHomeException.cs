//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using Xeptions;

namespace Shenam.API.Models.Foundation.Homes.Exceptions
{
    public class LockedHomeException:Xeption
    {
        public LockedHomeException(Exception innerException)
            : base(message: "Home is locked and connot be modified. Please try again later",
                 innerException)
        { }
    }
}
