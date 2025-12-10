//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System.Linq.Expressions;
using System.Runtime.Serialization;
using Microsoft.Data.SqlClient;
using Moq;
using Shenam.API.Brokers.loggings;
using Shenam.API.Brokers.Storages;
using Shenam.API.Models.Foundation.Hosts;
using Shenam.API.Services.Foundations.Hosts;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Shenam.Api.Tests.Unit.Services.Foundations.Hosts
{
    public partial class HostEntityServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IHostEntityService hostEntityService;

        public HostEntityServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.hostEntityService = new HostEntityService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static HostEntity CreateRandomHostEntity() =>
            CreateHostEntityFiller(date: GetRandomDateTimeoffset()).Create();

        private static DateTimeOffset GetRandomDateTimeoffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static SqlException GetSqlError() =>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static T GetInvalidEnum<T>()
        {
            int randomnumber = GetRandomNumber();
            while (Enum.IsDefined(typeof(T), randomnumber) is true)
            {
                randomnumber = GetRandomNumber();
            }

            return (T)(object)randomnumber;
        }

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        public static Filler<HostEntity> CreateHostEntityFiller(DateTimeOffset date)
        {
            var filler = new Filler<HostEntity>();

            filler.Setup()
                .OnProperty(host => host.DateOfBirth).Use(date);

            return filler;
        }
    }
}
