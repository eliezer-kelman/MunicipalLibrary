using Microsoft.EntityFrameworkCore;
using MunicipalLibrary.Models;
using NuGet.LibraryModel;

namespace MunicipalLibrary.DAL
{
    public class DataLayer : DbContext
    {
        // הבנאי מקבל את מסלול החיבור לשרת וגם שולח אותו לפונקציה שתשלח את התוצאה לבנאי של האבא
        public DataLayer(string ConnectionString) : base(GetOptions(ConnectionString))
        {
            // בדיקה שהבסיס נתונים אכן נוצר
            Database.EnsureCreated();
            Seed();
        }

        private void Seed()
        {
            if (!Libraries.Any())
            {
                Libary libary1 = new Libary
                {
                    Genre = "תורה"
                };

                libary1.ShelfList = GetDefaultShits(libary1);
                Libraries.Add(libary1);
                SaveChanges();
            }

        }

        private List<Shelf> GetDefaultShits(Libary NewLibary)
        {
            Shelf shelf = new Shelf
            {
                NumShelf = 101,
                HeightShelf = 60,
                Library = NewLibary
            };

            shelf.BookList = GetDefaultBooks(shelf);
            Shelves.Add(shelf);
            List<Shelf> ShelfList = new List<Shelf>();
            ShelfList.Add(shelf);
            return ShelfList;
        }

        private List<Book> GetDefaultBooks(Shelf NewShelf)
        {
            Book book = new Book
            {
                NameBook = "חומש",
                HeightBook = 52,
                Shelf = NewShelf
            };
            Books.Add(book);
            List<Book> bookList = new List<Book>();
            bookList.Add(book);
            return bookList;
        }




        public DbSet<Libary> Libraries { get; set; }

        public DbSet<Shelf> Shelves { get; set; }
        public DbSet<Book> Books { get; set; }
        private static DbContextOptions GetOptions(string ConnectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), ConnectionString).Options;
        }
    }
}
