﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SignageDigitalEntities : DbContext
    {
        public SignageDigitalEntities()
            : base("name=SignageDigitalEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CatMime> CatMime { get; set; }
        public virtual DbSet<CatScreenSize> CatScreenSize { get; set; }
        public virtual DbSet<Channel> Channel { get; set; }
        public virtual DbSet<ChannelSchedule> ChannelSchedule { get; set; }
        public virtual DbSet<Device> Device { get; set; }
        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<Screen> Screen { get; set; }
        public virtual DbSet<ScreenSchedule> ScreenSchedule { get; set; }
    }
}
