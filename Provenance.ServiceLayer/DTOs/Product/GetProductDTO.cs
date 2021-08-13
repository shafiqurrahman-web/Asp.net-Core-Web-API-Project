using System;

namespace Provenance.ServiceLayer.DTOs.Product
{
	public class GetProductDTO
	{
		public Guid Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
