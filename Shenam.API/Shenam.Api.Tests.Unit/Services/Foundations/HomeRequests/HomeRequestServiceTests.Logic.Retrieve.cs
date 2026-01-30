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
        public async Task ShouldRetrieveHomeRequestByIdAsync()
        {
            // given
            Guid randomHomeRequestId = Guid.NewGuid();
            Guid inputHomeRequestId = randomHomeRequestId;

            HomeRequest randomHomeRequest = CreateRandomHomeRequest();
            HomeRequest persistedHomeRequest = randomHomeRequest;
            HomeRequest expectedHomeRequest = persistedHomeRequest;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeRequestByIdAsync(inputHomeRequestId))
                    .ReturnsAsync(persistedHomeRequest);

            // when
            HomeRequest actualHomeRequest =
                await this.homeRequestService.RetrieveHomeRequestByIdAsync(inputHomeRequestId);

            // then
            actualHomeRequest.Should().BeEquivalentTo(expectedHomeRequest);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeRequestByIdAsync(inputHomeRequestId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

    }
}
