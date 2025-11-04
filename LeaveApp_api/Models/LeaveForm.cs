namespace LeaveApp_api.Models
{
    public class LeaveForm
    {
        public int ApplicantId { get; set; }
        public int ManagerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int NumberOfDays { get; set; }
        public string? GeneralComments { get; set; }
        public string LeaveType { get; set; }
    }
}
