//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Moq;
using Shenam.API.Models.Foundation.Guests;
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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfHostEntityIsInvalidAndLogItAsync(string invalidText)
        {
            //given
            var invalidHostEntity = new HostEntity
            {
                FirstName = invalidText
            };

            var invalidHostEntityException = new InvalidHostEntityException();

            invalidHostEntityException.AddData(
                key: nameof(HostEntity.Id),
                values: "Id is required");

            invalidHostEntityException.AddData(
                key: nameof(HostEntity.FirstName),
                values: "Text is required");

            invalidHostEntityException.AddData(
                key: nameof(HostEntity.LastName),
                values: "Text is required");

            invalidHostEntityException.AddData(
                key: nameof(HostEntity.DateOfBirth),
                values: "Date is required");

            invalidHostEntityException.AddData(
                key: nameof(HostEntity.Email),
                values: "Text is required");

            invalidHostEntityException.AddData(
                key: nameof(HostEntity.PhoneNumber),
                values: "Text is required");

            var expectedHostEntityValidationException =
                new HostEntityValidationException(invalidHostEntityException);

            //when
            ValueTask<HostEntity> addHostEntityTask =
                this.hostEntityService.AddHostEntityAsync(invalidHostEntity);

            //then
            await Assert.ThrowsAsync<HostEntityValidationException>(() =>
                addHostEntityTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                expectedHostEntityValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHostEntityAsync(It.IsAny<HostEntity>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfGenderIsInvalidAndLogItAsync()
        {
            //given
            HostEntity randomHostEntity = CreateRandomHostEntity();
            HostEntity invalidHostEntity = randomHostEntity;
            invalidHostEntity.Gender = GetInvalidEnum<GenderType>();
            var invalidHostEntityException = new InvalidHostEntityException();

            invalidHostEntityException.AddData(
                key: nameof(HostEntity.Gender),
                values: "Value is invalid");

            var expectedHostEntityValidationException =
                new HostEntityValidationException(invalidHostEntityException);

            //when
            ValueTask<HostEntity> addHostEntityTask =
                this.hostEntityService.AddHostEntityAsync(invalidHostEntity);

            //then
            await Assert.ThrowsAsync<HostEntityValidationException>(() =>
                addHostEntityTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHostEntityValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHostEntityAsync(It.IsAny<HostEntity>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
