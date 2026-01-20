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
    public partial class HomeServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyIfSqlErrorOccursAndLogItAsync()
        {
            //given
            Home someHome = CreateRandomHome();
            Guid homeId = someHome.Id;
            SqlException sqlException = GetSqlError();

            var failedHomeStorageException =
                new FailedHomeStorageException(sqlException);

            var expectedHomeDependencyException =
                new HomeDependencyException(failedHomeStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeByIdAsync(homeId))
                    .ThrowsAsync(sqlException);

            //when
            ValueTask<Home> modifyHomeTask =
                this.homeService.ModifyHomeAsync(someHome);

            //then
            await Assert.ThrowsAsync<HomeDependencyException>(() =>
                modifyHomeTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeByIdAsync(homeId),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(
                    It.Is(SameExceptionAs(expectedHomeDependencyException))),
                        Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnModifyIfDbUpdateConcurrencyoccursAndLogItAsync()
        {
            //given
            Home someHome = CreateRandomHome();
            Guid homeId = someHome.Id;
            var dbUpdateConcurencyException = new DbUpdateConcurrencyException();

            var lockedHomeExceotion =
                new LockedHomeException(dbUpdateConcurencyException);

            var expectedHomeDependencyValidationException =
                new HomeDependencyValidationException(lockedHomeExceotion);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeByIdAsync(homeId))
                    .ThrowsAsync(dbUpdateConcurencyException);

            //when
            ValueTask<Home> mofifyHomeTask =
                this.homeService.ModifyHomeAsync(someHome);

            //then
            await Assert.ThrowsAsync<HomeDependencyValidationException>(() =>
                mofifyHomeTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeByIdAsync(homeId),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(
                    It.Is(SameExceptionAs(expectedHomeDependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
