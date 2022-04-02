using Gardenia.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardenia.Data.DataAccess
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        public DbSet<AboutGardenia> AboutGardenia { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Development> Developments { get; set; }
        public DbSet<DevelopmentCategory> DevelopmentCategories { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsImages> NewsImages { get; set; }
        public DbSet<Compound> Compounds { get; set; }
        public DbSet<NewsComments> Comments { get; set; }
        public DbSet<GardeniaData> GardeniaData { get; set; }
        public DbSet<UnitStatus> UnitStatus { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<UnitType> UnitTypes { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Floar> Floars { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<UnitObservation> UnitObservations { get; set; }
        public DbSet<ObservationImages> ObservationImages { get; set; }
        public DbSet<ObservationType> ObservationTypes { get; set; }
        public DbSet<ObservationResponse> ObservationResponses { get; set; }
        public DbSet<ObservationResponseImages> ObservationResponseImages { get; set; }
        //public DbSet<UsersUnit> UsersUnits { get; set; }
        public DbSet<UserIdentity> UserIdentities { get; set; }
        public DbSet<PublicTraffic> PublicTraffics { get; set; }
        public DbSet<NormalPublicTraffic> NormalPublicTraffics { get; set; }
        public DbSet<Police> Polices { get; set; }
        public DbSet<Electricity> Electricities { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderImages> OrderImages { get; set; }
        public DbSet<OrderResponse> OrderResponses { get; set; }
        public DbSet<OrderResponseImages> OrderResponseImages { get; set; }
        public DbSet<ReportType> ReportTypes { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportImages> ReportImages { get; set; }
        public DbSet<ReportResponse> ReportResponses { get; set; }
        public DbSet<ReportResponseImages> ReportResponseImages { get; set; }
        public DbSet<MaintainanceType> MaintainanceTypes { get; set; }
        public DbSet<Maintainance> Maintainances { get; set; }
        public DbSet<MaintainanceImages> MaintainanceImages { get; set; }
        public DbSet<MaintainanceResponse> MaintainanceResponses { get; set; }
        public DbSet<MaintainanceResponseImages> MaintainanceResponseImages { get; set; }
        public DbSet<GateLog> GateLogs { get; set; }

        public DbSet<Visitor> Visitor { get; set; }
        public DbSet<QRVisitorInvitation> QRVisitorInvitation { get; set; }
        public DbSet<SMSVisitorInvitation> SMSVisitorInvitation { get; set; }
        public DbSet<VisitsLogs>VisitsLogs { get; set; }
        public DbSet<WorkImageID> WorkImageID { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<AppUser>()
            //    .HasMany(a => a.Units)
            //    .WithMany(u => u.Users)
            //    .UsingEntity<UsersUnit>(
            //        uu => uu
            //        .HasOne(us => us.Unit)
            //        .WithMany(u => u.UsersUnit)
            //        .HasForeignKey(a => a.UnitID),

            //        uu => uu
            //        .HasOne(us => us.User)
            //        .WithMany(u => u.UsersUnit)
            //        .HasForeignKey(us => us.UserID),

            //        uu => uu.HasKey(t => new { t.UserID, t.UnitID })

            //    );


            builder.Entity<AppUser>()
                .HasOne(owner=>owner.Owner)
                .WithMany(follower => follower.Followers)
                .HasForeignKey(owner=>owner.OwnerId);

            //To Solve The problem of The entity type 'IdentityUserLogin<string>' requires a primary key to be defined.
            base.OnModelCreating(builder);

            builder.Entity<WorkImageID>()
                .HasKey(i => new { i.ImagePath, i.VisitsLogId });
            //builder.Entity<UsersUnit>()

            builder.Entity<QRVisitorInvitation>()
                .Property(i => i.QRCode).IsRequired().HasMaxLength(50);

            //builder.Entity<QRVisitorInvitation>()
            //    .HasOne(i => i.VisitsLogs)
            //    .WithOne(i => i.QRVisitorInvitation)
            //    .HasForeignKey<VisitsLogs>(i => i.QRVisitorInvitationId);

            //builder.Entity<SMSVisitorInvitation>()
            //    .HasOne(i => i.VisitsLogs)
            //    .WithOne(i => i.SMSVisitorInvitation)
            //    .HasForeignKey<VisitsLogs>(i => i.SMSVisitorInvitationId);

            //builder.Entity<QRVisitorInvitation>()
            //    .Property(i => i.Email).IsRequired().HasMaxLength(20);
            //    .HasKey(uu => new { uu.UnitID, uu.UserID });
        }
    }
}
