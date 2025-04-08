using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Base.DTOs;

namespace Shared.DTOs.CRM
{
	public class CustomerDto : IDto
	{
		public string CompanyId { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public int NumberOfEmployees { get; set; }
		public string Site { get; set; } = string.Empty;
		public string OHServicesRequired { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public string Industry { get; set; } = string.Empty;
		public string Postcode { get; set; } = string.Empty;
		public string Website { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string InvoiceEmail { get; set; } = string.Empty;
		public string? Notes { get; set; }
	}
}
