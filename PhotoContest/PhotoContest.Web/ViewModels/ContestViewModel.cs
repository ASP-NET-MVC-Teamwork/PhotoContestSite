namespace PhotoContest.Web.ViewModels
{
    using System;
    using Common.Mappings;
    using PhotoContest.Models;

    public class ContestViewModel : IMapFrom<Contest>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}