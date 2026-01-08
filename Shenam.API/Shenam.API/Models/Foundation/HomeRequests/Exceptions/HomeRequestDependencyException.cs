//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Xeptions;

namespace Shenam.API.Models.Foundation.HomeRequests.Exceptions
{
    public class HomeRequestDependencyException:Xeption
    {
        public HomeRequestDependencyException(Xeption innerException)
            : base(message: "Home request dependency error occurred, contact support",
                  innerException)
        { }
    }
}
