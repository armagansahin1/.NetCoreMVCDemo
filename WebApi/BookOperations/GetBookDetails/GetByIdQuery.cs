using System;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBookDetails
{
    public class GetByIdQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public int Id { get; set; }
        public GetByIdQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BooksViewModel Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(b => b.Id == Id);

            if (book is null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı !");
            }

            BooksViewModel viewModel = new BooksViewModel()
            {
                Genre = ((GenreEnum)book.GenreId).ToString(),
                PageCount = book.PageCount,
                PublishDate = book.PublishDate.ToString("dd/mm/yyyy"),
                Title = book.Title
            };

            return viewModel;

        }
        public class BooksViewModel
        {
            public string Title { get; set; }
            public string Genre { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
        }

    }
}