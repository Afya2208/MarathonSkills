﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MarathonSkills.Database
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MarathonSkillsEntities : DbContext
    {
        public MarathonSkillsEntities()
            : base("name=MarathonSkillsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Charity> Charity { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventType> EventType { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<Marathon> Marathon { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<RaceKitOption> RaceKitOption { get; set; }
        public virtual DbSet<Registration> Registration { get; set; }
        public virtual DbSet<RegistrationEvent> RegistrationEvent { get; set; }
        public virtual DbSet<RegistrationStatus> RegistrationStatus { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Runner> Runner { get; set; }
        public virtual DbSet<Sponsorship> Sponsorship { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<Timesheet> Timesheet { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Volunteer> Volunteer { get; set; }
        public virtual DbSet<ArrivalItem> ArrivalItem { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<ItemsInKit> ItemsInKit { get; set; }
        public virtual DbSet<Storage> Storage { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<ItemComparateDistance> ItemComparateDistance { get; set; }
        public virtual DbSet<ItemComparateSpeed> ItemComparateSpeed { get; set; }
    }
}
