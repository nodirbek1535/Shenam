//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Moq;
using Shenam.API.Brokers.Storages;
using Shenam.API.Models.Foundation.HomeRequests;
using Shenam.API.Services.Foundations.HomeRequests;
using Tynamix.ObjectFiller;

namespace Shenam.Api.Tests.Unit.Services.Foundations.HomeRequests
{
    public partial class HomeRequestTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly IHomeRequestService homeRequestService;

        public HomeRequestTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();

            this.homeRequestService = new HomeRequestService(
                storageBroker: this.storageBrokerMock.Object);
        }

        private static HomeRequest CreateRandomHomeRequest() =>
            CreateHomeRequestFiller.Create(); 

        private static Filler<HomeRequest> CreateHomeRequestFiller =>
            new Filler<HomeRequest>();
    }
}
