using System;

namespace Provenance.DataLayer.DataObjects
{
	public class TransactionDataObject
	{
		public Guid Id { get; set; }
		public Guid ProviderId { get; set; }
		public Guid CustomerId { get; set; }
		public string ProviderName { get; set; }
		public string CustomerName { get; set; }
		public DateTime CreateDate { get; set; }
		public bool IsCompleted { get; set; }
	}
}
