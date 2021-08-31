﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Modalmais.Transacoes.API.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Modalmais.Transacoes.API.Data.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20210831151456_teste")]
    partial class teste
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Modalmais.Transacoes.API.Models.Transacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("uuid");

                    b.Property<string>("Chave")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Descricao")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<int>("StatusTransacao")
                        .HasColumnType("integer");

                    b.Property<int>("Tipo")
                        .HasColumnType("integer");

                    b.Property<decimal>("Valor")
                        .HasColumnType("numeric(8,2)");

                    b.HasKey("Id");

                    b.ToTable("Transacoes", "modalmais");

                    b.HasCheckConstraint("CK_Transacoes_StatusTransacao_Enum", "\"StatusTransacao\" IN (0, 1)");

                    b.HasCheckConstraint("CK_Transacoes_Tipo_Enum", "\"Tipo\" IN (1, 2, 3, 4)");
                });
#pragma warning restore 612, 618
        }
    }
}
