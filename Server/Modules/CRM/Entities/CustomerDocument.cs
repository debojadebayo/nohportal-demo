using ComposedHealthBase.Server.Entities;


namespace Server.Modules.CRM.Entities
{
	public class CustomerDocument : Document
	{
		public long CustomerId
		{
			get
			{
				return TenantId;
			}
			set
			{
				TenantId = value;
			}
		}
	}
}