//===============================================================
//NODIRBEKNING MOHIRDEV PLATFORMASIDA ORGANGAN API SINOV LOYIHASI
//===============================================================

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shenam.API.Models.Foundation.HomeRequests;
using Shenam.API.Models.Foundation.HomeRequests.Exceptions;
using System;
using System.Data;

namespace Shenam.API.Services.Foundations.HomeRequests
{
    public partial class HomeRequestService
    {
        private void ValidateHomeRequestOnAdd(HomeRequest homeRequest)
        {
            ValidateHomeRequestNotNull(homeRequest);

            Validate(
                (Rule: IsInvalid(homeRequest.Id), Parameter: nameof(HomeRequest.Id)),
                (Rule: IsInvalid(homeRequest.GuestId), Parameter: nameof(HomeRequest.GuestId)),
                (Rule: IsInvalid(homeRequest.HomeId), Parameter: nameof(HomeRequest.HomeId)),
                (Rule: IsInvalid(homeRequest.Message), Parameter: nameof(HomeRequest.Message)),
                (Rule: IsInvalid(homeRequest.StartDate), Parameter: nameof(HomeRequest.StartDate)),
                (Rule: IsInvalid(homeRequest.EndDate), Parameter: nameof(HomeRequest.EndDate)),
                (Rule: IsInvalid(homeRequest.CreatedDate), Parameter: nameof(HomeRequest.CreatedDate)),
                (Rule: IsInvalid(homeRequest.UpdatedDate), Parameter: nameof(HomeRequest.UpdatedDate))
                );
        }

        private void ValidateHomeRequestOnModify(HomeRequest homeRequest)
        {
            ValidateHomeRequestNotNull(homeRequest);

            ValidateHomeRequestId(homeRequest.Id);

            Validate(
                (Rule: IsInvalid(homeRequest.Message), Parameter: nameof(HomeRequest.Message)),
                (Rule: IsInvalid(homeRequest.StartDate), Parameter: nameof(HomeRequest.StartDate)),
                (Rule: IsInvalid(homeRequest.EndDate), Parameter: nameof(HomeRequest.EndDate)),
                (Rule: IsInvalid(homeRequest.UpdatedDate), Parameter: nameof(HomeRequest.UpdatedDate))
                );
        }
        private void ValidateHomeRequestNotNull(HomeRequest homeRequest)
        {
            if(homeRequest == null)
            {
                throw new NullHomeRequestException();
            }
        }

        private void ValidateHomeRequestId(Guid homeRequestId)
        {
            Validate(
                (Rule: IsInvalid(homeRequestId), Parameter: nameof(HomeRequest.Id))
                );
        }

        private void ValidateStorageHomeRequest(HomeRequest maybeHomeRequest, Guid homeRequestId)
        {
            if (maybeHomeRequest == null)
            {
                throw new NotFoundHomeRequestException(homeRequestId);
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

        private void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidHomeRequestException = 
                new InvalidHomeRequestException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidHomeRequestException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }
            
            invalidHomeRequestException.ThrowIfContainsErrors();
        }


    }
}
