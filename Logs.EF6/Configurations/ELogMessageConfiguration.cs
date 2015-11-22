using Logs.EF6.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logs.EF6.Configurations
{
    class ELogMessageConfiguration : EntityTypeConfiguration<ELogMessage>
    {
        public ELogMessageConfiguration()
        {
            ToTable("Messages", "Logs");
            HasMany(m => m.EventTypes)
                .WithMany()
                .Map(ca => ca.ToTable("EventTypes", "Logs"));
            HasMany(m => m.ExceptionTypes)
                .WithMany()
                .Map(ca => ca.ToTable("ExceptionTypes", "Logs"));                
        }
    }
}
