namespace SoloTravelAgent.Model.DTO
{
    public class TripStatistics
    {
        public int TripId { get; set; }
        public string TripName { get; set; }
        public int NumberOfBookings { get; set; }
        public decimal TotalMoneyEarned { get; set; }
    }
}
