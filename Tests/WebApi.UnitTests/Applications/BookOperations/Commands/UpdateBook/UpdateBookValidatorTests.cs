using FluentAssertions;
using TestSetup;
using WebApi.Applications.BookOperations.Commands.UpdateBook;
using Xunit;
using static WebApi.Applications.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace Applications.BookOperations.Commands.UpdateBook
{
    public class UpdateBookValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,0,"")]
        [InlineData(0,0," ")]
        [InlineData(0,1,"sad")]
        [InlineData(1,0,"sad")]
        [InlineData(1,0,"")]
        [InlineData(1,0," ")]
        [InlineData(0,1," ")]
        [InlineData(0,1,"")]
        [InlineData(1,1," ")]
        [InlineData(1,1,"")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id,int genreId,string title)
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Id = id;
            command.Model = new UpdateBookModel
            {
                GenreId = genreId,
                Title = title,
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Id = 1;
            command.Model = new UpdateBookModel
            {
                GenreId = 3,
                Title = "aasd",
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}