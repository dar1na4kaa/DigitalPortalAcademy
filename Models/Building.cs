namespace DigitalPortalAcademy.Models
{
    public class Building
    {
        public int BuildingId { get; set; }
        public string BuildingName { get; set; } 

        public virtual ICollection<PairSchedule> PairSchedules { get; set; } = new List<PairSchedule>();
    }

}
