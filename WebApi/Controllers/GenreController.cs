using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Applications.GenreOperations.Commands.CreateGenre;
using WebApi.Applications.GenreOperations.Commands.DeleteGenre;
using WebApi.Applications.GenreOperations.Commands.UpdateGenre;
using WebApi.Applications.GenreOperations.Queries.GetGenreDetails;
using WebApi.Applications.GenreOperations.Queries.GetGenres;
using WebApi.DbOperations;
using static WebApi.Applications.GenreOperations.Commands.CreateGenre.CreateGenreCommand;
using static WebApi.Applications.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    { 
         private readonly BookStoreDbContext _context;
         private readonly IMapper _mapper;

         public GenreController(BookStoreDbContext context,IMapper mapper)
         {
             _context = context;
             _mapper = mapper;
         }
        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetGenreDetails query = new GetGenreDetails(_context,_mapper);
            query.GenreId = id;
            GetGenreDetailsValidator validator = new GetGenreDetailsValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add([FromBody]CreateGenreModel model)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context,_mapper);
            command.Model = model;
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok("Eklendi");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UpdateGenreModel model)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context,_mapper);
            command.GenreId = id;
            command.Model = model;
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok("Güncellendi");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = id;
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.Validate(command);
            command.Handle();
            return Ok("Silindi");
        }
    }
}