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
        public async Task ShouldThrowCriticalDependencyExceptionOnRemoveIfSqlErrorOccursAndLogItAsync()
        {
            //given
            Guid someHostEntityId = Guid.NewGuid();
            SqlException sqlException = GetSqlError();

            var failedHostEntityStorageException =
                new FailedHostEntityStorageException(sqlException);

            var expectedHostEntityDependencyException =
                new HostEntityDependencyException(failedHostEntityStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHostEntityByIdAsync(someHostEntityId))
                    .ThrowsAsync(sqlException);

            //when
            ValueTask<HostEntity> removeHostEntityTask =
                this.hostEntityService.RemoveHostEntityByIdAsync(someHostEntityId);

            //then
            await Assert.ThrowsAsync<HostEntityDependencyException>(() =>
                removeHostEntityTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedHostEntityDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHostEntityByIdAsync(someHostEntityId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
