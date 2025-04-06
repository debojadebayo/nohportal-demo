using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.CRM
{
	public class CustomerDto
	{
		public int Id { get; set; } // Unique identifier for the customer
		public string? Name { get; set; } // Name of the customer organization
		public string? Address { get; set; } // Physical address of the customer
		public string? Email { get; set; } // Contact email
		public string? PhoneNumber { get; set; } // Contact phone number
		public string? ContactName { get; set; } // Main point of contact
		public List<ProductDto>? Products { get; set; } // List of products purchased by the customer
		public List<ContractDto>? Contracts { get; set; } // List of contracts associated with the customer
		public string? Notes { get; set; } // Additional notes or information about the customer
	}
}
