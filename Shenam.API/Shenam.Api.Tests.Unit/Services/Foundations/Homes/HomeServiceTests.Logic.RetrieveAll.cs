//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using FluentAssertions;
using Moq;
using Shenam.API.Models.Foundation.Homes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shenam.Api.Tests.Unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllHomes()
        {
            //given
            IQueryable<Home> randomHomes = CreateRandomHomes();
            IQueryable<Home> persistedHomes = randomHomes;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHomes())
                    .Returns(persistedHomes);

            //when
            IQueryable<Home> actualHomes =
                this.homeService.RetrieveAllHomes();

            //then
            actualHomes.Should().BeEquivalentTo(persistedHomes);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllHomes(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
