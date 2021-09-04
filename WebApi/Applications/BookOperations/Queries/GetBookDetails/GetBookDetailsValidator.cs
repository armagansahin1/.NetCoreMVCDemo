using FluentValidation;

namespace WebApi.Applications.BookOperations.Queries.GetBookDetails
{
    public class GetBookDetailsValidator : AbstractValidator<GetBookDetailsQuery>
    {
        public GetBookDetailsValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);
        }
    }
}