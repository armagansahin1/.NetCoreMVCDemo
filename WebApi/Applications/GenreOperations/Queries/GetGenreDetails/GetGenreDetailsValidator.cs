using FluentValidation;

namespace WebApi.Applications.GenreOperations.Queries.GetGenreDetails
{
    public class GetGenreDetailsValidator : AbstractValidator<GetGenreDetails>
    {
        public GetGenreDetailsValidator()
        {
            RuleFor(g => g.GenreId).NotEmpty();
            RuleFor(g => g.GenreId).GreaterThan(0);
        }
    }
}