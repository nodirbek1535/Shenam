//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using Shenam.API.Models.Foundation.Hosts.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Shenam.Api.Tests.Unit.Services.Foundations.Hosts
{
    public partial class HostEntityServiceTests
    {
        [Fact]
        public void ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfSqlErrorOccursAndLogIt()
        {
            //given
            SqlException sqlException = GetSqlError();

            var failedHostEntityStorageException =
                new FailedHostEntityStorageException(sqlException);

            var expectedHostEntityDependencyException =
                new HostEntityDependencyException(failedHostEntityStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHostEntities())
                    .Throws(sqlException);

            //when
            Action retrieveAllHostEntitysAction = () =>
                this.hostEntityService.RetrieveAllHostEntities();

            //then
            HostEntityDependencyException actualHostEntityDependencyException =
                Assert.Throws<HostEntityDependencyException>(retrieveAllHostEntitysAction);

            actualHostEntityDependencyException.SameExceptionAs(
                expectedHostEntityDependencyException).Should().BeTrue();


            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllHostEntities(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedHostEntityDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
