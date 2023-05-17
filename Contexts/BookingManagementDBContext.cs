using API_Web.Model;
using Microsoft.EntityFrameworkCore;

namespace API_Web.Contexts;

public class BookingManagementDBContext : DbContext
{
    public BookingManagementDBContext(DbContextOptions<BookingManagementDBContext> options) : base(options)
    {

    }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<University> Universities { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Employee>().HasIndex(e => new
        {
            e.NIK,
            e.Email,
            e.PhoneNumber,

        }).IsUnique();
        //Relation between University and Education ( 1 to many ) 
        builder.Entity<Education>()
               .HasOne(u => u.University)
               .WithMany(e => e.Educations)
               .HasForeignKey(e => e.UniversityGuid);

        //Relation between Education and Employee (1 to 1)
        builder.Entity<Education>()
               .HasOne(e => e.Employee)
               .WithOne(e => e.Education)
               .HasForeignKey<Education>(e => e.Guid);

        //Relation between Account and Employee (1 to 1)
        builder.Entity<Account>()
               .HasOne(e => e.Employee)
               .WithOne(a => a.Account)
               .HasForeignKey<Account>(a => a.Guid);

        //Relation between Account and AccountRole (1 to many)
        builder.Entity<AccountRole>()
               .HasOne(a => a.Account)
               .WithMany(ar => ar.AccountRoles)
               .HasForeignKey(ar => ar.AccountGuid);

        //Relation between Role and AccountRole (1 to many)
        builder.Entity<AccountRole>()
               .HasOne(r => r.Role)
               .WithMany(ar => ar.AccountRoles)
               .HasForeignKey(r => r.RoleGuid);

        //Relation between Employee and Booking (1 to many)
        builder.Entity<Booking>()
               .HasOne(e => e.Employee)
               .WithMany(b => b.Bookings)
               .HasForeignKey(e => e.EmployeeGuid);

        //Relation between Room and Booking (1 to many)
        builder.Entity<Booking>()
               .HasOne(r => r.Room)
               .WithMany(b => b.Bookings)
               .HasForeignKey(e => e.RoomGuid);

    }

}