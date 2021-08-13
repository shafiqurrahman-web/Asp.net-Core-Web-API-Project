using Provenance.DataLayer.Base;
using System;
using System.Collections.Generic;

namespace Provenance.DataLayer.Entities
{
	public class User : EntityBase
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }

		public Guid RoleId { get; set; }
		public Role Role { get; set; }


		public virtual ICollection<Transaction> Transactions { get; set; }
		public virtual Provider Provider { get; set; }


		public static IReadOnlyCollection<string> CanCreate (string email, string firstname, string lastname)
		{
			var errors = new List<string>();

			if (string.IsNullOrWhiteSpace(email))
				errors.Add("email is required");

			if (string.IsNullOrWhiteSpace(firstname))
				errors.Add("firstname is requred");

			if (string.IsNullOrWhiteSpace(lastname))
				errors.Add("lastname is requred");

			return errors;
		}
		public static User Create (Guid roleId, string email, string firstname, string lastname)
		{
			if (CanCreate(email, firstname, lastname).Count > 0)
			{
				throw new ApplicationException(string.Format("there are some errors accured when creating {0}", nameof(User)));
			}

			var item = new User()
			{
				Email = email,
				Firstname = firstname,
				Lastname = lastname
			};
			return item;
		}



	}
}
