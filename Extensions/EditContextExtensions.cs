using System.Linq.Expressions;
using Microsoft.AspNetCore.Components.Forms;

namespace NATSBlazor.Extensions;

public static class EditContextExtensions
{
    public static bool IsFieldValid<TField>(
            this EditContext context,
            Expression<Func<TField>> fieldSelector)
    {
        FieldIdentifier fieldIdentifier = FieldIdentifier.Create(fieldSelector);
        return context.GetValidationMessages(fieldIdentifier).Any();
    }

    public static string GetFieldInputClassName<TField>(
            this EditContext context,
            Expression<Func<TField>> fieldSelector)
    {
        return context.IsFieldValid(fieldSelector) ? string.Empty : "is-invalid";
    }

    public static string GetFieldValidationMessageClassName<TField>(
            this EditContext context,
            Expression<Func<TField>> fieldSelector)
    {
        return context.IsFieldValid(fieldSelector) ? string.Empty : "text-danger";
    }
}