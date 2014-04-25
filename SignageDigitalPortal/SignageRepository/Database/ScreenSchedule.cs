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
    
    public partial class ScreenSchedule
    {
        public long RelChannelScreenId { get; set; }
        public long ChannelId { get; set; }
        public long ScreenId { get; set; }
        public bool HasClose { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int PositionZ { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsFullScreen { get; set; }
        public Nullable<long> StartTime { get; set; }
        public Nullable<long> EndTime { get; set; }
        public Nullable<int> Day { get; set; }
        public Nullable<int> Year { get; set; }
        public string UserId { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Channel Channel { get; set; }
        public virtual Screen Screen { get; set; }
    }
}
