//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using FluentAssertions;
using Moq;
using Shenam.API.Models.Foundation.HomeRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shenam.Api.Tests.Unit.Services.Foundations.HomeRequests
{
    public partial class HomeRequestServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllHomeRequests()
        {
            // given
            IQueryable<HomeRequest> randomHomeRequests = CreateRandomHomeRequests();
            IQueryable<HomeRequest> persistedHomeRequests = randomHomeRequests;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHomeRequests())
                    .Returns(persistedHomeRequests);

            // when
            IQueryable<HomeRequest> actualHomeRequests =
                this.homeRequestService.RetrieveAllHomeRequests();

            // then
            actualHomeRequests.Should().BeEquivalentTo(persistedHomeRequests);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllHomeRequests(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

    }
}
