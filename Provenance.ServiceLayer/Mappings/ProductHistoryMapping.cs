using AutoMapper;
using Provenance.DataLayer.DataObjects;
using Provenance.DataLayer.Entities;
using Provenance.ServiceLayer.DTOs.ProductHistory;
using System;
using System.Collections.Generic;

namespace Provenance.ServiceLayer.Mappings
{

	public class ProductHistoryMapping : Profile
	{
		public ProductHistoryMapping ()
		{
			CreateMap<ProductHistory, GetProductHistoryDTO>()
				.ForMember(dest => dest.PictureStr, opt =>
				opt.MapFrom(src => "data:image/png;base64," + Convert.ToBase64String(src.Picture, 0, src.Picture.Length)))
				;
			CreateMap<ProductHistoryDataObject, GetProductHistoryDTO>()
				.ForMember(dest => dest.PictureStr, opt => 
				opt.MapFrom(src => "data:image/png;base64," + Convert.ToBase64String(src.Picture, 0, src.Picture.Length)))
				;
		}
	}


	public static class ProductHistoryMapper
	{

		static IMapper Mapper;
		static ProductHistoryMapper ()
		{
			Mapper = new MapperConfiguration(config => config.AddProfile<ProductHistoryMapping>()).CreateMapper();
		}



		public static GetProductHistoryDTO ToGetProductHistoryDTO (this ProductHistory item)
		{
			return Mapper.Map<GetProductHistoryDTO>(item);
		}
		public static IEnumerable<GetProductHistoryDTO> ToGetProductHistoryDTOList (this IEnumerable<ProductHistory> item)
		{
			return Mapper.Map<IEnumerable<GetProductHistoryDTO>>(item);
		}

		public static GetProductHistoryDTO ToGetProductHistoryDTO (this ProductHistoryDataObject item)
		{
			return Mapper.Map<GetProductHistoryDTO>(item);
		}
		public static IEnumerable<GetProductHistoryDTO> ToGetProductHistoryDTOList (this IEnumerable<ProductHistoryDataObject> item)
		{
			return Mapper.Map<IEnumerable<GetProductHistoryDTO>>(item);
		}



	}
}
