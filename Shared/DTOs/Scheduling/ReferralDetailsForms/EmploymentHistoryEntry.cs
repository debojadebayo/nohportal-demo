namespace Shared.DTOs.Scheduling
{
    public class EmploymentHistoryEntry
    {
        // Employment History Fields (from NOHMED 022 template)
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string EmployerOrganisation { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string KeyDuties { get; set; } = string.Empty;

        // Order for display (most recent first)
        public int DisplayOrder { get; set; }
    }
}