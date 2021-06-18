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
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.Heading).IsRequired();
            builder.Property(x => x.Body).IsRequired();

            builder.HasMany(x => x.PostComments).WithOne(x => x.Comment).HasForeignKey(x => x.CommentId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ParentComment).WithMany().HasForeignKey(x => x.Id).OnDelete(DeleteBehavior.Restrict);


        }
    }
}
