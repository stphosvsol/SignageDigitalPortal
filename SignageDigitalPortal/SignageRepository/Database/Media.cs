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
    
    public partial class Media
    {
        public Media()
        {
            this.ChannelSchedule = new HashSet<ChannelSchedule>();
        }
    
        public long MediaId { get; set; }
        public int CatMimeId { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public string Type { get; set; }
        public Nullable<long> Length { get; set; }
        public string Url { get; set; }
    
        public virtual CatMime CatMime { get; set; }
        public virtual ICollection<ChannelSchedule> ChannelSchedule { get; set; }
    }
}
