using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Applications.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenresQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetGenresQueryModel> Handle()
        {
           var result = _context.Genres.Where(g => g.IsActive == true).OrderBy(g => g.Id);

           var returnObj = _mapper.Map<List<GetGenresQueryModel>>(result);
           return returnObj;
        }

       public class GetGenresQueryModel
       {
           public string Name { get; set; }
           public bool IsActive {get; set;}
       }
    }
}