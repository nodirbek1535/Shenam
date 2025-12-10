//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;

namespace Shenam.API.Models.Foundation.Homes
{
    public class Home
    {
        public Guid Id { get; set; }
        public Guid HostId { get; set; }
        public string Address { get; set; }
        public string AdditionalInfo { get; set; }
        public bool IsVacant { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public double Area { get; set; }
        public bool IsPAllowed { get; set; }
        public TypeHome HomeType { get; set; }
        public decimal Price { get; set; }
        public bool IsShared { get; set; }
    }
}
