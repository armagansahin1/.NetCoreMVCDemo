using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Applications.BookOperations.Queries.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBooksQuery(BookStoreDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper ;
        }

        public List<BookViewModel> Handle()
        {
            var bookList = _dbContext.Books.Include(b => b.Genre).Include(b=>b.Author).OrderBy(b=>b.Id).ToList<Book>();
            List<BookViewModel> vmList = new List<BookViewModel>();
  
            vmList = _mapper.Map<List<BookViewModel>>(bookList);

            return vmList;
        }

        public class BookViewModel
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public string Genre { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set;}
        }
    }
}