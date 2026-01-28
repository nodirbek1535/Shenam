//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using Xeptions;

namespace Shenam.API.Models.Foundation.Hosts.Exceptions
{
    public class LockedHostEntityException : Xeption
    {
        public LockedHostEntityException(Exception innerException)
            : base(message: "HostEntity is locked and cannot be modified. Please try again later",
                 innerException)
        { }
    }
}
