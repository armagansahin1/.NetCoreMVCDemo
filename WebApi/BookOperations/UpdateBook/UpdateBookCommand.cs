using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;

        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle(int id, UpdateBookModel updateBookModel)
        {
            var bookToUpdate = _dbContext.Books.SingleOrDefault(b => b.Id == id);
            if (bookToUpdate is null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı");
            }

           bookToUpdate.GenreId = updateBookModel.GenreId != default ? updateBookModel.GenreId : bookToUpdate.GenreId;
           bookToUpdate.PageCount = updateBookModel.PageCount !=default ? updateBookModel.PageCount : bookToUpdate.PageCount;
           bookToUpdate.Title = updateBookModel.Title != default ? updateBookModel.Title : bookToUpdate.Title;
           bookToUpdate.PublishDate = updateBookModel.PublishDate;
           _dbContext.SaveChanges();


         }

        public class UpdateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}