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
        public async Task ShouldRemoveGuestByIdAsync()
        {
            // given
            Guest randomGuest = CreateRandomGuest();
            Guest storedGuest = randomGuest;
            Guest deletedGuest = storedGuest;
            Guid guestId = storedGuest.Id;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectGuestByIdAsync(guestId))
                    .ReturnsAsync(storedGuest);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteGuestAsync(storedGuest))
                    .ReturnsAsync(deletedGuest);

            // when
            Guest actualGuest =
                await this.guestService.RemoveGuestByIdAsync(guestId);

            // then
            actualGuest.Should().BeEquivalentTo(deletedGuest);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectGuestByIdAsync(guestId),
                Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteGuestAsync(storedGuest),
                Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

    }
}
