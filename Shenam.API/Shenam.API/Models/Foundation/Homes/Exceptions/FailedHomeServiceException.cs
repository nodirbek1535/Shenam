//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================


using System;
using Xeptions;

namespace Shenam.API.Models.Foundation.Homes.Exceptions
{
    public class FailedHomeServiceException:Xeption
    {
        public FailedHomeServiceException(Exception innerException)
            :base(message: "Failed home service error occurred, contact support",
                  innerException)
        {}
    }
}
