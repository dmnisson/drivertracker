﻿// <auto-generated />
using System;
using DriverTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DriverTracker.Migrations
{
    [DbContext(typeof(MvcDriverContext))]
    partial class MvcDriverContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("DriverTracker.Models.Analysis", b =>
                {
                    b.Property<int>("AnalysisID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnalystID");

                    b.Property<int>("DriverID");

                    b.HasKey("AnalysisID");

                    b.HasIndex("AnalystID");

                    b.HasIndex("DriverID");

                    b.ToTable("Analyses");
                });

            modelBuilder.Entity("DriverTracker.Models.Analyst", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountStatus");

                    b.Property<string>("Email");

                    b.Property<string>("FullName");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("ReceivesSMSAlertsDriversTerminated");

                    b.Property<bool>("ReceivesSMSAlertsLongDriverWaits");

                    b.Property<bool>("ReceivesSMSAlertsNewDrivers");

                    b.Property<double>("SMSAlertDriverWaitTime");

                    b.Property<string>("SMSNumber");

                    b.Property<int>("UserID");

                    b.Property<string>("Username");

                    b.HasKey("ID");

                    b.ToTable("Analysts");
                });

            modelBuilder.Entity("DriverTracker.Models.Driver", b =>
                {
                    b.Property<int>("DriverID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LicenseNumber")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("UserID");

                    b.HasKey("DriverID");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("DriverTracker.Models.Leg", b =>
                {
                    b.Property<int>("LegID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ArrivalTime");

                    b.Property<string>("DestinationAddress");

                    b.Property<decimal>("Distance");

                    b.Property<int>("DriverID");

                    b.Property<decimal>("Fare");

                    b.Property<int>("NumOfPassengersAboard");

                    b.Property<DateTime?>("PickupRequestTime");

                    b.Property<int?>("PreviousLegID");

                    b.Property<string>("StartAddress");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("LegID");

                    b.HasIndex("DriverID");

                    b.HasIndex("PreviousLegID");

                    b.ToTable("Legs");
                });

            modelBuilder.Entity("DriverTracker.Models.Analysis", b =>
                {
                    b.HasOne("DriverTracker.Models.Analyst", "Analyst")
                        .WithMany("Analyses")
                        .HasForeignKey("AnalystID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DriverTracker.Models.Driver", "Driver")
                        .WithMany("Analyses")
                        .HasForeignKey("DriverID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DriverTracker.Models.Leg", b =>
                {
                    b.HasOne("DriverTracker.Models.Driver", "Driver")
                        .WithMany("Legs")
                        .HasForeignKey("DriverID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DriverTracker.Models.Leg", "PreviousLeg")
                        .WithMany()
                        .HasForeignKey("PreviousLegID");
                });
#pragma warning restore 612, 618
        }
    }
}
