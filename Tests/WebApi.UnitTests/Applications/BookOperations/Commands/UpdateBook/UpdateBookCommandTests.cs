using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.BookOperations.Commands.UpdateBook;
using WebApi.DbOperations;
using Xunit;
using static WebApi.Applications.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace Applications.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;

        }

        [Fact]
        public void WhenTheGivenBookIdIsNotExist_InvalidOperationException_ShouldBeReturn()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.Id = 15;
            FluentActions.Invoking(()=>command.Handle()).Should().Throw<InvalidOperationException>().
            And.Message.Should().Be("Kitap Bulunamadı");
        }
        [Fact]
        public void WhenTheGivenBookIdIsExist_InvalidOperationException_ShouldNotBeReturn()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.Id = 3;

            FluentActions.Invoking(()=>command.Handle()).Should().NotThrow<InvalidOperationException>();

        }
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.Id = 3;
            command.Model = new UpdateBookModel
            {
                GenreId =1,
                Title = "sadsdasd"
            };

            FluentActions.Invoking(()=>command.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(x=>x.Id == command.Id);
            book.Should().NotBeNull();
            book.GenreId.Should().Be(command.Model.GenreId);
            book.Title.Should().Be(command.Model.Title);
        }
    }
}