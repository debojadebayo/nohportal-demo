using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComposedHealthBase.Server.BaseModule.Entities
{
	public class ApplicationUser : BaseEntity<ApplicationUser>
	{
		public string KeycloakUuid { get; set; }
	}
}