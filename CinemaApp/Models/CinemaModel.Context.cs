﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CinemaApp.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CinemaEntities : DbContext
    {
        private static CinemaEntities _context;
        public CinemaEntities()
            : base("name=CinemaEntities")
        {
        }

        public static CinemaEntities GetContext()
        {
            if (_context == null)
                _context = new CinemaEntities();

            return _context;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Halls> Halls { get; set; }
        public virtual DbSet<HallTypes> HallTypes { get; set; }
        public virtual DbSet<Movies> Movies { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<RowsAndSeats> RowsAndSeats { get; set; }
        public virtual DbSet<Sessions> Sessions { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tickets> Tickets { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
