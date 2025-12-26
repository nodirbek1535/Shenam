//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Microsoft.Data.SqlClient;
using Moq;
using RESTFulSense.Models.Foundations.Properties.Exceptions;
using Shenam.API.Models.Foundation.Homes;
using Shenam.API.Models.Foundation.Homes.Exceptions;

namespace Shenam.Api.Tests.Unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        { 
            //given
            Home someHome = CreateRandomHome();
            SqlException sqlException = GetSqlError();
            var failedHomeStorageException = new FailedHomeStorageException(sqlException);

            var expectedHomeDependencyException =
                new HomeDependencyException(failedHomeStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertHomeAsync(someHome))
                    .ThrowsAsync(sqlException);

            //when
            ValueTask<Home> addHomeTask =
                this.homeService.AddHomeAsync(someHome);

            //then
            await Assert.ThrowsAsync<HomeDependencyException>(() =>
                addHomeTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHomeAsync(someHome),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(
                    It.Is(SameExceptionAs(expectedHomeDependencyException))),
                Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }


    }
}
