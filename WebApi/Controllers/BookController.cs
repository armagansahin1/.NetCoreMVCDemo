using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.AddControllers
{   
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
       private readonly BookStoreDbContext _context;

       public BookController(BookStoreDbContext context)
       {
           _context = context;
       }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetByIdQuery query = new GetByIdQuery(_context);
           try
           {
               var result = query.Handle(id);
               return Ok(result);
           }
           catch (Exception ex)
           {
               
               return BadRequest(ex.Message);
           }
        }

        [HttpPost]
        public IActionResult AddBook ([FromBody] CreateBookModel newBook){
            
           CreateBookCommand command = new CreateBookCommand(_context);
            
            try
            {
                command.Handle(newBook);
                return Ok("Eklendi");
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook (int id,[FromBody] UpdateBookModel book)
        {
           UpdateBookCommand command = new UpdateBookCommand(_context);
           try
           {
               command.Handle(id,book);
               return Ok("Güncellendi");
           }
           catch (Exception ex)
           {
               
               return BadRequest(ex.Message);
           }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var bookToDelete = _context.Books.SingleOrDefault(b=>b.Id == id);
            if(bookToDelete is null)
            {
                return BadRequest();
            }

            _context.Books.Remove(bookToDelete);
            _context.SaveChanges();
            return Ok();

        }
    }
}