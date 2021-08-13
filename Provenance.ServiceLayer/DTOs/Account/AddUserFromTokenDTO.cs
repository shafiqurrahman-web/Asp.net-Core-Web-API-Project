using System;

namespace Provenance.ServiceLayer.DTOs.Account
{
	public class AddUserFromTokenDTO
	{
		public Guid Id { get; set; }
		public string Role { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Email { get; set; }
	}
}
