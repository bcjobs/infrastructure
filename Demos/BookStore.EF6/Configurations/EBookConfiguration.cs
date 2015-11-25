using BookStore.EF6.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.EF6.Configurations
{
    class EBookConfiguration : EntityTypeConfiguration<EBook>
    {
        public EBookConfiguration()
        {
            ToTable("Books", "BookStore");

            Property(b => b.Isbn)
                .HasMaxLength(13)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true })
                );

            Property(b => b.Title)
                .HasMaxLength(200)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = false })
                );

            HasMany(b => b.Authors)
                .WithMany(e => e.Books)
                .Map(ca => ca.ToTable("BookAuthors", "BookStore"));
        }
    }
}
