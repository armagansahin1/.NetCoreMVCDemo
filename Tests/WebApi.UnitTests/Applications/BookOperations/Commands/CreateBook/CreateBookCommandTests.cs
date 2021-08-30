using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.BookOperations.Commands.CreateBook;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Applications.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace Applications.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;

            
        }

        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            var book = new Book {Title="Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",GenreId = 1, AuthorId = 1, PageCount = 100, PublishDate = new DateTime(2000,1,1) };
            _context.Books.Add(book);
            _context.SaveChanges();

            
            command.Model = new CreateBookModel{Title = book.Title};

            FluentActions.Invoking(()=>command.Handle()).
            Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bu kitap bulunmakta");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
           CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            command.Model = new CreateBookModel
            {
                Title = "asdasdasd",
                GenreId = 1, 
                AuthorId = 1,           
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-10)
            };

            FluentActions.Invoking(()=>command.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(x=>x.Title == command.Model.Title);

            book.Should().NotBeNull();
            book.PageCount.Should().Be(command.Model.PageCount);
            book.GenreId.Should().Be(command.Model.GenreId);
            book.Title.Should().Be(command.Model.Title);
        }
        [Fact]
        public void WhenGivenAuthorIdDoesntExist_InvalidOperationException_ShouldBeReturn()
        {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            command.Model = new CreateBookModel
            {
                Title = "sadasdfg",
                GenreId = 1,
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-10),
                AuthorId = 20
            };

            FluentActions.Invoking(()=>command.Handle()).Should().Throw<InvalidOperationException>().
            And.Message.Should().Be("Kayıtlı Yazar Bulunamadı");
        }

        [Fact]
        public void WhenGivenGenreIdDoesntExist_InvalidOperationException_ShouldBeReturn()
        {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
             command.Model = new CreateBookModel
            {
                Title = "sadasd",
                GenreId = 15,
                AuthorId = 1,
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-10)
            };

            FluentActions.Invoking(()=>command.Handle()).Should().Throw<InvalidOperationException>().
            And.Message.Should().Be("Kategori Bulunamadı");
        }
    }
}