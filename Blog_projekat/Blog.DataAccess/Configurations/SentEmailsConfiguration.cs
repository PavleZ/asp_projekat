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
    public class SentEmailsConfiguration : IEntityTypeConfiguration<SentEmails>
    {
        public void Configure(EntityTypeBuilder<SentEmails> builder)
        {
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.Subject).IsRequired();
            builder.Property(x => x.From).IsRequired();
            builder.Property(x => x.To).IsRequired();



        }
    }
}
