//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Xeptions;

namespace Shenam.API.Models.Foundation.Hosts.Exceptions
{
    public class HostEntityServiceException : Xeption
    {
        public HostEntityServiceException(Xeption innerException)
            : base(message: "Host entity service error occured, contact support",
                 innerException)
        { }
    }
}
