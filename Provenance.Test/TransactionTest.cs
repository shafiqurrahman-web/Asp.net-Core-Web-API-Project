using Moq;
using Provenance.DataLayer.Base;
using Provenance.DataLayer.Entities;
using Provenance.DataLayer.Repositories;
using Provenance.ServiceLayer.Contracts;
using Provenance.ServiceLayer.Services;
using System;
using System.Linq.Expressions;
using Xunit;

namespace Provenance.Test
{


	public class TransactionTest
	{
		[Fact]
		public void transaction_should_created_after_scan_shopCode ()
		{

			//arrange
			var customer = new User ()
			{
				Id = new Guid("8F1D30E8-7C35-4BCA-9F63-B32EA035E26F")
			};

			var provider = new Provider ()
			{
				Id = new Guid("557B03DE-9005-4E67-AEFF-4431D1A8F5D9")
			};

			Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
			Mock<ITransactionRepository> transactionRepository = new Mock<ITransactionRepository>();
			Mock<IProviderRepository> providerRepository = new Mock<IProviderRepository>();
			Mock<IProductRepository> productRepository = new Mock<IProductRepository>();
			Mock<IUserRepository> userRepository = new Mock<IUserRepository>();
			Mock<ITransactionProductRepository> transactionProductRepository = new Mock<ITransactionProductRepository>();

			providerRepository.Setup(s => s.Exists(It.IsAny<Guid>())).Returns(true);
			userRepository.Setup(s => s.Exists(It.IsAny<Guid>())).Returns(true);

			ITransactionService transactionService = new TransactionService(
				unitOfWork.Object,
				transactionRepository.Object,
				providerRepository.Object,
				productRepository.Object,
				userRepository.Object,
				transactionProductRepository.Object
				);


			//act
			var result = transactionService.ScanShopCode(customer.Id, provider.Id);


			//assert
			transactionRepository.Verify(s => s.Add(It.IsAny<Transaction>()));
			transactionRepository.Verify(s => s.FindBy(It.IsAny<Expression<Func<Transaction, bool>>>()));
			unitOfWork.Verify(s => s.Commit());

		}

		[Fact]
		public void transaction_should_be_completed ()
		{

			//arrange
			var provider = new Provider ()
			{
				Id = new Guid("557B03DE-9005-4E67-AEFF-4431D1A8F5D9")
			};

			Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
			Mock<ITransactionRepository> transactionRepository = new Mock<ITransactionRepository>();
			Mock<IProviderRepository> providerRepository = new Mock<IProviderRepository>();
			Mock<IProductRepository> productRepository = new Mock<IProductRepository>();
			Mock<IUserRepository> userRepository = new Mock<IUserRepository>();
			Mock<ITransactionProductRepository> transactionProductRepository = new Mock<ITransactionProductRepository>();

			providerRepository.Setup(s => s.Exists(It.IsAny<Guid>())).Returns(true);
			userRepository.Setup(s => s.Exists(It.IsAny<Guid>())).Returns(true);
			transactionRepository.Setup(s => s.Find(It.IsAny<Expression<Func<Transaction, bool>>>()))
				.Returns(new Transaction()
				{
					Id = Guid.NewGuid(),
					ProviderId = new Guid("557B03DE-9005-4E67-AEFF-4431D1A8F5D9")
				});

			ITransactionService transactionService = new TransactionService(
				unitOfWork.Object,
				transactionRepository.Object,
				providerRepository.Object,
				productRepository.Object,
				userRepository.Object,
				transactionProductRepository.Object
				);

			//act
			transactionService.CompleteTransaction(provider.Id);


			//assert
			transactionRepository.Verify(s => s.Find(It.IsAny<Expression<Func<Transaction, bool>>>()));
			transactionRepository.Verify(s => s.Update(It.IsAny<Transaction>()));
			unitOfWork.Verify(s => s.Commit());

		}

	}
}
