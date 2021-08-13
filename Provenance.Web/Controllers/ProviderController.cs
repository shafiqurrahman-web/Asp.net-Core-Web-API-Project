using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Provenance.Common.Configurations;
using Provenance.ServiceLayer.Contracts;
using Provenance.ServiceLayer.DTOs.Provider;
using Provenance.Web.Modules;
using System;

namespace Provenance.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProviderController : ControllerBase
	{
		private readonly IProviderService _providerService;
		private readonly ITransactionService _transactionService;

		public ProviderController (
			ILogger<ProviderController> logger,
			IProviderService providerService,
			ITransactionService transactionService)
		{
			_providerService = providerService;
			_transactionService = transactionService;
		}

		/// <summary>
		/// this api will add product to active transaction wich created before by customer for provider
		/// </summary>
		/// <param name="id">userid/provider holder id</param>
		/// <param name="productId"></param>
		/// <returns></returns>
		//[Authorize(Policy = Policies.ACTIVE)]
		[HttpPost("{id}/ScanProduct/{productId}")]
		public IActionResult ScanProduct (Guid id, Guid productId)
		{
			_transactionService.ScanProduct(id, productId);
			return Ok();
		}

		/// <summary>
		/// this api will complete the active transaction
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		//[Authorize(Policy = Policies.ACTIVE)]
		[HttpPut("{id}/CompleteTheTransaction")]
		public IActionResult CompleteTheTransaction (Guid id)
		{
			_transactionService.CompleteTransaction(id);
			return Ok();
		}



		//[Authorize(Roles = RoleTypes.ADMIN)]
		[HttpPost]
		public IActionResult Post (AddProviderDTO inputData)
		{
			return _providerService.Add(inputData).ToHttpResponse();
		}

		//[Authorize(Roles = RoleTypes.ADMIN)]
		[HttpGet("{id}")]
		public IActionResult Get (Guid id)
		{
			return _providerService.Get(id).ToHttpResponse();
		}

		//[Authorize(Roles = RoleTypes.ADMIN)]
		[HttpPut("{id}")]
		public IActionResult Put (Guid id, UpdateProviderDTO inputData)
		{
			return _providerService.Update(id, inputData).ToHttpResponse();
		}

		//[Authorize(Roles = RoleTypes.ADMIN)]
		[HttpDelete("{id}")]
		public IActionResult Delete (Guid id)
		{
			return _providerService.Delete(id).ToHttpResponse();
		}

		//[Authorize(Roles = RoleTypes.ADMIN)]
		[HttpGet("all")]
		public IActionResult GetAll ()
		{
			return _providerService.GetAll().ToHttpResponse();
		}

		//[Authorize(Roles = RoleTypes.ADMIN)]
		[HttpGet]
		public IActionResult Get (int page, int pageSize)
		{
			return _providerService.GetAll(page, pageSize).ToHttpResponse();
		}


	}
}