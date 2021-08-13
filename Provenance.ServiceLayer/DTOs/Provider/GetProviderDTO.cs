using System;
using System.Collections.Generic;
using System.Text;

namespace Provenance.ServiceLayer.DTOs.Provider
{
	public class GetProviderDTO
	{
		public Guid Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
