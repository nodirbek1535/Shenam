//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Xeptions;

namespace Shenam.API.Models.Foundation.Hosts.Exceptions
{
    public class HostEntityDependencyValidationException:Xeption
    {
        public HostEntityDependencyValidationException(Xeption innerException)
            : base(message: "Host entity dependency validation error occured, fix the errors and try again",
                 innerException)
        { }
    }
}
