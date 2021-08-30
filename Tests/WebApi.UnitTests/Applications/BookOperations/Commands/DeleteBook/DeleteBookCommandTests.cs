using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.BookOperations.Commands.DeleteBook;
using WebApi.DbOperations;
using Xunit;

namespace Applications.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTest :IClassFixture<CommonTestFixture>
    {   
        private readonly BookStoreDbContext _context;
        public DeleteBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenGivenBookIdDoesntExist_InvalidOperationException_ShouldBeReturn()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.Id = 20;

            FluentActions.Invoking(()=>command.Handle()).Should().Throw<InvalidOperationException>().And.
            Message.Should().Be("Kitap Bulunamadı !");
        }
        [Fact]
        public void WhenGivenBookIdExist_Book_ShouldBeDeleted()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.Id = 1;

            FluentActions.Invoking(()=>command.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(x=>x.Id == command.Id);
            book.Should().BeNull();
        }
    }
}