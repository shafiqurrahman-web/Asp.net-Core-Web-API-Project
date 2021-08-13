using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Provenance.Common.Configurations;
using Provenance.ServiceLayer.Contracts;
using Provenance.ServiceLayer.DTOs.ProductHistory;
using Provenance.Web.Modules;
using System;
using System.Threading.Tasks;

namespace Provenance.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductHistoryController : ControllerBase
	{
		private readonly ILogger<ProductHistoryController> _logger;
		private readonly IProductHistoryService _productHistoryService;

		public ProductHistoryController (ILogger<ProductHistoryController> logger, IProductHistoryService productHistoryService)
		{
			_logger = logger;
			_productHistoryService = productHistoryService;
		}


		//[Authorize(Roles = RoleTypes.ADMIN)]
		[HttpPost]
		public async Task<IActionResult> PostAsync (IFormFile PictureFile, AddProductHistoryDTO inputData)
		{
			if (PictureFile != null)
				inputData.Picture = await PictureFile.ToByteArrayAsync();

			return _productHistoryService.Add(inputData).ToHttpResponse();
		}

		//[Authorize(Policy = Policies.ACTIVE)]
		[HttpGet("{id}")]
		public IActionResult Get (Guid id)
		{
			return _productHistoryService.Get(id).ToHttpResponse();
		}

		//[Authorize(Roles = RoleTypes.ADMIN)]
		[HttpPut("{id}")]
		public IActionResult Put (Guid id, UpdateProductHistoryDTO inputData)
		{
			return _productHistoryService.Update(id, inputData).ToHttpResponse();
		}

		//[Authorize(Roles = RoleTypes.ADMIN)]
		[HttpDelete("{id}")]
		public IActionResult Delete (Guid id)
		{
			return _productHistoryService.Delete(id).ToHttpResponse();
		}

		//[Authorize(Roles = RoleTypes.ADMIN)]
		[HttpGet("all")]
		public IActionResult GetAll ()
		{
			return _productHistoryService.GetAll().ToHttpResponse();
		}


		//[Authorize(Policy = Policies.ACTIVE)]
		[HttpGet]
		public IActionResult Get (int page, int pageSize)
		{
			return _productHistoryService.GetAll(page, pageSize).ToHttpResponse();
		}

	}
}