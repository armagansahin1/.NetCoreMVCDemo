using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Applications.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateGenreModel Model;

        public CreateGenreCommand(BookStoreDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(g=>g.Name == Model.Name);
            if(genre is not null)
            {
                throw new InvalidOperationException("Bu isimde kategori bulunmakta");
            }

            var newGenre = _mapper.Map<Genre>(Model);
            _context.Genres.Add(newGenre);
            _context.SaveChanges();
        }
        public class CreateGenreModel
        {
            public string Name { get; set; }
            public bool IsActive { get; set; } = true;
        }
    }
}