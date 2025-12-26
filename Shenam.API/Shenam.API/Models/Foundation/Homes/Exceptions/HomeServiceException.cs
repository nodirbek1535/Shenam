//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Xeptions;

namespace Shenam.API.Models.Foundation.Homes.Exceptions
{
    public class HomeServiceException:Xeption
    {
        public HomeServiceException(Xeption xeption)
            : base(message: "Home service error occurred, contact support",
                  xeption)
        { }
    }
}
