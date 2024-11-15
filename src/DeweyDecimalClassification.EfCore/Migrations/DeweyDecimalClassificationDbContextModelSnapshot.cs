﻿// <auto-generated />
using DeweyDecimalClassification.EfCore.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DeweyDecimalClassification.EfCore.Migrations
{
    [DbContext(typeof(DeweyDecimalClassificationDbContext))]
    partial class DeweyDecimalClassificationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0-rc.2.24474.1");

            modelBuilder.Entity("DeweyDecimalClassification.EfCore.Entities.DeweyEntry", b =>
                {
                    b.Property<float>("Id")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<float?>("ParentId")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("DeweyEntries");
                });

            modelBuilder.Entity("DeweyDecimalClassification.EfCore.Entities.DeweyEntry", b =>
                {
                    b.HasOne("DeweyDecimalClassification.EfCore.Entities.DeweyEntry", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("DeweyDecimalClassification.EfCore.Entities.DeweyEntry", b =>
                {
                    b.Navigation("Children");
                });
#pragma warning restore 612, 618
        }
    }
}
