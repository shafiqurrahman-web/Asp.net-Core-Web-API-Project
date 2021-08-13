using Provenance.Common.Responses;
using Provenance.DataLayer.Base;
using Provenance.DataLayer.Entities;
using Provenance.DataLayer.Repositories;
using Provenance.ServiceLayer.Contracts;
using Provenance.ServiceLayer.DTOs.Account;
using Provenance.ServiceLayer.Mappings;
using System;

namespace Provenance.ServiceLayer.Services
{
	public class AccountService : IAccountService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUserRepository _userRepository;
		private readonly IRoleRepository _roleRepository;

		private Guid userId;


		public AccountService (
			IUnitOfWork unitOfWork,
			IUserRepository userRepository,
			IRoleRepository roleRepository)
		{
			_unitOfWork = unitOfWork;
			_userRepository = userRepository;
			_roleRepository = roleRepository;
		}

		public void SetUserId (Guid id)
		{
			userId = id;
		}

		public Result AddUserFromToken (AddUserFromTokenDTO data)
		{

			if (string.IsNullOrWhiteSpace(data.Email) || data.Id.Equals(Guid.Empty))
				return Result.Error("all data about your profile, email and etc in consent page on SSO are needed for using this app.");

			var dbUser = _userRepository.Find(e => e.Email == data.Email);
			if (dbUser != null)
			{
				if (dbUser.Id != data.Id)
				{
					return Result.Error("there is a data inconsistency in app database, please contact with contact@wtxhub.com");
				}
				return Result.Ok(true);
			}

			var role = _roleRepository.Find(e => e.Name.Equals(data.Role));

			if (role == null)
				return Result.Error(string.Format("the role {0} is required", data.Role));

			var canCreate = User.CanCreate(data.Email, data.Firstname, data.Lastname);
			if (canCreate.Count > 0)
				return Result.Error(canCreate);

			var user = User.Create(role.Id, data.Email, data.Firstname, data.Lastname);
			_userRepository.Add(user);

			_unitOfWork.Commit();

			return Result.Ok(true);

		}

		public Result GetUserById (Guid userId)
		{
			var data = _userRepository.Get(userId).ToGetUserDTO();
			return Result.Ok(data);
		}

		public Guid GetUserId ()
		{
			return this.userId;
		}
	}
}
