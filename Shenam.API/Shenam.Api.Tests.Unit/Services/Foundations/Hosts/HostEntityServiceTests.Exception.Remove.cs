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

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRemoveIfDbUpdateConcurrencyOccursAndLogItAsync()
        {
            //given
            Guid someHostEntityId = Guid.NewGuid();
            var dbUpdateConcurrencyException = new DbUpdateConcurrencyException();

            var lockedHostEntityException =
                new LockedHostEntityException(dbUpdateConcurrencyException);

            var expectedHostEntityDependencyValidationException =
                new HostEntityDependencyValidationException(lockedHostEntityException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHostEntityByIdAsync(someHostEntityId))
                    .ThrowsAsync(dbUpdateConcurrencyException);

            //when
            ValueTask<HostEntity> removeHostEntityTask =
                this.hostEntityService.RemoveHostEntityByIdAsync(someHostEntityId);

            //then
            await Assert.ThrowsAsync<HostEntityDependencyValidationException>(() =>
                removeHostEntityTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHostEntityDependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHostEntityByIdAsync(someHostEntityId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRemoveIfDbUpdateErrorOccursAndLogItAsync()
        {
            //given
            Guid someHostEntityId = Guid.NewGuid();
            var dbUpdateException = new DbUpdateException();

            var failedHostEntityException =
                new FailedHostEntityStorageException(dbUpdateException);

            var expectedHostEntityDependencyException =
                new HostEntityDependencyException(failedHostEntityException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHostEntityByIdAsync(someHostEntityId))
                    .ThrowsAsync(dbUpdateException);

            //when
            ValueTask<HostEntity> removeHostEntityTask =
                this.hostEntityService.RemoveHostEntityByIdAsync(someHostEntityId);

            //then
            await Assert.ThrowsAsync<HostEntityDependencyException>(() =>
                removeHostEntityTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
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
