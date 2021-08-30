using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Applications.AuthorOperations.Queries.GetAuthorDetails
{
    public class GetAuthorDetails
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int AuthorId { get; set; }
        public GetAuthorDetails(IBookStoreDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public GetAuthorDetailsModel Handle()
        {
            var author = _context.Authors.SingleOrDefault(a=>a.Id == AuthorId);
            if(author is null)
            {
                throw new InvalidOperationException("Yazar Bulunamadı !");
            }

            var model = _mapper.Map<GetAuthorDetailsModel>(author);
            return model;
        }

        public class GetAuthorDetailsModel
        {
            public string Name { get; set; }
            public string DateOfBirth { get; set; }
        }
    }
}