using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Applications.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
         private readonly IBookStoreDbContext _dbContext;
        public int Id { get; set; }
        public DeleteBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(b=>b.Id == Id);
            if (book == null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı !");
            }

            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}