using FluentValidation;

namespace WebApi.Applications.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(g=>g.Model.Name).NotEmpty();
        }
    }
}