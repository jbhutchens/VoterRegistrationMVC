using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VoterRegistrationMVC.Models
{
    public class PetitionDetails
    {
        [Key, Column(Order = 0)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PetitionID { get; set; }

        [Key, Column(Order = 1)]
        public int PetitionDetailID { get; set; }


        public string PersonAssigned { get; set; } 
    }
}