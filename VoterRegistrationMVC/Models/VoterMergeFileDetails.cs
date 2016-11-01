using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web; 

namespace VoterRegistrationMVC.Models
{
    public class VoterMergeFileDetails 
    {
        [Key]
        public string VoterFileID { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateMerged { get; set; }
        [DataType(DataType.Date)]
        public DateTime JeffcoDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime AdamsCountyDate { get; set; }
    }
}