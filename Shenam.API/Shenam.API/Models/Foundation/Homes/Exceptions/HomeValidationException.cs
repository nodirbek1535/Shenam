//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Xeptions;

namespace Shenam.API.Models.Foundation.Homes.Exceptions
{
    public class HomeValidationException:Xeption
    {
        public HomeValidationException(Xeption innerException)
            : base(message: "Home validation error occurred, fix the errors and try again", 
                  innerException)
        { }
    }
}
