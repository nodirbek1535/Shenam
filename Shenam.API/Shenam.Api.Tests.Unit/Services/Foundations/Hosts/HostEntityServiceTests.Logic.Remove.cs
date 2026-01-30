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
        public async Task ShouldThrowRemoveHostEntityByIdAsync()
        {
            //given
            HostEntity randomHostEntity = CreateRandomHostEntity();
            HostEntity storedHostEntity = randomHostEntity;
            HostEntity deletedHostEntity = storedHostEntity;
            Guid hostEntityId = storedHostEntity.Id;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHostEntityByIdAsync(hostEntityId))
                    .ReturnsAsync(storedHostEntity);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteHostEntityAsync(storedHostEntity))
                    .ReturnsAsync(deletedHostEntity);

            //when
            HostEntity actualHostEntity =
                await this.hostEntityService.RemoveHostEntityByIdAsync(hostEntityId);

            //then
            actualHostEntity.Should().BeEquivalentTo(deletedHostEntity);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHostEntityByIdAsync(hostEntityId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteHostEntityAsync(storedHostEntity),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
