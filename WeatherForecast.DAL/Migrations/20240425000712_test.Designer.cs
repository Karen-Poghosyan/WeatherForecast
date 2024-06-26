﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherForecast.DAL.DataAccess;

#nullable disable

namespace WeatherForecast.DAL.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240425000712_test")]
    partial class test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WeatherForecast.Core.Entities.City", b =>
                {
                    b.Property<int>("CityId")
                        .HasColumnType("int")
                        .HasColumnName("cityId");

                    b.Property<string>("CountryId")
                        .IsRequired()
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("countryId");

                    b.Property<double>("Latitude")
                        .HasColumnType("float")
                        .HasColumnName("latitude");

                    b.Property<double>("Longitude")
                        .HasColumnType("float")
                        .HasColumnName("longitude");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.Property<int>("StateId")
                        .HasColumnType("int")
                        .HasColumnName("stateId");

                    b.HasKey("CityId");

                    b.HasIndex("StateId");

                    b.HasIndex(new[] { "CountryId", "StateId", "CityId" }, "UQ__cities__66F2B3B9D4F27225")
                        .IsUnique();

                    b.ToTable("cities", (string)null);
                });

            modelBuilder.Entity("WeatherForecast.Core.Entities.Country", b =>
                {
                    b.Property<string>("CountryId")
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("countryId");

                    b.Property<string>("Continent")
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("continent");

                    b.Property<double>("Latitude")
                        .HasColumnType("float")
                        .HasColumnName("latitude");

                    b.Property<string>("LocalName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("localName");

                    b.Property<double>("Longitude")
                        .HasColumnType("float")
                        .HasColumnName("longitude");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.Property<int>("Population")
                        .HasColumnType("int")
                        .HasColumnName("population");

                    b.Property<string>("Region")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("region");

                    b.Property<double>("SurfaceArea")
                        .HasColumnType("float")
                        .HasColumnName("surfaceArea");

                    b.Property<string>("WebCode")
                        .HasMaxLength(2)
                        .IsUnicode(false)
                        .HasColumnType("varchar(2)")
                        .HasColumnName("webCode");

                    b.HasKey("CountryId");

                    b.HasIndex(new[] { "Name" }, "UQ__countrie__0756ED8C3B95105A")
                        .IsUnique();

                    b.HasIndex(new[] { "WebCode" }, "UQ__countrie__B1F47F05FBB07F68")
                        .IsUnique()
                        .HasFilter("[webCode] IS NOT NULL");

                    b.ToTable("countries", (string)null);
                });

            modelBuilder.Entity("WeatherForecast.Core.Entities.State", b =>
                {
                    b.Property<int>("StateId")
                        .HasColumnType("int")
                        .HasColumnName("stateId");

                    b.Property<string>("CountryId")
                        .IsRequired()
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("countryId");

                    b.Property<double>("Latitude")
                        .HasColumnType("float")
                        .HasColumnName("latitude");

                    b.Property<double>("Longitude")
                        .HasColumnType("float")
                        .HasColumnName("longitude");

                    b.Property<string>("StateName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("stateName");

                    b.HasKey("StateId");

                    b.HasIndex(new[] { "CountryId", "StateName" }, "UQ__states__DB990C0B123F5C4C")
                        .IsUnique();

                    b.ToTable("states", (string)null);
                });

            modelBuilder.Entity("WeatherForecast.Core.Entities.City", b =>
                {
                    b.HasOne("WeatherForecast.Core.Entities.State", "State")
                        .WithMany("Cities")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_StateID");

                    b.Navigation("State");
                });

            modelBuilder.Entity("WeatherForecast.Core.Entities.State", b =>
                {
                    b.HasOne("WeatherForecast.Core.Entities.Country", "Country")
                        .WithMany("States")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_CountryID");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("WeatherForecast.Core.Entities.Country", b =>
                {
                    b.Navigation("States");
                });

            modelBuilder.Entity("WeatherForecast.Core.Entities.State", b =>
                {
                    b.Navigation("Cities");
                });
#pragma warning restore 612, 618
        }
    }
}
