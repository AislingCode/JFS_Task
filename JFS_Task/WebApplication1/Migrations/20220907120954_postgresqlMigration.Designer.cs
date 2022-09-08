﻿// <auto-generated />
using System;
using JFS_Task;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JFS_Task.Migrations
{
    [DbContext(typeof(DomainModelPostgreSqlContext))]
    [Migration("20220907120954_postgresqlMigration")]
    partial class postgresqlMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("JFS_Task.Balance", b =>
                {
                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<double>("Calculation")
                        .HasColumnType("double precision");

                    b.Property<double>("InBalance")
                        .HasColumnType("double precision");

                    b.Property<int>("Period")
                        .HasColumnType("integer");

                    b.ToTable("Balance");
                });

            modelBuilder.Entity("JFS_Task.Payment", b =>
                {
                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PaymentGuid")
                        .HasColumnType("uuid");

                    b.Property<double>("Sum")
                        .HasColumnType("double precision");

                    b.ToTable("Payment");
                });
#pragma warning restore 612, 618
        }
    }
}
