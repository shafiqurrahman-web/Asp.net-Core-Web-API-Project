using Provenance.DataLayer.Base;
using System;
using System.Collections.Generic;

namespace Provenance.DataLayer.Entities
{
	public class TransactionProduct : EntityBase
	{
		public Guid TransactionId { get; set; }
		public Guid ProductId { get; set; }
		public DateTime CreateDate { get; set; }

		public virtual Product Product { get; set; }
		public virtual Transaction Transaction { get; set; }

		
		public static IReadOnlyCollection<string> CanCreate (Guid transactionId, Guid productId)
		{
			var errors = new List<string>();

			if (transactionId == Guid.Empty)
				errors.Add("transactionId is required");

			if (productId == Guid.Empty)
				errors.Add("productId is requred");

			return errors;
		}
		public static TransactionProduct Create (Guid transactionId, Guid productId)
		{

			if (CanCreate(transactionId, productId).Count > 0)
			{
				throw new ApplicationException(string.Format("there are some errors accured when creating {0}", nameof(TransactionProduct)));
			}

			var item = new TransactionProduct
			{
				TransactionId = transactionId,
				ProductId = productId,
				CreateDate = DateTime.Now
			};
			return item;
		}


	}
}
