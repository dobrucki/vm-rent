﻿// <auto-generated />
using System;
using RentService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace RentService.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("RentService.Infrastructure.Persistence.Entities.CustomerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("EmailAddress")
                        .HasColumnName("email_address")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnName("first_name")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnName("last_name")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("customer_id_pkey");

                    b.ToTable("customer");
                });

            modelBuilder.Entity("RentService.Infrastructure.Persistence.Entities.RentalEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnName("customer_id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("EndTime")
                        .HasColumnName("end_time")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("StartTime")
                        .HasColumnName("start_time")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("VirtualMachineId")
                        .HasColumnName("virtual_machine_id")
                        .HasColumnType("uuid");

                    b.HasKey("Id")
                        .HasName("rental_id_pkey");

                    b.HasIndex("CustomerId")
                        .HasName("ix_rentals_customer_id");

                    b.HasIndex("VirtualMachineId")
                        .HasName("ix_rentals_virtual_machine_id");

                    b.ToTable("rental");
                });

            modelBuilder.Entity("RentService.Infrastructure.Persistence.Entities.VirtualMachineEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("virtual_machine_id_pkey");

                    b.ToTable("virtual_machine");
                });

            modelBuilder.Entity("RentService.Infrastructure.Persistence.Entities.RentalEntity", b =>
                {
                    b.HasOne("RentService.Infrastructure.Persistence.Entities.CustomerEntity", "Customer")
                        .WithMany("Rentals")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("customer_id_fkey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RentService.Infrastructure.Persistence.Entities.VirtualMachineEntity", "VirtualMachine")
                        .WithMany("Rentals")
                        .HasForeignKey("VirtualMachineId")
                        .HasConstraintName("virtual_machine_id_fkey")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}