using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HickoryPTAApp.Models
{
    public class NavigationViewModel
    {
        public ICollection<NavigationCommittee> NavigationCommittees { get; set; }

        public NavigationViewModel()
        {
            NavigationCommittees = new List<NavigationCommittee>();
        }
    }

    public class NavigationCommittee
    {
        public int CommitteeId { get; set; }
        public string CommitteeName { get; set; }
    }
}