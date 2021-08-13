using AutoMapper;
using Provenance.DataLayer.Entities;
using Provenance.ServiceLayer.DTOs.Account;
using System.Collections.Generic;

namespace Provenance.ServiceLayer.Mappings
{
	public class UserMapping : Profile
	{
		public UserMapping ()
		{
			CreateMap<User, GetUserDTO>();
			//CreateMap<UserDataObject, GetUserDTO>();
		}
	}


	public static class UserMapper
	{

		static IMapper Mapper;
		static UserMapper ()
		{
			Mapper = new MapperConfiguration(config => config.AddProfile<UserMapping>()).CreateMapper();
		}



		public static GetUserDTO ToGetUserDTO (this User item)
		{
			return Mapper.Map<GetUserDTO>(item);
		}
		public static IEnumerable<GetUserDTO> ToGetUserDTOList (this IEnumerable<User> item)
		{
			return Mapper.Map<IEnumerable<GetUserDTO>>(item);
		}


		//public static GetUserDTO ToGetUserDTO (this UserDataObject item)
		//{
		//	return Mapper.Map<GetUserDTO>(item);
		//}
		//public static IEnumerable<GetUserDTO> ToGetUserDTOList (this IEnumerable<UserDataObject> item)
		//{
		//	return Mapper.Map<IEnumerable<GetUserDTO>>(item);
		//}


	}
}
