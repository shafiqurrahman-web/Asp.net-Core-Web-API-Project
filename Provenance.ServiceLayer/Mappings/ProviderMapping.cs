using AutoMapper;
using Provenance.DataLayer.Entities;
using Provenance.ServiceLayer.DTOs.Provider;
using System.Collections.Generic;

namespace Provenance.ServiceLayer.Mappings
{
	public class ProviderMapping : Profile
	{
		public ProviderMapping ()
		{
			CreateMap<Provider, GetProviderDTO>();
		}
	}


	public static class ProviderMapper
	{

		static IMapper Mapper;
		static ProviderMapper ()
		{
			Mapper = new MapperConfiguration(config => config.AddProfile<ProviderMapping>()).CreateMapper();
		}



		public static GetProviderDTO ToGetProviderDTO (this Provider item)
		{
			return Mapper.Map<GetProviderDTO>(item);
		}
		public static IEnumerable<GetProviderDTO> ToGetProviderDTOList (this IEnumerable<Provider> item)
		{
			return Mapper.Map<IEnumerable<GetProviderDTO>>(item);
		}



	}
}
