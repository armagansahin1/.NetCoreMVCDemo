using FluentValidation;

namespace WebApi.Applications.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidator:AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(g=>g.GenreId).NotEmpty();
            RuleFor(g=>g.GenreId).GreaterThan(0);

        }
    }
}