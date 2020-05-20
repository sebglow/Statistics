﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SebGlow.DataAccess.Migrations
{
    [DbContext(typeof(SebGlowDbContext))]
    partial class SebGlowDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccess.Statistic", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("avgForks");

                    b.Property<decimal>("avgSize");

                    b.Property<decimal>("avgStargazers");

                    b.Property<decimal>("avgWatchers");

                    b.Property<DateTime>("createdAt");

                    b.Property<string>("letters");

                    b.Property<int>("owner_id");

                    b.Property<string>("owner_login");

                    b.Property<string>("owner_url");

                    b.HasKey("id");

                    b.ToTable("Statistics");
                });
#pragma warning restore 612, 618
        }
    }
}
