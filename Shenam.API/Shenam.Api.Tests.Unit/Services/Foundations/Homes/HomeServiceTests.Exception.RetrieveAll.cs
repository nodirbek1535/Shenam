//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using Newtonsoft.Json.Bson;
using Shenam.API.Models.Foundation.Homes.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Shenam.Api.Tests.Unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        [Fact]
        public void ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfSqlErrorOccursAndLogIt()
        {
            //given
            SqlException sqlException = GetSqlError();

            var failedHomeStorageException =
                new FailedHomeStorageException(sqlException);

            var expectedHomeDependencyException =
                new HomeDependencyException(failedHomeStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHomes())
                    .Throws(sqlException);

            //when
            Action retrieveAllHomesAction = () =>
                this.homeService.RetrieveAllHomes();

            //then
            HomeDependencyException actualHomeDependencyException =
                Assert.Throws<HomeDependencyException>(retrieveAllHomesAction);

            actualHomeDependencyException.SameExceptionAs(
                expectedHomeDependencyException).Should().BeTrue();


            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllHomes(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedHomeDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowServiceExceptionOnRetrieveAllIfSqlErrorOccursAndLogIt()
        {
            //given
            var serviceException = new Exception(GetRandomString());

            var failedServiceException =
                new FailedHomeServiceException(serviceException);

            var expectedServiceException =
                new HomeServiceException(failedServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHomes())
                    .Throws(serviceException);

            //when
            Action retrieveAllHomesAction = () =>
                this.homeService.RetrieveAllHomes();

            //then
            HomeServiceException actualException =
                Assert.Throws<HomeServiceException>(retrieveAllHomesAction);

            actualException
                .SameExceptionAs(expectedServiceException)
                .Should().BeTrue();

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllHomes(),Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(
                    SameExceptionAs(expectedServiceException))),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
