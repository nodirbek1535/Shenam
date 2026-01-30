//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Moq;
using Shenam.API.Models.Foundation.HomeRequests;
using Shenam.API.Models.Foundation.HomeRequests.Exceptions;
using Shenam.API.Models.Foundation.Hosts;
using Shenam.API.Models.Foundation.Hosts.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shenam.Api.Tests.Unit.Services.Foundations.HomeRequests
{
    public partial class HomeRequestServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRemoveIfHomeRequestIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidHomeRequestId = Guid.Empty;

            var invalidHomeRequestException =
                new InvalidHomeRequestException();

            invalidHomeRequestException.AddData(
                key: nameof(HomeRequest.Id),
                values: "Id is required");

            var expectedHomeRequestValidationException =
                new HomeRequestValidationException(invalidHomeRequestException);

            // when
            ValueTask<HomeRequest> removeHomeRequestTask =
                this.homeRequestService.RemoveHomeRequestByIdAsync(invalidHomeRequestId);

            // then
            await Assert.ThrowsAsync<HomeRequestValidationException>(() =>
                removeHomeRequestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeRequestValidationException))),
                Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeRequestByIdAsync(It.IsAny<Guid>()),
                Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        //bu 1 - commit bolib qolgan uni ozgartirish kerak
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRemoveIfHomeRequestDoesNotExistAndLogItAsync()
        {
            // given
            Guid randomHomeRequestId = Guid.NewGuid();
            Guid inputHomeRequestId = randomHomeRequestId;
            HomeRequest noHomeRequest = null;

            var notFoundHomeRequestException =
                new NotFoundHomeRequestException(inputHomeRequestId);

            var expectedHomeRequestValidationException =
                new HomeRequestValidationException(notFoundHomeRequestException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeRequestByIdAsync(inputHomeRequestId))
                    .ReturnsAsync(noHomeRequest);

            // when
            ValueTask<HomeRequest> removeHomeRequestTask =
                this.homeRequestService.RemoveHomeRequestByIdAsync(inputHomeRequestId);

            // then
            await Assert.ThrowsAsync<HomeRequestValidationException>(() =>
                removeHomeRequestTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeRequestByIdAsync(inputHomeRequestId),
                Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeRequestValidationException))),
                Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
