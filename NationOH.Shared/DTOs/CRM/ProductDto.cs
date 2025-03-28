using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationOH.Shared.DTOs.CRM
{
	public class ProductDto
	{
		public int Id { get; set; } // Unique identifier for the product
		public string Name { get; set; } // Name of the product (e.g., "Full Day Doctor", "15-Minute Consultation")
		public string Description { get; set; } // Brief description of the product
		public decimal Price { get; set; } // Price associated with the product
		public string Unit { get; set; } // Unit of the product (e.g., "per hour", "per day")
		public bool IsActive { get; set; } // Indicates if the product is currently active and available
	}
}
