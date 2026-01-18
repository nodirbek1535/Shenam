//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using Xeptions;

namespace Shenam.API.Models.Foundation.Homes.Exceptions
{
    public class NotFoundHomeException:Xeption
    {
        public NotFoundHomeException(Guid homeId)
            : base(message: $"Home with id: {homeId} is not found")
        { }
    }
}
