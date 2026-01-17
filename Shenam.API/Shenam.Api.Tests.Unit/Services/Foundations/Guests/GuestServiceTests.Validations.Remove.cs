//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Moq;
using Shenam.API.Models.Foundation.Guests;
using Shenam.API.Models.Foundation.Guests.Exceptions;
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
        public async Task ShouldThrowValidationExceptionOnRemoveIfGuestDoesNotExistAndLogItAsync()
        {
            // given
            Guid randomGuestId = Guid.NewGuid();
            Guid inputGuestId = randomGuestId;
            Guest noGuest = null;

            var notFoundGuestException =
                new NotFoundGuestException(inputGuestId);

            var expectedGuestValidationException =
                new GuestValidationException(notFoundGuestException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectGuestByIdAsync(inputGuestId))
                    .ReturnsAsync(noGuest);

            // when
            ValueTask<Guest> removeGuestTask =
                this.guestService.RemoveGuestByIdAsync(inputGuestId);

            // then
            await Assert.ThrowsAsync<GuestValidationException>(
                () => removeGuestTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectGuestByIdAsync(inputGuestId),
                Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuestValidationException))),
                Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

    }
}
