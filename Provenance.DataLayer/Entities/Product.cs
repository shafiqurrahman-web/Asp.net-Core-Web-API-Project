using Provenance.DataLayer.Base;
using System;
using System.Collections.Generic;

namespace Provenance.DataLayer.Entities
{
	public class Product : EntityBase
	{

		public string Code { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public virtual ICollection<TransactionProduct> TransactionProducts { get; set; }
		public virtual ICollection<ProductHistory> ProductHistorys { get; set; }


		public static IReadOnlyCollection<string> CanCreate (string code, string name, string description = null)
		{
			var errors = new List<string>();

			if (string.IsNullOrWhiteSpace(code))
				errors.Add("code is required");

			if (string.IsNullOrWhiteSpace(name))
				errors.Add("name is requred");

			if (description != null && description.Length > 2000)
				errors.Add("description is too long");

			return errors;

		}
		public static Product Create (string code, string name, string description = null)
		{

			if (CanCreate(code, name, description).Count > 0)
			{
				throw new ApplicationException(string.Format("there are some errors accured when creating {0}", nameof(Product)));
			}

			var item = new Product
			{
				Code = code,
				Name = name,
				Description = description
			};
			return item;
		}


		public static IReadOnlyCollection<string> CanUpdate (string code, string name, string description = null)
		{
			var errors = new List<string>();

			if (string.IsNullOrWhiteSpace(code))
				errors.Add("code is required");

			if (string.IsNullOrWhiteSpace(name))
				errors.Add("name is requred");

			if (description != null && description.Length > 2000)
				errors.Add("description is too long");

			return errors;

		}
		public void Update (string code, string name, string description)
		{

			if (CanUpdate(code, name, description).Count > 0)
			{
				throw new ApplicationException(string.Format("there are some errors accured when updating {0}", nameof(Product)));
			}

			Code = code;
			Name = name;
			Description = description;
		}

	}
}
