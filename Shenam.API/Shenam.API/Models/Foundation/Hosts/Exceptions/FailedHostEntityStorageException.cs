//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Microsoft.Data.SqlClient;
using System;
using Xeptions;

namespace Shenam.API.Models.Foundation.Hosts.Exceptions
{
    public class FailedHostEntityStorageException : Xeption
    {
        public FailedHostEntityStorageException(Exception innerException)
            : base(message: "Failed hostEntity storage error occurred, contact support",
                 innerException)
        { }
    }
}
