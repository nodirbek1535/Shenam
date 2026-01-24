//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using FluentAssertions;
using Moq;
using Shenam.API.Models.Foundation.Hosts;
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
        public void  ShouldRetrieveAllHostEntities()
        {
            //given
            IQueryable<HostEntity> randomHostEntities = CreateRandomHostEntities();
            IQueryable<HostEntity> persistedHostEntities = randomHostEntities;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHostEntities())
                    .Returns(persistedHostEntities);

            //when
            IQueryable<HostEntity> actualHostEntities =
                this.hostEntityService.RetrieveAllHostEntities();

            //then
            actualHostEntities.Should().BeEquivalentTo(persistedHostEntities);  

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllHostEntities(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
