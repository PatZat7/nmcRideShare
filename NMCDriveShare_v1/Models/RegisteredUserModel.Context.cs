﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NMCDriveShare_v1.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DriveShareEntities3 : DbContext
    {
        public DriveShareEntities3()
            : base("name=DriveShareEntities3")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<ChatThread> ChatThreads { get; set; }
        public virtual DbSet<ProfilePicture> ProfilePictures { get; set; }
        public virtual DbSet<RideRequest> RideRequests { get; set; }
        public virtual DbSet<RideRequestStatu> RideRequestStatus { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Geolocation> Geolocations { get; set; }
        public virtual DbSet<vw_UserLocations> vw_UserLocations { get; set; }
        public virtual DbSet<ChatMessage> ChatMessages { get; set; }
    
        public virtual int AddNewUser(string firstName, string lastName, Nullable<bool> isDriver, string gender, string authUserId)
        {
            var firstNameParameter = firstName != null ?
                new ObjectParameter("firstName", firstName) :
                new ObjectParameter("firstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("lastName", lastName) :
                new ObjectParameter("lastName", typeof(string));
    
            var isDriverParameter = isDriver.HasValue ?
                new ObjectParameter("isDriver", isDriver) :
                new ObjectParameter("isDriver", typeof(bool));
    
            var genderParameter = gender != null ?
                new ObjectParameter("gender", gender) :
                new ObjectParameter("gender", typeof(string));
    
            var authUserIdParameter = authUserId != null ?
                new ObjectParameter("authUserId", authUserId) :
                new ObjectParameter("authUserId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddNewUser", firstNameParameter, lastNameParameter, isDriverParameter, genderParameter, authUserIdParameter);
        }
    
        [DbFunction("DriveShareEntities3", "fn_UserGeolocation")]
        public virtual IQueryable<fn_UserGeolocation_Result> fn_UserGeolocation(string userId)
        {
            var userIdParameter = userId != null ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<fn_UserGeolocation_Result>("[DriveShareEntities3].[fn_UserGeolocation](@userId)", userIdParameter);
        }
    }
}