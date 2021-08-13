using AutoMapper;
using Provenance.DataLayer.DataObjects;
using Provenance.DataLayer.Entities;
using Provenance.ServiceLayer.DTOs.TransactionProduct;
using System.Collections.Generic;

namespace Provenance.ServiceLayer.Mappings
{
	public class TransactionProductMapping : Profile
	{
		public TransactionProductMapping ()
		{
			CreateMap<TransactionProduct, GetTransactionProductDTO>();
			CreateMap<TransactionProductDataObject, GetTransactionProductDTO>();
		}
	}


	public static class TransactionProductMapper
	{

		static IMapper Mapper;
		static TransactionProductMapper ()
		{
			Mapper = new MapperConfiguration(config => config.AddProfile<TransactionProductMapping>()).CreateMapper();
		}



		public static GetTransactionProductDTO ToGetTransactionProductDTO (this TransactionProduct item)
		{
			return Mapper.Map<GetTransactionProductDTO>(item);
		}
		public static IEnumerable<GetTransactionProductDTO> ToGetTransactionProductDTOList (this IEnumerable<TransactionProduct> item)
		{
			return Mapper.Map<IEnumerable<GetTransactionProductDTO>>(item);
		}


		public static GetTransactionProductDTO ToGetTransactionProductDTO (this TransactionProductDataObject item)
		{
			return Mapper.Map<GetTransactionProductDTO>(item);
		}
		public static IEnumerable<GetTransactionProductDTO> ToGetTransactionProductDTOList (this IEnumerable<TransactionProductDataObject> item)
		{
			return Mapper.Map<IEnumerable<GetTransactionProductDTO>>(item);
		}


	}

}
