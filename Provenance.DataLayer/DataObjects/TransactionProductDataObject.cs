using System;

namespace Provenance.DataLayer.DataObjects
{
	public class TransactionProductDataObject
	{
		public Guid Id { get; set; }
		public DateTime CreateDate { get; set; }
		public Guid TransactionId { get; set; }
		public Guid TransactionProviderId { get; set; }
		public Guid TransactionCustomerId { get; set; }
		public string TransactionProviderName { get; set; }
		public DateTime TransactionCreateDate { get; set; }
		public Guid ProductId { get; set; }
		public string ProductCode { get; set; }
		public string ProductName { get; set; }

	}
}
