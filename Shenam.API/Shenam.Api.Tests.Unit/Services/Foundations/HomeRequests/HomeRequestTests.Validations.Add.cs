//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Moq;
using Shenam.API.Models.Foundation.HomeRequests;
using Shenam.API.Models.Foundation.HomeRequests.Exceptions;

namespace Shenam.Api.Tests.Unit.Services.Foundations.HomeRequests
{
    public partial class HomeRequestTests
    {
        [Fact]
        public async Task ShouldTHrowValidationExceptionOnAddIfHomeRequestIsNullAndLogItAsync()
        {
            //given
            HomeRequest nullHomerequest = null;

            var nullHomeRequestException =
                new NullHomeRequestException();

            var expectedHomeRequestValidationException =
                new HomeRequestValidationException(
                    nullHomeRequestException);

            //when
            ValueTask<HomeRequest> addHomeRequestTask =
                this.homeRequestService.AddHomeRequestAsync(nullHomerequest);

            //then
            await Assert.ThrowsAsync<HomeRequestValidationException>(() =>
                addHomeRequestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                        expectedHomeRequestValidationException))),
                Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHomeRequestAsync(It.IsAny<HomeRequest>()),
                Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
