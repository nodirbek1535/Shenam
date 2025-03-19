//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Moq;
using Shenam.API.Brokers.Storages;
using Shenam.API.Models.Foundation.Guests;
using Shenam.API.Services.Foundations.Guests;
using Tynamix.ObjectFiller;

namespace Shenam.Api.Tests.Unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly IGuestService guestService;

        public GuestServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();

            this.guestService =
                new GuestService(storageBroker: this.storageBrokerMock.Object);
        }

        private static Guest CreateRandomGuest() =>
            CreateGuestFiller(date: GetRandomDateTimeoffset()).Create();

        private static DateTimeOffset GetRandomDateTimeoffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<Guest> CreateGuestFiller(DateTimeOffset date)
        {
            var filler = new Filler<Guest>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(date);

            return filler;

        }
    }
}
