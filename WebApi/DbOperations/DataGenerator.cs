using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                context.Authors.AddRange(
                    new Author
                    {
                        Name = "JK Rowling",
                        DateOfBirth = new DateTime(1978, 5, 6)
                    },
                    new Author
                    {
                        Name = "Armağan Şahin",
                        DateOfBirth = new DateTime(1995, 5, 19)
                    },
                    new Author
                    {
                        Name = "J.R.R Tolkien",
                        DateOfBirth = new DateTime(1985, 6, 8)
                    }
                );

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

                context.Books.AddRange(
                     new Book
                     {
                         //Id = 1,
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

                context.SaveChanges();
            }
        }
    }
}