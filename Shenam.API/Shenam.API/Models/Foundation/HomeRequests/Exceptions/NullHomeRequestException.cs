//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Xeptions;

namespace Shenam.API.Models.Foundation.HomeRequests.Exceptions
{
    public class NullHomeRequestException:Xeption
    {
        public NullHomeRequestException()
            : base(message: "HomeRequest is null")
        { }
    }
}
