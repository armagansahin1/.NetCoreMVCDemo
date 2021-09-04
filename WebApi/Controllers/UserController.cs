using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Applications.UserOperations.Commands.CreateToken;
using WebApi.Applications.UserOperations.Commands.CreateUser;
using WebApi.Applications.UserOperations.Commands.RefreshToken;
using WebApi.DbOperations;
using WebApi.TokenOperations.Models;
using static WebApi.Applications.UserOperations.Commands.CreateToken.CreateTokenCommand;
using static WebApi.Applications.UserOperations.Commands.CreateUser.CreateUserCommand;

namespace WebApi.AddControllers
{
    
    [ApiController]
    [Route("[controller]s")]
    public class UserController : ControllerBase
    { 
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserController(IBookStoreDbContext context,IMapper mapper,IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel userModel)
        {
            CreateUserCommand command = new CreateUserCommand(_context,_mapper);
            command.Model = userModel;
            command.Handle();

            return Ok();
        }

        [HttpPost("connection/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_context,_mapper,_configuration);
            command.Model = login;
            var token = command.Handle();
            return token;
        }

        [HttpGet("refreshtoken")]
        public ActionResult<Token> RefreshToken([FromQuery] string token)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_context,_configuration);
            command.RefreshToken=token;
            var resultToken =  command.Handle();
            return resultToken;
        }
    }
}