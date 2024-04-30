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
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("countries");

            builder.HasIndex(e => e.Name, "UQ__countrie__0756ED8C3B95105A")
                .IsUnique();

            builder.HasIndex(e => e.WebCode, "UQ__countrie__B1F47F05FBB07F68")
                .IsUnique();

            builder.Property(e => e.Id)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsRequired(true)
                .HasColumnName("Id");

            builder.Property(e => e.Continent)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("continent");

            builder.Property(e => e.Latitude).HasColumnName("latitude");

            builder.Property(e => e.LocalName)
                .HasMaxLength(100)
                .HasColumnName("localName");

            builder.Property(e => e.Longitude).HasColumnName("longitude");

            builder.Property(e => e.Name)
                .HasMaxLength(100)
                 .IsRequired(true)
                .HasColumnName("name");

            builder.Property(e => e.Population).HasColumnName("population");

            builder.Property(e => e.Region)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("region");

            builder.Property(e => e.SurfaceArea).HasColumnName("surfaceArea");

            builder.Property(e => e.WebCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("webCode");
        }
    }
}
