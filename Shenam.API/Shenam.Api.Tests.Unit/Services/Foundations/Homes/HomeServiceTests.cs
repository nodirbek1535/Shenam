//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Microsoft.Data.SqlClient;
using Moq;
using Shenam.API.Brokers.loggings;
using Shenam.API.Brokers.Storages;
using Shenam.API.Models.Foundation.Homes;
using Shenam.API.Services.Foundations.Homes;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Shenam.Api.Tests.Unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IHomeService homeService;

        public HomeServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.homeService = new HomeService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static Home CreateRandomHome() =>
            CreateHomeFiller().Create();

        private static int GetRandomNumber() =>
            new IntRange(min: 4, max: 20).GetValue();

        private static SqlException GetSqlError() =>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        private static T GetInvalidEnum<T>()
        { 
            int randomNumber = GetRandomNumber();
            while (Enum.IsDefined(typeof(T), randomNumber))
            {
                randomNumber = GetRandomNumber();
            }
            
            return (T)Enum.ToObject(typeof(T), randomNumber);
        }


        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
           actualException => actualException.SameExceptionAs(expectedException);

        private static Filler<Home> CreateHomeFiller()
        {
            var filler = new Filler<Home>();
            
            filler.Setup()
                .OnProperty(h => h.NumberOfBedrooms).Use(1)
                .OnProperty(h => h.NumberOfBathrooms).Use(1)
                .OnProperty(h => h.Area).Use(500.0)
                .OnProperty(h => h.Price).Use(10000.00M)
                .OnProperty(h => h.HomeType).Use(TypeHome.Bungalow);

            return filler;

        }

    }
}
