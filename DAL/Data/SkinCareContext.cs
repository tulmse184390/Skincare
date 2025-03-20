using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using Microsoft.Extensions.Configuration;

namespace DAL.Datas;

public partial class SkinCareContext : DbContext
{
    public SkinCareContext()
    {
    }

    public SkinCareContext(DbContextOptions<SkinCareContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<AppointmentDetail> AppointmentDetails { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());

    private string? GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        return configuration["ConnectionStrings:DefaultConnection"];
    }
}
