﻿// <auto-generated />
using Irisi_M_301072868.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Irisi_M_301072868.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200319155800_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Irisi_M_301072868.Models.player", b =>
                {
                    b.Property<int>("playerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AgeRange");

                    b.Property<string>("playerName");

                    b.Property<DateTime>("CreatedDate");

                    b.HasKey("playerId");

                    b.ToTable("players");
                });

            modelBuilder.Entity("Irisi_M_301072868.Models.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("playerId");

                    b.Property<DateTime>("playerJoinedDate");

                    b.Property<DateTime>("DayOfBirth");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("Gender");

                    b.Property<string>("LastName");

                    b.Property<string>("PhoneNo");

                    b.HasKey("PlayerId");

                    b.ToTable("Players");
                });
#pragma warning restore 612, 618
        }
    }
}