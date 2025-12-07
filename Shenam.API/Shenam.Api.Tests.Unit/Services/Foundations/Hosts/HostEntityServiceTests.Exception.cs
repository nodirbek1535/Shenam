//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using Shenam.API.Models.Foundation.Guests.Exceptions;
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
                new AlreadyExistsGuestException(duplicateKeyException);

            var hostEntityDependencyValidationException =
                new HostEntityDependencyException(alreadyExistsHostEntityException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertHostEntityAsync(someHostEntity))
                    .ThrowsAsync(duplicateKeyException);

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
                broker.LogError(
                    It.Is(SameExceptionAs(hostEntityDependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
