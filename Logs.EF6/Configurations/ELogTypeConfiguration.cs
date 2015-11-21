using Logs.EF6.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logs.EF6.Configurations
{
    class ELogTypeConfiguration : EntityTypeConfiguration<ELogType>
    {
        public ELogTypeConfiguration()
        {
            ToTable("Types", "Logs");
            HasKey(t => new { t.Id, t.LogMessageId });
            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = false }));
        }
    }
}
