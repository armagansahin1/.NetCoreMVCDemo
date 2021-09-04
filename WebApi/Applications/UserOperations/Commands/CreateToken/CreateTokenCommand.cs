using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WebApi.DbOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Applications.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateTokenModel Model { get; set; }
        private readonly IConfiguration _configuration;
        public CreateTokenCommand(IBookStoreDbContext dbContext, IMapper mapper,IConfiguration configuration)
        {
            _dbContext = dbContext ;
            _mapper = mapper ;
            _configuration = configuration;
           
        }
        
        public Token Handle()
        {
            var user = _dbContext.Users.FirstOrDefault(x=>x.Email == Model.Email && x.Password == Model.Password);
            if(user != null)
            {
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _dbContext.SaveChanges();
                return token;
            }
            else
            {
                throw new InvalidOperationException("Email veya password hatalı");
            }
        }

        public class CreateTokenModel
        {
            public string Email {get;set;}
            public string Password {get;set;}
        }
    }
}