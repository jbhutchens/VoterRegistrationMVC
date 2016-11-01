using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 

namespace VoterRegistrationMVC.Models
{
    public class VoterSearchesViewModel : IVoterSearchesResultsView, IVoterSearchesCriteriaView
    {
      

        public IEnumerable<VoterSearch> VoterSearchResults { get; set; }
        public VoterSearchCriteriaModel VoterSearchCriteriaModel { get; set; }
    }
}