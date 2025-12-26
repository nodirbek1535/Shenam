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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfHostEntityIsInvalidAndLogItAsync(string invalidText)
        {
            //given
            var invalidHome = new Home
            {
                Address = invalidText,
            };

            var invalidHomeException = new InvalidHomeException();

            invalidHomeException.AddData(
                key: nameof(Home.Id),
                values: "Id is required");

            invalidHomeException.AddData(
                key: nameof(Home.HostId),
                values: "HostId is required");

            invalidHomeException.AddData(
                key: nameof(Home.Address),
                values: "Text is required");

            invalidHomeException.AddData(
                key: nameof(Home.NumberOfBedrooms),
                values: "Number cannot be negative");

            invalidHomeException.AddData(
                key: nameof(Home.NumberOfBathrooms),
                values: "Number cannot be negative");

            invalidHomeException.AddData(
                key: nameof(Home.Area),
                values: "Number must be greater than zero");

            invalidHomeException.AddData(
                key: nameof(Home.Price),
                values: "Number must be greater than zero");

            invalidHomeException.AddData(
                key: nameof(Home.HomeType),
                values: "Value is invalid");

            var expectedHomeValidationException =
                new HomeValidationException(invalidHomeException);

            //when
            ValueTask<Home> addHomeTask = 
                this.homeService.AddHomeAsync(invalidHome);

            //then
            await Assert.ThrowsAsync<HomeValidationException>(() =>
                addHomeTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.IsAny<HomeValidationException>()),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHomeAsync(It.IsAny<Home>()),
                    Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
