using System;
using WebApi.DbOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Genres
    {
        public static void AddGenre(this BookStoreDbContext context)
        {
            context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Science Finction"
                    },
                    new Genre
                    {
                        Name = "Personal Growth"
                    },
                    new Genre
                    {
                        Name = "Learning Skills"
                    }
                );
        }
    }
}