using System;
using WebApi.DbOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                new Book
                {
                    GenreId = 1,
                    Title = "Harry Potter",
                    PageCount = 500,
                    PublishDate = new DateTime(2000, 5, 6),
                    AuthorId = 1

                },
                     new Book
                     {
                         // Id = 2,
                         GenreId = 1,
                         Title = "Lord Of The Rings",
                         PageCount = 1500,
                         PublishDate = new DateTime(2001, 7, 10),
                         AuthorId = 3


                     },
                     new Book
                     {
                         // Id = 3,
                         GenreId = 3,
                         Title = "C# Learning",
                         PageCount = 650,
                         PublishDate = new DateTime(2007, 2, 20),
                         AuthorId = 2
                     }
                 );
        }
    }
}