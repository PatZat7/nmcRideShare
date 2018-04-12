//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class RideRequest
    {
        public string RiderId { get; set; }
        public int RequestNum { get; set; }
        public Nullable<int> ScheduleId { get; set; }
        public System.TimeSpan DepartingTime { get; set; }
        public System.TimeSpan ArrivalTime { get; set; }
        public byte RequestStatusId { get; set; }
        public Nullable<int> PickupLocationId { get; set; }
        public Nullable<int> DropoffLocationId { get; set; }
    
        public virtual AspNetUser Rider { get; set; }
        public virtual RideRequestStatu RideRequestStatu { get; set; }
        public virtual Schedule Schedule { get; set; }
        public virtual Geolocation DropoffLocation { get; set; }
        public virtual Geolocation PickupLocation { get; set; }
    }
}