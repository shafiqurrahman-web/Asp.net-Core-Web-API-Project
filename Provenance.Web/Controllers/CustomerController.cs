using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Provenance.Common.Configurations;
using Provenance.ServiceLayer.Contracts;
using Provenance.Web.Modules;
using System;

namespace Provenance.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly ITransactionService _transactionService;

		public CustomerController (ILogger<CustomerController> logger, ITransactionService transactionService)
		{
			_transactionService = transactionService;
		}

		/// <summary>
		/// the start point of app is this api, first of all the customer scan the shop code to start a transaction
		/// </summary>
		/// <param name="id">userid</param>
		/// <param name="providerId">providerid/shopid</param>
		/// <returns></returns>
		//[Authorize(Policy = Policies.ACTIVE)]
		[HttpPost("{id}/ScanShopCode/{providerId}")]
		public IActionResult ScanShopCode (Guid id, Guid providerId)
		{
			var result = _transactionService.ScanShopCode(id, providerId);
			return result.ToHttpResponse();
		}



	}
}