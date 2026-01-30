//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using FluentAssertions;
using Moq;
using Shenam.API.Models.Foundation.HomeRequests;
using Shenam.API.Models.Foundation.HomeRequests.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Shenam.Api.Tests.Unit.Services.Foundations.HomeRequests
{
    public partial class HomeRequestServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionIfHomeRequestIsNullAndLogItAsync()
        {
            // given
            HomeRequest nullHomeRequest = null;
            var nullHomeRequestException = new NullHomeRequestException();

            var expectedHomeRequestValidationException =
                new HomeRequestValidationException(nullHomeRequestException);

            // when
            ValueTask<HomeRequest> modifyHomeRequestTask =
                this.homeRequestService.ModifyHomeRequestAsync(nullHomeRequest);

            HomeRequestValidationException actualHomeRequestValidationException =
                await Assert.ThrowsAsync<HomeRequestValidationException>(
                    modifyHomeRequestTask.AsTask);

            // then
            object value = actualHomeRequestValidationException
                .SameExceptionAs(expectedHomeRequestValidationException)
                .Should().BeTrue();

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeRequestValidationException))),
                Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateHomeRequestAsync(It.IsAny<HomeRequest>()),
                Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfHomeRequestIsInvalidAndLogItAsync()
        {
            // given
            var invalidHomeRequest = new HomeRequest
            {
                Id = Guid.Empty
            };

            var invalidHomeRequestException = new InvalidHomeRequestException();

            invalidHomeRequestException.AddData(
                key: nameof(HomeRequest.Id),
                values: "Id is required");

            var expectedHomeRequestValidationException =
                new HomeRequestValidationException(invalidHomeRequestException);

            // when
            ValueTask<HomeRequest> modifyHomeRequestTask =
                this.homeRequestService.ModifyHomeRequestAsync(invalidHomeRequest);

            HomeRequestValidationException actualHomeRequestValidationException =
                await Assert.ThrowsAsync<HomeRequestValidationException>(
                    modifyHomeRequestTask.AsTask);

            // then
            actualHomeRequestValidationException
                .SameExceptionAs(expectedHomeRequestValidationException)
                .Should().BeTrue();

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeRequestValidationException))),
                Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateHomeRequestAsync(It.IsAny<HomeRequest>()),
                Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfHomeRequestDoesNotExistAndLogItAsync()
        {
            // given
            HomeRequest randomHomeRequest = CreateRandomHomeRequest();
            HomeRequest nonExistingHomeRequest = randomHomeRequest;
            HomeRequest nullHomeRequest = null;

            var notFoundHomeRequestException =
                new NotFoundHomeRequestException(nonExistingHomeRequest.Id);

            var expectedHomeRequestValidationException =
                new HomeRequestValidationException(notFoundHomeRequestException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeRequestByIdAsync(nonExistingHomeRequest.Id))
                    .ReturnsAsync(nullHomeRequest);

            // when
            ValueTask<HomeRequest> modifyHomeRequestTask =
                this.homeRequestService.ModifyHomeRequestAsync(nonExistingHomeRequest);

            HomeRequestValidationException actualHomeRequestValidationException =
                await Assert.ThrowsAsync<HomeRequestValidationException>(
                    modifyHomeRequestTask.AsTask);

            // then
            actualHomeRequestValidationException
                .SameExceptionAs(expectedHomeRequestValidationException)
                .Should().BeTrue();

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeRequestByIdAsync(nonExistingHomeRequest.Id),
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
