using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
                    return ;
                }

                context.Books.AddRange(
                     new Book 
                    {
                        //Id = 1,
                        GenreId = 3,
                        Title = "Harry Potter",
                        PageCount = 500,
                        PublishDate = new DateTime(2000,5,6)
                    },
                    new Book 
                    {
                       // Id = 2,
                        GenreId = 3,
                        Title = "Lord Of The Rings",
                        PageCount = 1500,
                        PublishDate = new DateTime(2001,7,10)
                    },
                    new Book 
                    {
                       // Id = 3,
                        GenreId = 1,
                        Title = "C# Learning",
                        PageCount = 650,
                        PublishDate = new DateTime(2007,2,20)
                    }
                );

                context.SaveChanges();
            }
        }
    }
}