using bookSansar.Entities;
using Microsoft.EntityFrameworkCore;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

    public DbSet<Book> Books { get; set; }

    public DbSet<Announcement> Announcements { get; set; }


}
