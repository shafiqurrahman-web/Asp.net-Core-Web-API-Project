using Provenance.DataLayer.Base;
using System;
using System.Collections.Generic;

namespace Provenance.DataLayer.Entities
{
	public class Transaction : EntityBase
	{
		public Guid ProviderId { get; set; }
		public Guid CustomerId { get; set; }
		public DateTime CreateDate { get; set; }
		public bool IsCompleted { get; set; }


		public virtual Provider Provider { get; set; }
		public virtual User Customer { get; set; }
		public virtual ICollection<TransactionProduct> TransactionProducts { get; set; }


		public static IReadOnlyCollection<string> CanCreate (Guid providerId, Guid customerId)
		{
			var errors = new List<string>();

			if (providerId == Guid.Empty)
				errors.Add("providerId is required");

			if (customerId == Guid.Empty)
				errors.Add("customerId is requred");

			return errors;
		}
		public static Transaction Create (Guid providerId, Guid customerId)
		{

			if (CanCreate(providerId, customerId).Count > 0)
			{
				throw new ApplicationException(string.Format("there are some errors accured when creating {0}", nameof(Transaction)));
			}

			var item = new Transaction
			{
				ProviderId = providerId,
				CustomerId = customerId,
				CreateDate = DateTime.Now,
				IsCompleted = false
			};
			return item;
		}


		public void Complete ()
		{
			IsCompleted = true;
		}


		public IReadOnlyCollection<string> CanUpdate (Guid providerId, Guid customerId, bool isCompleted)
		{
			var errors = new List<string>();

			if (providerId == Guid.Empty)
				errors.Add("providerId is required");

			if (customerId == Guid.Empty)
				errors.Add("customerId is requred");

			return errors;
		}
		public void Update (Guid providerId, Guid customerId, bool isCompleted)
		{
			if (CanUpdate(providerId, customerId, isCompleted).Count > 0)
			{
				throw new ApplicationException(string.Format("there are some errors accured when creating {0}", nameof(Transaction)));
			}

			this.ProviderId = providerId;
			this.CustomerId = customerId;
			this.IsCompleted = IsCompleted;
		}


	}
}
