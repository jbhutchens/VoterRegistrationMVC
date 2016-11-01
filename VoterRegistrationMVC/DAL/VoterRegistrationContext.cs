using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoterRegistrationMVC.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace VoterRegistrationMVC.DAL
{
    public class VoterRegistrationContext : DbContext
    {
         
            public VoterRegistrationContext() : base("VoterRegistrationContext")
            {
            }

            public DbSet<VoterMergeFileDetails> VoterMergeFileDetails { get; set; }
            public DbSet<PetitionSignatures> PetitionSignatures { get; set; }
            public DbSet<VoterSearch> VoterSearch { get; set; }
            public DbSet<VoterRegistrationMVC.Models.Petition> Petitions { get; set; }
            public DbSet<VoterRegistrationMVC.Models.PetitionDetails> PetitionDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                modelBuilder.Entity<PetitionDetails>().MapToStoredProcedures(s => s.Insert(i => i.HasName("[dbo].[sp_InsertPetitionDetails]"))
                                                                                   .Update(u => u.HasName("[dbo].[sp_UpdatePetitionDetails]"))
                                                                                   .Delete(d => d.HasName("[dbo].[sp_DeletePetitionDetails]"))

                                                                             );
            }

        
         

    }
}