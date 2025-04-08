namespace ComposedHealthBase.Server.BaseModule.Entities
{
	public class BaseEntity<T> : IBaseEntity
	where T : class
	{
		public long Id { get; set; }
		public bool IsActive { get; set; }
		public int CreatedBy { get; set; }
		public int LastModifiedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
	}
}