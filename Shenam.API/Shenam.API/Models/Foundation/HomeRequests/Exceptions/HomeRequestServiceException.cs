//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using Xeptions;

namespace Shenam.API.Models.Foundation.HomeRequests.Exceptions
{
    public class HomeRequestServiceException:Xeption
    {
        public HomeRequestServiceException(Exception innerException)
            : base(message: "Home request service error occurred, contact support", innerException)
        { }
    }
}
