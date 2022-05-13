﻿// <auto-generated />
using System;
using Itmo.Dormitory.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Itmo.Dormitory.DataAccess.Migrations
{
    [DbContext(typeof(DormitoryDbContext))]
    [Migration("20220512191619_initialCreate")]
    partial class initialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.3");

            modelBuilder.Entity("Itmo.Dormitory.DataAccess.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Itmo.Dormitory.Domain.Entities.Announcement", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Announcements");
                });

            modelBuilder.Entity("Itmo.Dormitory.Domain.Entities.Application", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("ApplicationType")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsResolved")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ResidentId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ResidentId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("Itmo.Dormitory.Domain.Entities.Resident", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Residents");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Itmo.Dormitory.Domain.Entities.Announcement", b =>
                {
                    b.OwnsOne("Itmo.Dormitory.Domain.ValueObjects.AttachedInformation", "Information", b1 =>
                        {
                            b1.Property<Guid>("AnnouncementId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Description")
                                .HasMaxLength(2000)
                                .HasColumnType("TEXT");

                            b1.Property<string>("Title")
                                .HasMaxLength(60)
                                .HasColumnType("TEXT");

                            b1.HasKey("AnnouncementId");

                            b1.ToTable("Announcements");

                            b1.WithOwner()
                                .HasForeignKey("AnnouncementId");
                        });

                    b.Navigation("Information");
                });

            modelBuilder.Entity("Itmo.Dormitory.Domain.Entities.Application", b =>
                {
                    b.HasOne("Itmo.Dormitory.Domain.Entities.Resident", "Resident")
                        .WithMany("Applications")
                        .HasForeignKey("ResidentId");

                    b.OwnsOne("Itmo.Dormitory.Domain.ValueObjects.AttachedInformation", "Information", b1 =>
                        {
                            b1.Property<Guid>("ApplicationId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Description")
                                .HasMaxLength(2000)
                                .HasColumnType("TEXT");

                            b1.Property<string>("Title")
                                .HasMaxLength(60)
                                .HasColumnType("TEXT");

                            b1.HasKey("ApplicationId");

                            b1.ToTable("Applications");

                            b1.WithOwner()
                                .HasForeignKey("ApplicationId");
                        });

                    b.Navigation("Information");

                    b.Navigation("Resident");
                });

            modelBuilder.Entity("Itmo.Dormitory.Domain.Entities.Resident", b =>
                {
                    b.OwnsOne("Itmo.Dormitory.Domain.ValueObjects.ISUNumber", "ISUNumber", b1 =>
                        {
                            b1.Property<Guid>("ResidentId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Value")
                                .HasColumnType("TEXT");

                            b1.HasKey("ResidentId");

                            b1.ToTable("Residents");

                            b1.WithOwner()
                                .HasForeignKey("ResidentId");
                        });

                    b.OwnsOne("Itmo.Dormitory.Domain.ValueObjects.PersonName", "Name", b1 =>
                        {
                            b1.Property<Guid>("ResidentId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("FirstName")
                                .HasColumnType("TEXT");

                            b1.Property<string>("LastName")
                                .HasColumnType("TEXT");

                            b1.Property<string>("MiddleName")
                                .HasColumnType("TEXT");

                            b1.HasKey("ResidentId");

                            b1.ToTable("Residents");

                            b1.WithOwner()
                                .HasForeignKey("ResidentId");
                        });

                    b.OwnsOne("Itmo.Dormitory.Domain.ValueObjects.RoomNumber", "RoomNumber", b1 =>
                        {
                            b1.Property<Guid>("ResidentId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Value")
                                .HasColumnType("TEXT");

                            b1.HasKey("ResidentId");

                            b1.ToTable("Residents");

                            b1.WithOwner()
                                .HasForeignKey("ResidentId");
                        });

                    b.Navigation("ISUNumber");

                    b.Navigation("Name");

                    b.Navigation("RoomNumber");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Itmo.Dormitory.DataAccess.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Itmo.Dormitory.DataAccess.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Itmo.Dormitory.DataAccess.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Itmo.Dormitory.DataAccess.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Itmo.Dormitory.Domain.Entities.Resident", b =>
                {
                    b.Navigation("Applications");
                });
#pragma warning restore 612, 618
        }
    }
}
