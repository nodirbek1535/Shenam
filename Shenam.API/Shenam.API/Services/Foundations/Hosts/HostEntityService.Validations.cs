//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using System;
using System.Data;
using Shenam.API.Models.Foundation.Guests;
using Shenam.API.Models.Foundation.Hosts;
using Shenam.API.Models.Foundation.Hosts.Exceptions;

namespace Shenam.API.Services.Foundations.Hosts
{
    public partial class HostEntityService
    {
        private void ValidateHostEntityOnAdd(HostEntity hostEntity)
        {
            ValidateHostEntityNotNull(hostEntity);

            Validate(
                    (Rule: IsInvalid(hostEntity.Id), Parameter: nameof(HostEntity.Id)),
                    (Rule: IsInvalid(hostEntity.FirstName), Parameter: nameof(HostEntity.FirstName)),
                    (Rule: IsInvalid(hostEntity.LastName), Parameter: nameof(HostEntity.LastName)),
                    (Rule: IsInvalid(hostEntity.DateOfBirth), Parameter: nameof(HostEntity.DateOfBirth)),
                    (Rule: IsInvalid(hostEntity.Email), Parameter: nameof(HostEntity.Email)),
                    (Rule: IsInvalid(hostEntity.PhoneNumber), Parameter: nameof(HostEntity.PhoneNumber)),
                    (Rule: IsInvalid(hostEntity.Gender), Parameter: nameof(HostEntity.Gender))
                        );
        }

        private void ValidateHostEntityNotNull(HostEntity hostEntity)
        {
            if (hostEntity is null)
            {
                throw new NullHostEntityException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static dynamic IsInvalid(GenderType gender) => new
        {
            Condition = Enum.IsDefined(gender) is false,
            Message = "Value is invalid"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidHostEntityException = new InvalidHostEntityException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidHostEntityException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidHostEntityException.ThrowIfContainsErrors();
        }

    }
}
