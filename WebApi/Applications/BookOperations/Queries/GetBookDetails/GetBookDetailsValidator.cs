using FluentValidation;

namespace WebApi.Applications.BookOperations.Queries.GetBookDetails
{
    public class GetBookDetailsValidator : AbstractValidator<GetBookDetails>
    {
        public GetBookDetailsValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);
        }
    }
}