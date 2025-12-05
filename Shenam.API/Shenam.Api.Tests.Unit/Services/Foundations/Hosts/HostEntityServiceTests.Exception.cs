//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

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
    }
}
