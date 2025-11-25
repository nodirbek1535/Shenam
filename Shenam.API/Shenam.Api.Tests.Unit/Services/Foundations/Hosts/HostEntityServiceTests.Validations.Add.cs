//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Shenam.API.Models.Foundation.Hosts;
using Shenam.API.Models.Foundation.Hosts.Exceptions;

namespace Shenam.Api.Tests.Unit.Services.Foundations.Hosts
{
    public partial class HostEntityServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfHostEntityIsNullAndLogItAsync()
        {
            //given 
            HostEntity nullHostEntity = null;
            var nullHostEntityException = new NullHostEntityException();

            var expectedHostEntityValidationException = 
                new HostEntityValidationException(nullHostEntityException);

            //when
            ValueTask<HostEntity> addHostEntityTask =
                this.hostEntityService.AddHostEntityAsync(nullHostEntity);

            //then
            await Assert.ThrowsAsync<HostEntityValidationException>(() =>
                addHostEntityTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedHostEntityValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHostEntityAsync(It.IsAny<HostEntity>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
