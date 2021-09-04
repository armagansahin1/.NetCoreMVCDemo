using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.BookOperations.Queries.GetBookDetails;
using WebApi.DbOperations;
using Xunit;

namespace Applications.BookOperations.Queries.GetBookDetails
{
    public class GetBookDetailsTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMapper _mapper;
         private readonly BookStoreDbContext _context;
         public GetBookDetailsTests(CommonTestFixture fixture)
         {
             _mapper = fixture.Mapper;
             _context = fixture.Context;
         }

        [Fact]
         public void WhenTheGivenBookIdDoesntExist_InvalidOperationException_ShouldBeReturn()
         {
             GetBookDetailsQuery query = new GetBookDetailsQuery(_context,_mapper);
             query.Id = 100;

             FluentActions.Invoking(()=>query.Handle()).Should().Throw<InvalidOperationException>()
             .And.Message.Should().Be("Kitap Bulunamadı !");
         }
        [Fact]
         public void WhenTheGivenBookIdExist_Book_ShouldBeReturn()
         {
             GetBookDetailsQuery query = new GetBookDetailsQuery(_context,_mapper);
             query.Id = 1;

            var bookToCheck = FluentActions.Invoking(()=>query.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(x=>x.Id == query.Id);

            book.Genre.Name.Should().Equals(bookToCheck.Genre);
            book.PageCount.Should().Equals(bookToCheck.PageCount);
            book.PublishDate.ToString("dd/mm/yyyy").Should().Equals(bookToCheck.PublishDate);
            book.Title.Should().Equals(bookToCheck.Title);
         }
    }
}
