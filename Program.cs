using Microsoft.EntityFrameworkCore;

public class Author
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Email { get; set; }
    public ICollection<Book> Books { get; set; } = new List<Book>();
}

public class Book
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int? PublicationYear { get; set; }
    public int AuthorId { get; set; }
    public Author? Author { get; set; }
    public int? Pages { get; set; }
}

public class AppDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=../../../LibraryDB.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
            .Property(a => a.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Author>()
            .HasIndex(a => a.Name)
            .IsUnique(false);

        modelBuilder.Entity<Author>()
            .Property(a => a.Name)
            .IsRequired();

        modelBuilder.Entity<Author>()
            .HasIndex(a => a.Email)
            .IsUnique();

        modelBuilder.Entity<Book>()
            .Property(b => b.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Book>()
            .HasIndex(b => b.Title)
            .IsUnique(false);

        modelBuilder.Entity<Book>()
            .Property(b => b.Title)
            .IsRequired();

        modelBuilder.Entity<Book>()
            .Property(b => b.PublicationYear)
            .ValueGeneratedOnAdd();
    }
}

public static class DbInitializer
{
    public static void Initialize(AppDbContext db)
    {
        if (!db.Authors.Any())
        {
            var authors = new Author[]
            {
                new Author { Name = "George Orwell", BirthDate = new DateTime(1903, 6, 25), Email = "george.orwell@example.com" },
                new Author { Name = "Aldous Huxley", BirthDate = new DateTime(1894, 7, 26), Email = "aldous.huxley@example.com" }
            };

            db.Authors.AddRange(authors);
            db.SaveChanges();

            var books = new Book[]
            {
                new Book { Title = "1984", PublicationYear = 1949, AuthorId = authors[0].Id },
                new Book { Title = "Brave New World", PublicationYear = 1932, AuthorId = authors[1].Id }
            };

            db.Books.AddRange(books);
            db.SaveChanges();
        }
    }
}

class Program
{
    static void Main()
    {
        using (AppDbContext db = new AppDbContext())
        {
            DbInitializer.Initialize(db);

            PrintAllAuthorsAndBooks(db);

            var newAuthor = new Author
            {
                Name = "Isaac Asimo",
                BirthDate = new DateTime(1920, 1, 2),
                Email = "asimo@example.com"
            };

            if (ValidateAuthor(newAuthor))
            {
                AddAuthor(db, newAuthor);
            }

            UpdateBookPages(db);

            PrintAllAuthorsAndBooks(db);
        }
    }

    public static void UpdateBookPages(AppDbContext db)
    {
        var books = db.Books.Where(b => b.PublicationYear > 2000);

        foreach (var book in books)
        {
            book.Pages = 300;
        }
        db.SaveChanges();
    }

    public static void AddAuthor(AppDbContext db, Author author)
    {
        db.Authors.Add(author);
        db.SaveChanges();
    }

    public static void AddBook(AppDbContext db, Book book)
    {
        db.Books.Add(book);
        db.SaveChanges();
    }

    public static void PrintAllAuthorsAndBooks(AppDbContext db)
    {
        var authors = db.Authors.Include(a => a.Books).ToList();

        foreach (var author in authors)
        {
            Console.WriteLine($"{author.Name} ({author.BirthDate?.ToShortDateString()}) - Email: {author.Email}");

            foreach (var book in author.Books)
            {
                Console.WriteLine($"{book.Title} ({book.PublicationYear}) - Pages: {book.Pages}");
            }
        }
    }

    public static bool ValidateAuthor(Author author)
    {
        if (string.IsNullOrWhiteSpace(author.Name))
        {
            Console.WriteLine("Name is required.");
            return false;
        }
        return true;
    }
}
