//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using Shenam.API.Models.Foundation.Hosts;
using Shenam.API.Models.Foundation.Hosts.Exceptions;

namespace Shenam.Api.Tests.Unit.Services.Foundations.Hosts
{
    public partial class HostEntityServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            //given 
            HostEntity someHostEntity = CreateRandomHostEntity();
            SqlException sqlException = GetSqlError();
            var failledHostEntityStorageException =
                new FailedHostEntityStorageException(sqlException);

            var expectedHostEntityDependencyException =
                new HostEntityDependencyException(failledHostEntityStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertHostEntityAsync(someHostEntity))
                    .ThrowsAsync(sqlException);

            //when
            ValueTask<HostEntity> addHostEntityTask =
                this.hostEntityService.AddHostEntityAsync(someHostEntity);

            //then
            await Assert.ThrowsAsync<HostEntityDependencyException>(() =>
                addHostEntityTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHostEntityAsync(someHostEntity),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(
                    It.Is(SameExceptionAs(expectedHostEntityDependencyException))),
                Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationOnAddIfDuplicateKeyErrorOccursAndLogItAsync()
        {
            //given
            HostEntity someHostEntity = CreateRandomHostEntity();
            string someMessage = GetRandomString();

            var duplicateKeyException =
                new DuplicateKeyException(someMessage);

            var alreadyExistsHostEntityException =
                new AlreadyExistsHostEntityException(duplicateKeyException);

            var hostEntityDependencyValidationException =
                new HostEntityDependencyValidationException(alreadyExistsHostEntityException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertHostEntityAsync(someHostEntity))
                    .ThrowsAsync(duplicateKeyException);

            //when
            ValueTask<HostEntity> addHostEntityTask =
                this.hostEntityService.AddHostEntityAsync(someHostEntity);

            //then
            await Assert.ThrowsAsync<HostEntityDependencyValidationException>(() =>
                addHostEntityTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHostEntityAsync(someHostEntity),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(
                    It.Is(SameExceptionAs(hostEntityDependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            //given
            HostEntity someHostEntity = CreateRandomHostEntity();
            var serviceException = new Exception();

            var failedHostEntityServiceException =
                new FailedHostEntityServiceException(serviceException);

            var expectedHostEntityServiceException =
                new HostEntityServiceException(failedHostEntityServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertHostEntityAsync(someHostEntity))
                    .ThrowsAsync(serviceException);

            //when
            ValueTask<HostEntity> addHostEntityTask =
                this.hostEntityService.AddHostEntityAsync(someHostEntity);

            //then
            await Assert.ThrowsAsync<HostEntityServiceException>(() =>
                addHostEntityTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHostEntityAsync(someHostEntity),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(
                    It.Is(SameExceptionAs(expectedHostEntityServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
