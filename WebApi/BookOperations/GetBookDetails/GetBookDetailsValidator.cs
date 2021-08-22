using FluentValidation;

namespace WebApi.BookOperations.GetBookDetails
{
    public class GetBookDetailsValidator : AbstractValidator<GetBookDetails>
    {
        public GetBookDetailsValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);
        }
    }
}