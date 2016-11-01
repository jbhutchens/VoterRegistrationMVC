using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace VoterRegistrationMVC.Models
{
    public class Petition  
    {
        public string VoterFileID { get; set; }
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PetitionID { get; set; }
        public string PetitionName { get; set; }
        public int PetitionCount { get; set; }
        [DataType(DataType.Date)]
        public DateTime PetitionDate { get; set; }
    }
}