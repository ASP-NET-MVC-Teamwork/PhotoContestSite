using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoContest.Web.ImputModels
{
    using PhotoContest.Models.Enums;

    public class ContestInputModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public RewardStrategy RewardStrategy { get; set; }

        public ContestType Type { get; set; }

        public VotingStrategy VotingStrategy { get; set; }

        public DeadlineStrategy DeadlineStrategy { get; set; }


    }
}