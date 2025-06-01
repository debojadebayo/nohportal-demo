using ComposedHealthBase.Server.Entities;


namespace Server.Modules.CRM.Entities
{
	public class EmployeeDocument : Document
	{
		public long EmployeeId
		{
			get
			{
				return SubjectId;
			}
			set
			{
				SubjectId = value;
			}
		}
	}
}