using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Applications.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
         private readonly BookStoreDbContext _context;
         public int GenreId { get; set; }

         public DeleteGenreCommand(BookStoreDbContext context)
         {
             _context = context;
         }

         public void Handle()
         {
             var genreToDelete = _context.Genres.SingleOrDefault(g=> g.Id == GenreId);
            
            if (genreToDelete is null)
            {
                throw new InvalidOperationException("Kategori bulunamadı");
            }

            _context.Genres.Remove(genreToDelete);
            _context.SaveChanges();
         }
    }
}