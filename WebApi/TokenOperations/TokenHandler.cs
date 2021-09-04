using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities;
using WebApi.TokenOperations.Models;

namespace WebApi.TokenOperations
{
    public class TokenHandler
    {
        public IConfiguration Configuraiton {get; set;}
        public TokenHandler(IConfiguration configuraiton)
        {
            Configuraiton = configuraiton;
        }

        public Token CreateAccessToken(User user)
        {
            Token tokenModel = new Token();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuraiton["Token:SecurityKey"]));
            SigningCredentials credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            tokenModel.Expiration = DateTime.Now.AddMinutes(15);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer:Configuraiton["Token:Issuer"],
                audience:Configuraiton["Token:Audience"],
                expires:tokenModel.Expiration,
                signingCredentials:credentials
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            tokenModel.AccessToken = tokenHandler.WriteToken(securityToken);
            tokenModel.RefreshToken=CreateRefreshToken();

            return tokenModel;
        }

        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

    }
}