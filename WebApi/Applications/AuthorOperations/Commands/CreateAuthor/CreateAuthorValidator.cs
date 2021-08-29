using System;
using FluentValidation;

namespace WebApi.Applications.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorValidator()
        {
            RuleFor(x=>x.Model.Name).NotNull();
            RuleFor(x=>x.Model.Name).MinimumLength(2);
            RuleFor(x=>x.Model.DateOfBirth.Year).LessThan((DateTime.Now.Year-15));
            RuleFor(x=>x.Model.DateOfBirth).LessThan((DateTime.Now.Date));
        }
    }
}