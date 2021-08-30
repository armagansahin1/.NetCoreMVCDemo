using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;
using FluentValidation;
using WebApi.Applications.BookOperations.Queries.GetBooks;
using WebApi.Applications.BookOperations.Queries.GetBookDetails;
using WebApi.Applications.BookOperations.Commands.CreateBook;
using WebApi.Applications.BookOperations.Commands.UpdateBook;
using WebApi.Applications.BookOperations.Commands.DeleteBook;
using static WebApi.Applications.BookOperations.Commands.CreateBook.CreateBookCommand;
using static WebApi.Applications.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookDetails query = new GetBookDetails(_context, _mapper);
            
                query.Id = id;
                GetBookDetailsValidator validator = new GetBookDetailsValidator();
                validator.ValidateAndThrow(query);
                var result = query.Handle();
                return Ok(result);
 
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);

         
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                command.Model = newBook;
                validator.ValidateAndThrow(command);
                command.Handle();
                return Ok("Eklendi");
           

        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel book)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
           
                command.Model = book;
                command.Id = id;
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
                return Ok("Güncellendi");
        
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
          
                command.Id = id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
                return Ok("Silindi");
   
        }
    }
}