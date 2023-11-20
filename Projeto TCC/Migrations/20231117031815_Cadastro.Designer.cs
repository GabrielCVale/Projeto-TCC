﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projeto_TCC.Data;

#nullable disable

namespace Projeto_TCC.Migrations
{
    [DbContext(typeof(BancoContext))]
    [Migration("20231117031815_Cadastro")]
    partial class Cadastro
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Projeto_TCC.Models.CadastroModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("LugarViagem")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("contato")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("dataate")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("datede")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("valor")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("cadastro");
                });
#pragma warning restore 612, 618
        }
    }
}
