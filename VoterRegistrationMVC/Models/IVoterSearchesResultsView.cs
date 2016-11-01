using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoterRegistrationMVC.Models;

namespace VoterRegistrationMVC.Models
{
    public interface IVoterSearchesResultsView
    {
        IEnumerable<VoterSearch> VoterSearchResults { get; }
    }
}

 