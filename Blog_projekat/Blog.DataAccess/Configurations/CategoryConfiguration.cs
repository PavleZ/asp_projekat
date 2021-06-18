using Blog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasOne(x => x.ParentCategory).WithMany().HasForeignKey(x => x.Id).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x=>x.PostCategories).WithOne(x=>x.Category).HasForeignKey(x=> x.CategoryId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
