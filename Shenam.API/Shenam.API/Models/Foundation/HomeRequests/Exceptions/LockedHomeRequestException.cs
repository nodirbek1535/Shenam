//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using Xeptions;

namespace Shenam.API.Models.Foundation.HomeRequests.Exceptions
{
    public class LockedHomeRequestException:Xeption
    {
        public LockedHomeRequestException(Exception innerException)
            : base(message: "Home request is locked, please try again later", 
                  innerException)
        { } 
    }
}
