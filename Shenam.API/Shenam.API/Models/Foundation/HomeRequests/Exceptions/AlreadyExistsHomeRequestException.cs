//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using Xeptions;

namespace Shenam.API.Models.Foundation.HomeRequests.Exceptions
{
    public class AlreadyExistsHomeRequestException:Xeption
    {
        public AlreadyExistsHomeRequestException(Exception innerException)
            : base(message: "Home request with the same id already exists", innerException)
        { }
    }
}
