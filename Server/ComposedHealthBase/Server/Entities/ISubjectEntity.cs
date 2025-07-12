namespace ComposedHealthBase.Server.Entities
{
    public interface ISubjectEntity
    {
        Guid SubjectId { get; set; }
        string? RoleName { get; set; }
    }
}
