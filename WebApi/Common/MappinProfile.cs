using AutoMapper;
using WebApi.Entities;
using static WebApi.Applications.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using static WebApi.Applications.AuthorOperations.Queries.GetAuthorDetails.GetAuthorDetails;
using static WebApi.Applications.AuthorOperations.Queries.GetAuthors.GetAuthorsQuery;
using static WebApi.Applications.BookOperations.Commands.CreateBook.CreateBookCommand;
using static WebApi.Applications.BookOperations.Queries.GetBookDetails.GetBookDetailsQuery;
using static WebApi.Applications.BookOperations.Queries.GetBooks.GetBooksQuery;
using static WebApi.Applications.GenreOperations.Commands.CreateGenre.CreateGenreCommand;
using static WebApi.Applications.GenreOperations.Queries.GetGenreDetails.GetGenreDetails;
using static WebApi.Applications.GenreOperations.Queries.GetGenres.GetGenresQuery;
using static WebApi.Applications.UserOperations.Commands.CreateUser.CreateUserCommand;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();



            CreateMap<Book, BookViewModel>().ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.ToString("dd/mm/yyyy")))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name));

            CreateMap<Book, BookDetailModel>().ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.ToString("dd/mm/yyyy")))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name)); ;

            CreateMap<Genre, GetGenresQueryModel>();
            CreateMap<Genre, GetGenreDetailsModel>();

            CreateMap<CreateGenreModel, Genre>();

            CreateMap<Author, GetAuthorsModel>().ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString("dd/mm/yyyy")));
            CreateMap<Author, GetAuthorDetailsModel>().ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString("dd/mm/yyyy")));

            CreateMap<CreateAuthorModel,Author>();

            CreateMap<CreateUserModel,User>();
        }
    }
}