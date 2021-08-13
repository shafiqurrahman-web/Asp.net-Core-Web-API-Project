using System;
using System.Collections.Generic;
using System.Text;

namespace Provenance.ServiceLayer.DTOs.Transaction
{
	public class AddTransactionDTO
	{
		public Guid ProviderId { get; set; }
		public Guid CustomerId { get; set; }

	}
}
