//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Microsoft.Data.SqlClient;
using System;
using Xeptions;

namespace Shenam.API.Models.Foundation.Homes.Exceptions
{
    public class FailedHomeStorageException: Xeption
    {
        public FailedHomeStorageException(Exception innerException)
            : base("Failed home storage error occurred, contact support", 
                  innerException)
        {}

    }
}
