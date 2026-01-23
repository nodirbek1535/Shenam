//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shenam.API.Models.Foundation.Homes;
using Shenam.API.Models.Foundation.Homes.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shenam.Api.Tests.Unit.Services.Foundations.Homes
{
    public  partial class HomeServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRemoveIfSqlErrorOccursAndLogItAsync()
        {
            //given
            Guid someHomeId = Guid.NewGuid();
            SqlException sqlException = GetSqlError();

            var failedHomeStorageException = 
                new FailedHomeStorageException(sqlException);

            var expectedHomeDependencyException =
                new HomeDependencyException(failedHomeStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeByIdAsync(someHomeId))
                    .ThrowsAsync(sqlException);

            //when
            ValueTask<Home> removeHomeTask =
                this.homeService.RemoveHomeByIdAsync(someHomeId);

            //then
            await Assert.ThrowsAsync<HomeDependencyException>(() =>
                removeHomeTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedHomeDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeByIdAsync(someHomeId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRemoveIfDbUpdateConcurrencyOccursAndLogItAsync()
        {
            //given
            Guid someHomeId = Guid.NewGuid();
            var dbUpdateConcurrencyException = new DbUpdateConcurrencyException();

            var lockedHomeException =
                new LockedHomeException(dbUpdateConcurrencyException);

            var expectedHomeDependencyValidationException =
                new HomeDependencyValidationException(lockedHomeException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeByIdAsync(someHomeId))
                    .ThrowsAsync(dbUpdateConcurrencyException);

            //when
            ValueTask<Home> removeHomeTask =
                this.homeService.RemoveHomeByIdAsync(someHomeId);

            //then
            await Assert.ThrowsAsync<HomeDependencyValidationException>(() =>
                removeHomeTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeDependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeByIdAsync(someHomeId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
