using FluentAssertions;
using TestSetup;
using WebApi.Applications.BookOperations.Commands.DeleteBook;
using Xunit;

namespace Applications.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenGivenBookIdLessOrEqualThanZero_Validator_ShouldBeReturnError(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.Id = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void WhenGivenBookIdGreaterThanZero_Validator_ShouldNotBeReturnError(int id)
        {
             DeleteBookCommand command = new DeleteBookCommand(null);
            command.Id = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}