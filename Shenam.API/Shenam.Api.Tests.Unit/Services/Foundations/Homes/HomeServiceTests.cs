//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Moq;
using Shenam.API.Brokers.Storages;
using Shenam.API.Models.Foundation.Homes;
using Shenam.API.Services.Foundations.Homes;
using Tynamix.ObjectFiller;

namespace Shenam.Api.Tests.Unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly IHomeService homeService;

        public HomeServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();

            this.homeService = new HomeService(
                storageBroker: this.storageBrokerMock.Object);
        }

        private static Home CreateRandomHome() =>
            CreateHomeFiller().Create();

        private static Filler<Home> CreateHomeFiller() =>
            new Filler<Home>();
    }
}
