//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Moq;
using Shenam.API.Brokers.loggings;
using Shenam.API.Brokers.Storages;
using Shenam.API.Models.Foundation.Hosts;
using Shenam.API.Services.Foundations.Hosts;
using Tynamix.ObjectFiller;

namespace Shenam.Api.Tests.Unit.Services.Foundations.Hosts
{
    public partial class HostEntityServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly IHostEntityService hostEntityService;

        public HostEntityServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();

            this.hostEntityService =
                new HostEntityService(storageBroker: this.storageBrokerMock.Object);
        }
        
        private static HostEntity CreateRandomHostEntity() =>
            CreateHostEntityFiller(date: GetRandomDateTimeoffset()).Create();

        private static DateTimeOffset GetRandomDateTimeoffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();


        public static Filler<HostEntity> CreateHostEntityFiller(DateTimeOffset date)
        {
            var filler = new Filler<HostEntity>();

            filler.Setup()
                .OnProperty(host => host.DateOfBirth).Use(date);

            return filler;
        }
    }
}
