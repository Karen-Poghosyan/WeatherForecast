using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Core.Entities;

namespace WeatherForecast.DAL.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("locations");

            builder.HasIndex(e => new { e.StateId, e.CityId }).IsUnique();

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .IsRequired(true)
                .HasColumnName("Id");

            builder.Property(e => e.CountryId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsRequired(true)
                .HasColumnName("countryId");

            builder.Property(e => e.StateId)
                .IsRequired(false)
                .HasColumnName("stateId");

            builder.Property(e => e.CityId)
                .IsRequired(false)
                .HasColumnName("cityId");

            builder.HasOne(d => d.Country)
                .WithMany(p => p.Locations)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_Location_CountryId");

            builder.HasOne(d => d.City)
                .WithMany(p => p.Locations)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_Location_CityId");

            builder.HasOne(d => d.State)
                .WithMany(p => p.Locations)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK_Location_StateId");
        }
    }
}
