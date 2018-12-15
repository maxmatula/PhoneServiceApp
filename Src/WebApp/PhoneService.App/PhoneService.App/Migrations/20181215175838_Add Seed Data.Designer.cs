﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhoneService.Persistance;

namespace PhoneService.App.Migrations
{
    [DbContext(typeof(PhoneServiceDbContext))]
    [Migration("20181215175838_Add Seed Data")]
    partial class AddSeedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PhoneService.Domain.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("LastName");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNum");

                    b.Property<string>("TaxNum");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");

                    b.HasData(
                        new { CustomerId = 1, Email = "pereiradawid@example.com", LastName = "Pereira", Name = "Dawid", PhoneNum = "123456789", TaxNum = "987654321" },
                        new { CustomerId = 2, Email = "maksymilainmatula@example.com", LastName = "Matuła", Name = "Maksymilian", PhoneNum = "123456789", TaxNum = "987654321" },
                        new { CustomerId = 3, Email = "miloszwinnicki@example.com", LastName = "Winnicki", Name = "Miłosz", PhoneNum = "123456789", TaxNum = "987654321" },
                        new { CustomerId = 4, Email = "rafalpasek@example.com", LastName = "Pasek", Name = "Rafał", PhoneNum = "123456789", TaxNum = "987654321" },
                        new { CustomerId = 5, Email = "grzegorzwojcik@example.com", LastName = "Wójcik", Name = "Grzegorz", PhoneNum = "123456789", TaxNum = "987654321" }
                    );
                });

            modelBuilder.Entity("PhoneService.Domain.CustomerAddres", b =>
                {
                    b.Property<int>("CustomerAddresId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adress");

                    b.Property<string>("City");

                    b.Property<int>("CustomerId");

                    b.Property<string>("PostCode");

                    b.HasKey("CustomerAddresId");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("CustomerAddres");

                    b.HasData(
                        new { CustomerAddresId = 1, City = "Przeworsk", CustomerId = 1, PostCode = "37-200" },
                        new { CustomerAddresId = 2, City = "Dąbrowa Górnicza", CustomerId = 2, PostCode = "43-204" },
                        new { CustomerAddresId = 3, City = "Rzeszów", CustomerId = 3, PostCode = "35-356" },
                        new { CustomerAddresId = 4, City = "Sosnowiec", CustomerId = 4, PostCode = "30-300" },
                        new { CustomerAddresId = 5, City = "Żwirki i Muchomorki", CustomerId = 5, PostCode = "11-222" }
                    );
                });

            modelBuilder.Entity("PhoneService.Domain.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Model");

                    b.Property<string>("Producer");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new { ProductId = 1, Description = "Nie wiem po co nam opis produktu", Model = "S8", Producer = "Samsung" },
                        new { ProductId = 2, Description = "Mam wrażenie, że trzeba usunąć opis", Model = "3310", Producer = "Nokia" },
                        new { ProductId = 3, Description = "Przepraszam czy to pomyłka", Model = "Lepszy", Producer = "Xiaomi" },
                        new { ProductId = 4, Description = "Policja? - Proszę przyjechać na facebooka", Model = "XXX", Producer = "Apple" },
                        new { ProductId = 5, Description = "Co jam ma tu wpisać ?", Model = "Ericson Sony", Producer = "Sony Ericson" }
                    );
                });

            modelBuilder.Entity("PhoneService.Domain.ProductSaparePart", b =>
                {
                    b.Property<int>("ProductId");

                    b.Property<int>("SaparePartId");

                    b.HasKey("ProductId", "SaparePartId");

                    b.HasIndex("SaparePartId");

                    b.ToTable("ProductSapareParts");

                    b.HasData(
                        new { ProductId = 1, SaparePartId = 1 },
                        new { ProductId = 1, SaparePartId = 3 },
                        new { ProductId = 2, SaparePartId = 5 },
                        new { ProductId = 2, SaparePartId = 2 },
                        new { ProductId = 3, SaparePartId = 3 },
                        new { ProductId = 3, SaparePartId = 4 },
                        new { ProductId = 4, SaparePartId = 1 },
                        new { ProductId = 4, SaparePartId = 2 },
                        new { ProductId = 5, SaparePartId = 4 },
                        new { ProductId = 5, SaparePartId = 3 }
                    );
                });

            modelBuilder.Entity("PhoneService.Domain.Repair", b =>
                {
                    b.Property<int>("RepairId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("CustomerId");

                    b.Property<string>("Description");

                    b.Property<int>("ProductId");

                    b.Property<int>("RepairStatusId");

                    b.HasKey("RepairId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProductId");

                    b.HasIndex("RepairStatusId");

                    b.ToTable("Repairs");

                    b.HasData(
                        new { RepairId = 1, CreateDate = new DateTime(2018, 12, 15, 17, 58, 38, 682, DateTimeKind.Utc), CustomerId = 1, Description = "Tutaj powinien być jakiś opis naprawy", ProductId = 1, RepairStatusId = 1 },
                        new { RepairId = 2, CreateDate = new DateTime(2018, 12, 15, 17, 58, 38, 682, DateTimeKind.Utc), CustomerId = 1, Description = "Opis z produktu dodamu tutaj", ProductId = 5, RepairStatusId = 2 },
                        new { RepairId = 3, CreateDate = new DateTime(2018, 12, 15, 17, 58, 38, 682, DateTimeKind.Utc), CustomerId = 2, Description = "Klient nie może dodzwonić się do nikogo - nie opłacił abonamentu", ProductId = 2, RepairStatusId = 3 },
                        new { RepairId = 4, CreateDate = new DateTime(2018, 12, 15, 17, 58, 38, 682, DateTimeKind.Utc), CustomerId = 3, Description = "Klientowi nie działa klawiatura", ProductId = 3, RepairStatusId = 4 },
                        new { RepairId = 5, CreateDate = new DateTime(2018, 12, 15, 17, 58, 38, 682, DateTimeKind.Utc), CustomerId = 3, Description = "Popsuty głośnik", ProductId = 4, RepairStatusId = 5 },
                        new { RepairId = 6, CreateDate = new DateTime(2018, 12, 15, 17, 58, 38, 682, DateTimeKind.Utc), CustomerId = 4, Description = "Klient przyniusł zalany telefon w skarpecie z ryżem", ProductId = 1, RepairStatusId = 6 },
                        new { RepairId = 7, CreateDate = new DateTime(2018, 12, 15, 17, 58, 38, 682, DateTimeKind.Utc), CustomerId = 5, Description = "Coś nie diała", ProductId = 2, RepairStatusId = 2 },
                        new { RepairId = 8, CreateDate = new DateTime(2018, 12, 15, 17, 58, 38, 682, DateTimeKind.Utc), CustomerId = 5, Description = "Pan nie był zadowolony", ProductId = 5, RepairStatusId = 6 }
                    );
                });

            modelBuilder.Entity("PhoneService.Domain.RepairItem", b =>
                {
                    b.Property<int>("RepairId");

                    b.Property<int>("SaparePartId");

                    b.Property<int>("Quantity");

                    b.HasKey("RepairId", "SaparePartId");

                    b.HasIndex("SaparePartId");

                    b.ToTable("RepairItems");

                    b.HasData(
                        new { RepairId = 1, SaparePartId = 1, Quantity = 1 },
                        new { RepairId = 1, SaparePartId = 2, Quantity = 2 },
                        new { RepairId = 2, SaparePartId = 3, Quantity = 1 },
                        new { RepairId = 3, SaparePartId = 4, Quantity = 2 },
                        new { RepairId = 4, SaparePartId = 5, Quantity = 1 },
                        new { RepairId = 5, SaparePartId = 1, Quantity = 1 },
                        new { RepairId = 2, SaparePartId = 2, Quantity = 2 },
                        new { RepairId = 3, SaparePartId = 3, Quantity = 3 },
                        new { RepairId = 4, SaparePartId = 4, Quantity = 1 },
                        new { RepairId = 5, SaparePartId = 5, Quantity = 10 },
                        new { RepairId = 2, SaparePartId = 1, Quantity = 1 }
                    );
                });

            modelBuilder.Entity("PhoneService.Domain.RepairStatus", b =>
                {
                    b.Property<int>("RepairStatusId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("RepairStatusId");

                    b.ToTable("RepairStatuses");

                    b.HasData(
                        new { RepairStatusId = 1, Name = "Przyjeta" },
                        new { RepairStatusId = 2, Name = "Wyceniona" },
                        new { RepairStatusId = 3, Name = "W trakcie realizacji" },
                        new { RepairStatusId = 4, Name = "Zrealizowana" },
                        new { RepairStatusId = 5, Name = "Odrzucona" },
                        new { RepairStatusId = 6, Name = "Będziesz Pan zadowolony" }
                    );
                });

            modelBuilder.Entity("PhoneService.Domain.SaparePart", b =>
                {
                    b.Property<int>("SaparePartId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<string>("ReferenceNumebr");

                    b.HasKey("SaparePartId");

                    b.ToTable("SapareParts");

                    b.HasData(
                        new { SaparePartId = 1, Name = "Dioda W34", Price = 10m },
                        new { SaparePartId = 2, Name = "Tranzystor BX11", Price = 10m },
                        new { SaparePartId = 3, Name = "Wyświetlacz uniwersalny", Price = 10m },
                        new { SaparePartId = 4, Name = "O co to za śróbka", Price = 10m },
                        new { SaparePartId = 5, Name = "Klawiatura 3310", Price = 10m }
                    );
                });

            modelBuilder.Entity("PhoneService.Domain.CustomerAddres", b =>
                {
                    b.HasOne("PhoneService.Domain.Customer", "Customer")
                        .WithOne("Addres")
                        .HasForeignKey("PhoneService.Domain.CustomerAddres", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PhoneService.Domain.ProductSaparePart", b =>
                {
                    b.HasOne("PhoneService.Domain.Product", "Product")
                        .WithMany("ProductSapareParts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PhoneService.Domain.SaparePart", "SaparePart")
                        .WithMany("ProductSapareParts")
                        .HasForeignKey("SaparePartId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PhoneService.Domain.Repair", b =>
                {
                    b.HasOne("PhoneService.Domain.Customer", "Customer")
                        .WithMany("Repairs")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PhoneService.Domain.Product", "Product")
                        .WithMany("Repairs")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PhoneService.Domain.RepairStatus", "RepairStatus")
                        .WithMany("Repairs")
                        .HasForeignKey("RepairStatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PhoneService.Domain.RepairItem", b =>
                {
                    b.HasOne("PhoneService.Domain.Repair", "Repair")
                        .WithMany("RepairItems")
                        .HasForeignKey("RepairId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PhoneService.Domain.SaparePart", "SaparePart")
                        .WithMany("RepairItems")
                        .HasForeignKey("SaparePartId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}