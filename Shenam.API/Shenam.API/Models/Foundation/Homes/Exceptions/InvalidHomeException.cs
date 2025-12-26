//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System.Net.Http;
using Xeptions;

namespace Shenam.API.Models.Foundation.Homes.Exceptions
{
    public class InvalidHomeException:Xeption
    {
        public InvalidHomeException()
            : base(message: "Home is invalid")
        { }
    }
}
