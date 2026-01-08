//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Shenam.API.Models.Foundation.Homes;
using System.Threading.Tasks;

namespace Shenam.API.Services.Foundations.Homes
{
    public interface IHomeService
    {
        ValueTask<Home> AddHomeAsync(Home home);
    }
}
