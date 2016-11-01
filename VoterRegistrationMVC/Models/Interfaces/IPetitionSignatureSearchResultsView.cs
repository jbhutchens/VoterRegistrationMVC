using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoterRegistrationMVC.Models;

namespace VoterRegistrationMVC.Models
{
    public interface IPetitionSignatureSearchResultsView
    {
        IEnumerable<PetitionSignatureSearch> PetitionSignatureSearchResults { get; }
    }
}
