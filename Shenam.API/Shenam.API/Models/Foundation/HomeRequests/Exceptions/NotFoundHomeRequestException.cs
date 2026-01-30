//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using Xeptions;

namespace Shenam.API.Models.Foundation.HomeRequests.Exceptions
{
    public class NotFoundHomeRequestException:Xeption
    {
        public NotFoundHomeRequestException(Guid homeRequestId)
            : base(message: $"Home request with id: {homeRequestId} is not found")
        { }
    }
}
