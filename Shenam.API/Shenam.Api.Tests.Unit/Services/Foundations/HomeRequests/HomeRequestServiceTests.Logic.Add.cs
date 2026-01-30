//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using FluentAssertions;
using Moq;
using Shenam.API.Models.Foundation.HomeRequests;

namespace Shenam.Api.Tests.Unit.Services.Foundations.HomeRequests
{
    public partial class HomeRequestServiceTests
    {
        [Fact]
        public async Task ShouldAddHomeRequestWrongWayAsync()
        {
            //arrange
            HomeRequest randomHomeRequest = new HomeRequest()
            {
                Id = Guid.NewGuid(),
                GuestId = Guid.NewGuid(),
                HomeId = Guid.NewGuid(),
                Message = "I would like it",
                StartDate = DateTimeOffset.UtcNow.AddDays(10),
                EndDate = DateTimeOffset.UtcNow.AddDays(20),
                CreatedDate = DateTimeOffset.UtcNow,
                UpdatedDate = DateTimeOffset.UtcNow,
            };

            this.storageBrokerMock.Setup(broker =>
                broker.InsertHomeRequestAsync(randomHomeRequest))
                    .ReturnsAsync(randomHomeRequest);

            //assert
            HomeRequest actualHomeRequest = await this.homeRequestService.AddHomeRequestAsync(randomHomeRequest);

            //act
            actualHomeRequest.Should().BeEquivalentTo(randomHomeRequest);
        }

        [Fact]
        public async Task ShouldAddHomeRequestAsync()
        {
            //given
            HomeRequest randomHomeRequest = CreateRandomHomeRequest();
            HomeRequest inputHomeRequest = randomHomeRequest;
            HomeRequest expectedHomeRequest = inputHomeRequest;

            this.storageBrokerMock.Setup(broker =>
            broker.InsertHomeRequestAsync(inputHomeRequest))
                .ReturnsAsync(expectedHomeRequest);

            //when
            HomeRequest actualHomeRequest =
                await this.homeRequestService.AddHomeRequestAsync(inputHomeRequest);

            //then
            actualHomeRequest.Should().BeEquivalentTo(expectedHomeRequest);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHomeRequestAsync(inputHomeRequest),
                Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
