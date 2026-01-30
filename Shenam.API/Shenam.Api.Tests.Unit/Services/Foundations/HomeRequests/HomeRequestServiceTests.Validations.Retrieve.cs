//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Moq;
using Shenam.API.Models.Foundation.HomeRequests;
using Shenam.API.Models.Foundation.HomeRequests.Exceptions;
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
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfIdIsInvalidAndLogItAsync()
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
            ValueTask<HomeRequest> retrieveHomeRequestById =
                this.homeRequestService.RetrieveHomeRequestByIdAsync(invalidHomeRequestId);

            // then
            await Assert.ThrowsAsync<HomeRequestValidationException>(() =>
                retrieveHomeRequestById.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeRequestValidationException))),
                Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeRequestByIdAsync(It.IsAny<Guid>()),
                Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowNotFoundExceptionOnRetrieveByIdIfHomeRequestIsNotFoundAndLogItAsync()
        {
            // given
            Guid someHomeRequestId = Guid.NewGuid();
            HomeRequest noHomeRequest = null;

            var notFoundHomeRequestException =
                new NotFoundHomeRequestException(someHomeRequestId);

            var expectedHomeRequestValidationException =
                new HomeRequestValidationException(notFoundHomeRequestException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeRequestByIdAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(noHomeRequest);

            // when
            ValueTask<HomeRequest> retrieveHomeRequestByIdTask =
                this.homeRequestService.RetrieveHomeRequestByIdAsync(someHomeRequestId);

            // then
            await Assert.ThrowsAsync<HomeRequestValidationException>(
                retrieveHomeRequestByIdTask.AsTask);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeRequestByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(
                    SameExceptionAs(expectedHomeRequestValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
