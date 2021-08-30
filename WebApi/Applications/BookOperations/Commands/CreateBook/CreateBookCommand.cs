using System;
using System.Linq;
using AutoMapper;
using WebApi.Common;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Applications.BookOperations.Commands.CreateBook
{
    public class CreateBookCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateBookModel Model { get; set; }
        public CreateBookCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ;
            _mapper = mapper ;
           
        }
        
        public void Handle()
        {
            
            var book = IsBookExist(Model.Title);
            IsAuthorExist(Model.AuthorId);
            IsGenreExist(Model.GenreId);
            
            book = _mapper.Map<Book>(Model);

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }

        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int AuthorId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }

        private Book IsBookExist(string title)
        {
            var book = _dbContext.Books.SingleOrDefault(b=>b.Title == title);
            if (book is not null)
            {
                throw new InvalidOperationException("Bu kitap bulunmakta");
            }
            return book;
        }

        private void IsAuthorExist(int authorId)
        {
            var result = _dbContext.Authors.SingleOrDefault(x=>x.Id==authorId);
            if (result is null)
            {
                throw new InvalidOperationException("Kayıtlı Yazar Bulunamadı");
            }
        }
        private void IsGenreExist(int genreId)
        {
            var result = _dbContext.Genres.SingleOrDefault(x=>x.Id==genreId);
            if (result is null)
            {
                throw new InvalidOperationException("Kategori Bulunamadı");
            }
        }

    }
}