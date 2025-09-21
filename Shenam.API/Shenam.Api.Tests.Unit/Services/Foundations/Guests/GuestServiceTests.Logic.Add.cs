//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Shenam.API.Models.Foundation.Guests;

namespace Shenam.Api.Tests.Unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]

        public async Task ShouldAddGuestWrongWayAsync()
        {
            //Arrange
            Guest randomGuest = new Guest()
            {
                Id = Guid.NewGuid(),
                FirstName = "Alez",
                LastName = "Terminator",
                Address = "Brooks Str.#12",
                DateOfBirth = new DateTimeOffset(),
                Email = "1234567890@outlock.com",
                Gender = GenderType.Male,
                PhoneNumber = "1234567890",
            };

            this.storageBrokerMock.Setup(broker =>
                broker.InsertGuestAsync(randomGuest))
                    .ReturnsAsync(randomGuest);
            //Act
            Guest actual = await this.guestService.AddGuestAsync(randomGuest);
            //Assert
            actual.Should().BeEquivalentTo(randomGuest);
        }


        [Fact]
        public async Task ShouldAddGuestAsync()
        {
            //given
            Guest randomGuest = CreateRandomGuest();
            Guest inputGuest = randomGuest;
            Guest storageGuest = inputGuest;
            Guest expectedGuest = storageGuest.DeepClone();

            this.storageBrokerMock.Setup(broker =>
            broker.InsertGuestAsync(inputGuest))
                .ReturnsAsync(storageGuest);

            //when
            Guest actualGuest =
                await this.guestService.AddGuestAsync(inputGuest);

            //then
            actualGuest.Should().BeEquivalentTo(expectedGuest);

            this.storageBrokerMock.Verify(broker =>
            broker.InsertGuestAsync(inputGuest),
            Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
