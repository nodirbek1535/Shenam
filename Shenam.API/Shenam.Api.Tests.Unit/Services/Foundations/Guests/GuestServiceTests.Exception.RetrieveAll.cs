//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using Shenam.API.Models.Foundation.Guests.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Shenam.Api.Tests.Unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public void ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfSqlErrorOccursAndLogIt()
        {
            // given
            SqlException sqlException = GetSqlError();

            var failedStorageException =
                new FailedGuestStorageException(sqlException);

            var expectedDependencyException =
                new GuestDependencyException(failedStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllGuests()).Throws(sqlException);

            // when
            Action retrieveAllGuestsAction = () =>
                this.guestService.RetrieveAllGuests();

            // then
            GuestDependencyException actualException =
                Assert.Throws<GuestDependencyException>(retrieveAllGuestsAction);

            actualException.SameExceptionAs(expectedDependencyException)
                .Should().BeTrue();

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllGuests(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedDependencyException))), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogIt()
        {
            // given
            var serviceException = new Exception(GetRandomString());

            var failedServiceException =
                new FailedGuestServiceException(serviceException);

            var expectedServiceException =
                new GuestServiceException(failedServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllGuests())
                    .Throws(serviceException);

            // when
            Action retrieveAllGuestsAction = () =>
                this.guestService.RetrieveAllGuests();

            // then
            GuestServiceException actualException =
                Assert.Throws<GuestServiceException>(retrieveAllGuestsAction);

            actualException
                .SameExceptionAs(expectedServiceException)
                .Should().BeTrue();

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllGuests(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(
                    SameExceptionAs(expectedServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

    }
}
