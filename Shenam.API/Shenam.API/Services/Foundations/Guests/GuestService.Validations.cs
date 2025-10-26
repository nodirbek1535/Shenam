//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using System.Data;
using Shenam.API.Models.Foundation.Guests;
using Shenam.API.Models.Foundation.Guests.Exceptions;

namespace Shenam.API.Services.Foundations.Guests
{
    public partial class GuestService
    {

        private void ValidateGuestOnAdd(Guest guest)
        {
            ValidateGuestNotNull(guest);

            Validate(
                    (Rule: Isinvalid(guest.Id), Parameter: nameof(Guest.Id)),
                    (Rule: Isinvalid(guest.FirstName), Parameter: nameof(Guest.FirstName)),
                    (Rule: Isinvalid(guest.LastName), Parameter: nameof(Guest.LastName)),
                    (Rule: Isinvalid(guest.DateOfBirth), Parameter: nameof(Guest.DateOfBirth)),
                    (Rule: Isinvalid(guest.Email), Parameter: nameof(Guest.Email)),
                    (Rule: Isinvalid(guest.Address), Parameter: nameof(Guest.Address))
                    );

        }
        private void ValidateGuestNotNull(Guest guest)
        {
            if (guest is null) 
            {
                throw new NullGuestException();
            }
        }

        private static dynamic Isinvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };


        private static dynamic Isinvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic Isinvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };


        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidGuestException = new InvalidGuestException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidGuestException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidGuestException.ThrowIfContainsErrors();
        }
    }
}
