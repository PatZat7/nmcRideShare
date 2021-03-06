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
    
    public partial class RideBoard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RideBoard()
        {
            this.Riders = new HashSet<User>();
        }
    
        public int boardId { get; set; }
        public int driverId { get; set; }
        public Nullable<int> destinationId { get; set; }
    
        public virtual Geolocation Destination { get; set; }
        public virtual User Driver { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Riders { get; set; }
    }
}
