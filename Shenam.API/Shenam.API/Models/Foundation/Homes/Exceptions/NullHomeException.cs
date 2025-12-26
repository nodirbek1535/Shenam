//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Xeptions;

namespace Shenam.API.Models.Foundation.Homes.Exceptions
{
    public class NullHomeException:Xeption
    {
        public NullHomeException()
            : base(message: "Home is null")
        { }
    }
}
