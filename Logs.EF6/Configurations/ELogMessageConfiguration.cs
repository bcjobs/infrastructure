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
                .WithRequired()
                .HasForeignKey(t => t.LogMessageId);
            HasMany(m => m.ExceptionTypes)
                .WithRequired()
                .HasForeignKey(t => t.LogMessageId);
        }
    }
}
