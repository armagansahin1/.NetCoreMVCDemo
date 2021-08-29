using FluentValidation;

namespace WebApi.Applications.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorValidator()
        {
            RuleFor(x=>x.AuthorId).GreaterThan(0);
        }
    }
}