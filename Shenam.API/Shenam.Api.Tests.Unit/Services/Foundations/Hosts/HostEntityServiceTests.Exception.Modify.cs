//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyIfSqlErrorOccursAndLogItAsync()
        {
            //given
            HostEntity someHostEntity = CreateRandomHostEntity();
            Guid hostEntityId = someHostEntity.Id;
            SqlException sqlException = GetSqlError();

            var failedHostEntityStorageException =
                new FailedHostEntityStorageException(sqlException);

            var expectedHostEntityDependencyException =
                new HostEntityDependencyException(failedHostEntityStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHostEntityByIdAsync(hostEntityId))
                    .ThrowsAsync(sqlException);

            //when
            ValueTask<HostEntity> modifyHostEntityTask =
                this.hostEntityService.ModifyHostEntityAsync(someHostEntity);

            //then
            await Assert.ThrowsAsync<HostEntityDependencyException>(() =>
                modifyHostEntityTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHostEntityByIdAsync(hostEntityId),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(
                    It.Is(SameExceptionAs(expectedHostEntityDependencyException))),
                        Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnModifyIfDbUpdateConcurrencyoccursAndLogItAsync()
        {
            //given
            HostEntity someHostEntity = CreateRandomHostEntity();
            Guid hostEntityId = someHostEntity.Id;
            var dbUpdateConcurencyException = new DbUpdateConcurrencyException();

            var lockedHostEntityExceotion =
                new LockedHostEntityException(dbUpdateConcurencyException);

            var expectedHostEntityDependencyValidationException =
                new HostEntityDependencyValidationException(lockedHostEntityExceotion);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHostEntityByIdAsync(hostEntityId))
                    .ThrowsAsync(dbUpdateConcurencyException);

            //when
            ValueTask<HostEntity> mofifyHostEntityTask =
                this.hostEntityService.ModifyHostEntityAsync(someHostEntity);

            //then
            await Assert.ThrowsAsync<HostEntityDependencyValidationException>(() =>
                mofifyHostEntityTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHostEntityByIdAsync(hostEntityId),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(
                    It.Is(SameExceptionAs(expectedHostEntityDependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
