using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VoterRegistrationMVC.Models
{
    public class PetitionSignatures 
    {
        [Key, Column(Order = 0)]
        public int PetitionID { get; set; }
        [Key, Column(Order = 1)]
        public int PetitionDetailID { get; set; }
        [Key, Column(Order = 2)]
        public int LineNumber { get; set; }
        public string VoteFileID { get; set; }
        public string VoterID { get; set; }
    }
}