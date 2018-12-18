//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AirlineManagementAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Crew
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Crew()
        {
            this.CrewMembers = new HashSet<CrewMember>();
            this.Schedules = new HashSet<Schedule>();
        }
    
        public int CrewId { get; set; }
        public Nullable<int> NumberOfMembers { get; set; }
        public string CrewName { get; set; }
        public Nullable<int> OfficeId { get; set; }
    
        public virtual Office Office { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CrewMember> CrewMembers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
