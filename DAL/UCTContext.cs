﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UCT
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using UCT.Models;

    public partial class UCTContext : DbContext
    {
        public UCTContext()
            : base("name=UCTEntities1")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public DbSet<Competency> Competencies { get; set; }
        public DbSet<LearningActivity> LearningActivities { get; set; }
        public DbSet<ProgramLearningActivity> ProgramLearningActivities { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Program> Roles { get; set; }
        public DbSet<UserProgram> UserPrograms { get; set; }
        public DbSet<ProgramLearningActivitiesCompetency> ProgramLearningActivitiesCompetency { get; set; }
    }
}