//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Xeptions;

namespace Shenam.API.Models.Foundation.Hosts.Exceptions
{
    public class NullHostEntityException : Xeption
    {
        public NullHostEntityException()
        : base(message: "Host entity is null")
        { }
    }
}
