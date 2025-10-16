using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DbDataAccess:IdentityDbContext
    {
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Tarea> Tareas { get; set; }
        public virtual DbSet<TipoTarea> TipoTareas { get; set; }
        public virtual DbSet<HistorialTarea> HistorialTareas { get; set; }
        public virtual DbSet<UsuarioPorTarea> UsuariosPorTareas { get; set; }
        public DbDataAccess(DbContextOptions<DbDataAccess> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)=>optionsBuilder.LogTo(Console.WriteLine).EnableDetailedErrors();

    }
}
