using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VoterRegistrationMVC.Models
{
    public class PetitionSignatureSearch
    {

        public int PetitionID { get; set; }

        public int PetitionDetailID { get; set; }

        [Key, Column(Order = 0)]
        public string MergeFileID { get; set; }

        [Key, Column(Order = 1)]
        public string VoterID { get; set; }

        public string County { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Lasst Name")]
        public string LastName { get; set; }

        [Display(Name = "House Num")]
        public string HouseNum { get; set; }

        public string StreetName { get; set; }

        public string AddressValue { get; set; }
    }
}