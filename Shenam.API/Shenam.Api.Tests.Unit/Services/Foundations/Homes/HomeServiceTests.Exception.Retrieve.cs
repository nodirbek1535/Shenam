//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Microsoft.Data.SqlClient;
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
        public async Task ShouldThrowSqlExceptionOnRetrieveByIdIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Guid someHomeId = Guid.NewGuid();
            SqlException sqlException = GetSqlError();

            var failedStorageHomeException =
                new FailedHomeStorageException(sqlException);

            var expectedHomeDependencyException =
                new HomeDependencyException(failedStorageHomeException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<Home> retrieveHomeByIdTask =
                this.homeService.RetrieveHomeByIdAsync(someHomeId);

            // then
            await Assert.ThrowsAsync<HomeDependencyException>(
                retrieveHomeByIdTask.AsTask);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(
                    SameExceptionAs(expectedHomeDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveByIdIfServiceErrorOccursAndLogItAsync()
        {
            // given
            Guid someHomeId = Guid.NewGuid();
            var serviceException = new Exception();

            var failedHomeServiceException =
                new FailedHomeServiceException(serviceException);

            var expectedHomeServiceException =
                new HomeServiceException(failedHomeServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<Home> retrieveHomeByIdTask =
                this.homeService.RetrieveHomeByIdAsync(someHomeId);

            // then
            await Assert.ThrowsAsync<HomeServiceException>(
                retrieveHomeByIdTask.AsTask);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(
                    SameExceptionAs(expectedHomeServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
