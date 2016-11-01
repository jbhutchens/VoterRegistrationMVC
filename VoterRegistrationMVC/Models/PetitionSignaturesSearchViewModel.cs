using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoterRegistrationMVC.Models
{
    public class PetitionSignaturesSearchViewModel : IPetitionSignatureSearchResultsView, IPetitionSignatureSearchCriteriaView
    {
        
        public IEnumerable<PetitionSignatureSearch> PetitionSignatureSearchResults { get; set; }
        public PetitionSignatureSearchCriteriaModel PetitionSignatureSearchCriteriaModel { get; set; }
    }
}