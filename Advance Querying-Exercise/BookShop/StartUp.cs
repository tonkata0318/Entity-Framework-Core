namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
    using System.Globalization;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Text.RegularExpressions;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);
            Console.WriteLine(RemoveBooks(db));
        }
        //public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        //{
        //    if (!Enum.TryParse<AgeRestriction>(command,true,out var ageRestriction))
        //    {
        //        return $"{command} is not a valid age restriction";
        //    }

        //    StringBuilder sb = new StringBuilder();
        //    var books = context.Books
        //        .Where(x=>x.AgeRestriction==ageRestriction)
        //        .Select(x => new
        //        {
        //            x.Title
        //        }
        //        ).OrderBy(x=>x.Title);
        //    foreach (var item in books)
        //    {
        //        sb.AppendLine($"{item.Title}");
        //    }


        //    return sb.ToString().Trim();
        //}
        //public static string GetGoldenBooks(BookShopContext context)
        //{
        //    Enum.TryParse<EditionType>("Gold", false, out var GoldenRestriction);
        //    var books=context.Books
        //        .Where(b=>b.EditionType==GoldenRestriction)
        //        .Where(b=>b.Copies<5000)
        //        .Select(b=> new
        //        {
        //            b.Title,
        //            b.BookId
        //        }).OrderBy(b=>b.BookId).ToList();

        //    return string.Join(Environment.NewLine, books.Select(b => b.Title));
        //}
        //public static string GetBooksByPrice(BookShopContext context)
        //{
        //    StringBuilder sb= new StringBuilder();

        //    var books = context.Books
        //        .Where(b => b.Price > 40)
        //        .Select(b => new
        //        {
        //            b.Title,
        //            b.Price
        //        }).OrderByDescending(b => b.Price);
        //    foreach (var book in books)
        //    {
        //        sb.AppendLine($"{book.Title} - ${book.Price:f2}");
        //    }
        //    return sb.ToString().Trim();
        //}
        //public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        //{
        //    var books = context.Books
        //        .Where(b=>b.ReleaseDate.HasValue && b.ReleaseDate.Value.Year!=year)
        //        .Select(b=> new
        //        {
        //            b.Title,
        //            b.BookId
        //        }).OrderBy(b=>b.BookId).ToList();
        //   return string.Join(Environment.NewLine, books.Select(b=>b.Title));
        //}
        //public static string GetBooksByCategory(BookShopContext context, string input)
        //{
        //    string[] categories = input.Split(" ", StringSplitOptions.RemoveEmptyEntries)
        //        .Select(c=>c.ToLower()).ToArray();

        //    var books = context.Books
        //        .Select(b=> new
        //        {
        //            b.Title,
        //            b.BookCategories
        //        })
        //        .Where(b => b.BookCategories.Any(bc => categories.Contains(bc.Category.Name.ToLower())))
        //        .OrderBy(b => b.Title)
        //        .ToList();

        //    return string.Join(Environment.NewLine, books.Select(b=>b.Title));
        //}
        //public static string GetBooksReleasedBefore(BookShopContext context, string date)
        //{
        //    var parsedData=DateTime.ParseExact(date, "dd-MM-yyyy",CultureInfo.InvariantCulture);
        //    StringBuilder stringBuilder=new StringBuilder();
        //    var books = context.Books
        //        .Where(b => b.ReleaseDate.HasValue && b.ReleaseDate.Value < parsedData)
        //        .Select(b => new
        //        {
        //            b.Title,
        //            b.EditionType,
        //            b.Price,
        //            b.ReleaseDate
        //        }).OrderByDescending(b => b.ReleaseDate).ToList();

        //    foreach (var book in books)
        //    {
        //        stringBuilder.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:f2}");
        //    }
        //    return stringBuilder.ToString().Trim();
        //}
        //public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        //{
        //    var authors = context.Authors
        //        .Where(a => a.FirstName.EndsWith(input))
        //        .Select(a => new
        //        {
        //            FullName=a.FirstName+" "+a.LastName
        //        }).OrderBy(a=>a.FullName).ToList();
        //    return string.Join(Environment.NewLine, authors.Select(a=>a.FullName));
        //}
        //public static string GetBookTitlesContaining(BookShopContext context, string input)
        //{
        //    input= input.ToLower();
        //    var booksTitles = context.Books
        //        .Where(x => x.Title.ToLower().Contains(input))
        //        .Select(b=>new {
        //        b.Title
        //        }).OrderBy(b=>b.Title).ToList();

        //    return string.Join(Environment.NewLine, booksTitles.Select(b => b.Title));
        //}
        //public static string GetBooksByAuthor(BookShopContext context, string input)
        //{
        //    StringBuilder sb= new StringBuilder();
        //    input=input.ToLower();
        //    var books = context.Books
        //        .Where(b=>b.Author.LastName.ToLower().StartsWith(input))
        //        .Select(b=> new
        //        {
        //            b.Title,
        //            AuthorName=b.Author.FirstName+" "+b.Author.LastName,
        //            b.BookId
        //        }).OrderBy(b=>b.BookId).ToList();

        //    foreach (var book in books)
        //    {
        //        sb.AppendLine($"{book.Title} ({book.AuthorName})");
        //    }
        //    return sb.ToString().Trim();
        //}
        //public static int CountBooks(BookShopContext context, int lengthCheck)
        //{
        //    var books = context.Books
        //        .Where(b => b.Title.Length > lengthCheck);

        //    return books.Count();
        //}
        //public static string CountCopiesByAuthor(BookShopContext context)
        //{
        //    var authors = context.Authors
        //        .Select(a => new
        //        {
        //            AuthorFullName = a.FirstName + " " + a.LastName,
        //            TotalBooksByAuthor = a.Books.Sum(b => b.Copies)
        //        }).OrderByDescending(a=>a.TotalBooksByAuthor).ToList();

        //    return string.Join(Environment.NewLine, authors.Select(a => $"{a.AuthorFullName} - {a.TotalBooksByAuthor}"));
        //}
        //public static string GetTotalProfitByCategory(BookShopContext context)
        //{
        //    var profirByCategories = context.Categories
        //        .Select(c => new
        //        {
        //            CategoryName = c.Name,
        //            TotalProfit = c.CategoryBooks.Sum(cb => cb.Book.Copies * cb.Book.Price)
        //        }).OrderByDescending(c => c.TotalProfit)
        //        .ThenBy(c => c.CategoryName).ToList();

        //    return string.Join(Environment.NewLine, profirByCategories.Select(p => $"{p.CategoryName} ${p.TotalProfit:f2}"));
        //}
        //public static string GetMostRecentBooks(BookShopContext context)
        //{
        //    var recentBooks = context.Categories
        //        .Select(c => new
        //        {
        //            CategoryName = c.Name,
        //            MostRecentBooks = c.CategoryBooks.OrderByDescending(bc => bc.Book.ReleaseDate).Take(3)
        //            .Select(c => new
        //            {
        //                BookTitle = c.Book.Title,
        //                BookYear = c.Book.ReleaseDate.Value.Year
        //            })
        //        }).OrderBy(c => c.CategoryName).ToList();

        //    StringBuilder sb= new StringBuilder();
        //    foreach (var book in recentBooks)
        //    {
        //        sb.AppendLine($"--{book.CategoryName}");
        //        foreach (var book1 in book.MostRecentBooks)
        //        {
        //            sb.AppendLine($"{book1.BookTitle} ({book1.BookYear})");
        //        }
        //    }
        //    return sb.ToString().Trim();
        //}
        //public static void IncreasePrices(BookShopContext context)
        //{
        //    var books = context.Books
        //        .Where(b => b.ReleaseDate.Value.Year < 2010);

        //    foreach (var book in books)
        //    {
        //        book.Price += 5;
        //    }

        //    context.SaveChanges();
        //}
        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Copies < 4200);
            int count = books.Count();
            context.Books.RemoveRange(books);
            context.SaveChanges();
            return count;
        }
    }
}


