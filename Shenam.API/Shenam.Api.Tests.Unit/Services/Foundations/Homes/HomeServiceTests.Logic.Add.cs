//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using FluentAssertions;
using Moq;
using Shenam.API.Models.Foundation.Homes;

namespace Shenam.Api.Tests.Unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        [Fact]
        public async Task ShouldAddHomeWrongWayAsync()
        {
            //Arrange
            Home randomHome = new Home()
            {
                Id = Guid.NewGuid(),
                HostId = Guid.NewGuid(),
                Address = "123 Main",
                AdditionalInfo = "Near park",
                IsVacant = true,
                NumberOfBedrooms = 3,
                NumberOfBathrooms = 2,
                Area = 1500.5,
                IsPAllowed = true,
                HomeType = TypeHome.Bungalow,
                Price = 250000.00M,
                IsShared = false
            };

            this.storageBrokerMock.Setup(broker =>
                broker.InsertHomeAsync(randomHome))
                    .ReturnsAsync(randomHome);

            //Act
            Home actualHome = await this.homeService.AddHomeAsync(randomHome);

            //Assert
            actualHome.Should().BeEquivalentTo(randomHome);
        }

        [Fact]
        public async Task ShouldAddHomeAsync()
        {
            // given
            Home randomHome = CreateRandomHome();
            Home inputHome = randomHome;
            Home expectedHome = inputHome;

            this.storageBrokerMock.Setup(broker =>
                broker.InsertHomeAsync(inputHome))
                    .ReturnsAsync(expectedHome);

            // when
            Home actualHome =
                await this.homeService.AddHomeAsync(inputHome);

            // then
            actualHome.Should().BeEquivalentTo(expectedHome);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHomeAsync(inputHome),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
