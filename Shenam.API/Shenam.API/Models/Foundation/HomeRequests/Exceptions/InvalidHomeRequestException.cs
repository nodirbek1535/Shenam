//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Xeptions;

namespace Shenam.API.Models.Foundation.HomeRequests.Exceptions
{
    public class InvalidHomeRequestException : Xeption
    {
        public InvalidHomeRequestException()
            : base(message: "HomeRequest is invalid")
        {}
    }
}
