﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AddToComputersDB.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ComponentsDBEntities : DbContext
    {
        public ComponentsDBEntities()
            : base("name=ComponentsDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<MobosSockets> MobosSockets { get; set; }
        public virtual DbSet<Processors> Processors { get; set; }
        public virtual DbSet<ProcessorSocket> ProcessorSocket { get; set; }
        public virtual DbSet<Sockets> Sockets { get; set; }
        public virtual DbSet<MoBos> MoBos { get; set; }
        public virtual DbSet<Bits> Bits { get; set; }
        public virtual DbSet<Computers> Computers { get; set; }
        public virtual DbSet<Disks> Disks { get; set; }
        public virtual DbSet<Memories> Memories { get; set; }
        public virtual DbSet<OSs> OSs { get; set; }
        public virtual DbSet<Components> Components { get; set; }
    }
}
