//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using FluentAssertions;
using Moq;
using Shenam.API.Models.Foundation.Guests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shenam.Api.Tests.Unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveGuestByIdAsync()
        {
            // given
            Guid randomGuestId = Guid.NewGuid();
            Guid inputGuestId = randomGuestId;

            Guest randomGuest = CreateRandomGuest();
            Guest persistedGuest = randomGuest;
            Guest expectedGuest = persistedGuest;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectGuestByIdAsync(inputGuestId))
                    .ReturnsAsync(persistedGuest);

            // when
            Guest actualGuest =
                await this.guestService.RetrieveGuestByIdAsync(inputGuestId);

            // then
            actualGuest.Should().BeEquivalentTo(expectedGuest);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectGuestByIdAsync(inputGuestId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
