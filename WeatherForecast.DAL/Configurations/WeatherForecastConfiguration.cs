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
    public class WeatherForecastConfiguration : IEntityTypeConfiguration<WeatherObservation>
    {
        public void Configure(EntityTypeBuilder<WeatherObservation> builder)
        {
            builder.ToTable("weather_forecasts");

            builder.HasIndex(e => new { e.TemperatureInCel, e.LocationId, e.Condition });

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .IsRequired(true)
                .HasColumnName("Id");

            builder.Property(e => e.LocationId)
                .IsRequired(true);

            builder.Property(e => e.Condition)
                .IsRequired(true);

            

            builder.Property(e => e.TemperatureInCel)
                .IsRequired(true);

            builder.HasOne(d => d.Location)
                .WithMany(p => p.WeatherForecasts)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_WeatherForecast_LocationId");
        }
    }
}

