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
        public async Task ShouldModifyHomeAsync()
        {
            //given 
            Home randomHome = CreateRandomHome();
            Home inputHome = randomHome;
            Home persistedHome = inputHome;
            Home updatedHome = inputHome;
            Home expectedHome = updatedHome;
            Guid homeId = inputHome.Id;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeByIdAsync(homeId))
                    .ReturnsAsync(persistedHome);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateHomeAsync(inputHome))
                    .ReturnsAsync(updatedHome);

            //when
            Home actualHome =
                await this.homeService.ModifyHomeAsync(inputHome);

            //then
            actualHome.Should().BeEquivalentTo(expectedHome);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeByIdAsync(homeId), Times.Once());

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateHomeAsync(inputHome), Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
