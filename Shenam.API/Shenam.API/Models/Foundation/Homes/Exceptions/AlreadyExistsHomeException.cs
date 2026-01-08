//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using Xeptions;

namespace Shenam.API.Models.Foundation.Homes.Exceptions
{
    public class AlreadyExistsHomeException:Xeption
    {
        public AlreadyExistsHomeException(Exception innerException)
            : base("Home already exists.", innerException)
        {
        }
    }
}
