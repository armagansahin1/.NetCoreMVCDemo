using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Applications.AuthorOperations.Commands.CreateAuthor;
using WebApi.Applications.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Applications.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Applications.AuthorOperations.Queries.GetAuthorDetails;
using WebApi.Applications.AuthorOperations.Queries.GetAuthors;
using WebApi.DbOperations;
using static WebApi.Applications.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using static WebApi.Applications.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorController(IBookStoreDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetAuthorDetails query = new GetAuthorDetails(_context, _mapper);
            query.AuthorId = id;
            GetAuthorDetailsValidator validator = new GetAuthorDetailsValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Add([FromBody] CreateAuthorModel model)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = model;
            CreateAuthorValidator validator = new CreateAuthorValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok("Eklendi");
        }
        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel model)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = id;
            command.Model = model;
            UpdateAuthorValidator validator = new UpdateAuthorValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok("Güncellendi");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = id;
            DeleteAuthorValidator validator = new DeleteAuthorValidator();
            validator.Validate(command);
            command.Handle();
            return Ok("Silindi");
        }
    }
}