using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int Id { get; set; }
        public UpdateBookModel Model { get; set; }

        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var bookToUpdate = _dbContext.Books.SingleOrDefault(b => b.Id == Id);
            if (bookToUpdate is null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı");
            }

            bookToUpdate.GenreId = Model.GenreId != default ? Model.GenreId : bookToUpdate.GenreId;

            bookToUpdate.Title = Model.Title != default ? Model.Title : bookToUpdate.Title;

            _dbContext.SaveChanges();


        }

        public class UpdateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
        }
    }
}