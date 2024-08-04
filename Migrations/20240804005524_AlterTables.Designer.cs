﻿// <auto-generated />
using System;
using CinemaApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CinemaApi.Migrations
{
    [DbContext(typeof(CinemaContext))]
    [Migration("20240804005524_AlterTables")]
    partial class AlterTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CinemaAPI.Models.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("MovieId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("CinemaAPI.Models.MovieRoom", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "RoomId");

                    b.HasIndex("RoomId");

                    b.ToTable("MovieRooms");
                });

            modelBuilder.Entity("CinemaAPI.Models.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("RoomNumber")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("RoomId");

                    b.HasIndex("RoomNumber")
                        .IsUnique();

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("CinemaAPI.Models.MovieRoom", b =>
                {
                    b.HasOne("CinemaAPI.Models.Movie", "Movie")
                        .WithMany("MovieRooms")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CinemaAPI.Models.Room", "Room")
                        .WithMany("MovieRooms")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("CinemaAPI.Models.Movie", b =>
                {
                    b.Navigation("MovieRooms");
                });

            modelBuilder.Entity("CinemaAPI.Models.Room", b =>
                {
                    b.Navigation("MovieRooms");
                });
#pragma warning restore 612, 618
        }
    }
}
