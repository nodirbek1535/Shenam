//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Shenam.API.Models.Foundation.Guests;
using Shenam.API.Models.Foundation.Hosts;

namespace Shenam.Api.Tests.Unit.Services.Foundations.Hosts
{
    public partial class HostEntityServiceTests
    {
        [Fact]
        public async Task ShouldAddHostEntityWrongWayAsync()
        {
            //Arrange
            HostEntity randomHostEntity = new HostEntity()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = DateTimeOffset.UtcNow.AddYears(-30),
                Email = "1223423@outlok.com",
                PhoneNumber = "0987654321",
                Gender = GenderType.Male,
            };

            this.storageBrokerMock.Setup(broker =>
                broker.InsertHostEntityAsync(randomHostEntity))
                    .ReturnsAsync(randomHostEntity);

            //Act
            HostEntity actual = await this.hostEntityService.AddHostEntityAsync(randomHostEntity);

            //Assert
            actual.Should().BeEquivalentTo(randomHostEntity);
        }

        [Fact]
        public async Task ShouldAddHostEntityAsync()
        {
            //given
            HostEntity randomHostEntity = CreateRandomHostEntity();
            HostEntity inputHostEntity = randomHostEntity;
            HostEntity storageHostEntity = inputHostEntity;
            HostEntity expectedHostEntity = storageHostEntity.DeepClone();

            this.storageBrokerMock.Setup(broker =>
            broker.InsertHostEntityAsync(inputHostEntity))
                .ReturnsAsync(storageHostEntity);

            //when
            HostEntity actualHostEntity =
                await this.hostEntityService.AddHostEntityAsync(inputHostEntity);

            //then
            actualHostEntity.Should().BeEquivalentTo(expectedHostEntity);

            this.storageBrokerMock.Verify(broker =>
            broker.InsertHostEntityAsync(inputHostEntity),
            Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();

        }
    }
}
