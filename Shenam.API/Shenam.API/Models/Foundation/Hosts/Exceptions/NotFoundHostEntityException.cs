//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using Xeptions;

namespace Shenam.API.Models.Foundation.Hosts.Exceptions
{
    public class NotFoundHostEntityException:Xeption
    {
        public NotFoundHostEntityException(Guid hostEntityId)
            : base($"Host entity with id: {hostEntityId} is not found")
        { }
    }
}
