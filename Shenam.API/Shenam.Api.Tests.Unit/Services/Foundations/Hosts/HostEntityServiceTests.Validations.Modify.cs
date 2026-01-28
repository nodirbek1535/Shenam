//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using FluentAssertions;
using Moq;
using Shenam.API.Models.Foundation.Hosts;
using Shenam.API.Models.Foundation.Hosts.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Shenam.Api.Tests.Unit.Services.Foundations.Hosts
{
    public partial class HostEntityServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionIfHostEntityIsNullAndLogItAsync()
        {
            //given
            HostEntity nullHostEntity = null;
            var nullHostEntityException = new NullHostEntityException();

            var expectedHostEntityValidationException =
                new HostEntityValidationException(nullHostEntityException);

            //when
            ValueTask<HostEntity> modifyHostEntityTask =
                this.hostEntityService.ModifyHostEntityAsync(nullHostEntity);

            HostEntityValidationException actualHostEntityValidationException =
                await Assert.ThrowsAsync<HostEntityValidationException>(
                    modifyHostEntityTask.AsTask);

            //then
            actualHostEntityValidationException
                .SameExceptionAs(expectedHostEntityValidationException)
                .Should().BeTrue();

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHostEntityValidationException))), Times.Once());

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateHostEntityAsync(It.IsAny<HostEntity>()), Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

    }
}
