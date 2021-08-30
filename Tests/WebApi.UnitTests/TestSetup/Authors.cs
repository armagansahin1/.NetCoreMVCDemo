using System;
using WebApi.DbOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
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
        }
    }
}