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
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfIdIsInvalidAndLogItAsync()
        {
            //given
            Guid invalidHostEntityId = Guid.Empty;

            var invalidHostEntityException =
                new InvalidHostEntityException();

            invalidHostEntityException.AddData(
                key: nameof(HostEntity.Id),
                values: "Id is required");

            var expectedHostEntityValidationException =
                new HostEntityValidationException(invalidHostEntityException);

            //when
            ValueTask<HostEntity> retrieveHostEntityById =
                this.hostEntityService.RetrieveHostEntityByIdAsync(invalidHostEntityId);

            //then
            await Assert.ThrowsAsync<HostEntityValidationException>(() =>
                retrieveHostEntityById.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHostEntityValidationException))),
                Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHostEntityByIdAsync(It.IsAny<Guid>()),
                Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
