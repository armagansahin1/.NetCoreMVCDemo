using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Applications.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorModel Model { get; set; }
        public CreateAuthorCommand(BookStoreDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x=>x.Name == Model.Name);
            if(author is not null)
            {
                throw new InvalidOperationException("Aynı İsimde Yazar Bulunmaktadır");
            }
            author = _mapper.Map<Author>(Model);
            _context.Authors.Add(author);
            _context.SaveChanges();
        }
        public class CreateAuthorModel
        {
            public string Name { get; set; }
            public DateTime DateOfBirth { get; set; }
        }
    }
}