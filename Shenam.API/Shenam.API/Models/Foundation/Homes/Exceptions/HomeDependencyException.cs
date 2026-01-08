//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Xeptions;

namespace Shenam.API.Models.Foundation.Homes.Exceptions
{
    public class HomeDependencyException : Xeption
    {
        public HomeDependencyException(Xeption innerException)
            : base("Home dependency error occurred, contact support",
                  innerException)
        {
        }
    }
}
