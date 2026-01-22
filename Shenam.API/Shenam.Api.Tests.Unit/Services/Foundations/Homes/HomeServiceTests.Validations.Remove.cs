//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Moq;
using Shenam.API.Models.Foundation.Homes;
using Shenam.API.Models.Foundation.Homes.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shenam.Api.Tests.Unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRemoveIfHomeDoesNotExistAndLogItAsync()
        {
            //given
            Guid randomHomeId = Guid.NewGuid();
            Guid inputHomeId = randomHomeId;
            Home noHome = null;

            var notFoundHomeException =
                new NotFoundHomeException(inputHomeId);

            var expectedHomeValidationException =
                new HomeValidationException(notFoundHomeException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeByIdAsync(inputHomeId))
                    .ReturnsAsync(noHome);

            //when
            ValueTask<Home> removeHomeTask =
                this.homeService.RemoveHomeByIdAsync(inputHomeId);

            //then
            await Assert.ThrowsAsync<HomeValidationException>(() =>
                removeHomeTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeByIdAsync(inputHomeId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
