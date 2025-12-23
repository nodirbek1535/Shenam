//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Moq;
using Shenam.API.Models.Foundation.Homes;
using Shenam.API.Models.Foundation.Homes.Exceptions;

namespace Shenam.Api.Tests.Unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfHomeIsNullAndLogItAsync()
        {
            //given
            Home nullHome = null;
            var nullHomeException = new NullHomeException();

            var expectedHomeValidationException =
                new HomeValidationException(nullHomeException);

            //when
            ValueTask<Home> addHomeTask =
                this.homeService.AddHomeAsync(nullHome);

            //then
            await Assert.ThrowsAsync<HomeValidationException>(() =>
                addHomeTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHomeAsync(It.IsAny<Home>()),
                    Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();

        }
    }
}
