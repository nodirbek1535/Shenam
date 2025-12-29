//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Xeptions;

namespace Shenam.API.Models.Foundation.HomeRequests.Exceptions
{
    public class HomeRequestValidationException:Xeption
    {
        public HomeRequestValidationException(Xeption innerExceprion)
            : base(message: "HomeRequest validation error occurred, fix the errors and try again",
                 innerExceprion)
        { }
    }
}
