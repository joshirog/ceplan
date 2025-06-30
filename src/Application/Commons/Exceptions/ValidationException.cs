using FluentValidation.Results;

namespace Application.Commons.Exceptions;

public class ValidationException : Exception
{
    private ValidationException() : base("One or more validation failures have occurred.")
    {
        Errors = [];
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        var errors = failures
            .Select(x => x.ErrorMessage)
            .ToList();
        
        Errors = errors;
    }

    public List<string> Errors { get; }
}