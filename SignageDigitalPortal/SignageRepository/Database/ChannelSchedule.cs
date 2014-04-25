//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SignageRepository.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChannelSchedule
    {
        public long ScheduleId { get; set; }
        public long MediaId { get; set; }
        public long ChannelId { get; set; }
        public Nullable<long> StartTime { get; set; }
        public Nullable<long> EndTime { get; set; }
        public Nullable<int> Day { get; set; }
        public Nullable<int> Year { get; set; }
        public System.DateTime Timestamp { get; set; }
    
        public virtual Channel Channel { get; set; }
        public virtual Media Media { get; set; }
    }
}
