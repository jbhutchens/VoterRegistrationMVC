using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VoterRegistrationMVC.Models
{
    public class PetitionDetailsViewModel
    {
        [Display(Name = "Petition ID:")]
        public int PetitionID { get; set;}
        
        public IEnumerable<SelectListItem> PetitionIDValues { get; set; }

        public int PetitionDetailID { get; set; }

        [Display(Name = "Person Assigned:")]
        public string PersonAssigned { get; set; }
    }
}