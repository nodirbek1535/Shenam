//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;

namespace Shenam.API.Models.Foundation.HomeRequests
{
    public class HomeRequest
    {
        public Guid Id { get; set; }
        public Guid GuestId { get; set; }
        public Guid HomeId { get; set; }
        public string Message { get; set; } 
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}
