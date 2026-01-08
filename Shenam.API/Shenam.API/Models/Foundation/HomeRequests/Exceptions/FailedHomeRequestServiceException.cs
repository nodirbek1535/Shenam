//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using Xeptions;

namespace Shenam.API.Models.Foundation.HomeRequests.Exceptions
{
    public class FailedHomeRequestServiceException:Xeption
    {
        public FailedHomeRequestServiceException(Exception innerException)
            : base(message: "Home request service error occurred, contact support", innerException)
        { }
    }
}
