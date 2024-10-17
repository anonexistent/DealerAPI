﻿// <auto-generated />
using System;
using DealerAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DealerAPI.Migrations
{
    [DbContext(typeof(DealerDbContext))]
    [Migration("20241017161808_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("DealerAPI.Model.Dealer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<Guid>("DealerTypeId")
                        .HasColumnType("TEXT")
                        .HasColumnName("dealer_type_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_dealers");

                    b.HasIndex("DealerTypeId")
                        .HasDatabaseName("ix_dealers_dealer_type_id");

                    b.ToTable("dealers", (string)null);
                });

            modelBuilder.Entity("DealerAPI.Model.DealerType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_dealer_types");

                    b.ToTable("dealer_types", (string)null);
                });

            modelBuilder.Entity("DealerAPI.Model.Dealer", b =>
                {
                    b.HasOne("DealerAPI.Model.DealerType", "DealerType")
                        .WithMany("Dealers")
                        .HasForeignKey("DealerTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_dealers_dealer_types_dealer_type_id");

                    b.Navigation("DealerType");
                });

            modelBuilder.Entity("DealerAPI.Model.DealerType", b =>
                {
                    b.Navigation("Dealers");
                });
#pragma warning restore 612, 618
        }
    }
}
