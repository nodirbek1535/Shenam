//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Xeptions;

namespace Shenam.API.Models.Foundation.Guests.Exceptions
{
    public class InvalidGuestException:Xeption
    {
        public InvalidGuestException()
            : base(message: "Guest is invalid")
        { }
    }
}
