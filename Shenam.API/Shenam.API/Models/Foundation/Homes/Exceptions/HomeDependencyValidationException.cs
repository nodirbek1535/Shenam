//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Xeptions;

namespace Shenam.API.Models.Foundation.Homes.Exceptions
{
    public class HomeDependencyValidationException:Xeption
    {
        public HomeDependencyValidationException(Xeption innerException)
            : base(message: "Home dependency validation error occurred, fix the errors and try again",
                  innerException)
        { }
    }
}
