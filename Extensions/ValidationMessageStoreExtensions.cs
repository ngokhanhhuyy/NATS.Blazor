using Microsoft.AspNetCore.Components.Forms;
using NATS.Services.Exceptions;
using FluentValidation.Results;

namespace NATS.Extensions;

public static class ValidationMessageStoreExtensions
{
    public static void AddFromValidationErrors(
            this ValidationMessageStore validationMessageStore,
            EditContext editContext,
            List<ValidationFailure> failures)
    {
        foreach (ValidationFailure failure in failures)
        {
            FieldIdentifier field = editContext.Field(failure.PropertyName);
            validationMessageStore.Add(field, failure.ErrorMessage);
        }
    }

    public static void AddFromServiceException(
            this ValidationMessageStore validationMessageStore,
            EditContext editContext,
            OperationException exception)
    {
        FieldIdentifier field = editContext.Field(exception.PropertyName);
        validationMessageStore.Add(field, exception.Message);
    }
}