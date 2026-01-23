//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Microsoft.Data.SqlClient;
using Moq;
using Shenam.API.Models.Foundation.Hosts;
using Shenam.API.Models.Foundation.Hosts.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shenam.Api.Tests.Unit.Services.Foundations.Hosts
{
    public partial class HostEntityServiceTests
    {
        [Fact]
        public async Task ShouldThrowSqlExceptionOnRetrieveByIdIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Guid someHostEntityId = Guid.NewGuid();
            SqlException sqlException = GetSqlError();

            var failedStorageHostEntityException =
                new FailedHostEntityStorageException(sqlException);

            var expectedHostEntityDependencyException =
                new HostEntityDependencyException(failedStorageHostEntityException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHostEntityByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<HostEntity> retrieveHostEntityByIdTask =
                this.hostEntityService.RetrieveHostEntityByIdAsync(someHostEntityId);

            // then
            await Assert.ThrowsAsync<HostEntityDependencyException>(
                retrieveHostEntityByIdTask.AsTask);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHostEntityByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(
                    SameExceptionAs(expectedHostEntityDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveByIdIfServiceErrorOccursAndLogItAsync()
        {
            // given
            Guid someHostEntityId = Guid.NewGuid();
            var serviceException = new Exception();

            var failedHostEntityServiceException =
                new FailedHostEntityServiceException(serviceException);

            var expectedHostEntityServiceException =
                new HostEntityServiceException(failedHostEntityServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHostEntityByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<HostEntity> retrieveHostEntityByIdTask =
                this.hostEntityService.RetrieveHostEntityByIdAsync(someHostEntityId);

            // then
            await Assert.ThrowsAsync<HostEntityServiceException>(
                retrieveHostEntityByIdTask.AsTask);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHostEntityByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(
                    SameExceptionAs(expectedHostEntityServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
