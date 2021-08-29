using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Applications.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        private readonly BookStoreDbContext _context;
        public int AuthorId { get; set; }
        public UpdateAuthorModel Model { get; set; }
        public UpdateAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
            {
                new InvalidOperationException("Yazar Bulunamadı");
            }
            author.Name = Model.Name != default ? Model.Name : author.Name;
            author.DateOfBirth = Model.DateOfBirth != default ? Model.DateOfBirth : author.DateOfBirth;
            _context.SaveChanges();
        }
        public class UpdateAuthorModel
        {
            public string Name { get; set; }
            public DateTime DateOfBirth { get; set; }
        }
    }
}