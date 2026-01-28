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
        public async Task ShouldModifyHostEntityAsync()
        {
            //given 
            HostEntity randomHostEntity = CreateRandomHostEntity();
            HostEntity inputHostEntity = randomHostEntity;
            HostEntity persistedHostEntity = inputHostEntity;
            HostEntity updatedHostEntity = inputHostEntity;
            HostEntity expectedHostEntity = updatedHostEntity;
            Guid hostEntityId = inputHostEntity.Id;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHostEntityByIdAsync(hostEntityId))
                    .ReturnsAsync(persistedHostEntity);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateHostEntityAsync(inputHostEntity))
                    .ReturnsAsync(updatedHostEntity);

            //when
            HostEntity actualHostEntity =
                await this.hostEntityService.ModifyHostEntityAsync(inputHostEntity);

            //then
            actualHostEntity.Should().BeEquivalentTo(expectedHostEntity);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHostEntityByIdAsync(hostEntityId), Times.Once());

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateHostEntityAsync(inputHostEntity), Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}