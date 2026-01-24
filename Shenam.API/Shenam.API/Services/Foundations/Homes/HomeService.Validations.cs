//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Shenam.API.Models.Foundation.Homes;
using Shenam.API.Models.Foundation.Homes.Exceptions;
using System;

namespace Shenam.API.Services.Foundations.Homes
{
    public partial class HomeService
    {
        private void ValidateHomeOnAdd(Home home)
        {
            ValidateHomeNotNull(home);

            Validate(
                (Rule: IsInvalid(home.Id), Parameter: nameof(Home.Id)),
                (Rule: IsInvalid(home.HostId), Parameter: nameof(Home.HostId)),
                (Rule: IsInvalid(home.Address), Parameter: nameof(Home.Address)),
                (Rule: IsInvalid(home.NumberOfBedrooms), Parameter: nameof(Home.NumberOfBedrooms)),
                (Rule: IsInvalid(home.NumberOfBathrooms), Parameter: nameof(Home.NumberOfBathrooms)),
                (Rule: IsInvalid(home.Area), Parameter: nameof(Home.Area)),
                (Rule: IsInvalid(home.Price), Parameter: nameof(Home.Price)),
                (Rule: IsInvalid(home.HomeType), Parameter: nameof(Home.HomeType))
            );
        }

        private void ValidateHomeOnModify(Home home)
        {
            ValidateHomeNotNull(home);

            ValidateHomeId(home.Id);

            Validate(
                (Rule: IsInvalid(home.Address), Parameter: nameof(Home.Address)),
                (Rule: IsInvalid(home.NumberOfBedrooms), Parameter: nameof(Home.NumberOfBedrooms)),
                (Rule: IsInvalid(home.NumberOfBathrooms), Parameter: nameof(Home.NumberOfBathrooms)),
                (Rule: IsInvalid(home.Area), Parameter: nameof(Home.Area)),
                (Rule: IsInvalid(home.Price), Parameter: nameof(Home.Price)),
                (Rule: IsInvalid(home.HomeType), Parameter: nameof(Home.HomeType))
            );
        }
        private void ValidateHomeNotNull(Home home)
        {
            if (home is null)
            {
                throw new NullHomeException();
            }
        }

        private void ValidateHomeId(Guid homeId)
        {
            Validate(
                (Rule: IsInvalid(homeId), Parameter: nameof(Home.Id)));
        }

        private void ValidateStorageHome(Home maybeHome, Guid homeId)
        {
            if (maybeHome is null)
            {
                throw new NotFoundHomeException(homeId);
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

        private static dynamic IsInvalid(int number) => new
        {
            Condition = number < 0,
            Message = "Number cannot be negative"
        };

        private static dynamic IsInvalid(double number) => new
        {
            Condition = number <= 0,
            Message = "Number must be greater than zero"
        };

        private static dynamic IsInvalid(decimal number) => new
        {
            Condition = number <= 0,
            Message = "Number must be greater than zero"
        };

        private static dynamic IsInvalid(TypeHome homeType) => new
        {
            Condition = Enum.IsDefined(homeType) is false,
            Message = "Value is invalid"
        };

        private void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidHomeException =
                new InvalidHomeException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidHomeException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidHomeException.ThrowIfContainsErrors();
        }
    }
}
