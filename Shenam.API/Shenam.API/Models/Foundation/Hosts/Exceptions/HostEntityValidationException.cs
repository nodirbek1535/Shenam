//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Xeptions;

namespace Shenam.API.Models.Foundation.Hosts.Exceptions
{
    public class HostEntityValidationException:Xeption
    {
        public HostEntityValidationException(Xeption innerException)
            :base(message: "Host entity validation error occurred, fix the errors and try again",
              innerException)
        { }
    }
}
