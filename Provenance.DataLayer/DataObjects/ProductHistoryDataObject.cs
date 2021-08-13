using System;

namespace Provenance.DataLayer.DataObjects
{
	public class ProductHistoryDataObject
	{
		public Guid Id { get; set; }
		public Guid ProductId { get; set; }
		public string ProductName { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public byte[] Picture { get; set; }
		public string LocationTitle { get; set; }
		public string LocationAddress { get; set; }
		public DateTime Date { get; set; }
	}
}
