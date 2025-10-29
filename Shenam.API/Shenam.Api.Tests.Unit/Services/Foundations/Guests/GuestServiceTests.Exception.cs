//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using Shenam.API.Models.Foundation.Guests;
using Shenam.API.Models.Foundation.Guests.Exceptions;

namespace Shenam.Api.Tests.Unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            //given
            Guest someGuest = CreateRandomGuest();
            SqlException sqlException = GetSqlError();
            var failedGuestStorageException = new FailedGuestStorageException(sqlException);

            var expectedGuestDependencyException =
                new GuestDependencyException(failedGuestStorageException);  

            this.storageBrokerMock.Setup(broker=>
                broker.InsertGuestAsync(someGuest))
                    .ThrowsAsync(sqlException);

            //when                     
            ValueTask<Guest> addGuestTask =
                this.guestService.AddGuestAsync(someGuest);


            //then
            await Assert.ThrowsAsync<GuestDependencyException>(()=>
                addGuestTask.AsTask());

            this.storageBrokerMock.Verify(broker=>
                broker.InsertGuestAsync(someGuest),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(
                    It.Is(SameExceptionAs(expectedGuestDependencyException))),
                Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationOnAddIfDuplicateKeyErrorOccursAndLogItAsync()
        {
            //given
            Guest someGuest = CreateRandomGuest();
            string someMessage = GetRandomString();

            var duplicateKeyException =
                new DuplicateKeyException(someMessage);

            var alreadyExistsGuestException =
                new AlreadyExistsGuestException(duplicateKeyException);

            var guestDependencyValidationException =
                new GuestDependencyValidationException(alreadyExistsGuestException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertGuestAsync(someGuest))
                    .ThrowsAsync(duplicateKeyException);

            //when
            ValueTask<Guest> addGuestTask =
                this.guestService.AddGuestAsync(someGuest);


            //then
            await Assert.ThrowsAsync<GuestDependencyValidationException>(() =>
                addGuestTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGuestAsync(someGuest),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(
                    It.Is(SameExceptionAs(guestDependencyValidationException))),
                Times.Once);
            
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
