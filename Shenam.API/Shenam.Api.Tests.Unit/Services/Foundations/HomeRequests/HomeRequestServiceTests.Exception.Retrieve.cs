//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Microsoft.Data.SqlClient;
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
        public async Task ShouldThrowSqlExceptionOnRetrieveByIdIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Guid someHomeRequestId = Guid.NewGuid();
            SqlException sqlException = GetSqlError();

            var failedStorageHomeRequestException =
                new FailedHomeRequestStorageException(sqlException);

            var expectedHomeRequestDependencyException =
                new HomeRequestDependencyException(failedStorageHomeRequestException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeRequestByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<HomeRequest> retrieveHomeRequestByIdTask =
                this.homeRequestService.RetrieveHomeRequestByIdAsync(someHomeRequestId);

            // then
            await Assert.ThrowsAsync<HomeRequestDependencyException>(
                retrieveHomeRequestByIdTask.AsTask);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeRequestByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(
                    SameExceptionAs(expectedHomeRequestDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveByIdIfServiceErrorOccursAndLogItAsync()
        {
            // given
            Guid someHomeRequestId = Guid.NewGuid();
            var serviceException = new Exception();

            var failedHomeRequestServiceException =
                new FailedHomeRequestServiceException(serviceException);

            var expectedHomeRequestServiceException =
                new HomeRequestServiceException(failedHomeRequestServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeRequestByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<HomeRequest> retrieveHomeRequestByIdTask =
                this.homeRequestService.RetrieveHomeRequestByIdAsync(someHomeRequestId);

            // then
            await Assert.ThrowsAsync<HomeRequestServiceException>(
                retrieveHomeRequestByIdTask.AsTask);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeRequestByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(
                    SameExceptionAs(expectedHomeRequestServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
