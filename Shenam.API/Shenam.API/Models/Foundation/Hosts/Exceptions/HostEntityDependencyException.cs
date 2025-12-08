//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Xeptions;

namespace Shenam.API.Models.Foundation.Hosts.Exceptions
{
    public class HostEntityDependencyException:Xeption
    {
        public HostEntityDependencyException(Xeption innerException)
            : base(message: "Host entity dependency error occurred, contact support",
                  innerException)
        { }
    }
}
