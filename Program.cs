using BookstoreApp.Data;
using BookstoreApp.Data.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookstoreApp
{
    class Program
    {
        static void Main()
        {
            var serviceProvider = new ServiceCollection()
            .AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=../../../bookstore.db"))
            .AddScoped<BookstoreService>()
            .BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            var bookstoreService = scope.ServiceProvider.GetRequiredService<BookstoreService>();

            if (!bookstoreService.Login())
            {
                return;
            }

            while (true)
            {
                Console.WriteLine("\n1. Add Book\n2. List Books\n3. Search Books\n4. Sell Book\n5. View Statistics\n6. Delete book\n7. Exit");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        bookstoreService.AddBookViaConsole();
                        break;
                    case "2":
                        bookstoreService.ListBooks();
                        break;
                    case "3":
                        bookstoreService.SearchBooks();
                        break;
                    case "4":
                        bookstoreService.SellBook();
                        break;
                    case "5":
                        bookstoreService.ViewStatistics();
                        break;
                    case "6":
                        bookstoreService.DeleteBook();
                        break;
                    case "7":
                        Console.WriteLine("Closing store.");
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}
