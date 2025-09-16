using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Shenam.API.Models.Foundation.Guests;
using Shenam.API.Models.Foundation.Guests.Exceptions;
using Xunit.Sdk;

namespace Shenam.Api.Tests.Unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfGuestIsNullAndLogItAsync()
        {
            //given 
            Guest nullGuest = null;
            var nullGuestException = new NullGuestException();

            var expectedGuestValidtionException = 
                new GuestValidationException(nullGuestException);

            //when
            ValueTask<Guest> addGuestTask =
                this.guestService.AddGuestAsync(nullGuest);

            //then
            await Assert.ThrowsAsync<GuestValidationException>(() =>
                addGuestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedGuestValidtionException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGuestAsync(It.IsAny<Guest>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
