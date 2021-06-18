using Blog.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Post> builder)
        {
            builder.Property(x => x.Heading).IsRequired();
                builder.HasIndex(x => x.Heading).IsUnique();
            builder.Property(x => x.Text).IsRequired();
                builder.HasIndex(x => x.Text).IsUnique();
            builder.Property(x => x.Image).IsRequired();
            builder.Property(x => x.ReadTime).IsRequired();


            builder.HasMany(x => x.PostCategories).WithOne(x => x.Post).HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.PostComments).WithOne(x => x.Post).HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.PostRatings).WithOne(x => x.Post).HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.Restrict);





        }
    }
}
