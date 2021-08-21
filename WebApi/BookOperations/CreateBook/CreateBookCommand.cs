using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;

        public CreateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext ;
        }
        
        public void Handle(CreateBookModel createBookModel)
        {
            var book = _dbContext.Books.SingleOrDefault(b=>b.Title == createBookModel.Title);
            if (book is not null)
            {
                throw new InvalidOperationException("Bu kitap bulunmakta");
            }

            book = new Book(){
                GenreId = createBookModel.GenreId,
                PageCount = createBookModel.PageCount,
                PublishDate = createBookModel.PublishDate,
                Title = createBookModel.Title
            };

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }

        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}