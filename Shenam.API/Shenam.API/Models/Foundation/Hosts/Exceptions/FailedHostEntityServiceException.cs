//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using Xeptions;

namespace Shenam.API.Models.Foundation.Hosts.Exceptions
{
    public class FailedHostEntityServiceException : Xeption
    {
        public FailedHostEntityServiceException(Exception innerException)
            : base(message: "Failed host entity service error occured, contact support",
                 innerException)
        { }
    }
}
