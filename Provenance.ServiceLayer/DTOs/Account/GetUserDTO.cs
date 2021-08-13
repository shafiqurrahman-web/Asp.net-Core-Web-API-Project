using System;
using System.Collections.Generic;
using System.Text;

namespace Provenance.ServiceLayer.DTOs.Account
{
	public class GetUserDTO
	{

		public Guid Id { get; set; }
		public string Email { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }

		public Guid RoleId { get; set; }
		public string RoleName { get; set; }

		public Guid CompanyId { get; set; }
		public string CompanyName { get; set; }
		public string CompanyDescription { get; set; }

		public Guid ProfileId { get; set; }
		public string ProfileIdentifier { get; set; }
		public bool HasProfile { get; set; }

	}
}
