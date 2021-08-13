using System;

namespace Provenance.ServiceLayer.DTOs.ProductHistory
{
	public class UpdateProductHistoryDTO
	{
		public Guid ProductId { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public byte[] Picture { get; set; }
		public string LocationTitle { get; set; }
		public string LocationAddress { get; set; }
		public DateTime Date { get; set; }
	}
}
