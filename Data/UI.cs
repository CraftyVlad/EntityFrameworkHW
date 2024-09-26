using BookstoreApp.Data.Entities;

namespace BookstoreApp.Data.UI
{
    public class BookstoreService
    {
        private readonly AppDbContext _dbContext;

        public BookstoreService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Login()
        {
            _dbContext.Database.EnsureCreated();
            Console.WriteLine("=== Welcome to the Bookstore ===");

            while (true)
            {
                Console.WriteLine("1. Log in to existing account");
                Console.WriteLine("2. Create a new account");
                var choice = Console.ReadLine();

                if (choice == "1")
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Console.Write("Enter login: ");
                        var login = Console.ReadLine();

                        Console.Write("Enter password: ");
                        var password = Console.ReadLine();

                        var user = _dbContext.Users.SingleOrDefault(u => u.Login == login && u.Password == password);
                        if (user != null)
                        {
                            Console.WriteLine("Login successful!");
                            return true;
                        }

                        Console.WriteLine("Invalid login or password. Please try again.");
                    }

                    Console.WriteLine("Failed login attempts. Exiting...");
                    return false;
                }
                else if (choice == "2")
                {
                    Console.Write("Enter a new login: ");
                    var newLogin = Console.ReadLine();

                    if (_dbContext.Users.Any(u => u.Login == newLogin))
                    {
                        Console.WriteLine("This login already exists. Please try again.");
                        continue;
                    }

                    Console.Write("Enter a new password: ");
                    var newPassword = Console.ReadLine();

                    var newUser = new User { Login = newLogin, Password = newPassword };
                    _dbContext.Users.Add(newUser);
                    _dbContext.SaveChanges();

                    Console.WriteLine("Account created successfully! You can now log in.");
                }
                else
                {
                    Console.WriteLine("Invalid choice, please select either 1 or 2.");
                }
            }
        }

        public void AddBookViaConsole()
        {
            _dbContext.Database.EnsureCreated();
            var book = new Book();

            Console.Write("Enter title: ");
            book.Title = Console.ReadLine();

            Console.Write("Enter author: ");
            book.Author = Console.ReadLine();

            Console.Write("Enter genre: ");
            book.Genre = Console.ReadLine();

            Console.Write("Enter publisher: ");
            book.Publisher = Console.ReadLine();

            Console.Write("Enter page count: ");
            book.PageCount = int.Parse(Console.ReadLine());

            Console.Write("Enter publication year: ");
            book.PublicationYear = int.Parse(Console.ReadLine());

            Console.Write("Enter cost price: ");
            book.CostPrice = decimal.Parse(Console.ReadLine());

            Console.Write("Enter sale percentage (0-100): ");
            book.SalePercentage = int.Parse(Console.ReadLine());

            Console.Write("Enter books in stock: ");
            book.BooksInStock = int.Parse(Console.ReadLine());

            Console.Write("Is this book a sequel? (y/n): ");
            var isSequelInput = Console.ReadLine().ToLower();
            book.IsSequel = isSequelInput == "y";

            if (book.IsSequel)
            {
                Console.Write("Enter the title of the book this is a sequel to: ");
                book.SequelTo = Console.ReadLine();
            }
            else
            {
                book.SequelTo = null;
            }

            Console.Write("Is this book reserved? (y/n): ");
            var isReservedInput = Console.ReadLine().ToLower();
            book.IsReserved = isReservedInput == "y";

            book.BooksSold = 0;
            book.CreatedDate = DateTime.Now;

            book.SalePrice = (book.SalePercentage) > 0
            ? book.CostPrice - (book.CostPrice * (book.SalePercentage / 100m))
            : book.CostPrice;

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
            Console.WriteLine("Book added successfully.");
        }


        public void ListBooks()
        {
            _dbContext.Database.EnsureCreated();
            var books = _dbContext.Books.ToList();

            if (!books.Any())
            {
                Console.WriteLine("No books available.");
                return;
            }

            DisplayBooks(books);
        }

        public void SearchBooks()
        {
            _dbContext.Database.EnsureCreated();
            Console.WriteLine("Enter search criteria (leave blank to skip):");

            Console.Write("1. Search by Title\n2. Search by Author\n3. Search by Genre\n");
            var choice = Console.ReadLine();

            var query = _dbContext.Books.AsQueryable();

            if (choice == "1")
            {
                Console.Write("Title: ");
                var title = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(title))
                {
                    query = query.Where(b => b.Title.Contains(title));
                }
            }
            else if (choice == "2")
            {
                Console.Write("Author: ");
                var author = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(author))
                {
                    query = query.Where(b => b.Author.Contains(author));
                }
            }
            else if (choice == "3")
            {
                Console.Write("Genre: ");
                var genre = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(genre))
                {
                    query = query.Where(b => b.Genre.Contains(genre));
                }
            }
            else
            {
                Console.WriteLine("Invalid choice. Returning to menu.");
                return;
            }

            var books = query.ToList();

            if (!books.Any())
            {
                Console.WriteLine("No books found.");
            }
            else
            {
                DisplayBooks(books);
            }
        }

        public void ViewStatistics()
        {
            _dbContext.Database.EnsureCreated();
            var today = DateTime.Now.Date;
            var week = today.AddDays(-7);
            var month = today.AddMonths(-1);
            var year = today.AddYears(-1);

            Console.WriteLine("Popular books today:");
            var popularToday = _dbContext.Books.OrderByDescending(b => b.BooksSold).Where(b => b.CreatedDate >= today).Take(3).ToList();
            DisplayBooks(popularToday);

            Console.WriteLine("Popular books this week:");
            var popularWeek = _dbContext.Books.OrderByDescending(b => b.BooksSold).Where(b => b.CreatedDate >= week).Take(3).ToList();
            DisplayBooks(popularWeek);

            Console.WriteLine("Popular books this month:");
            var popularMonth = _dbContext.Books.OrderByDescending(b => b.BooksSold).Where(b => b.CreatedDate >= month).Take(3).ToList();
            DisplayBooks(popularMonth);

            Console.WriteLine("Popular books this year:");
            var popularYear = _dbContext.Books.OrderByDescending(b => b.BooksSold).Where(b => b.CreatedDate >= year).Take(3).ToList();
            DisplayBooks(popularYear);
        }

        private void DisplayBooks(IEnumerable<Book> books)
        {
            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.Id} | Title: {book.Title} | Author: {book.Author} | Genre: {book.Genre} | Price: ${book.CostPrice} | Sale Price: ${book.SalePrice} ({book.SalePercentage}% off) | Sold: {book.BooksSold} | In Stock: {book.BooksInStock} | Sequel: {(book.IsSequel ? "Yes" : "No")} | Reserved: {(book.IsReserved ? "Yes" : "No")}");
            }
        }

        public void SellBook()
        {
            _dbContext.Database.EnsureCreated();
            Console.Write("Enter the ID of the book to sell: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            var book = _dbContext.Books.Find(bookId);
            if (book == null || book.BooksInStock == 0)
            {
                Console.WriteLine("Book not found or out of stock.");
                return;
            }

            if (book.IsReserved)
            {
                Console.WriteLine("This book is reserved and cannot be sold.");
                return;
            }

            book.BooksInStock -= 1;
            book.BooksSold += 1;

            _dbContext.SaveChanges();
            Console.WriteLine($"Book '{book.Title}' sold successfully.");
        }

        public void DeleteBook()
        {
            _dbContext.Database.EnsureCreated();

            Console.Write("Enter the ID of the book to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            var book = _dbContext.Books.Find(bookId);
            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            _dbContext.Remove(book);
            _dbContext.SaveChanges();
            Console.WriteLine($"Book deleted.");
        }

        private decimal CalculateSalePrice(Book book)
        {
            var costPrice = Convert.ToDecimal(book.CostPrice);
            var discount = book.SalePercentage / 100.0m;
            return costPrice * (1 - discount);
        }
    }
}
