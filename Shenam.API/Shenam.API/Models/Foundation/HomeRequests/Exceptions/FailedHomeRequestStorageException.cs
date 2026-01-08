//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Microsoft.Data.SqlClient;
using System;
using Xeptions;

namespace Shenam.API.Models.Foundation.HomeRequests.Exceptions
{
    public class FailedHomeRequestStorageException:Xeption
    {
        //private readonly SqlException sqlException;

        public FailedHomeRequestStorageException(Exception innerException)
            : base(message: "Failed home request storage error occurred, contact support",
                  innerException)
        { }

        //public FailedHomeRequestStorageException(SqlException sqlException)
        //{
        //    this.sqlException = sqlException;
        //}
    }
}
