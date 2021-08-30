using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Applications.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(IBookStoreDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetAuthorsModel> Handle()
        {
            var author = _context.Authors.OrderBy(a=> a.Id).ToList();

            var model = _mapper.Map<List<GetAuthorsModel>>(author);
            return model;
        }
        public class GetAuthorsModel
        {
            public string Name { get; set; }
            public string DateOfBirth { get; set; }
        }
    }
}