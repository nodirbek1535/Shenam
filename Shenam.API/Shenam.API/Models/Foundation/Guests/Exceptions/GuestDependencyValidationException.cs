//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Xeptions;

namespace Shenam.API.Models.Foundation.Guests.Exceptions
{
    public class GuestDependencyValidationException:Xeption
    {
        public GuestDependencyValidationException(Xeption innerException)
            : base(message: "Guest dependency validation error occurred, fix the errors and try again",
                  innerException)
        { }
    }
}
