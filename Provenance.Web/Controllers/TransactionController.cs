using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Provenance.Common.Configurations;
using Provenance.ServiceLayer.Contracts;
using Provenance.ServiceLayer.DTOs.Transaction;
using Provenance.Web.Modules;
using System;

namespace Provenance.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TransactionController : ControllerBase
	{

		private readonly ITransactionService _transactionService;
		private readonly IProductService _productService;

		public TransactionController (
			ILogger<TransactionController> logger,
			ITransactionService transactionService,
			IProductService productService)
		{
			_transactionService = transactionService;
			_productService = productService;
		}

		
		//[Authorize(Policy = Policies.ACTIVE)]
		[HttpGet("{id}")]
		public IActionResult Get (Guid id)
		{
			return _transactionService.Get(id).ToHttpResponse();
		}
		
		//[Authorize(Policy = Policies.ACTIVE)]
		[HttpGet("{id}/Product")]
		public IActionResult GetProducts (Guid id)
		{
			return _productService.GetByTransactionId(id).ToHttpResponse();
		}		

		//[Authorize(Roles = RoleTypes.ADMIN)]
		[HttpPut("{id}")]
		public IActionResult Put (Guid id, UpdateTransactionDTO inputData)
		{
			return _transactionService.Update(id, inputData).ToHttpResponse();
		}

		//[Authorize(Roles = RoleTypes.ADMIN)]
		[HttpDelete("{id}")]
		public IActionResult Delete (Guid id)
		{
			return _transactionService.Delete(id).ToHttpResponse();
		}

		//[Authorize(Roles = RoleTypes.ADMIN)]
		[HttpGet("all")]
		public IActionResult GetAll ()
		{
			return _transactionService.GetAll().ToHttpResponse();
		}
		
		//[Authorize(Policy = Policies.ACTIVE)]
		[HttpGet]
		public IActionResult Get (int page, int pageSize)
		{
			return _transactionService.GetAll(page, pageSize).ToHttpResponse();
		}

	}
}