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
        public async Task ShouldModifyHomeRequestAsync()
        {
            // given
            HomeRequest randomHomeRequest = CreateRandomHomeRequest();
            HomeRequest inputHomeRequest = randomHomeRequest;
            HomeRequest persistedHomeRequest = inputHomeRequest;
            HomeRequest updatedHomeRequest = inputHomeRequest;
            HomeRequest expectedHomeRequest = updatedHomeRequest;
            Guid homeRequestId = inputHomeRequest.Id;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeRequestByIdAsync(homeRequestId))
                    .ReturnsAsync(persistedHomeRequest);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateHomeRequestAsync(inputHomeRequest))
                    .ReturnsAsync(updatedHomeRequest);

            // when
            HomeRequest actualHomeRequest =
                await this.homeRequestService.ModifyHomeRequestAsync(inputHomeRequest);

            // then
            actualHomeRequest.Should().BeEquivalentTo(expectedHomeRequest);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeRequestByIdAsync(homeRequestId),
                Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateHomeRequestAsync(inputHomeRequest),
                Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
