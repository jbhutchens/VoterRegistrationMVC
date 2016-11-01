using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VoterRegistrationMVC.Models
{
    public class VoterSearchCriteriaModel
    {
        public string PetitionName { get; set; }
        public int PetitionID { get; set; }
        public string PetitionDetailName { get; set; }
        public int PetitionDetailID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HouseNumber { get; set; }
        
        [Display(Name = "Petitions:")] 
        public IEnumerable<SelectListItem> Petitions { get; set; }

        [Display(Name = "Detail Petition ID:")]
        public IEnumerable<SelectListItem> PetitionDetailValues { get; set; }

    }
}