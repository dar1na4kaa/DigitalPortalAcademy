namespace DigitalPortalAcademy.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public int GroupId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public string DayOfWeek { get; set; }
        public int PairScheduleId { get; set; }

        public virtual Group Group { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual PairSchedule PairSchedule { get; set; } 
    }
}
