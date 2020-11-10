using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VilevePay.Domain.Models;
using VilevePay.Infra.Data.Mappings;

namespace VilevePay.Infra.Data.Context
{
    public class VilevePayContext : DbContext
    {
        public VilevePayContext(DbContextOptions<VilevePayContext> options)
            : base(options)
        {
        }

        public DbSet<Onboarding> Onboarding { get; set; }
        public DbSet<Consultor> Consultores { get; set; }
        public DbSet<DadosBancarios> DadosBancarios { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Representante> Representantes { get; set; }
        public DbSet<RepresentanteEmail> RepresentantesEmails { get; set; }
        public DbSet<RepresentanteTelefone> RepresentantesTelefones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseLazyLoadingProxies();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OnboardingMap());
            modelBuilder.ApplyConfiguration(new ConsultorMap());
            modelBuilder.ApplyConfiguration(new DadosBancariosMap());
            modelBuilder.ApplyConfiguration(new EnderecoMap());
            modelBuilder.ApplyConfiguration(new RepresentanteMap());
            modelBuilder.ApplyConfiguration(new RepresentanteEmailMap());
            modelBuilder.ApplyConfiguration(new RepresentanteTelefoneMap());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            Save();

            return base.SaveChanges();
        }

        private void Save()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreationDate") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("CreationDate").CurrentValue = DateTime.Now;

                if (entry.State == EntityState.Modified)
                    entry.Property("CreationDate").IsModified = false;
            }

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("UpdateDate") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("UpdateDate").IsModified = false;

                if (entry.State == EntityState.Modified)
                    entry.Property("UpdateDate").CurrentValue = DateTime.Now;
            }
        }
    }
}