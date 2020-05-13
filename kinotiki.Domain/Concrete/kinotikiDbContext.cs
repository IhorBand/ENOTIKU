using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kinotiki.Domain.Entity;
using System.Data.Entity;

namespace kinotiki.Domain.Concrete
{
    public class kinotikiDbContext : DbContext
    {
        public kinotikiDbContext() : base("kinotikiMainDb")
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<GlobalSettings> GlobalSettings { get; set; }
        
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Properties<User>().Configure(c => c.HasColumnType("datetime2"));
        //}
    }
}
