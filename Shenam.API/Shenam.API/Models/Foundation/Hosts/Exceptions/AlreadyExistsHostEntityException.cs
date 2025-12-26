//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using Xeptions;

namespace Shenam.API.Models.Foundation.Hosts.Exceptions
{
    public class AlreadyExistsHostEntityException : Xeption
    {
        public AlreadyExistsHostEntityException(Exception innerException)
            : base(message: "HostEntity Already exists", innerException)
        { }
    }
}
