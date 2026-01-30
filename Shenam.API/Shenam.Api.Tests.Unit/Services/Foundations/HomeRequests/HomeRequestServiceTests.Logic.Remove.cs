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
        public async Task ShouldRemoveHomeRequestByIdAsync()
        {
            // given
            HomeRequest randomHomeRequest = CreateRandomHomeRequest();
            HomeRequest storedHomeRequest = randomHomeRequest;
            HomeRequest deletedHomeRequest = storedHomeRequest;
            Guid homeRequestId = storedHomeRequest.Id;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeRequestByIdAsync(homeRequestId))
                    .ReturnsAsync(storedHomeRequest);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteHomeRequestAsync(storedHomeRequest))
                    .ReturnsAsync(deletedHomeRequest);

            // when
            HomeRequest actualHomeRequest =
                await this.homeRequestService.RemoveHomeRequestByIdAsync(homeRequestId);

            // then
            actualHomeRequest.Should().BeEquivalentTo(deletedHomeRequest);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeRequestByIdAsync(homeRequestId),
                Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteHomeRequestAsync(storedHomeRequest),
                Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
