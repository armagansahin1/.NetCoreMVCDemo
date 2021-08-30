using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Applications.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDbContext _context;
        public int AuthorId { get; set; }
        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x=>x.Id==AuthorId);
            if(author is null)
            {
                throw new InvalidOperationException("Yazar Bulunamadı");
            }
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}