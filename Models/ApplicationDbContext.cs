namespace TChart.Models;


using Microsoft.EntityFrameworkCore; 
public class ApplicationDbContext : DbContext 
{ 
public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
{ } 
// Defina suas DbSets aqui, por exemplo: 
public DbSet<VisoesClienteModel> Clientes { get; set; }

protected override void OnModelCreating(ModelBuilder modelBuilder) { modelBuilder.Entity<VisoesClienteModel>().HasNoKey(); }

}