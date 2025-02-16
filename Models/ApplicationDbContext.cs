namespace TChart.Models;


using Microsoft.EntityFrameworkCore; 
public class ApplicationDbContext : DbContext 
{ 
public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
{ } 
// Defina suas DbSets aqui, por exemplo: 
public DbSet<ClienteModel> Clientes { get; set; }
public DbSet<CategoriaClienteModel> CategoriaCliente { get; set; }
public DbSet<ResponsavelClienteModel> ResponsavelCliente { get; set; }
public DbSet<ClientePeriodoModel> ClientesPeriodo { get; set; }
public DbSet<ContratoModel> Contratos { get; set; }
public DbSet<ContratoPeriodoModel> ContratosPeriodo { get; set; }
public DbSet<CategoriaContratoModel> CategoriaContrato { get; set; }
public DbSet<ResponsavelContratoModel> ResponsavelContrato { get; set; }

protected override void OnModelCreating(ModelBuilder modelBuilder) 
{ 
    modelBuilder.Entity<ClienteModel>().HasNoKey(); 
    modelBuilder.Entity<CategoriaClienteModel>().HasNoKey();
    modelBuilder.Entity<ClientePeriodoModel>().HasNoKey();
    modelBuilder.Entity<ResponsavelClienteModel>().HasNoKey();
    modelBuilder.Entity<ContratoModel>().HasNoKey();
    modelBuilder.Entity<ContratoPeriodoModel>().HasNoKey();
    modelBuilder.Entity<CategoriaContratoModel>().HasNoKey();
    modelBuilder.Entity<ResponsavelContratoModel>().HasNoKey();
}

}