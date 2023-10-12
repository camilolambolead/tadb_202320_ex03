﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Transportadora.Data;

#nullable disable

namespace Transportadora.Migrations
{
    [DbContext(typeof(TransporteContext))]
    [Migration("20231012072134_Autobuses")]
    partial class Autobuses
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Transportadora.Models.Autobus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CargadorId")
                        .HasColumnType("integer");

                    b.Property<bool>("EnOperacion")
                        .HasColumnType("boolean");

                    b.Property<bool>("EnUso")
                        .HasColumnType("boolean");

                    b.Property<int>("HorasEnOperacion")
                        .HasColumnType("integer");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TiempoUltimoMantenimiento")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CargadorId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Autobuses");
                });

            modelBuilder.Entity("Transportadora.Models.Cargador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CiclosDeCarga")
                        .HasColumnType("integer");

                    b.Property<bool>("EnUso")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("HoraFin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("HoraInicio")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Cargadores");
                });

            modelBuilder.Entity("Transportadora.Models.Horario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BusesEnOperacion")
                        .HasColumnType("integer");

                    b.Property<int>("CargadoresEnUso")
                        .HasColumnType("integer");

                    b.Property<bool>("EsHorarioPico")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Hora")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Horarios");
                });

            modelBuilder.Entity("Transportadora.Models.UsoAutobus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AutobusId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("HoraFin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("HoraInicio")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AutobusId");

                    b.ToTable("UsosAutobus");
                });

            modelBuilder.Entity("Transportadora.Models.Autobus", b =>
                {
                    b.HasOne("Transportadora.Models.Cargador", "Cargador")
                        .WithMany("Autobuses")
                        .HasForeignKey("CargadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cargador");
                });

            modelBuilder.Entity("Transportadora.Models.UsoAutobus", b =>
                {
                    b.HasOne("Transportadora.Models.Autobus", "Autobus")
                        .WithMany("Usos")
                        .HasForeignKey("AutobusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Autobus");
                });

            modelBuilder.Entity("Transportadora.Models.Autobus", b =>
                {
                    b.Navigation("Usos");
                });

            modelBuilder.Entity("Transportadora.Models.Cargador", b =>
                {
                    b.Navigation("Autobuses");
                });
#pragma warning restore 612, 618
        }
    }
}