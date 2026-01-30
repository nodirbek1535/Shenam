//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Microsoft.Data.SqlClient;
using Moq;
using Shenam.API.Brokers.loggings;
using Shenam.API.Brokers.Storages;
using Shenam.API.Models.Foundation.HomeRequests;
using Shenam.API.Services.Foundations.HomeRequests;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Shenam.Api.Tests.Unit.Services.Foundations.HomeRequests
{
    public partial class HomeRequestServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IHomeRequestService homeRequestService;

        public HomeRequestServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.homeRequestService = new HomeRequestService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static HomeRequest CreateRandomHomeRequest() =>
            CreateHomeRequestFiller().Create(); 

        private static SqlException GetSqlError()=>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static Filler<HomeRequest> CreateHomeRequestFiller()
        {
            var filler = new Filler<HomeRequest>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(DateTimeOffset.UtcNow);

            return filler;
        }

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);
    }
}
