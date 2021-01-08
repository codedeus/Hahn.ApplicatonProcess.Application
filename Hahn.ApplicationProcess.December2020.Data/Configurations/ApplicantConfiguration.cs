using Hahn.ApplicationProcess.December2020.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Data.Configurations
{
    public class ApplicantConfiguration : IEntityTypeConfiguration<Applicant>
    {
        public void Configure(EntityTypeBuilder<Applicant> builder)
        {
            builder.HasKey(s => s.ID);
            builder.Property(s => s.Name).IsRequired(true).HasMaxLength(256);
            builder.Property(s => s.FamilyName).IsRequired(true).HasMaxLength(256);
            builder.Property(s => s.Address).IsRequired(true).HasMaxLength(256);
            builder.Property(s => s.CountryOfOrigin).IsRequired(true).HasMaxLength(256);
            builder.Property(s => s.EmailAddress).IsRequired(true).HasMaxLength(256);
        }
    }
}
