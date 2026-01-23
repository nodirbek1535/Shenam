//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using FluentAssertions;
using Moq;
using Shenam.API.Models.Foundation.Homes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shenam.Api.Tests.Unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        [Fact]
        public async Task ShouldThrowRemoveHomeByIdAsync()
        {
            //given
            Home randomHome = CreateRandomHome();
            Home storedHome = randomHome;
            Home deletedHome = storedHome;
            Guid homeId = storedHome.Id;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeByIdAsync(homeId))
                    .ReturnsAsync(storedHome);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteHomeAsync(storedHome))
                    .ReturnsAsync(deletedHome);

            //when
            Home actualHome =
                await this.homeService.RemoveHomeByIdAsync(homeId);

            //then
            actualHome.Should().BeEquivalentTo(deletedHome);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeByIdAsync(homeId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteHomeAsync(storedHome),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
