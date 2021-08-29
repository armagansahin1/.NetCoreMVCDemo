using FluentValidation;

namespace WebApi.Applications.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(g=>g.GenreId).NotEmpty();
            RuleFor(g=>g.GenreId).GreaterThan(0);
            RuleFor(g=>g.Model.Name).NotEmpty();
        }
    }
}