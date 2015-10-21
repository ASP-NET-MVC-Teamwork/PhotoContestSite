namespace PhotoContest.Web.ViewModel
{
    using PhotoContest.Models;
    using SportSystem.Common.Mappings;

    public class ContestViewModel : IMapFrom<Contest>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}