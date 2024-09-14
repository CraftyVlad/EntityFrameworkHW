using Microsoft.EntityFrameworkCore;

public class Author
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public ICollection<Book>? Books { get; set; }
}

public class Book
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int PublicationYear { get; set; }
    public int AuthorId { get; set; }
    public Author? Author { get; set; }
    public ICollection<Review>? Reviews { get; set; }
    public ICollection<BookPublisher>? BookPublishers { get; set; }
}

public class Review
{
    public int Id { get; set; }
    public string? Content { get; set; }
    public int BookId { get; set; }
    public Book? Book { get; set; }
}

public class Publisher
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public ICollection<BookPublisher>? BookPublishers { get; set; }

}

public class BookPublisher
{
    public int BookId { get; set; }
    public Book? Book { get; set; }
    public int PublisherId { get; set; }
    public Publisher? Publisher { get; set; }
}

public class AppDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<BookPublisher> BookPublishers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        ;
        optionsBuilder.UseSqlite($"Data Source=../../../LibraryDB.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
            .HasMany(a => a.Books)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Reviews)
            .WithOne(r => r.Book)
            .HasForeignKey(r => r.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BookPublisher>()
            .HasKey(bp => new { bp.BookId, bp.PublisherId });

        modelBuilder.Entity<BookPublisher>()
            .HasOne(bp => bp.Book)
            .WithMany(b => b.BookPublishers)
            .HasForeignKey(bp => bp.BookId);

        modelBuilder.Entity<BookPublisher>()
            .HasOne(bp => bp.Publisher)
            .WithMany(p => p.BookPublishers)
            .HasForeignKey(bp => bp.PublisherId);
    }

    public static void InsertData(AppDbContext db)
    {
        if (!db.Authors.Any())
        {
            var author = new Author { Name = "Author1", Email = "author1@mail.com" };
            var book = new Book { Title = "Book1", PublicationYear = 2024, Author = author };
            var review = new Review { Content = "Great book!", Book = book };
            var publisher = new Publisher { Name = "Publisher1" };
            var bookPublisher = new BookPublisher { Book = book, Publisher = publisher };

            db.Authors.Add(author);
            db.Books.Add(book);
            db.Reviews.Add(review);
            db.Publishers.Add(publisher);
            db.BookPublishers.Add(bookPublisher);

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
            db.Database.EnsureCreated();

            AppDbContext.InsertData(db);

            Console.WriteLine("Inserted data");
        }
    }
}