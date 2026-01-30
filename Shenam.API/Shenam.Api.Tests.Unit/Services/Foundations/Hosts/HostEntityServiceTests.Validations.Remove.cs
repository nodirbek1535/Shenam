//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Moq;
using Shenam.API.Models.Foundation.Hosts;
using Shenam.API.Models.Foundation.Hosts.Exceptions;
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
        public async Task ShouldThrowValidationExceptionOnRemoveIfHostEntityDoesNotExistAndLogItAsync()
        {
            //given
            Guid randomHostEntityId = Guid.NewGuid();
            Guid inputHostEntityId = randomHostEntityId;
            HostEntity noHostEntity = null;

            var notFoundHostEntityException =
                new NotFoundHostEntityException(inputHostEntityId);

            var expectedHostEntityValidationException =
                new HostEntityValidationException(notFoundHostEntityException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHostEntityByIdAsync(inputHostEntityId))
                    .ReturnsAsync(noHostEntity);

            //when
            ValueTask<HostEntity> removeHostEntityTask =
                this.hostEntityService.RemoveHostEntityByIdAsync(inputHostEntityId);

            //then
            await Assert.ThrowsAsync<HostEntityValidationException>(() =>
                removeHostEntityTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHostEntityByIdAsync(inputHostEntityId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHostEntityValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
