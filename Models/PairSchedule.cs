namespace DigitalPortalAcademy.Models
{
    public class PairSchedule
    {
        public int PairScheduleId { get; set; }
        public int BuildingId { get; set; }
        public int PairNumber { get; set; }
        public DateTime StartTime { get; set; } 
        public DateTime EndTime { get; set; }

        public virtual Building Building { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    }

}
