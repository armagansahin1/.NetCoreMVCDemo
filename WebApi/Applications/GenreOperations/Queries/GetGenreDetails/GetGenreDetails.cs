using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Applications.GenreOperations.Queries.GetGenreDetails
{
    public class GetGenreDetails
    {
        public int GenreId { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetails(BookStoreDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GetGenreDetailsModel Handle()
        {
            var result = _context.Genres.SingleOrDefault(g => g.Id == GenreId);

            if(result is null)
            {
                throw new  InvalidOperationException("Kategori Bulunamadı !");
            }

            var returnObj = _mapper.Map<GetGenreDetailsModel>(result);
            return returnObj;
        }
        public class GetGenreDetailsModel
        {
            public string Name { get; set; }
        }
    }
}