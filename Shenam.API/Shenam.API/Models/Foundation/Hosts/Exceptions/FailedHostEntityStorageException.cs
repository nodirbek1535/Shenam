//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Microsoft.Data.SqlClient;
using Xeptions;

namespace Shenam.API.Models.Foundation.Hosts.Exceptions
{
    public class FailedHostEntityStorageException : Xeption
    {
        private SqlException sqlException;

        public FailedHostEntityStorageException(Xeption innerException)
            : base(message: "Failed hostEntity storage error occurred, contact support",
                 innerException)
        { }

        public FailedHostEntityStorageException(SqlException sqlException)
        {
            this.sqlException = sqlException;
        }
    }
}
