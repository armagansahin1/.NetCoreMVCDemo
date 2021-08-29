using FluentValidation;

namespace WebApi.Applications.AuthorOperations.Queries.GetAuthorDetails
{
    public class GetAuthorDetailsValidator : AbstractValidator<GetAuthorDetails>
    {   
        public GetAuthorDetailsValidator()
        {
            RuleFor(x=>x.AuthorId).NotEmpty();
            RuleFor(x=>x.AuthorId).NotNull();
            RuleFor(x=>x.AuthorId).GreaterThan(0);
        }
    }
}