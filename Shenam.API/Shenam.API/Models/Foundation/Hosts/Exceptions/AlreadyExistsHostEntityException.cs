//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Xeptions;

namespace Shenam.API.Models.Foundation.Hosts.Exceptions
{
    public class AlreadyExistsHostEntityException:Xeption
    {
        public AlreadyExistsHostEntityException(Xeption innerException)
            : base(message: "HostEntity Already exists", innerException)
        { }
    }
}
