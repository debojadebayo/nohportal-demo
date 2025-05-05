using ComposedHealthBase.Server.Entities;

namespace Server.Modules.CRM.Entities
{
	public class Document : BaseEntity<Document>, IEntity
	{
		public string FilePath { get; set; }
		public string Description { get; set; }
	}
}