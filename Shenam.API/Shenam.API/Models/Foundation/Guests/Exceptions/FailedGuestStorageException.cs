//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using Microsoft.Data.SqlClient;
using Xeptions;

namespace Shenam.API.Models.Foundation.Guests.Exceptions
{
    public class FailedGuestStorageException: Xeption
    {
        private SqlException sqlException;

        public FailedGuestStorageException(Xeption innerException)
            : base(message: "Failed guest storage error occurred, contact support", 
                  innerException)
        { }

        public FailedGuestStorageException(SqlException sqlException)
        {
            this.sqlException = sqlException;
        }
    }
}
