namespace ComposedHealthBase.Server.BaseModule.Entities
{
	public interface IBaseEntity
	{
		public long Id { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
	}
}
