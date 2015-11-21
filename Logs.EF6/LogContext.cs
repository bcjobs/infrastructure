using Logs.EF6.Configurations;
using Logs.EF6.Entities;
using Logs.EF6.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logs.EF6
{
    class LogContext : DbContext
    {
        static LogContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LogContext, Configuration>());            
        }

        public LogContext() 
            : base("Name=Logs")
        {
        }

        public DbSet<ELogMessage> Messages { get; set; }
        public DbSet<ELogType> Types { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new ELogMessageConfiguration());
            modelBuilder.Configurations.Add(new ELogTypeConfiguration());
        }
    }
}
