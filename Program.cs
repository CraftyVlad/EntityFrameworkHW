using Microsoft.EntityFrameworkCore;

public class Product
{
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
}

public class ApplicationContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=ProductsDB.db");
    }
}

class Program
{
    static void Main()
    {
        using (ApplicationContext context = new ApplicationContext())
        {
            context.Database.EnsureCreated();

            var product = new Product { Name = "Laptop", Price = 1000M };
            context.Products.Add(product);
            context.SaveChanges();

            var products = context.Products.ToList();
            foreach (var p in products)
            {
                Console.WriteLine($"{p.ProductId}: {p.Name} - {p.Price}");
            }

            var productToUpdate = context.Products.First();
            productToUpdate.Price = 950M;
            context.SaveChanges();

            var productToDelete = context.Products.First();
            context.Products.Remove(productToDelete);
            context.SaveChanges();

            foreach (var p in products)
            {
                Console.WriteLine($"{p.ProductId}: {p.Name} - {p.Price}");
            }
        }
    }
}