using AutoMapper;
using Provenance.DataLayer.DataObjects;
using Provenance.DataLayer.Entities;
using Provenance.ServiceLayer.DTOs.Transaction;
using System.Collections.Generic;
using System.Linq;

namespace Provenance.ServiceLayer.Mappings
{
	public class TransactionMapping : Profile
	{
		public TransactionMapping ()
		{
			CreateMap<Transaction, GetTransactionDTO>();
			CreateMap<TransactionDataObject, GetTransactionDTO>();
		}
	}


	public static class TransactionMapper
	{

		static IMapper Mapper;
		static TransactionMapper ()
		{
			Mapper = new MapperConfiguration(config => config.AddProfile<TransactionMapping>()).CreateMapper();
		}



		public static IEnumerable<GetTransactionDTO> ToGetTransactionQuery (this IQueryable<Transaction> item)
		{
			return Mapper.ProjectTo<GetTransactionDTO>(item).ToList();
		}
		public static GetTransactionDTO ToGetTransactionDTO (this Transaction item)
		{
			return Mapper.Map<GetTransactionDTO>(item);
		}
		public static IEnumerable<GetTransactionDTO> ToGetTransactionDTOList (this IEnumerable<Transaction> item)
		{
			return Mapper.Map<IEnumerable<GetTransactionDTO>>(item);
		}


		public static GetTransactionDTO ToGetTransactionDTO (this TransactionDataObject item)
		{
			return Mapper.Map<GetTransactionDTO>(item);
		}
		public static IEnumerable<GetTransactionDTO> ToGetTransactionDTOList (this IEnumerable<TransactionDataObject> item)
		{
			return Mapper.Map<IEnumerable<GetTransactionDTO>>(item);
		}


	}
}
