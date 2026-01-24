//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using FluentAssertions;
using Moq;
using Shenam.API.Models.Foundation.Hosts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shenam.Api.Tests.Unit.Services.Foundations.Hosts
{
    public partial class HostEntityServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveHostEntityByIdAsync()
        {
            // given
            Guid randomHostEntityId = Guid.NewGuid();
            Guid inputHostEntityId = randomHostEntityId;

            HostEntity randomHostEntity = CreateRandomHostEntity();
            HostEntity persistedHostEntity = randomHostEntity;
            HostEntity expectedHostEntity = persistedHostEntity;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHostEntityByIdAsync(inputHostEntityId))
                    .ReturnsAsync(persistedHostEntity);

            // when
            HostEntity actualHostEntity =
                await this.hostEntityService.RetrieveHostEntityByIdAsync(inputHostEntityId);

            // then
            actualHostEntity.Should().BeEquivalentTo(expectedHostEntity);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHostEntityByIdAsync(inputHostEntityId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();

        }
    }
}
