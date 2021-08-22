using System;
using System.Linq;
using AutoMapper;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBookDetails
{
    public class GetBookDetails
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int Id { get; set; }

        public BookDetailModel viewModel;
        public GetBookDetails(BookStoreDbContext dbContext, IMapper mapper )
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailModel Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(b => b.Id == Id);

            if (book is null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı !");
            }

            viewModel = _mapper.Map<BookDetailModel>(book);

            return viewModel;

        }
        public class BookDetailModel
        {
            public string Title { get; set; }
            public string Genre { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
        }

    }
}