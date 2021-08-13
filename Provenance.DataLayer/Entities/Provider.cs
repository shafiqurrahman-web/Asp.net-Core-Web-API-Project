using Provenance.DataLayer.Base;
using System;
using System.Collections.Generic;

namespace Provenance.DataLayer.Entities
{
	public class Provider : EntityBase
	{
		public Guid UserId { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public User User { get; set; }

		public ICollection<Transaction> Transactions { get; set; }

		public static IReadOnlyCollection<string> CanCreate (Guid userId, string code, string name, string description = null)
		{
			var errors = new List<string>();

			if (userId == Guid.Empty)
				errors.Add("userid is required");

			if (string.IsNullOrWhiteSpace(code))
				errors.Add("code is required");

			if (string.IsNullOrWhiteSpace(name))
				errors.Add("name is requred");

			if (description != null && description.Length > 2000)
				errors.Add("description is too long");

			return errors;
		}
		public static Provider Create (Guid userId, string code, string name, string description = null)
		{
			if (CanCreate(userId, code, name, description).Count > 0)
			{
				throw new ApplicationException(string.Format("there are some errors accured when creating {0}", nameof(Provider)));
			}

			var item = new Provider()
			{
				UserId = userId,
				Code = code,
				Name = name,
				Description = description
			};
			return item;
		}


		public IReadOnlyCollection<string> CanUpdate (string code, string name, string description = null)
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
				throw new ApplicationException(string.Format("there are some errors accured when updating {0}", nameof(Provider)));
			}

			Code = code;
			Name = name;
			Description = description;
		}


	}
}
