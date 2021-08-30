using System;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.BookOperations.Commands.CreateBook;
using Xunit;
using static WebApi.Applications.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace Applications.BookOperations.Commands.CreateBook
{
  
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("",0,0,1)]
        [InlineData(" ",0,0,1)]
        [InlineData("asdas",0,0,0)]
        [InlineData("asdas",1,0,0)]
        [InlineData("asdas",0,1,0)]
        [InlineData("",1,0,0)]
        [InlineData("",0,1,1)]
        [InlineData("",0,1,1)]
        [InlineData("",1,1,1)]
        [InlineData(" ",1,1,1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int genreId,int pageCount,int authorId)
        {
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model = new CreateBookModel
            {
                Title = title,
                GenreId = genreId,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                AuthorId = authorId
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);
            
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenPublishDateIsEqualNow_Validator_ShouldBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model = new CreateBookModel
            {
                Title = "asdasd",
                GenreId = 1,
                PageCount = 100,
                PublishDate = DateTime.Now

            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);
            
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model = new CreateBookModel
            {
                Title = "asdasd",
                GenreId = 1,
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                AuthorId = 1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);
            
            result.Errors.Count.Should().Be(0);
        }
    }
}