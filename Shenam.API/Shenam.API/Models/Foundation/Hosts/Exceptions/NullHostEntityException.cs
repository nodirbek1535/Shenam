//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System.Security.Cryptography.X509Certificates;
using Xeptions;

namespace Shenam.API.Models.Foundation.Hosts.Exceptions
{
    public class NullHostEntityException:Xeption
    {
        public NullHostEntityException()
        : base(message: "Host entity is null")
        {}
    }
}
