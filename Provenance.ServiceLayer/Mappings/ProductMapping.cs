using AutoMapper;
using Provenance.DataLayer.Entities;
using Provenance.ServiceLayer.DTOs.Product;
using System.Collections.Generic;

namespace Provenance.ServiceLayer.Mappings
{

	public class ProductMapping : Profile
	{
		public ProductMapping ()
		{
			CreateMap<Product, GetProductDTO>();
		}
	}


	public static class ProductMapper
	{

		static IMapper Mapper;
		static ProductMapper ()
		{
			Mapper = new MapperConfiguration(config => config.AddProfile<ProductMapping>()).CreateMapper();
		}



		public static GetProductDTO ToGetProductDTO (this Product item)
		{
			return Mapper.Map<GetProductDTO>(item);
		}
		public static IEnumerable<GetProductDTO> ToGetProductDTOList (this IEnumerable<Product> item)
		{
			return Mapper.Map<IEnumerable<GetProductDTO>>(item);
		}



	}

}
