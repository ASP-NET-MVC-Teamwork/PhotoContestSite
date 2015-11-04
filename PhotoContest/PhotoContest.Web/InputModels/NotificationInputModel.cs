namespace PhotoContest.Web.InputModels
{
    using PhotoContest.Models;

    public class NotificationInputModel
    {
        public string SenderId { get; set; }
        
        public string RecieverId { get; set; }

        public int ContestId { get; set; }
    }
}