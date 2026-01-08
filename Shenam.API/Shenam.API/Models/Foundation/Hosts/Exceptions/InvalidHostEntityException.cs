//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Xeptions;

namespace Shenam.API.Models.Foundation.Hosts.Exceptions
{
    public class InvalidHostEntityException : Xeption
    {
        public InvalidHostEntityException()
        : base(message: "Host entity is invalid")
        { }
    }
}
