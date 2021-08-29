using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Applications.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateGenreModel Model;
        public int GenreId { get; set; }

        public UpdateGenreCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(g=>g.Id == GenreId);
            if(genre is null)
            {
                throw new InvalidOperationException("Kategori bulunamadı");
            }

            genre.Name = genre.Name != default ? Model.Name : genre.Name;
            genre.IsActive = genre.IsActive !=default ? Model.IsActive : genre.IsActive;
            _context.SaveChanges();
        }
        public class UpdateGenreModel
        {
            public string Name { get; set; }
            public bool IsActive { get; set; }
        }
    }
}