//using Microsoft.EntityFrameworkCore;

//public class Customer
//{
//    public int Id { get; set; }
//    public string? Name { get; set; }
//    public ContactInfo? ContactInfo { get; set; }
//    public ICollection<Order>? Orders { get; set; }
//}

//public class Order
//{
//    public int Id { get; set; }
//    public DateTime OrderDate { get; set; }
//    public int CustomerId { get; set; }
//    public Customer? Customer { get; set; }
//    public Address? ShippingAddress { get; set; }
//}

//public class ContactInfo
//{
//    public string? Email { get; set; }
//    public string? PhoneNumber { get; set; }
//}

//public class Address
//{
//    public string? Street { get; set; }
//    public string? City { get; set; }
//    public string? PostalCode { get; set; }
//}

//public class AppDbContext : DbContext
//{
//    public DbSet<Customer> Customers { get; set; }
//    public DbSet<Order> Orders { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlite($"Data Source=../../../OrdersDB.db");
//    }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Customer>()
//            .OwnsOne(c => c.ContactInfo, ci =>
//            {
//                ci.Property(c => c.Email).HasColumnName("Email");
//                ci.Property(c => c.PhoneNumber).HasColumnName("PhoneNumber");
//            });

//        modelBuilder.Entity<Order>()
//            .OwnsOne(o => o.ShippingAddress, sa =>
//            {
//                sa.Property(a => a.Street).HasColumnName("Street");
//                sa.Property(a => a.City).HasColumnName("City");
//                sa.Property(a => a.PostalCode).HasColumnName("PostalCode");
//            });

//        modelBuilder.Entity<Customer>()
//            .HasMany(c => c.Orders)
//            .WithOne(o => o.Customer)
//            .HasForeignKey(o => o.CustomerId);
//    }

//    public static void InsertData(AppDbContext db)
//    {
//        var customer = new Customer
//        {
//            Name = "Customer1",
//            ContactInfo = new ContactInfo { Email = "customer1@mail.com", PhoneNumber = "123456789" }
//        };

//        var order = new Order
//        {
//            OrderDate = DateTime.Now,
//            Customer = customer,
//            ShippingAddress = new Address { Street = "123 Street", City = "City", PostalCode = "00000" }
//        };

//        db.Customers.Add(customer);
//        db.Orders.Add(order);
//        db.SaveChanges();
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        using (AppDbContext db = new AppDbContext())
//        {
//            db.Database.EnsureCreated();

//            AppDbContext.InsertData(db);

//            Console.WriteLine("Inserted data");
//        }
//    }
//}