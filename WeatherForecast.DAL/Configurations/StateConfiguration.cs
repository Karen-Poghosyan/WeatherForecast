using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Core.Entities;

namespace WeatherForecast.DAL.Configurations
{
    public class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable("states");

            builder.HasIndex(e => new { e.CountryId, e.Name }, "UQ__states__DB990C0B123F5C4C")
                .IsUnique();

            builder.Property(e => e.Id)
                .ValueGeneratedNever()
                .IsRequired(true)
                .HasColumnName("Id");

            builder.Property(e => e.CountryId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsRequired(true)
                .HasColumnName("countryId");

            builder.Property(e => e.Latitude).HasColumnName("latitude");

            builder.Property(e => e.Longitude).HasColumnName("longitude");

            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired(true)
                .HasColumnName("stateName");

            builder.HasOne(d => d.Country)
                .WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_CountryID");
        }
    }
}
