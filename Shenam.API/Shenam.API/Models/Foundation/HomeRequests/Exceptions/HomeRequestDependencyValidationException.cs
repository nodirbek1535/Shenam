//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Xeptions;

namespace Shenam.API.Models.Foundation.HomeRequests.Exceptions
{
    public class HomeRequestDependencyValidationException:Xeption
    {
        public HomeRequestDependencyValidationException(Xeption innerException)
            : base(message: "Home request dependency validation error occurred, fix the errors and try again", innerException)
        { }
    }
}
